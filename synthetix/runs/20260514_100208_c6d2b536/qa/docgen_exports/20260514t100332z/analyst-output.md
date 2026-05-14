# Analyst Brief

## Header
- Objective: Upgrade ### FILE: .github/workflows/deploy.yml
name: Deploy Chirp Backend

on:
  push:
    branches:
      - master

jobs:
  deploy:
    runs-on: ubuntu-latest

    steps:
      - name: Checkout code
        uses: actions/checkout@v3

      - name: Setup SSH
        uses: webfactory/ssh-agent@v0.5.3
        with:
          ssh-private-key: ${{ secrets.DEPLOY_KEY }}

      - name: Add server to known hosts
        run: |
          ssh-keyscan -H 85.10.247.47 > ~/.ssh/known_hosts

      - name: Create Firebase credentials file
        run: |
          mkdir -p app/src/main/resources/firebase-credentials
          echo "${{ secrets.FIREBASE_CREDENTIALS_BASE64 }}" | base64 -d > app/src/main/resources/firebase-credentials/chirp-firebase-adminsdk.json

      - name: Build JAR
        run: |
          ./gradlew :app:bootJar

      - name: Deploy JAR to Server
        run: |
          JAR_NAME="app-0.0.1-SNAPSHOT.jar"
          LOCAL_JAR_PATH="app/build/libs/$JAR_NAME"
          REMOTE_SERVER="admin@85.10.247.47"
          REMOTE_JAR_DIR="/opt/chirp"

          rsync -avz -e "ssh" $LOCAL_JAR_PATH $REMOTE_SERVER:$REMOTE_JAR_DIR/$JAR_NAME

          ssh $REMOTE_SERVER << EOF
            mv $REMOTE_JAR_DIR/$JAR_NAME $REMOTE_JAR_DIR/chirp.jar
            sudo systemctl restart chirp.service
          EOF


### FILE: app/build.gradle.kts
import org.springframework.boot.gradle.tasks.bundling.BootJar

plugins {
	id("chirp.spring-boot-app")
}

group = "com.plcoding"
version = "0.0.1-SNAPSHOT"

tasks {
	named<BootJar>("bootJar") {
		from(project(":notification").projectDir.resolve("src/main/resources")) {
			into("")
		}
		from(project(":user").projectDir.resolve("src/main/resources")) {
			into("")
		}
	}
}

dependencies {
	implementation(projects.user)
	implementation(projects.chat)
	implementation(projects.notification)
	implementation(projects.common)

	implementation(libs.kotlin.reflect)
	implementation(libs.spring.boot.starter.security)

	implementation(libs.spring.boot.starter.mail)
	implementation(libs.spring.boot.starter.amqp)
	implementation(libs.spring.boot.starter.data.redis)
	implementation(libs.spring.boot.starter.data.jpa)
	runtimeOnly(libs.postgresql)
}

### FILE: app/src/main/kotlin/com/plcoding/chirp/api/security/SecurityConfig.kt
package com.plcoding.chirp.api.security

import com.plcoding.chirp.api.config.ApiKeyAuthFilter
import com.plcoding.chirp.api.config.JwtAuthFilter
import jakarta.servlet.DispatcherType
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration
import org.springframework.http.HttpStatus
import org.springframework.security.config.annotation.web.builders.HttpSecurity
import org.springframework.security.config.http.SessionCreationPolicy
import org.springframework.security.web.SecurityFilterChain
import org.springframework.security.web.authentication.HttpStatusEntryPoint
import org.springframework.security.web.authentication.UsernamePasswordAuthenticationFilter

@Configuration
class SecurityConfig {

    @Bean
    fun filterChain(httpSecurity: HttpSecurity, jwtAuthFilter: JwtAuthFilter,
                    apiKeyAuthFilter: ApiKeyAuthFilter
    ): SecurityFilterChain {
        return httpSecurity
            .csrf { it.disable() }
            .sessionManagement { it.sessionCreationPolicy(SessionCreationPolicy.STATELESS) }
            .authorizeHttpRequests { auth ->
                auth
                    .requestMatchers("/api/auth/**")
                    .permitAll()
                    .requestMatchers("/api/auth/change-password")
                    .authenticated()
                    .dispatcherTypeMatchers(
                        DispatcherType.ERROR,
                        DispatcherType.FORWARD
                    )
                    .permitAll()
                    .anyRequest()
                    .authenticated()
            }
            .addFilterBefore(apiKeyAuthFilter, UsernamePasswordAuthenticationFilter::class.java)
            .addFilterAfter(jwtAuthFilter, ApiKeyAuthFilter::class.java)
            .exceptionHandling { configurer ->
                configurer
                    .authenticationEntryPoint(HttpStatusEntryPoint(HttpStatus.UNAUTHORIZED))
            }
            .build()
    }
}

### FILE: app/src/main/kotlin/com/plcoding/chirp/ChirpApplication.kt
package com.plcoding.chirp

import com.plcoding.chirp.infra.database.entities.UserEntity
import com.plcoding.chirp.infra.database.repositories.UserRepository
import jakarta.annotation.PostConstruct
import org.springframework.boot.autoconfigure.SpringBootApplication
import org.springframework.boot.runApplication
import org.springframework.scheduling.annotation.EnableScheduling
import org.springframework.stereotype.Component

@SpringBootApplication
@EnableScheduling
class ChirpApplication

fun main(args: Array<String>) {
	runApplication<ChirpApplication>(*args)
}

### FILE: app/src/main/kotlin/com/plcoding/chirp/infra/caching/RedisConfig.kt
package com.plcoding.chirp.infra.caching

import org.springframework.cache.annotation.EnableCaching
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration
import org.springframework.data.redis.cache.RedisCacheConfiguration
import org.springframework.data.redis.cache.RedisCacheManager
import org.springframework.data.redis.connection.lettuce.LettuceConnectionFactory
import org.springframework.data.redis.serializer.GenericJacksonJsonRedisSerializer
import org.springframework.data.redis.serializer.RedisSerializationContext
import tools.jackson.databind.DefaultTyping
import tools.jackson.databind.json.JsonMapper
import tools.jackson.databind.jsontype.BasicPolymorphicTypeValidator
import tools.jackson.module.kotlin.kotlinModule
import java.time.Duration

@Configuration
@EnableCaching
class RedisConfig {

    @Bean
    fun cacheManager(
        connectionFactory: LettuceConnectionFactory
    ): RedisCacheManager {
        val polymorphicTypeValidator = BasicPolymorphicTypeValidator.builder()
            .allowIfSubType("java.util.") // Allow Java lists
            .allowIfSubType("kotlin.collections.") // Kotlin collections
            .allowIfSubType("com.plcoding.chirp.")
            .build()

        val objectMapper = JsonMapper.builder()
            .addModule(kotlinModule())
            .findAndAddModules()
            .polymorphicTypeValidator(polymorphicTypeValidator)
            .activateDefaultTyping(polymorphicTypeValidator, DefaultTyping.NON_FINAL)
            .build()

        val cacheConfig = RedisCacheConfiguration.defaultCacheConfig()
            .entryTtl(Duration.ofHours(1L))
            .serializeValuesWith(
                RedisSerializationContext.SerializationPair.fromSerializer(
                    GenericJacksonJsonRedisSerializer(objectMapper)
                )
            )

        return RedisCacheManager.builder(connectionFactory)
            .cacheDefaults(cacheConfig)
            .withCacheConfiguration(
                "messages",
                cacheConfig.entryTtl(Duration.ofMinutes(30))
            )
            .transactionAware()
            .build()
    }
}

### FILE: app/src/main/resources/application-dev.yml
spring:
  jpa:
    hibernate:
      ddl-auto: update
  mail:
    host: smtp.mailgun.org
    username: postmaster@sandbox52dc7dc8bb034abca4a367ef5329fb58.mailgun.org

jwt:
  expiration-minutes: 1000

nginx:
  require-proxy: false

chirp:
  email:
    url: "http://localhost:8080"
  rate-limit:
    ip:
      apply-limit: false
  web-socket:
    allowed-origin: "http://localhost:8080"

logging:
  level:
    com.plcoding.chirp: DEBUG

### FILE: app/src/main/resources/application-prod.yml
spring:
  jpa:
    hibernate:
      ddl-auto: validate
  mail:
    host: smtp.eu.mailgun.org
    username: mail@mg.pl-coding.com
    test-connection: true

jwt:
  expiration-minutes: 15

nginx:
  require-proxy: false

chirp:
  email:
    url: "https://chirp.pl-coding.com"
  rate-limit:
    ip:
      apply-limit: true
  web-socket:
    allowed-origin: "https://chirp.pl-coding.com"

logging:
  level:
    com.plcoding.chirp: DEBUG

### FILE: app/src/main/resources/application.yml

spring:
  application:
    name: chirp

  data:
    redis:
      host: "redis-14109.c325.us-east-1-4.ec2.redns.redis-cloud.com"
      password: ${REDIS_PASSWORD}
      username: "default"
      port: 14109
      connect-timeout: 5000ms
      timeout: 2000ms

  datasource:
    url: jdbc:postgresql://db.wxufamvuixbezaecetbe.supabase.co:5432/postgres?sslmode=require
    username: postgres
    password: ${POSTGRES_PASSWORD}
    driver-class-name: org.postgresql.Driver

    hikari:
      maximum-pool-size: 10
      minimum-idle: 5
      connection-timeout: 20000
      idle-timeout: 300000
      max-lifetime: 12000000
      validation-timeout: 5000
      pool-name: SpringBootHikariCP
      data-source-properties:
        prepareThreshold: 5
        preparedStatementCacheQueries: 256
        preparedStatementCacheSizeMiB: 5

        tcpKeepAlive: true
        socketTimeout: 30
        connectTimeout: 10
  jpa:
    properties:
      hibernate:
        dialect: org.hibernate.dialect.PostgreSQLDialect
        format_sql: true
        show_sql: true

  mail:
    port: 587
    password: ${MAILGUN_PASSWORD}
    properties:
      mail:
        smtp:
          auth: true
          starttls:
            enable: true

  rabbitmq:
    host: seal.lmq.cloudamqp.com
    password: ${RABBITMQ_PASSWORD}
    username: udnlwpbs
    ssl:
      enabled: true
    virtual-host: udnlwpbs
    listener:
      simple:
        acknowledge-mode: auto
        concurrency: 1
        max-concurrency: 3
        prefetch: 10
        default-requeue-rejected: true
        retry:
          enabled: true
          initial-interval: 1000ms
          max-interval: 10000ms
          max-attempts: 3
          multiplier: 2

jwt:
  secret: ${JWT_SECRET_BASE64}

chirp:
  api-key:
    expires-in-days: 365
    admin:
      username: ${ADMIN_USERNAME}
      password: ${ADMIN_PASSWORD}
  email:
    from: mail@pl-coding.com
    verification:
      expiry-hours: 24
    reset-password:
      expiry-minutes: 30

nginx:
  trusted-ips:
    - "127.0.0.1" # nginx on same machine
    - "::1" # IPv6 localhost
    - "172.17.0.0/16" # nginx container IP when using Docker
    - "10.0.0.5/32" # nginx server IP (if separate machine)

supabase:
  url: "https://wxufamvuixbezaecetbe.supabase.co"
  service-key: ${SUPABASE_SERVICE_KEY}

firebase:
  credentials-path: "classpath:firebase-credentials/chirp-firebase-adminsdk.json"

server:
  port: 8095

### FILE: app/src/test/kotlin/com/plcoding/chirp/ChirpApplicationTests.kt
package com.plcoding.chirp

import org.junit.jupiter.api.Test
import org.springframework.boot.test.context.SpringBootTest

@SpringBootTest
class ChirpApplicationTests {

	@Test
	fun contextLoads() {
	}

}


### FILE: build-logic/build.gradle.kts
plugins {
    `kotlin-dsl`
}

repositories {
    gradlePluginPortal()
    mavenCentral()
    maven { url = uri("https://repo.spring.io/milestone") }
    maven { url = uri("https://repo.spring.io/snapshot") }
}

dependencies {
    implementation("org.jetbrains.kotlin:kotlin-gradle-plugin:2.2.0")
    implementation("org.jetbrains.kotlin:kotlin-allopen:2.2.0")
    implementation("org.springframework.boot:spring-boot-gradle-plugin:4.0.0-SNAPSHOT")
    implementation("io.spring.gradle:dependency-management-plugin:1.1.7")
}

### FILE: build-logic/settings.gradle.kts
rootProject.name = "build-logic"

dependencyResolutionManagement {
    repositories {
        maven { url = uri("https://repo.spring.io/milestone") }
        maven { url = uri("https://repo.spring.io/snapshot") }
        gradlePluginPortal()
        mavenCentral()
    }
}

### FILE: build-logic/src/main/kotlin/chirp.kotlin-common.gradle.kts
import org.jetbrains.kotlin.gradle.dsl.JvmTarget
import org.jetbrains.kotlin.gradle.dsl.KotlinJvmProjectExtension
import org.jetbrains.kotlin.gradle.tasks.KotlinCompile
import kotlin.collections.addAll

plugins {
    kotlin("jvm")
    kotlin("plugin.spring")
    id("io.spring.dependency-management")
}

repositories {
    mavenCentral()
    maven { url = uri("https://repo.spring.io/milestone") }
    maven { url = uri("https://repo.spring.io/snapshot") }
}

dependencyManagement {
    imports {
        mavenBom("org.springframework.boot:spring-boot-dependencies:${libraries.findVersion("spring-boot").get()}")
    }
}

configure<KotlinJvmProjectExtension> {
    jvmToolchain(21)
}

tasks.withType<KotlinCompile> {
    compilerOptions {
        freeCompilerArgs.addAll("-Xjsr305=strict", "-Xannotation-default-target=param-property")
        jvmTarget = JvmTarget.JVM_21
    }
}

tasks.withType<Test> {
    useJUnitPlatform()
}


### FILE: build-logic/src/main/kotlin/chirp.spring-boot-app.gradle.kts
import gradle.kotlin.dsl.accessors._3ef9c89b436b2435895044c4cd9d19d0.allOpen
import gradle.kotlin.dsl.accessors._3ef9c89b436b2435895044c4cd9d19d0.java

plugins {
    id("chirp.spring-boot-service")
    id("org.springframework.boot")
    kotlin("plugin.spring")
}

java {
    toolchain {
        languageVersion = JavaLanguageVersion.of(21)
    }
}

allOpen {
    annotation("jakarta.persistence.Entity")
    annotation("jakarta.persistence.MappedSuperclass")
    annotation("jakarta.persistence.Embeddable")
}

### FILE: build-logic/src/main/kotlin/chirp.spring-boot-service.gradle.kts
plugins {
    id("chirp.kotlin-common")
    id("io.spring.dependency-management")
}

dependencies {
    "implementation"(libraries.findLibrary("kotlin-reflect").get())
    "implementation"(libraries.findLibrary("kotlin-stdlib").get())
    "implementation"(libraries.findLibrary("spring-boot-starter-web").get())

    "testImplementation"(libraries.findLibrary("spring-boot-starter-test").get())
    "testImplementation"(libraries.findLibrary("kotlin-test-junit5").get())
    "testRuntimeOnly"(libraries.findLibrary("junit-platform-launcher").get())
}

### FILE: build-logic/src/main/kotlin/VersionCatalogExt.kt
import org.gradle.api.Project
import org.gradle.api.artifacts.VersionCatalog
import org.gradle.api.artifacts.VersionCatalogsExtension
import org.gradle.kotlin.dsl.getByType

val Project.libraries: VersionCatalog
    get() = extensions.getByType<VersionCatalogsExtension>().named("libs")

### FILE: build.gradle.kts
plugins {
    alias(libs.plugins.kotlin.jvm) apply false
    alias(libs.plugins.kotlin.spring) apply false
    alias(libs.plugins.spring.boot) apply false
    alias(libs.plugins.spring.dependency.management) apply false
    alias(libs.plugins.kotlin.jpa) apply false
}

group = "com.plcoding"
version = "0.0.1-SNAPSHOT"

subprojects {
    group = rootProject.group
    version = rootProject.version
}

### FILE: chat/build.gradle.kts
plugins {
    id("java-library")
    id("chirp.spring-boot-service")
    kotlin("plugin.jpa")
}

group = "com.plcoding"
version = "unspecified"

repositories {
    mavenCentral()
    maven { url = uri("https://repo.spring.io/milestone") }
    maven { url = uri("https://repo.spring.io/snapshot") }
}

dependencies {
    implementation(projects.common)

    implementation(libs.spring.boot.starter.validation)
    implementation(libs.spring.boot.starter.amqp)
    implementation(libs.spring.boot.starter.websocket)

    implementation(libs.spring.boot.starter.data.jpa)
    runtimeOnly(libs.postgresql)

    testImplementation(kotlin("test"))
}

tasks.test {
    useJUnitPlatform()
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/controllers/ChatController.kt
package com.plcoding.chirp.api.controllers

import com.plcoding.chirp.api.dto.AddParticipantToChatDto
import com.plcoding.chirp.api.dto.ChatDto
import com.plcoding.chirp.api.dto.ChatMessageDto
import com.plcoding.chirp.api.dto.CreateChatRequest
import com.plcoding.chirp.api.mappers.toChatDto
import com.plcoding.chirp.service.ChatService
import com.plcoding.chirp.api.util.requestUserId
import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.service.ChatMessageService
import jakarta.validation.Valid
import org.springframework.http.HttpStatus
import org.springframework.web.bind.annotation.DeleteMapping
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.PathVariable
import org.springframework.web.bind.annotation.PostMapping
import org.springframework.web.bind.annotation.RequestBody
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RequestParam
import org.springframework.web.bind.annotation.RestController
import org.springframework.web.server.ResponseStatusException
import java.time.Instant

@RestController
@RequestMapping("/api/chat")
class ChatController(
    private val chatService: ChatService,
) {

    companion object {
        private const val DEFAULT_PAGE_SIZE = 20
    }

    @GetMapping("/{chatId}/messages")
    fun getMessagesForChat(
        @PathVariable("chatId") chatId: ChatId,
        @RequestParam("before", required = false) before: Instant? = null,
        @RequestParam("pageSize", required = false) pageSize: Int = DEFAULT_PAGE_SIZE
    ): List<ChatMessageDto> {
        return chatService.getChatMessages(
            chatId = chatId,
            before = before,
            pageSize = pageSize
        )
    }

    @GetMapping("/{chatId}")
    fun getChat(
        @PathVariable("chatId") chatId: ChatId,
    ): ChatDto {
        return chatService.getChatById(
            chatId = chatId,
            requestUserId = requestUserId
        )?.toChatDto() ?: throw ResponseStatusException(HttpStatus.NOT_FOUND)
    }

    @GetMapping
    fun getChatsForUser(): List<ChatDto> {
        return chatService.findChatsByUser(
            userId = requestUserId,
        ).map { it.toChatDto() }
    }

    @PostMapping
    fun createChat(
        @Valid @RequestBody body: CreateChatRequest
    ): ChatDto {
        return chatService.createChat(
            creatorId = requestUserId,
            otherUserIds = body.otherUserIds.toSet()
        ).toChatDto()
    }

    @PostMapping("/{chatId}/add")
    fun addChatParticipants(
        @PathVariable chatId: ChatId,
        @Valid @RequestBody body: AddParticipantToChatDto
    ): ChatDto {
        return chatService.addParticipantsToChat(
            requestUserId = requestUserId,
            chatId = chatId,
            userIds = body.userIds.toSet()
        ).toChatDto()
    }

    @DeleteMapping("/{chatId}/leave")
    fun leaveChat(
        @PathVariable chatId: ChatId
    ) {
        chatService.removeParticipantFromChat(
            chatId = chatId,
            userId = requestUserId
        )
    }
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/controllers/ChatMessageController.kt
package com.plcoding.chirp.api.controllers

import com.plcoding.chirp.api.util.requestUserId
import com.plcoding.chirp.domain.type.ChatMessageId
import com.plcoding.chirp.service.ChatMessageService
import org.springframework.web.bind.annotation.DeleteMapping
import org.springframework.web.bind.annotation.PathVariable
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RestController

@RestController
@RequestMapping("/api/messages")
class ChatMessageController(private val chatMessageService: ChatMessageService) {

    @DeleteMapping("/{messageId}")
    fun deleteMessage(
        @PathVariable("messageId") messageId: ChatMessageId
    ) {
        chatMessageService.deleteMessage(messageId, requestUserId)
    }
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/controllers/ChatParticipantController.kt
package com.plcoding.chirp.api.controllers

import com.plcoding.chirp.api.dto.ChatParticipantDto
import com.plcoding.chirp.api.dto.ConfirmProfilePictureRequest
import com.plcoding.chirp.api.dto.PictureUploadResponse
import com.plcoding.chirp.api.mappers.toChatParticipantDto
import com.plcoding.chirp.api.mappers.toResponse
import com.plcoding.chirp.service.ChatParticipantService
import com.plcoding.chirp.api.util.requestUserId
import com.plcoding.chirp.service.ProfilePictureService
import jakarta.validation.Valid
import org.springframework.http.HttpStatus
import org.springframework.web.bind.annotation.DeleteMapping
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.PostMapping
import org.springframework.web.bind.annotation.RequestBody
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RequestParam
import org.springframework.web.bind.annotation.RestController
import org.springframework.web.server.ResponseStatusException

@RestController
@RequestMapping("/api/participants")
class ChatParticipantController(
    private val chatParticipantService: ChatParticipantService,
    private val profilePictureService: ProfilePictureService
) {

    @GetMapping
    fun getChatParticipantByUsernameOrEmail(
        @RequestParam(required = false) query: String?
    ): ChatParticipantDto {
        val participant = if(query == null) {
            chatParticipantService.findChatParticipantById(requestUserId)
        } else {
            chatParticipantService.findChatParticipantByEmailOrUsername(query)
        }

        return participant?.toChatParticipantDto()
            ?: throw ResponseStatusException(HttpStatus.NOT_FOUND)
    }

    @PostMapping("/profile-picture-upload")
    fun getProfilePictureUploadUrl(
        @RequestParam mimeType: String
    ): PictureUploadResponse {
        return profilePictureService.generateUploadCredentials(
            userId = requestUserId,
            mimeType = mimeType
        ).toResponse()
    }

    @PostMapping("/confirm-profile-picture")
    fun confirmProfilePictureUpload(
        @Valid @RequestBody body: ConfirmProfilePictureRequest
    ) {
        profilePictureService.confirmProfilePictureUpload(
            userId = requestUserId,
            publicUrl = body.publicUrl
        )
    }

    @DeleteMapping("/profile-picture")
    fun deleteProfilePicture() {
        profilePictureService.deleteProfilePicture(
            userId = requestUserId
        )
    }
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/dto/AddParticipantToChatDto.kt
package com.plcoding.chirp.api.dto

import com.plcoding.chirp.domain.type.UserId
import jakarta.validation.constraints.Size

data class AddParticipantToChatDto(
    @field:Size(min = 1)
    val userIds: List<UserId>
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/dto/ChatDto.kt
package com.plcoding.chirp.api.dto

import com.plcoding.chirp.domain.type.ChatId
import java.time.Instant

data class ChatDto(
    val id: ChatId,
    val participants: List<ChatParticipantDto>,
    val lastActivityAt: Instant,
    val lastMessage: ChatMessageDto?,
    val creator: ChatParticipantDto
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/dto/ChatMessageDto.kt
package com.plcoding.chirp.api.dto

import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.domain.type.ChatMessageId
import com.plcoding.chirp.domain.type.UserId
import java.time.Instant

data class ChatMessageDto(
    val id: ChatMessageId,
    val chatId: ChatId,
    val content: String,
    val createdAt: Instant,
    val senderId: UserId
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/dto/ChatParticipantDto.kt
package com.plcoding.chirp.api.dto

import com.plcoding.chirp.domain.type.UserId

data class ChatParticipantDto(
    val userId: UserId,
    val username: String,
    val email: String,
    val profilePictureUrl: String?
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/dto/ConfirmProfilePictureRequest.kt
package com.plcoding.chirp.api.dto

import jakarta.validation.constraints.NotBlank

data class ConfirmProfilePictureRequest(
    @field:NotBlank
    val publicUrl: String
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/dto/CreateChatRequest.kt
package com.plcoding.chirp.api.dto

import com.plcoding.chirp.domain.type.UserId
import jakarta.validation.constraints.Size

data class CreateChatRequest(
    @field:Size(
        min = 1,
        message = "Chats must have at least 2 unique participants"
    )
    val otherUserIds: List<UserId>
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/dto/PictureUploadResponse.kt
package com.plcoding.chirp.api.dto

import java.time.Instant

data class PictureUploadResponse(
    val uploadUrl: String,
    val publicUrl: String,
    val headers: Map<String, String>,
    val expiresAt: Instant
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/dto/ws/ChatParticipantsChangedDto.kt
package com.plcoding.chirp.api.dto.ws

import com.plcoding.chirp.domain.type.ChatId

data class ChatParticipantsChangedDto(
    val chatId: ChatId
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/dto/ws/DeleteMessageDto.kt
package com.plcoding.chirp.api.dto.ws

import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.domain.type.ChatMessageId

data class DeleteMessageDto(
    val chatId: ChatId,
    val messageId: ChatMessageId
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/dto/ws/ErrorDto.kt
package com.plcoding.chirp.api.dto.ws

data class ErrorDto(
    val code: String,
    val message: String
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/dto/ws/ProfilePictureUpdateDto.kt
package com.plcoding.chirp.api.dto.ws

import com.plcoding.chirp.domain.type.UserId

data class ProfilePictureUpdateDto(
    val userId: UserId,
    val newUrl: String?
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/dto/ws/SendMessageDto.kt
package com.plcoding.chirp.api.dto.ws

import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.domain.type.ChatMessageId

data class SendMessageDto(
    val chatId: ChatId,
    val content: String,
    val messageId: ChatMessageId? = null
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/dto/ws/WebSocketEvent.kt
package com.plcoding.chirp.api.dto.ws

enum class IncomingWebSocketMessageType {
    NEW_MESSAGE
}

enum class OutgoingWebSocketMessageType {
    NEW_MESSAGE,
    MESSAGE_DELETED,
    PROFILE_PICTURE_UPDATED,
    CHAT_PARTICIPANTS_CHANGED,
    ERROR
}

data class IncomingWebSocketMessage(
    val type: IncomingWebSocketMessageType,
    val payload: String
)

data class OutgoingWebSocketMessage(
    val type: OutgoingWebSocketMessageType,
    val payload: String
)

### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/exception_handling/ChatExceptionHandler.kt
package com.plcoding.chirp.api.exception_handling

import com.plcoding.chirp.domain.exception.ChatNotFoundException
import com.plcoding.chirp.domain.exception.ChatParticipantNotFoundException
import com.plcoding.chirp.domain.exception.ForbiddenException
import com.plcoding.chirp.domain.exception.InvalidChatSizeException
import com.plcoding.chirp.domain.exception.InvalidProfilePictureException
import com.plcoding.chirp.domain.exception.MessageNotFoundException
import com.plcoding.chirp.domain.exception.StorageException
import org.springframework.http.HttpStatus
import org.springframework.web.bind.annotation.ExceptionHandler
import org.springframework.web.bind.annotation.ResponseStatus

class ChatExceptionHandler {

    @ExceptionHandler(
        ChatNotFoundException::class,
        MessageNotFoundException::class,
        ChatParticipantNotFoundException::class,
    )
    @ResponseStatus(HttpStatus.NOT_FOUND)
    fun onForbidden(e: Exception) = mapOf(
        "code" to "NOT_FOUND",
        "message" to e.message
    )

    @ExceptionHandler(InvalidChatSizeException::class)
    @ResponseStatus(HttpStatus.BAD_REQUEST)
    fun onForbidden(e: InvalidChatSizeException) = mapOf(
        "code" to "INVALID_CHAT_SIZE",
        "message" to e.message
    )

    @ExceptionHandler(InvalidProfilePictureException::class)
    @ResponseStatus(HttpStatus.BAD_REQUEST)
    fun onInvalidProfilePicture(e: InvalidProfilePictureException) = mapOf(
        "code" to "INVALID_PROFILE_PICTURE",
        "message" to e.message
    )

    @ExceptionHandler(StorageException::class)
    @ResponseStatus(HttpStatus.INTERNAL_SERVER_ERROR)
    fun onInvalidProfilePicture(e: StorageException) = mapOf(
        "code" to "STORAGE_ERROR",
        "message" to e.message
    )
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/mappers/ChatDtoMappers.kt
package com.plcoding.chirp.api.mappers

import com.plcoding.chirp.api.dto.ChatDto
import com.plcoding.chirp.api.dto.ChatMessageDto
import com.plcoding.chirp.api.dto.ChatParticipantDto
import com.plcoding.chirp.domain.models.Chat
import com.plcoding.chirp.domain.models.ChatMessage
import com.plcoding.chirp.domain.models.ChatParticipant

fun Chat.toChatDto(): ChatDto {
    return ChatDto(
        id = id,
        participants = participants.map {
            it.toChatParticipantDto()
        },
        lastActivityAt = lastActivityAt,
        lastMessage = lastMessage?.toChatMessageDto(),
        creator = creator.toChatParticipantDto()
    )
}

fun ChatMessage.toChatMessageDto(): ChatMessageDto {
    return ChatMessageDto(
        id = id,
        chatId = chatId,
        content = content,
        createdAt = createdAt,
        senderId = sender.userId
    )
}

fun ChatParticipant.toChatParticipantDto(): ChatParticipantDto {
    return ChatParticipantDto(
        userId = userId,
        username = username,
        email = email,
        profilePictureUrl = profilePictureUrl
    )
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/mappers/ProfilePictureMappers.kt
package com.plcoding.chirp.api.mappers

import com.plcoding.chirp.api.dto.PictureUploadResponse
import com.plcoding.chirp.domain.models.ProfilePictureUploadCredentials

fun ProfilePictureUploadCredentials.toResponse(): PictureUploadResponse {
    return PictureUploadResponse(
        uploadUrl = uploadUrl,
        publicUrl = publicUrl,
        headers = headers,
        expiresAt = expiresAt
    )
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/websocket/ChatWebSocketHandler.kt
package com.plcoding.chirp.api.websocket

import com.plcoding.chirp.api.dto.ws.ChatParticipantsChangedDto
import com.plcoding.chirp.api.dto.ws.DeleteMessageDto
import com.plcoding.chirp.api.dto.ws.ErrorDto
import com.plcoding.chirp.api.dto.ws.IncomingWebSocketMessage
import com.plcoding.chirp.api.dto.ws.IncomingWebSocketMessageType
import com.plcoding.chirp.api.dto.ws.OutgoingWebSocketMessage
import com.plcoding.chirp.api.dto.ws.OutgoingWebSocketMessageType
import com.plcoding.chirp.api.dto.ws.ProfilePictureUpdateDto
import com.plcoding.chirp.api.dto.ws.SendMessageDto
import com.plcoding.chirp.api.mappers.toChatMessageDto
import com.plcoding.chirp.domain.event.ChatCreatedEvent
import com.plcoding.chirp.domain.event.ChatParticipantLeftEvent
import com.plcoding.chirp.domain.event.ChatParticipantsJoinedEvent
import com.plcoding.chirp.domain.event.MessageDeletedEvent
import com.plcoding.chirp.domain.event.ProfilePictureUpdatedEvent
import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.domain.type.UserId
import com.plcoding.chirp.service.ChatMessageService
import com.plcoding.chirp.service.ChatService
import com.plcoding.chirp.service.JwtService
import com.rabbitmq.tools.jsonrpc.JsonRpcMappingException
import jakarta.websocket.CloseReason
import org.slf4j.LoggerFactory
import org.springframework.http.HttpHeaders
import org.springframework.scheduling.annotation.Scheduled
import org.springframework.stereotype.Component
import org.springframework.transaction.event.TransactionPhase
import org.springframework.transaction.event.TransactionalEventListener
import org.springframework.web.socket.CloseStatus
import org.springframework.web.socket.PingMessage
import org.springframework.web.socket.PongMessage
import org.springframework.web.socket.TextMessage
import org.springframework.web.socket.WebSocketSession
import org.springframework.web.socket.handler.TextWebSocketHandler
import tools.jackson.core.JacksonException
import tools.jackson.databind.ObjectMapper
import java.util.concurrent.ConcurrentHashMap
import java.util.concurrent.locks.ReentrantReadWriteLock
import kotlin.concurrent.read
import kotlin.concurrent.write

@Component
class ChatWebSocketHandler(
    private val chatMessageService: ChatMessageService,
    private val objectMapper: ObjectMapper,
    private val chatService: ChatService,
    private val jwtService: JwtService
): TextWebSocketHandler() {

    companion object {
        private const val PING_INTERVAL_MS = 30_000L
        private const val PONG_TIMEOUT_MS = 60_000L
    }

    private val logger = LoggerFactory.getLogger(javaClass)

    private val connectionLock = ReentrantReadWriteLock()

    private val sessions = ConcurrentHashMap<String, UserSession>()
    private val userToSessions = ConcurrentHashMap<UserId, MutableSet<String>>()
    private val userChatIds = ConcurrentHashMap<UserId, MutableSet<ChatId>>()
    private val chatToSessions = ConcurrentHashMap<ChatId, MutableSet<String>>()

    override fun afterConnectionEstablished(session: WebSocketSession) {
        val authHeader = session
            .handshakeHeaders
            .getFirst(HttpHeaders.AUTHORIZATION)
            ?: run {
                logger.warn("Session ${session.id} was closed due to missing Authorization header")
                session.close(CloseStatus.SERVER_ERROR.withReason("Authentication failed"))
                return
            }

        val userId = jwtService.getUserIdFromToken(authHeader)

        val userSession = UserSession(
            userId = userId,
            session = session
        )

        connectionLock.write {
            sessions[session.id] = userSession

            userToSessions.compute(userId) { _, existingSessions ->
                (existingSessions ?: mutableSetOf()).apply {
                    add(session.id)
                }
            }

            val chatIds = userChatIds.computeIfAbsent(userId) {
                val chatIds = chatService.findChatsByUser(userId).map { it.id }
                ConcurrentHashMap.newKeySet<ChatId>().apply {
                    addAll(chatIds)
                }
            }

            chatIds.forEach { chatId ->
                chatToSessions.compute(chatId) { _, sessions ->
                    (sessions ?: mutableSetOf()).apply {
                        add(session.id)
                    }
                }
            }
        }

        logger.info("Websocket connection established for user $userId")
    }

    override fun afterConnectionClosed(session: WebSocketSession, status: CloseStatus) {
        connectionLock.write {
            sessions.remove(session.id)?.let { userSession ->
                val userId = userSession.userId

                userToSessions.compute(userId) { _, sessions ->
                    sessions
                        ?.apply { remove(session.id) }
                        ?.takeIf { it.isNotEmpty() }
                }

                userChatIds[userId]?.forEach { chatId ->
                    chatToSessions.compute(chatId) { _, sessions ->
                        sessions
                            ?.apply { remove(session.id) }
                            ?.takeIf { it.isNotEmpty() }
                    }
                }

                logger.info("Websocket session closed for user $userId")
            }
        }
    }

    override fun handleTransportError(session: WebSocketSession, exception: Throwable) {
        logger.error("Transport error for session ${session.id}", exception)
        session.close(CloseStatus.SERVER_ERROR.withReason("Transport error"))
    }

    override fun handleTextMessage(session: WebSocketSession, message: TextMessage) {
        logger.debug("Received message ${message.payload}")

        val userSession = connectionLock.read {
            sessions[session.id] ?: return
        }

        try {
            val webSocketMessage = objectMapper.readValue(
                message.payload,
                IncomingWebSocketMessage::class.java
            )
            when(webSocketMessage.type) {
                IncomingWebSocketMessageType.NEW_MESSAGE -> {
                    val dto = objectMapper.readValue(
                        webSocketMessage.payload,
                        SendMessageDto::class.java
                    )
                    logger.debug("Sending chat message from {}", userSession.userId)
                    handleSendMessage(
                        dto = dto,
                        senderId = userSession.userId
                    )
                }
            }
        } catch(e: JacksonException) {
            logger.warn("Could not parse message ${message.payload}", e)
            sendError(
                session = userSession.session,
                error = ErrorDto(
                    code = "INVALID_JSON",
                    message = "Incoming JSON or UUID is invalid"
                )
            )
        }
    }

    @TransactionalEventListener(phase = TransactionPhase.AFTER_COMMIT)
    fun onDeleteMessage(event: MessageDeletedEvent) {
        broadcastToChat(
            chatId = event.chatId,
            message = OutgoingWebSocketMessage(
                type = OutgoingWebSocketMessageType.MESSAGE_DELETED,
                payload = objectMapper.writeValueAsString(
                    DeleteMessageDto(
                        chatId = event.chatId,
                        messageId = event.messageId
                    )
                )
            )
        )
    }

    @TransactionalEventListener(phase = TransactionPhase.AFTER_COMMIT)
    fun onJoinChat(event: ChatParticipantsJoinedEvent) {
        connectionLock.write {
            event.userIds.forEach { userId ->
                userChatIds.compute(userId) { _, chatIds ->
                    (chatIds ?: mutableSetOf()).apply {
                        add(event.chatId)
                    }
                }

                userToSessions[userId]?.forEach { sessionId ->
                    chatToSessions.compute(event.chatId) { _, sessions ->
                        (sessions ?: mutableSetOf()).apply { add(sessionId) }
                    }
                }
            }
        }

        broadcastToChat(
            chatId = event.chatId,
            message = OutgoingWebSocketMessage(
                type = OutgoingWebSocketMessageType.CHAT_PARTICIPANTS_CHANGED,
                payload = objectMapper.writeValueAsString(
                    ChatParticipantsChangedDto(
                        chatId = event.chatId
                    )
                )
            )
        )
    }

    override fun handlePongMessage(session: WebSocketSession, message: PongMessage) {
        connectionLock.write {
            sessions.compute(session.id) { _, userSession ->
                userSession?.copy(
                    lastPongTimestamp = System.currentTimeMillis()
                )
            }
        }
        logger.debug("Received pong from ${session.id}")
    }

    @Scheduled(fixedDelay = PING_INTERVAL_MS)
    fun pingClients() {
        val currentTime = System.currentTimeMillis()
        val sessionsToClose = mutableListOf<String>()

        val sessionsSnapshot = connectionLock.read { sessions.toMap() }

        sessionsSnapshot.forEach { (sessionId, userSession) ->
            try {
                if(userSession.session.isOpen) {
                    val lastPong = userSession.lastPongTimestamp
                    if(currentTime - lastPong > PONG_TIMEOUT_MS) {
                        logger.warn("Session $sessionId has timed out, closing connection.")
                        sessionsToClose.add(sessionId)
                        return@forEach
                    }

                    userSession.session.sendMessage(PingMessage())
                    logger.debug("Sent ping to {}", userSession.userId)
                }
            } catch(e: Exception) {
                logger.error("Could not ping session $sessionId", e)
                sessionsToClose.add(sessionId)
            }
        }

        sessionsToClose.forEach { sessionId ->
            connectionLock.read {
                sessions[sessionId]?.session?.let { session ->
                    try {
                        session.close(CloseStatus.GOING_AWAY.withReason("Ping timeout"))
                    } catch(e: Exception) {
                        logger.error("Couldn't close sessions for session ${session.id}")
                    }
                }
            }
        }
    }

    @TransactionalEventListener(phase = TransactionPhase.AFTER_COMMIT)
    fun onLeftChat(event: ChatParticipantLeftEvent) {
        connectionLock.write {
            userChatIds.compute(event.userId) { _, chatIds ->
                chatIds
                    ?.apply { remove(event.chatId) }
                    ?.takeIf { it.isNotEmpty() }
            }

            userToSessions[event.userId]?.forEach { sessionId ->
                chatToSessions.compute(event.chatId) { _, sessions ->
                    sessions
                        ?.apply { remove(sessionId) }
                        ?.takeIf { it.isNotEmpty() }
                }
            }
        }

        broadcastToChat(
            chatId = event.chatId,
            message = OutgoingWebSocketMessage(
                type = OutgoingWebSocketMessageType.CHAT_PARTICIPANTS_CHANGED,
                payload = objectMapper.writeValueAsString(
                    ChatParticipantsChangedDto(
                        chatId = event.chatId
                    )
                )
            )
        )
    }

    @TransactionalEventListener(phase = TransactionPhase.AFTER_COMMIT)
    fun onProfilePictureUpdated(event: ProfilePictureUpdatedEvent) {
        val userChats = connectionLock.read {
            userChatIds[event.userId]?.toList() ?: emptyList()
        }

        val dto = ProfilePictureUpdateDto(
            userId = event.userId,
            newUrl = event.newUrl,
        )

        val sessionIds = mutableSetOf<String>()
        userChats.forEach { chatId ->
            connectionLock.read {
                chatToSessions[chatId]?.let { sessions ->
                    sessionIds.addAll(sessions)
                }
            }
        }

        val webSocketMessage = OutgoingWebSocketMessage(
            type = OutgoingWebSocketMessageType.PROFILE_PICTURE_UPDATED,
            payload = objectMapper.writeValueAsString(dto)
        )
        val messageJson = objectMapper.writeValueAsString(webSocketMessage)

        sessionIds.forEach { sessionId ->
            val userSession = connectionLock.read {
                sessions[sessionId]
            } ?: return@forEach
            try {
                if(userSession.session.isOpen) {
                    userSession.session.sendMessage(TextMessage(messageJson))
                }
            } catch(e: Exception) {
                logger.error("Could not send profile picture update to session $sessionId", e)
            }
        }
    }

    @TransactionalEventListener(phase = TransactionPhase.AFTER_COMMIT)
    fun onChatCreated(event: ChatCreatedEvent) {
        connectionLock.write {
            event.participantIds.forEach { userId ->
                userChatIds.compute(userId) { _, chatIds ->
                    (chatIds ?: mutableSetOf()).apply {
                        add(event.chatId)
                    }
                }

                userToSessions[userId]?.forEach { sessionId ->
                    chatToSessions.compute(event.chatId) { _, sessions ->
                        (sessions ?: mutableSetOf()).apply { add(sessionId) }
                    }
                }
            }
        }
    }

    private fun sendError(
        session: WebSocketSession,
        error: ErrorDto
    ) {
        val webSocketMessage = objectMapper.writeValueAsString(
            OutgoingWebSocketMessage(
                type = OutgoingWebSocketMessageType.ERROR,
                payload = objectMapper.writeValueAsString(error)
            )
        )

        try {
            session.sendMessage(TextMessage(webSocketMessage))
        } catch(e: Exception) {
            logger.warn("Couldn't send error message", e)
        }
    }

    private fun broadcastToChat(
        chatId: ChatId,
        message: OutgoingWebSocketMessage
    ) {
        val chatSessions = connectionLock.read {
            chatToSessions[chatId]?.toList() ?: emptyList()
        }

        chatSessions.forEach { sessionId ->
            val userSession = connectionLock.read {
                sessions[sessionId]
            } ?: return@forEach

            sendToUser(
                userId = userSession.userId,
                message = message
            )
        }
    }

    private fun handleSendMessage(
        dto: SendMessageDto,
        senderId: UserId
    ) {
        val userChatIds = connectionLock.read { this@ChatWebSocketHandler.userChatIds[senderId] } ?: return

        if(dto.chatId !in userChatIds) {
            return
        }

        val savedMessage = chatMessageService.sendMessage(
            chatId = dto.chatId,
            senderId = senderId,
            content = dto.content,
            messageId = dto.messageId
        )

        broadcastToChat(
            chatId = dto.chatId,
            message = OutgoingWebSocketMessage(
                type = OutgoingWebSocketMessageType.NEW_MESSAGE,
                payload = objectMapper.writeValueAsString(
                    savedMessage.toChatMessageDto()
                )
            )
        )
    }

    private fun sendToUser(userId: UserId, message: OutgoingWebSocketMessage) {
        val userSessions = connectionLock.read {
            userToSessions[userId] ?: emptySet()
        }
        userSessions.forEach { sessionId ->
            val userSession = connectionLock.read {
                sessions[sessionId] ?: return@forEach
            }
            if(userSession.session.isOpen) {
                try {
                    val messageJson = objectMapper.writeValueAsString(message)
                    userSession.session.sendMessage(TextMessage(messageJson))
                    logger.debug("Sent message to user {}: {}", userId, messageJson)
                } catch(e: Exception) {
                    logger.error("Error while sending message to $userId", e)
                }
            }
        }
    }

    private data class UserSession(
        val userId: UserId,
        val session: WebSocketSession,
        val lastPongTimestamp: Long = System.currentTimeMillis()
    )
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/api/websocket/WebSocketConfig.kt
package com.plcoding.chirp.api.websocket

import org.springframework.beans.factory.annotation.Value
import org.springframework.context.annotation.Configuration
import org.springframework.web.socket.config.annotation.EnableWebSocket
import org.springframework.web.socket.config.annotation.WebSocketConfigurer
import org.springframework.web.socket.config.annotation.WebSocketHandlerRegistry

@Configuration
@EnableWebSocket
class WebSocketConfig(
    private val handler: ChatWebSocketHandler,
    @param:Value("\${chirp.web-socket.allowed-origin}")
    private val allowedOrigin: String,
): WebSocketConfigurer {

    override fun registerWebSocketHandlers(registry: WebSocketHandlerRegistry) {
        registry
            .addHandler(handler, "/ws/chat")
            .setAllowedOrigins(allowedOrigin)
    }
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/domain/event/ChatCreatedEvent.kt
package com.plcoding.chirp.domain.event

import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.domain.type.UserId

data class ChatCreatedEvent(
    val chatId: ChatId,
    val participantIds: List<UserId>
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/domain/event/ChatParticipantLeftEvent.kt
package com.plcoding.chirp.domain.event

import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.domain.type.UserId

data class ChatParticipantLeftEvent(
    val chatId: ChatId,
    val userId: UserId
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/domain/event/ChatParticipantsJoinedEvent.kt
package com.plcoding.chirp.domain.event

import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.domain.type.UserId

data class ChatParticipantsJoinedEvent(
    val chatId: ChatId,
    val userIds: Set<UserId>
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/domain/event/MessageDeletedEvent.kt
package com.plcoding.chirp.domain.event

import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.domain.type.ChatMessageId

data class MessageDeletedEvent(
    val chatId: ChatId,
    val messageId: ChatMessageId,
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/domain/event/ProfilePictureUpdatedEvent.kt
package com.plcoding.chirp.domain.event

import com.plcoding.chirp.domain.type.UserId

data class ProfilePictureUpdatedEvent(
    val userId: UserId,
    val newUrl: String?
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/domain/exception/ChatNotFoundException.kt
package com.plcoding.chirp.domain.exception

class ChatNotFoundException: RuntimeException("Chat not found")

### FILE: chat/src/main/kotlin/com/plcoding/chirp/domain/exception/ChatParticipantNotFoundException.kt
package com.plcoding.chirp.domain.exception

import com.plcoding.chirp.domain.type.UserId

class ChatParticipantNotFoundException(
    private val id: UserId
): RuntimeException(
    "The chat participant with the ID $id was not found."
)

### FILE: chat/src/main/kotlin/com/plcoding/chirp/domain/exception/InvalidChatSizeException.kt
package com.plcoding.chirp.domain.exception

class InvalidChatSizeException: RuntimeException(
    "There must be at least 2 unique participants to create a chat."
)

### FILE: chat/src/main/kotlin/com/plcoding/chirp/domain/exception/InvalidProfilePictureException.kt
package com.plcoding.chirp.domain.exception

class InvalidProfilePictureException(
    override val message: String? = null
): RuntimeException(
    message ?: "Invalid profile picture data"
)

### FILE: chat/src/main/kotlin/com/plcoding/chirp/domain/exception/MessageNotFoundException.kt
package com.plcoding.chirp.domain.exception

import com.plcoding.chirp.domain.type.ChatMessageId

class MessageNotFoundException(
    private val id: ChatMessageId
): RuntimeException(
    "Message with ID $id not found"
)

### FILE: chat/src/main/kotlin/com/plcoding/chirp/domain/exception/StorageException.kt
package com.plcoding.chirp.domain.exception

class StorageException(
    override val message: String?
): RuntimeException(message ?: "Unable to store file")

### FILE: chat/src/main/kotlin/com/plcoding/chirp/domain/models/Chat.kt
package com.plcoding.chirp.domain.models

import com.plcoding.chirp.domain.type.ChatId
import java.time.Instant

data class Chat(
    val id: ChatId,
    val participants: Set<ChatParticipant>,
    val lastMessage: ChatMessage?,
    val creator: ChatParticipant,
    val lastActivityAt: Instant,
    val createdAt: Instant
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/domain/models/ChatMessage.kt
package com.plcoding.chirp.domain.models

import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.domain.type.ChatMessageId
import java.time.Instant

data class ChatMessage(
    val id: ChatMessageId,
    val chatId: ChatId,
    val sender: ChatParticipant,
    val content: String,
    val createdAt: Instant
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/domain/models/ChatParticipant.kt
package com.plcoding.chirp.domain.models

import com.plcoding.chirp.domain.type.UserId

data class ChatParticipant(
    val userId: UserId,
    val username: String,
    val email: String,
    val profilePictureUrl: String?
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/domain/models/ProfilePictureUploadCredentials.kt
package com.plcoding.chirp.domain.models

import java.time.Instant

data class ProfilePictureUploadCredentials(
    val uploadUrl: String,
    val publicUrl: String,
    val headers: Map<String, String>,
    val expiresAt: Instant
)


### FILE: chat/src/main/kotlin/com/plcoding/chirp/infra/database/entities/ChatEntity.kt
package com.plcoding.chirp.infra.database.entities

import com.plcoding.chirp.domain.type.ChatId
import jakarta.persistence.Entity
import jakarta.persistence.FetchType
import jakarta.persistence.GeneratedValue
import jakarta.persistence.GenerationType
import jakarta.persistence.Id
import jakarta.persistence.Index
import jakarta.persistence.JoinColumn
import jakarta.persistence.JoinTable
import jakarta.persistence.ManyToMany
import jakarta.persistence.ManyToOne
import jakarta.persistence.Table
import org.hibernate.annotations.CreationTimestamp
import java.time.Instant

@Entity
@Table(
    name = "chats",
    schema = "chat_service"
)
class ChatEntity(
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    var id: ChatId? = null,
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(
        name = "creator_id",
        nullable = false
    )
    var creator: ChatParticipantEntity,
    @ManyToMany(fetch = FetchType.LAZY)
    @JoinTable(
        name = "chat_participants_cross_ref",
        schema = "chat_service",
        joinColumns = [JoinColumn(name = "chat_id")],
        inverseJoinColumns = [JoinColumn(name = "user_id")],
        indexes = [
            // Answers efficiently:
            // Who is in chat X?
            Index(
                name = "idx_chat_participant_chat_id_user_id",
                columnList = "chat_id,user_id",
                unique = true
            ),
            // Answers efficiently:
            // What chats is user X in?
            Index(
                name = "idx_chat_participant_user_id_chat_id",
                columnList = "user_id,chat_id",
                unique = true
            ),
        ]
    )
    var participants: Set<ChatParticipantEntity> = emptySet(),
    @CreationTimestamp
    var createdAt: Instant = Instant.now(),
)

### FILE: chat/src/main/kotlin/com/plcoding/chirp/infra/database/entities/ChatMessageEntity.kt
package com.plcoding.chirp.infra.database.entities

import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.domain.type.ChatMessageId
import jakarta.persistence.Column
import jakarta.persistence.Entity
import jakarta.persistence.FetchType
import jakarta.persistence.GeneratedValue
import jakarta.persistence.GenerationType
import jakarta.persistence.Id
import jakarta.persistence.Index
import jakarta.persistence.JoinColumn
import jakarta.persistence.ManyToOne
import jakarta.persistence.Table
import org.hibernate.annotations.CreationTimestamp
import org.hibernate.annotations.OnDelete
import org.hibernate.annotations.OnDeleteAction
import java.time.Instant

@Entity
@Table(
    name = "chat_messages",
    schema = "chat_service",
    indexes = [
        Index(
            name = "idx_chat_message_chat_id_created_at",
            columnList = "chat_id,created_at DESC"
        )
    ]
)
class ChatMessageEntity(
    @Id
    var id: ChatMessageId? = null,
    @Column(nullable = false)
    var content: String,
    @Column(
        name = "chat_id",
        nullable = false,
        updatable = false
    )
    var chatId: ChatId,
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(
        name = "chat_id",
        nullable = false,
        insertable = false,
        updatable = false
    )
    @OnDelete(action = OnDeleteAction.CASCADE)
    var chat: ChatEntity? = null,
    @ManyToOne(fetch = FetchType.EAGER)
    @JoinColumn(
        name = "sender_id",
        nullable = false,
    )
    var sender: ChatParticipantEntity,
    @CreationTimestamp
    var createdAt: Instant = Instant.now()
)

### FILE: chat/src/main/kotlin/com/plcoding/chirp/infra/database/entities/ChatParticipantEntity.kt
package com.plcoding.chirp.infra.database.entities

import com.plcoding.chirp.domain.type.UserId
import jakarta.persistence.Column
import jakarta.persistence.Entity
import jakarta.persistence.Id
import jakarta.persistence.Index
import jakarta.persistence.Table
import org.hibernate.annotations.CreationTimestamp
import java.time.Instant

@Entity
@Table(
    name = "chat_participants",
    schema = "chat_service",
    indexes = [
        Index(name = "idx_chat_participant_username", columnList = "username"),
        Index(name = "idx_chat_participant_email", columnList = "email"),
    ]
)
class ChatParticipantEntity(
    @Id
    var userId: UserId,
    @Column(nullable = false, unique = true)
    var username: String,
    @Column(nullable = false, unique = true)
    var email: String,
    @Column(nullable = true)
    var profilePictureUrl: String? = null,
    @CreationTimestamp
    var createdAt: Instant = Instant.now()
)

### FILE: chat/src/main/kotlin/com/plcoding/chirp/infra/database/mappers/ChatMappers.kt
package com.plcoding.chirp.infra.database.mappers

import com.plcoding.chirp.domain.models.Chat
import com.plcoding.chirp.domain.models.ChatMessage
import com.plcoding.chirp.domain.models.ChatParticipant
import com.plcoding.chirp.infra.database.entities.ChatEntity
import com.plcoding.chirp.infra.database.entities.ChatMessageEntity
import com.plcoding.chirp.infra.database.entities.ChatParticipantEntity

fun ChatEntity.toChat(lastMessage: ChatMessage? = null): Chat {
    return Chat(
        id = id!!,
        participants = participants.map {
            it.toChatParticipant()
        }.toSet(),
        creator = creator.toChatParticipant(),
        lastActivityAt = lastMessage?.createdAt ?: createdAt,
        createdAt = createdAt,
        lastMessage = lastMessage
    )
}

fun ChatParticipantEntity.toChatParticipant(): ChatParticipant {
    return ChatParticipant(
        userId = userId,
        username = username,
        email = email,
        profilePictureUrl = profilePictureUrl
    )
}

fun ChatParticipant.toChatParticipantEntity(): ChatParticipantEntity {
    return ChatParticipantEntity(
        userId = userId,
        username = username,
        email = email,
        profilePictureUrl = profilePictureUrl
    )
}

fun ChatMessageEntity.toChatMessage(): ChatMessage {
    return ChatMessage(
        id = id!!,
        chatId = chatId,
        sender = sender.toChatParticipant(),
        content = content,
        createdAt = createdAt
    )
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/infra/database/repositories/ChatMessageRepository.kt
package com.plcoding.chirp.infra.database.repositories

import com.plcoding.chirp.infra.database.entities.ChatMessageEntity
import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.domain.type.ChatMessageId
import org.springframework.data.domain.Pageable
import org.springframework.data.domain.Slice
import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.data.jpa.repository.Query
import java.time.Instant

interface ChatMessageRepository: JpaRepository<ChatMessageEntity, ChatMessageId> {

    @Query("""
        SELECT m
        FROM ChatMessageEntity m
        WHERE m.chatId = :chatId
        AND m.createdAt < :before
        ORDER BY m.createdAt DESC
    """)
    fun findByChatIdBefore(
        chatId: ChatId,
        before: Instant,
        pageable: Pageable
    ): Slice<ChatMessageEntity>

    @Query("""
        SELECT m
        FROM ChatMessageEntity m
        LEFT JOIN FETCH m.sender
        WHERE m.chatId IN :chatIds
        AND (m.createdAt, m.id) = (
            SELECT m2.createdAt, m2.id
            FROM ChatMessageEntity m2
            WHERE m2.chatId = m.chatId
            ORDER BY m2.createdAt DESC 
            LIMIT 1
        )
    """)
    fun findLatestMessagesByChatIds(
        chatIds: Set<ChatId>
    ): List<ChatMessageEntity>
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/infra/database/repositories/ChatParticipantRepository.kt
package com.plcoding.chirp.infra.database.repositories

import com.plcoding.chirp.infra.database.entities.ChatParticipantEntity
import com.plcoding.chirp.domain.type.UserId
import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.data.jpa.repository.Query

interface ChatParticipantRepository: JpaRepository<ChatParticipantEntity, UserId> {
    fun findByUserIdIn(userIds: Set<UserId>): Set<ChatParticipantEntity>

    @Query("""
        SELECT p
        FROM ChatParticipantEntity p
        WHERE LOWER(p.username) = :query OR LOWER(p.email) = :query
    """)
    fun findByEmailOrUsername(query: String): ChatParticipantEntity?
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/infra/database/repositories/ChatRepository.kt
package com.plcoding.chirp.infra.database.repositories

import com.plcoding.chirp.infra.database.entities.ChatEntity
import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.domain.type.UserId
import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.data.jpa.repository.Query

interface ChatRepository: JpaRepository<ChatEntity, ChatId> {
    @Query("""
        SELECT c
        FROM ChatEntity c
        LEFT JOIN FETCH c.participants
        LEFT JOIN FETCH c.creator
        WHERE c.id = :id
        AND EXISTS (
            SELECT 1
            FROM c.participants p
            WHERE p.userId = :userId
        )
    """)
    fun findChatById(id: ChatId, userId: UserId): ChatEntity?

    @Query("""
        SELECT c
        FROM ChatEntity c
        LEFT JOIN FETCH c.participants
        LEFT JOIN FETCH c.creator
        WHERE EXISTS (
            SELECT 1
            FROM c.participants p
            WHERE p.userId = :userId
        )
    """)
    fun findAllByUserId(userId: UserId): List<ChatEntity>
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/infra/messaging/ChatUserEventListener.kt
package com.plcoding.chirp.infra.messaging

import com.plcoding.chirp.domain.models.ChatParticipant
import com.plcoding.chirp.service.ChatParticipantService
import com.plcoding.chirp.domain.events.user.UserEvent
import com.plcoding.chirp.infra.message_queue.MessageQueues
import org.slf4j.LoggerFactory
import org.springframework.amqp.rabbit.annotation.RabbitListener
import org.springframework.stereotype.Component

@Component
class ChatUserEventListener(
    private val chatParticipantService: ChatParticipantService
) {

    private val logger = LoggerFactory.getLogger(javaClass)

    @RabbitListener(queues = [MessageQueues.CHAT_USER_EVENTS])
    fun handleUserEvent(event: UserEvent) {
        logger.info("Received user event: {}", event)
        when(event) {
            is UserEvent.Verified -> {
                chatParticipantService.createChatParticipant(
                    chatParticipant = ChatParticipant(
                        userId = event.userId,
                        username = event.username,
                        email = event.email,
                        profilePictureUrl = null
                    )
                )
            }
            else -> Unit
        }
    }
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/infra/storage/SupabaseRestClientConfig.kt
package com.plcoding.chirp.infra.storage

import org.springframework.beans.factory.annotation.Value
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration
import org.springframework.web.client.RestClient

@Configuration
class SupabaseRestClientConfig(
    @param:Value("\${supabase.url}") private val supabaseUrl: String,
    @param:Value("\${supabase.service-key}") private val supabaseServiceKey: String,
) {

    @Bean
    fun supabaseRestClient(): RestClient {
        return RestClient.builder()
            .baseUrl(supabaseUrl)
            .defaultHeader("Authorization", "Bearer $supabaseServiceKey")
            .build()
    }
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/infra/storage/SupabaseStorageService.kt
package com.plcoding.chirp.infra.storage

import com.plcoding.chirp.domain.exception.InvalidProfilePictureException
import com.plcoding.chirp.domain.exception.StorageException
import com.plcoding.chirp.domain.models.ProfilePictureUploadCredentials
import com.plcoding.chirp.domain.type.UserId
import org.springframework.beans.factory.annotation.Value
import org.springframework.stereotype.Service
import org.springframework.web.client.RestClient
import java.time.Instant
import java.util.UUID

@Service
class SupabaseStorageService(
    @param:Value("\${supabase.url}") private val supabaseUrl: String,
    private val supabaseRestClient: RestClient,
) {
    companion object {
        private val allowedMimeTypes = mapOf(
            "image/jpeg" to "jpg",
            "image/jpg" to "jpg",
            "image/png" to "png",
            "image/webp" to "webp",
        )
    }

    fun generateSignedUploadUrl(userId: UserId, mimeType: String): ProfilePictureUploadCredentials {
        val extension = allowedMimeTypes[mimeType]
            ?: throw InvalidProfilePictureException("Invalid mime type $mimeType")

        val fileName = "user_${userId}_${UUID.randomUUID()}.$extension"
        val path = "profile-pictures/$fileName"

        val publicUrl = "$supabaseUrl/storage/v1/object/public/$path"

        return ProfilePictureUploadCredentials(
            uploadUrl = createSignedUrl(
                path = path,
                expiresInSeconds = 300
            ),
            publicUrl = publicUrl,
            headers = mapOf(
                "Content-Type" to mimeType
            ),
            expiresAt = Instant.now().plusSeconds(300)
        )
    }

    fun deleteFile(url: String) {
        val path = if(url.contains("/object/public/")) {
            url.substringAfter("/object/public/")
        } else throw StorageException("Invalid file URL format")

        val deleteUrl = "/storage/v1/object/$path"

        val response = supabaseRestClient
            .delete()
            .uri(deleteUrl)
            .retrieve()
            .toBodilessEntity()

        if(response.statusCode.isError) {
            throw StorageException("Unable to delete file: ${response.statusCode.value()}")
        }
    }

    private fun createSignedUrl(
        path: String,
        expiresInSeconds: Int
    ): String {
        val json = """
            { "expiresIn": $expiresInSeconds }
        """.trimIndent()

        val response = supabaseRestClient
            .post()
            .uri("/storage/v1/object/upload/sign/$path")
            .header("Content-Type", "application/json")
            .body(json)
            .retrieve()
            .body(SignedUploadResponse::class.java)
            ?: throw StorageException("Failed to create signed URL")

        return "$supabaseUrl/storage/v1${response.url}"
    }

    private data class SignedUploadResponse(
        val url: String
    )
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/service/ChatMessageService.kt
package com.plcoding.chirp.service

import com.plcoding.chirp.api.dto.ChatMessageDto
import com.plcoding.chirp.api.mappers.toChatMessageDto
import com.plcoding.chirp.domain.event.MessageDeletedEvent
import com.plcoding.chirp.domain.events.chat.ChatEvent
import com.plcoding.chirp.domain.exception.ChatNotFoundException
import com.plcoding.chirp.domain.exception.ChatParticipantNotFoundException
import com.plcoding.chirp.domain.exception.ForbiddenException
import com.plcoding.chirp.domain.exception.MessageNotFoundException
import com.plcoding.chirp.domain.models.ChatMessage
import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.domain.type.ChatMessageId
import com.plcoding.chirp.domain.type.UserId
import com.plcoding.chirp.infra.database.entities.ChatMessageEntity
import com.plcoding.chirp.infra.database.mappers.toChatMessage
import com.plcoding.chirp.infra.database.repositories.ChatMessageRepository
import com.plcoding.chirp.infra.database.repositories.ChatParticipantRepository
import com.plcoding.chirp.infra.database.repositories.ChatRepository
import com.plcoding.chirp.infra.message_queue.EventPublisher
import org.springframework.cache.annotation.CacheEvict
import org.springframework.context.ApplicationEventPublisher
import org.springframework.data.domain.PageRequest
import org.springframework.data.repository.findByIdOrNull
import org.springframework.stereotype.Component
import org.springframework.stereotype.Service
import org.springframework.transaction.annotation.Transactional
import java.time.Instant
import java.util.UUID

@Service
class ChatMessageService(
    private val chatRepository: ChatRepository,
    private val chatMessageRepository: ChatMessageRepository,
    private val chatParticipantRepository: ChatParticipantRepository,
    private val applicationEventPublisher: ApplicationEventPublisher,
    private val eventPublisher: EventPublisher,
    private val messageCacheManager: MessageCacheManager
) {

    @Transactional
    @CacheEvict(
        value = ["messages"],
        key = "#chatId",
    )
    fun sendMessage(
        chatId: ChatId,
        senderId: UserId,
        content: String,
        messageId: ChatMessageId? = null
    ): ChatMessage {
        val chat = chatRepository.findChatById(chatId, senderId)
            ?: throw ChatNotFoundException()
        val sender = chatParticipantRepository.findByIdOrNull(senderId)
            ?: throw ChatParticipantNotFoundException(senderId)

        val savedMessage = chatMessageRepository.saveAndFlush(
            ChatMessageEntity(
                id = messageId ?: UUID.randomUUID(),
                content = content.trim(),
                chatId = chatId,
                chat = chat,
                sender = sender
            )
        )

        eventPublisher.publish(
            event = ChatEvent.NewMessage(
                senderId = sender.userId,
                senderUsername = sender.username,
                recipientIds = chat.participants.map { it.userId }.toSet(),
                chatId = chatId,
                message = savedMessage.content
            )
        )

        return savedMessage.toChatMessage()
    }

    @Transactional
    fun deleteMessage(
        messageId: ChatMessageId,
        requestUserId: UserId
    ) {
        val message = chatMessageRepository.findByIdOrNull(messageId)
            ?: throw MessageNotFoundException(messageId)

        if(message.sender.userId != requestUserId) {
            throw ForbiddenException()
        }

        chatMessageRepository.delete(message)

        applicationEventPublisher.publishEvent(
            MessageDeletedEvent(
                chatId = message.chatId,
                messageId = messageId
            )
        )

        messageCacheManager.evictMessagesCache(message.chatId)
    }


}

@Component
class MessageCacheManager {
    @CacheEvict(
        value = ["messages"],
        key = "#chatId",
    )
    fun evictMessagesCache(chatId: ChatId) {
        // NO-OP: Let Spring handle the cache evict
    }
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/service/ChatParticipantService.kt
package com.plcoding.chirp.service

import com.plcoding.chirp.domain.models.ChatParticipant
import com.plcoding.chirp.infra.database.mappers.toChatParticipant
import com.plcoding.chirp.infra.database.mappers.toChatParticipantEntity
import com.plcoding.chirp.infra.database.repositories.ChatParticipantRepository
import com.plcoding.chirp.domain.type.UserId
import org.springframework.context.ApplicationEventPublisher
import org.springframework.data.repository.findByIdOrNull
import org.springframework.stereotype.Service

@Service
class ChatParticipantService(
    private val chatParticipantRepository: ChatParticipantRepository,
) {

    fun createChatParticipant(
        chatParticipant: ChatParticipant
    ) {
        chatParticipantRepository.save(
            chatParticipant.toChatParticipantEntity()
        )
    }

    fun findChatParticipantById(userId: UserId): ChatParticipant? {
        return chatParticipantRepository.findByIdOrNull(userId)?.toChatParticipant()
    }

    fun findChatParticipantByEmailOrUsername(
        query: String
    ): ChatParticipant? {
        val normalizedQuery = query.lowercase().trim()
        return chatParticipantRepository.findByEmailOrUsername(
            query = normalizedQuery
        )?.toChatParticipant()
    }
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/service/ChatService.kt
package com.plcoding.chirp.service

import com.plcoding.chirp.api.dto.ChatMessageDto
import com.plcoding.chirp.api.mappers.toChatMessageDto
import com.plcoding.chirp.domain.event.ChatCreatedEvent
import com.plcoding.chirp.domain.event.ChatParticipantLeftEvent
import com.plcoding.chirp.domain.event.ChatParticipantsJoinedEvent
import com.plcoding.chirp.domain.exception.ChatNotFoundException
import com.plcoding.chirp.domain.exception.ChatParticipantNotFoundException
import com.plcoding.chirp.domain.exception.ForbiddenException
import com.plcoding.chirp.domain.exception.InvalidChatSizeException
import com.plcoding.chirp.domain.models.Chat
import com.plcoding.chirp.domain.models.ChatMessage
import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.infra.database.entities.ChatEntity
import com.plcoding.chirp.infra.database.mappers.toChat
import com.plcoding.chirp.infra.database.repositories.ChatParticipantRepository
import com.plcoding.chirp.infra.database.repositories.ChatRepository
import com.plcoding.chirp.domain.type.UserId
import com.plcoding.chirp.infra.database.mappers.toChatMessage
import com.plcoding.chirp.infra.database.repositories.ChatMessageRepository
import org.springframework.cache.annotation.Cacheable
import org.springframework.context.ApplicationEventPublisher
import org.springframework.data.domain.PageRequest
import org.springframework.data.repository.findByIdOrNull
import org.springframework.stereotype.Service
import org.springframework.transaction.annotation.Transactional
import java.time.Instant

@Service
class ChatService(
    private val chatRepository: ChatRepository,
    private val chatParticipantRepository: ChatParticipantRepository,
    private val chatMessageRepository: ChatMessageRepository,
    private val applicationEventPublisher: ApplicationEventPublisher,
) {

    @Cacheable(
        value = ["messages"],
        key = "#chatId",
        condition = "#before == null && #pageSize <= 50",
        sync = true
    )
    fun getChatMessages(
        chatId: ChatId,
        before: Instant?,
        pageSize: Int
    ): List<ChatMessageDto> {
        return chatMessageRepository
            .findByChatIdBefore(
                chatId = chatId,
                before = before ?: Instant.now(),
                pageable = PageRequest.of(0, pageSize)
            )
            .content
            .asReversed()
            .map { it.toChatMessage().toChatMessageDto() }
    }

    fun getChatById(
        chatId: ChatId,
        requestUserId: UserId
    ): Chat? {
        return chatRepository
            .findChatById(chatId, requestUserId)
            ?.toChat(lastMessageForChat(chatId))
    }

    fun findChatsByUser(userId: UserId): List<Chat> {
        val chatEntities = chatRepository.findAllByUserId(userId)
        val chatIds = chatEntities.mapNotNull { it.id }
        val latestMessages = chatMessageRepository
            .findLatestMessagesByChatIds(chatIds.toSet())
            .associateBy { it.chatId }

        return chatEntities
            .map {
                it.toChat(lastMessage = latestMessages[it.id]?.toChatMessage())
            }
            .sortedByDescending { it.lastActivityAt }
    }

    @Transactional
    fun createChat(
        creatorId: UserId,
        otherUserIds: Set<UserId>
    ): Chat {
        val otherParticipants = chatParticipantRepository.findByUserIdIn(
            userIds = otherUserIds
        )

        val allParticipants = (otherParticipants + creatorId)
        if(allParticipants.size < 2) {
            throw InvalidChatSizeException()
        }

        val creator = chatParticipantRepository.findByIdOrNull(creatorId)
            ?: throw ChatParticipantNotFoundException(creatorId)

        return chatRepository.saveAndFlush(
            ChatEntity(
                creator = creator,
                participants = setOf(creator) + otherParticipants
            )
        ).toChat(lastMessage = null).also {
            applicationEventPublisher.publishEvent(
                ChatCreatedEvent(
                    chatId = it.id,
                    participantIds = it.participants.map { it.userId }
                )
            )
        }
    }

    @Transactional
    fun addParticipantsToChat(
        requestUserId: UserId,
        chatId: ChatId,
        userIds: Set<UserId>
    ): Chat {
        val chat = chatRepository.findByIdOrNull(chatId)
            ?: throw ChatNotFoundException()

        val isRequestingUserInChat = chat.participants.any {
            it.userId == requestUserId
        }
        if(!isRequestingUserInChat) {
            throw ForbiddenException()
        }

        val users = userIds.map { userId ->
            chatParticipantRepository.findByIdOrNull(userId)
                ?: throw ChatParticipantNotFoundException(userId)
        }

        val lastMessage = lastMessageForChat(chatId)
        val updatedChat = chatRepository.save(
            chat.apply {
                this.participants = chat.participants + users
            }
        ).toChat(lastMessage)

        applicationEventPublisher.publishEvent(
            ChatParticipantsJoinedEvent(
                chatId = chatId,
                userIds = userIds
            )
        )

        return updatedChat
    }

    @Transactional
    fun removeParticipantFromChat(
        chatId: ChatId,
        userId: UserId
    ) {
        val chat = chatRepository.findByIdOrNull(chatId)
            ?: throw ChatNotFoundException()
        val participant = chat.participants.find { it.userId == userId }
            ?: throw ChatParticipantNotFoundException(userId)

        val newParticipantsSize = chat.participants.size - 1
        if(newParticipantsSize == 0) {
            chatRepository.deleteById(chatId)
            return
        }

        chatRepository.save(
            chat.apply {
                this.participants = chat.participants - participant
            }
        )

        applicationEventPublisher.publishEvent(
            ChatParticipantLeftEvent(
                chatId = chatId,
                userId = userId
            )
        )
    }

    private fun lastMessageForChat(chatId: ChatId): ChatMessage? {
        return chatMessageRepository
            .findLatestMessagesByChatIds(setOf(chatId))
            .firstOrNull()
            ?.toChatMessage()
    }
}

### FILE: chat/src/main/kotlin/com/plcoding/chirp/service/ProfilePictureService.kt
package com.plcoding.chirp.service

import com.plcoding.chirp.domain.event.ProfilePictureUpdatedEvent
import com.plcoding.chirp.domain.exception.ChatParticipantNotFoundException
import com.plcoding.chirp.domain.exception.InvalidProfilePictureException
import com.plcoding.chirp.domain.models.ProfilePictureUploadCredentials
import com.plcoding.chirp.domain.type.UserId
import com.plcoding.chirp.infra.database.repositories.ChatParticipantRepository
import com.plcoding.chirp.infra.storage.SupabaseStorageService
import org.slf4j.LoggerFactory
import org.springframework.beans.factory.annotation.Value
import org.springframework.context.ApplicationEventPublisher
import org.springframework.data.repository.findByIdOrNull
import org.springframework.stereotype.Service
import org.springframework.transaction.annotation.Transactional

@Service
class ProfilePictureService(
    private val supabaseStorageService: SupabaseStorageService,
    private val chatParticipantRepository: ChatParticipantRepository,
    private val applicationEventPublisher: ApplicationEventPublisher,
    @param:Value("\${supabase.url}") private val supabaseUrl: String,
) {

    private val logger = LoggerFactory.getLogger(ProfilePictureService::class.java)

    fun generateUploadCredentials(
        userId: UserId,
        mimeType: String,
    ): ProfilePictureUploadCredentials {
        return supabaseStorageService.generateSignedUploadUrl(
            userId = userId,
            mimeType = mimeType
        )
    }

    @Transactional
    fun deleteProfilePicture(userId: UserId) {
        val participant = chatParticipantRepository.findByIdOrNull(userId)
            ?: throw ChatParticipantNotFoundException(userId)

        participant.profilePictureUrl?.let { url ->
            chatParticipantRepository.save(
                participant.apply { profilePictureUrl = null }
            )

            supabaseStorageService.deleteFile(url)

            applicationEventPublisher.publishEvent(
                ProfilePictureUpdatedEvent(
                    userId = userId,
                    newUrl = null
                )
            )
        }
    }

    @Transactional
    fun confirmProfilePictureUpload(userId: UserId, publicUrl: String) {
        if(!publicUrl.startsWith(supabaseUrl)) {
            throw InvalidProfilePictureException("Invalid profile picture URL")
        }

        val participant = chatParticipantRepository.findByIdOrNull(userId)
            ?: throw ChatParticipantNotFoundException(userId)

        val oldUrl = participant.profilePictureUrl

        chatParticipantRepository.save(
            participant.apply { profilePictureUrl = publicUrl }
        )

        try {
            oldUrl?.let {
                supabaseStorageService.deleteFile(oldUrl)
            }
        } catch(e: Exception) {
            logger.warn("Deleting old profile picture for $userId failed", e)
        }

        applicationEventPublisher.publishEvent(
            ProfilePictureUpdatedEvent(
                userId = userId,
                newUrl = publicUrl
            )
        )
    }
}

### FILE: common/build.gradle.kts
plugins {
    id("java-library")
    id("chirp.kotlin-common")
}

group = "com.plcoding"
version = "unspecified"

repositories {
    mavenCentral()
    maven { url = uri("https://repo.spring.io/milestone") }
    maven { url = uri("https://repo.spring.io/snapshot") }
}

dependencies {
    api(libs.kotlin.reflect)
    api(libs.jackson.module.kotlin)

    implementation(libs.spring.boot.starter.amqp)
    implementation(libs.spring.boot.starter.security)

    implementation(libs.jwt.api)
    runtimeOnly(libs.jwt.impl)
    runtimeOnly(libs.jwt.jackson)

    testImplementation(kotlin("test"))
}

tasks.test {
    useJUnitPlatform()
}

### FILE: common/src/main/kotlin/com/plcoding/chirp/api/exception_handling/CommonExceptionHandler.kt
package com.plcoding.chirp.api.exception_handling

import com.plcoding.chirp.domain.exception.ForbiddenException
import com.plcoding.chirp.domain.exception.UnauthorizedException
import org.springframework.http.HttpStatus
import org.springframework.web.bind.annotation.ExceptionHandler
import org.springframework.web.bind.annotation.ResponseStatus
import org.springframework.web.bind.annotation.RestControllerAdvice

@RestControllerAdvice
class CommonExceptionHandler {

    @ExceptionHandler(ForbiddenException::class)
    @ResponseStatus(HttpStatus.FORBIDDEN)
    fun onForbidden(e: ForbiddenException) = mapOf(
        "code" to "FORBIDDEN",
        "message" to e.message
    )

    @ExceptionHandler(UnauthorizedException::class)
    @ResponseStatus(HttpStatus.UNAUTHORIZED)
    fun onUnauthorized(e: UnauthorizedException) = mapOf(
        "code" to "UNAUTHORIZED",
        "message" to e.message
    )
}

### FILE: common/src/main/kotlin/com/plcoding/chirp/api/util/requestUserId.kt
package com.plcoding.chirp.api.util

import com.plcoding.chirp.domain.exception.UnauthorizedException
import com.plcoding.chirp.domain.type.UserId
import org.springframework.security.core.context.SecurityContextHolder

val requestUserId: UserId
    get() = SecurityContextHolder.getContext().authentication?.principal as? UserId
        ?: throw UnauthorizedException()

### FILE: common/src/main/kotlin/com/plcoding/chirp/domain/events/chat/ChatEvent.kt
package com.plcoding.chirp.domain.events.chat

import com.plcoding.chirp.domain.events.ChirpEvent
import com.plcoding.chirp.domain.events.user.UserEventConstants
import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.domain.type.UserId
import java.time.Instant
import java.util.UUID

sealed class ChatEvent(
    override val eventId: String = UUID.randomUUID().toString(),
    override val exchange: String = ChatEventConstants.CHAT_EXCHANGE,
    override val occurredAt: Instant = Instant.now(),
): ChirpEvent {

    data class NewMessage(
        val senderId: UserId,
        val senderUsername: String,
        val recipientIds: Set<UserId>,
        val chatId: ChatId,
        val message: String,
        override val eventKey: String = ChatEventConstants.CHAT_NEW_MESSAGE
    ): ChatEvent(), ChirpEvent
}

### FILE: common/src/main/kotlin/com/plcoding/chirp/domain/events/chat/ChatEventConstants.kt
package com.plcoding.chirp.domain.events.chat

object ChatEventConstants {
    const val CHAT_EXCHANGE = "chat.events"

    const val CHAT_NEW_MESSAGE = "chat.new_message"
}

### FILE: common/src/main/kotlin/com/plcoding/chirp/domain/events/ChirpEvent.kt
package com.plcoding.chirp.domain.events

import java.time.Instant

interface ChirpEvent {
    val eventId: String
    val eventKey: String
    val occurredAt: Instant
    val exchange: String
}

### FILE: common/src/main/kotlin/com/plcoding/chirp/domain/events/user/UserEvent.kt
package com.plcoding.chirp.domain.events.user

import com.plcoding.chirp.domain.events.ChirpEvent
import com.plcoding.chirp.domain.type.UserId
import java.time.Instant
import java.util.UUID

sealed class UserEvent(
    override val eventId: String = UUID.randomUUID().toString(),
    override val exchange: String = UserEventConstants.USER_EXCHANGE,
    override val occurredAt: Instant = Instant.now(),
): ChirpEvent {

    data class Created(
        val userId: UserId,
        val email: String,
        val username: String,
        val verificationToken: String,
        override val eventKey: String = UserEventConstants.USER_CREATED_KEY
    ): UserEvent(), ChirpEvent

    data class Verified(
        val userId: UserId,
        val email: String,
        val username: String,
        override val eventKey: String = UserEventConstants.USER_VERIFIED
    ): UserEvent(), ChirpEvent

    data class RequestResendVerification(
        val userId: UserId,
        val email: String,
        val username: String,
        val verificationToken: String,
        override val eventKey: String = UserEventConstants.USER_REQUEST_RESEND_VERIFICATION
    ): UserEvent(), ChirpEvent

    data class RequestResetPassword(
        val userId: UserId,
        val email: String,
        val username: String,
        val passwordResetToken: String,
        val expiresInMinutes: Long,
        override val eventKey: String = UserEventConstants.USER_REQUEST_RESET_PASSWORD
    ): UserEvent(), ChirpEvent
}

### FILE: common/src/main/kotlin/com/plcoding/chirp/domain/events/user/UserEventConstants.kt
package com.plcoding.chirp.domain.events.user

object UserEventConstants {

    const val USER_EXCHANGE = "user.events"

    const val USER_CREATED_KEY = "user.created"
    const val USER_VERIFIED = "user.verified"
    const val USER_REQUEST_RESEND_VERIFICATION = "user.request_resend_verification"
    const val USER_REQUEST_RESET_PASSWORD = "user.request_reset_password"
}

### FILE: common/src/main/kotlin/com/plcoding/chirp/domain/exception/ForbiddenException.kt
package com.plcoding.chirp.domain.exception

class ForbiddenException: RuntimeException("You are not allowed to do that")

### FILE: common/src/main/kotlin/com/plcoding/chirp/domain/exception/InvalidTokenException.kt
package com.plcoding.chirp.domain.exception

import java.lang.RuntimeException

class InvalidTokenException(
    override val message: String?
): RuntimeException(
    message ?: "Invalid token"
)

### FILE: common/src/main/kotlin/com/plcoding/chirp/domain/exception/UnauthorizedException.kt
package com.plcoding.chirp.domain.exception

class UnauthorizedException: RuntimeException("Missing auth details")

### FILE: common/src/main/kotlin/com/plcoding/chirp/domain/type/ChatId.kt
package com.plcoding.chirp.domain.type

import java.util.UUID

typealias ChatId = UUID

### FILE: common/src/main/kotlin/com/plcoding/chirp/domain/type/ChatMessageId.kt
package com.plcoding.chirp.domain.type

import java.util.UUID

typealias ChatMessageId = UUID

### FILE: common/src/main/kotlin/com/plcoding/chirp/domain/type/UserId.kt
package com.plcoding.chirp.domain.type

import java.util.UUID

typealias UserId = UUID

### FILE: common/src/main/kotlin/com/plcoding/chirp/infra/message_queue/EventPublisher.kt
package com.plcoding.chirp.infra.message_queue

import com.plcoding.chirp.domain.events.ChirpEvent
import org.slf4j.LoggerFactory
import org.springframework.amqp.rabbit.core.RabbitTemplate
import org.springframework.stereotype.Component

@Component
class EventPublisher(
    private val rabbitTemplate: RabbitTemplate
) {

    private val logger = LoggerFactory.getLogger(javaClass)

    fun <T: ChirpEvent> publish(event: T) {
        try {
            rabbitTemplate.convertAndSend(
                event.exchange,
                event.eventKey,
                event
            )
            logger.info("Successfully published event: ${event.eventKey}")
        } catch(e: Exception) {
            logger.error("Failed to publish ${event.eventKey} event", e)
        }
    }
}

### FILE: common/src/main/kotlin/com/plcoding/chirp/infra/message_queue/MessageQueues.kt
package com.plcoding.chirp.infra.message_queue

object MessageQueues {
    const val NOTIFICATION_USER_EVENTS = "notification.user.events"
    const val NOTIFICATION_CHAT_EVENTS = "notification.chat.events"
    const val CHAT_USER_EVENTS = "chat.user.events"
}

### FILE: common/src/main/kotlin/com/plcoding/chirp/infra/message_queue/RabbitMqConfig.kt
@file:Suppress("DEPRECATION")

package com.plcoding.chirp.infra.message_queue

import com.plcoding.chirp.domain.events.ChirpEvent
import com.plcoding.chirp.domain.events.chat.ChatEventConstants
import com.plcoding.chirp.domain.events.user.UserEventConstants
import org.springframework.amqp.core.Binding
import org.springframework.amqp.core.BindingBuilder
import org.springframework.amqp.core.Queue
import org.springframework.amqp.core.TopicExchange
import org.springframework.amqp.rabbit.connection.ConnectionFactory
import org.springframework.amqp.rabbit.core.RabbitTemplate
import org.springframework.amqp.support.converter.JacksonJavaTypeMapper
import org.springframework.amqp.support.converter.JacksonJsonMessageConverter
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration
import org.springframework.transaction.annotation.EnableTransactionManagement
import tools.jackson.databind.DefaultTyping
import tools.jackson.databind.json.JsonMapper
import tools.jackson.databind.jsontype.BasicPolymorphicTypeValidator
import tools.jackson.module.kotlin.kotlinModule

@Configuration
@EnableTransactionManagement
class RabbitMqConfig {

    @Bean
    fun messageConverter(): JacksonJsonMessageConverter {
        val polymorphicTypeValidator = BasicPolymorphicTypeValidator.builder()
            .allowIfBaseType(ChirpEvent::class.java)
            .allowIfSubType("java.util.") // Allow Java lists
            .allowIfSubType("kotlin.collections.") // Kotlin collections
            .build()

        val objectMapper = JsonMapper.builder()
            .addModule(kotlinModule())
            .polymorphicTypeValidator(polymorphicTypeValidator)
            .activateDefaultTyping(polymorphicTypeValidator, DefaultTyping.NON_FINAL)
            .build()

        return JacksonJsonMessageConverter(objectMapper).apply {
            typePrecedence = JacksonJavaTypeMapper.TypePrecedence.TYPE_ID
        }
    }

    @Bean
    fun rabbitTemplate(
        connectionFactory: ConnectionFactory,
        messageConverter: JacksonJsonMessageConverter,
    ): RabbitTemplate {
        return RabbitTemplate(connectionFactory).apply {
            this.messageConverter = messageConverter
        }
    }

    @Bean
    fun userExchange() = TopicExchange(
        UserEventConstants.USER_EXCHANGE,
        true,
        false
    )

    @Bean
    fun chatExchange() = TopicExchange(
        ChatEventConstants.CHAT_EXCHANGE,
        true,
        false
    )

    @Bean
    fun chatUserEventsQueue() = Queue(
        MessageQueues.CHAT_USER_EVENTS,
        true
    )

    @Bean
    fun notificationUserEventsQueue() = Queue(
        MessageQueues.NOTIFICATION_USER_EVENTS,
        true
    )

    @Bean
    fun notificationChatEventsQueue() = Queue(
        MessageQueues.NOTIFICATION_CHAT_EVENTS,
        true
    )

    @Bean
    fun notificationChatEventsBinding(
        notificationChatEventsQueue: Queue,
        chatExchange: TopicExchange,
    ): Binding {
        return BindingBuilder
            .bind(notificationChatEventsQueue)
            .to(chatExchange)
            .with(ChatEventConstants.CHAT_NEW_MESSAGE)
    }

    @Bean
    fun notificationUserEventsBinding(
        notificationUserEventsQueue: Queue,
        userExchange: TopicExchange,
    ): Binding {
        return BindingBuilder
            .bind(notificationUserEventsQueue)
            .to(userExchange)
            .with("user.*")
    }

    @Bean
    fun chatUserEventsBinding(
        chatUserEventsQueue: Queue,
        userExchange: TopicExchange,
    ): Binding {
        return BindingBuilder
            .bind(chatUserEventsQueue)
            .to(userExchange)
            .with("user.*")
    }
}

### FILE: common/src/main/kotlin/com/plcoding/chirp/service/JwtService.kt
package com.plcoding.chirp.service

import com.plcoding.chirp.domain.exception.InvalidTokenException
import com.plcoding.chirp.domain.type.UserId
import io.jsonwebtoken.Claims
import io.jsonwebtoken.Jwts
import io.jsonwebtoken.security.Keys
import org.springframework.beans.factory.annotation.Value
import org.springframework.stereotype.Service
import java.util.Date
import java.util.UUID
import kotlin.io.encoding.Base64

@Service
class JwtService(
    @param:Value("\${jwt.secret}") private val secretBase64: String,
    @param:Value("\${jwt.expiration-minutes}") private val expirationMinutes: Int,
) {

    private val secretKey = Keys.hmacShaKeyFor(
        Base64.Default.decode(secretBase64)
    )
    private val accessTokenValidityMs = expirationMinutes * 60 * 1000L
    val refreshTokenValidityMs = 30 * 24 * 60 * 60 * 1000L

    fun generateAccessToken(userId: UserId): String {
        return generateToken(
            userId = userId,
            type = "access",
            expiry = accessTokenValidityMs
        )
    }

    fun generateRefreshToken(userId: UserId): String {
        return generateToken(
            userId = userId,
            type = "refresh",
            expiry = refreshTokenValidityMs
        )
    }

    fun validateAccessToken(token: String): Boolean {
        val claims = parseAllClaims(token) ?: return false
        val tokenType = claims["type"] as? String ?: return false
        return tokenType == "access"
    }

    fun validateRefreshToken(token: String): Boolean {
        val claims = parseAllClaims(token) ?: return false
        val tokenType = claims["type"] as? String ?: return false
        return tokenType == "refresh"
    }

    fun getUserIdFromToken(token: String): UserId {
        val claims = parseAllClaims(token) ?: throw InvalidTokenException(
            message = "The attached JWT token is not valid"
        )
        return UUID.fromString(claims.subject)
    }

    private fun generateToken(
        userId: UserId,
        type: String,
        expiry: Long
    ): String {
        val now = Date()
        val expiryDate = Date(now.time + expiry)
        return Jwts.builder()
            .subject(userId.toString())
            .claim("type", type)
            .issuedAt(now)
            .expiration(expiryDate)
            .signWith(secretKey, Jwts.SIG.HS256)
            .compact()
    }

    private fun parseAllClaims(token: String): Claims? {
        val rawToken = if(token.startsWith("Bearer ")) {
            token.removePrefix("Bearer ")
        } else token

        return try {
            Jwts.parser()
                .verifyWith(secretKey)
                .build()
                .parseSignedClaims(rawToken)
                .payload
        } catch(e: Exception) {
            null
        }
    }
}

### FILE: gradle/wrapper/gradle-wrapper.properties
distributionBase=GRADLE_USER_HOME
distributionPath=wrapper/dists
distributionUrl=https\://services.gradle.org/distributions/gradle-8.14.3-bin.zip
networkTimeout=10000
validateDistributionUrl=true
zipStoreBase=GRADLE_USER_HOME
zipStorePath=wrapper/dists


### FILE: notification/build.gradle.kts
plugins {
    id("java-library")
    id("chirp.spring-boot-service")
    kotlin("plugin.jpa")
}

group = "com.plcoding"
version = "unspecified"

repositories {
    mavenCentral()
    maven { url = uri("https://repo.spring.io/milestone") }
    maven { url = uri("https://repo.spring.io/snapshot") }
}

dependencies {
    implementation(projects.common)

    implementation(libs.firebase.admin.sdk)

    implementation(libs.spring.boot.starter.web)
    implementation(libs.spring.boot.starter.mail)
    implementation(libs.spring.boot.starter.amqp)
    implementation(libs.spring.boot.starter.thymeleaf)
    implementation(libs.spring.boot.starter.validation)
    implementation(libs.spring.boot.starter.data.jpa)

    runtimeOnly(libs.postgresql)

    testImplementation(kotlin("test"))
}

tasks.test {
    useJUnitPlatform()
}

### FILE: notification/src/main/kotlin/com/plcoding/chirp/api/controllers/DeviceTokenController.kt
package com.plcoding.chirp.api.controllers

import com.plcoding.chirp.api.dto.DeviceTokenDto
import com.plcoding.chirp.api.dto.RegisterDeviceRequest
import com.plcoding.chirp.api.mappers.toDeviceTokenDto
import com.plcoding.chirp.api.mappers.toPlatformDto
import com.plcoding.chirp.api.util.requestUserId
import com.plcoding.chirp.service.PushNotificationService
import jakarta.validation.Valid
import org.springframework.web.bind.annotation.DeleteMapping
import org.springframework.web.bind.annotation.PathVariable
import org.springframework.web.bind.annotation.PostMapping
import org.springframework.web.bind.annotation.RequestBody
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RestController

@RestController
@RequestMapping("/api/notification")
class DeviceTokenController(private val pushNotificationService: PushNotificationService) {

    @PostMapping("/register")
    fun registerDeviceToken(
        @Valid @RequestBody body: RegisterDeviceRequest
    ): DeviceTokenDto {
        return pushNotificationService.registerDevice(
            userId = requestUserId,
            token = body.token,
            platform = body.platform.toPlatformDto()
        ).toDeviceTokenDto()
    }

    @DeleteMapping("/{token}")
    fun unregisterDeviceToken(
        @PathVariable("token") token: String
    ) {
        pushNotificationService.unregisterDevice(token)
    }
}

### FILE: notification/src/main/kotlin/com/plcoding/chirp/api/dto/DeviceTokenDto.kt
package com.plcoding.chirp.api.dto

import com.plcoding.chirp.domain.type.UserId
import java.time.Instant

data class DeviceTokenDto(
    val userId: UserId,
    val token: String,
    val createdAt: Instant
)


### FILE: notification/src/main/kotlin/com/plcoding/chirp/api/dto/RegisterDeviceRequest.kt
package com.plcoding.chirp.api.dto

import jakarta.validation.constraints.NotBlank

data class RegisterDeviceRequest(
    @field:NotBlank
    val token: String,
    val platform: PlatformDto
)

enum class PlatformDto {
    ANDROID, IOS
}

### FILE: notification/src/main/kotlin/com/plcoding/chirp/api/exception_handling/NotificationExceptionHandler.kt
package com.plcoding.chirp.api.exception_handling

import com.plcoding.chirp.domain.exception.InvalidDeviceTokenException
import org.springframework.http.HttpStatus
import org.springframework.web.bind.annotation.ExceptionHandler
import org.springframework.web.bind.annotation.ResponseStatus
import org.springframework.web.bind.annotation.RestControllerAdvice

@RestControllerAdvice
class NotificationExceptionHandler {

    @ExceptionHandler(InvalidDeviceTokenException::class)
    @ResponseStatus(HttpStatus.BAD_REQUEST)
    fun onInvalidDeviceToken(e: InvalidDeviceTokenException) = mapOf(
        "code" to "INVALID_DEVICE_TOKEN",
        "message" to e.message
    )
}

### FILE: notification/src/main/kotlin/com/plcoding/chirp/api/mappers/DeviceTokenMappers.kt
package com.plcoding.chirp.api.mappers

import com.plcoding.chirp.api.dto.DeviceTokenDto
import com.plcoding.chirp.api.dto.PlatformDto
import com.plcoding.chirp.domain.model.DeviceToken

fun DeviceToken.toDeviceTokenDto(): DeviceTokenDto {
    return DeviceTokenDto(
        userId = userId,
        token = token,
        createdAt = createdAt
    )
}

fun PlatformDto.toPlatformDto(): DeviceToken.Platform {
    return when(this) {
        PlatformDto.ANDROID -> DeviceToken.Platform.ANDROID
        PlatformDto.IOS -> DeviceToken.Platform.IOS
    }
}

### FILE: notification/src/main/kotlin/com/plcoding/chirp/domain/exception/InvalidDeviceTokenException.kt
package com.plcoding.chirp.domain.exception

class InvalidDeviceTokenException: RuntimeException("Invalid device token.")

### FILE: notification/src/main/kotlin/com/plcoding/chirp/domain/model/DeviceToken.kt
package com.plcoding.chirp.domain.model

import com.plcoding.chirp.domain.type.UserId
import java.time.Instant

data class DeviceToken(
    val id: Long,
    val userId: UserId,
    val token: String,
    val platform: Platform,
    val createdAt: Instant = Instant.now(),
) {
    enum class Platform {
        ANDROID, IOS
    }
}

### FILE: notification/src/main/kotlin/com/plcoding/chirp/domain/model/PushNotification.kt
package com.plcoding.chirp.domain.model

import com.plcoding.chirp.domain.type.ChatId
import java.util.UUID

data class PushNotification(
    val id: String = UUID.randomUUID().toString(),
    val title: String,
    val recipients: List<DeviceToken>,
    val message: String,
    val chatId: ChatId,
    val data: Map<String, String>
)


### FILE: notification/src/main/kotlin/com/plcoding/chirp/domain/model/PushNotificationSendResult.kt
package com.plcoding.chirp.domain.model

data class PushNotificationSendResult(
    val succeeded: List<DeviceToken>,
    val temporaryFailures: List<DeviceToken>,
    val permanentFailures: List<DeviceToken>,
)


### FILE: notification/src/main/kotlin/com/plcoding/chirp/infra/database/DeviceTokenEntity.kt
package com.plcoding.chirp.infra.database

import com.plcoding.chirp.domain.type.UserId
import jakarta.persistence.Column
import jakarta.persistence.Entity
import jakarta.persistence.EnumType
import jakarta.persistence.Enumerated
import jakarta.persistence.GeneratedValue
import jakarta.persistence.GenerationType
import jakarta.persistence.Id
import jakarta.persistence.Index
import jakarta.persistence.Table
import org.hibernate.annotations.CreationTimestamp
import java.time.Instant

@Entity
@Table(
    name = "device_tokens",
    schema = "notification_service",
    indexes = [
        Index(name = "idx_device_tokens_user_id", columnList = "user_id"),
        Index(name = "idx_device_tokens_token", columnList = "token", unique = true),
    ]
)
class DeviceTokenEntity(
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    var id: Long = 0,
    @Column(nullable = false)
    var userId: UserId,
    @Column(nullable = false)
    var token: String,
    @Enumerated(EnumType.STRING)
    @Column(nullable = false)
    var platform: PlatformEntity,
    @CreationTimestamp
    var createdAt: Instant = Instant.now(),
)

### FILE: notification/src/main/kotlin/com/plcoding/chirp/infra/database/DeviceTokenRepository.kt
package com.plcoding.chirp.infra.database

import com.plcoding.chirp.domain.type.UserId
import org.springframework.data.jpa.repository.JpaRepository

interface DeviceTokenRepository: JpaRepository<DeviceTokenEntity, Long> {
    fun findByUserIdIn(userIds: List<UserId>): List<DeviceTokenEntity>
    fun findByToken(token: String): DeviceTokenEntity?
    fun deleteByToken(token: String)
}

### FILE: notification/src/main/kotlin/com/plcoding/chirp/infra/database/PlatformEntity.kt
package com.plcoding.chirp.infra.database

enum class PlatformEntity {
    ANDROID, IOS
}

### FILE: notification/src/main/kotlin/com/plcoding/chirp/infra/mappers/DeviceTokenMappers.kt
package com.plcoding.chirp.infra.mappers

import com.plcoding.chirp.domain.model.DeviceToken
import com.plcoding.chirp.infra.database.DeviceTokenEntity

fun DeviceTokenEntity.toDeviceToken(): DeviceToken {
    return DeviceToken(
        userId = userId,
        token = token,
        platform = platform.toPlatform(),
        createdAt = createdAt,
        id = id
    )
}

### FILE: notification/src/main/kotlin/com/plcoding/chirp/infra/mappers/PlatformMappers.kt
package com.plcoding.chirp.infra.mappers

import com.plcoding.chirp.domain.model.DeviceToken
import com.plcoding.chirp.infra.database.DeviceTokenEntity
import com.plcoding.chirp.infra.database.PlatformEntity

fun DeviceToken.Platform.toPlatformEntity(): PlatformEntity {
    return when(this) {
        DeviceToken.Platform.ANDROID -> PlatformEntity.ANDROID
        DeviceToken.Platform.IOS -> PlatformEntity.IOS
    }
}

fun PlatformEntity.toPlatform(): DeviceToken.Platform {
    return when(this) {
        PlatformEntity.ANDROID -> DeviceToken.Platform.ANDROID
        PlatformEntity.IOS -> DeviceToken.Platform.IOS
    }
}

### FILE: notification/src/main/kotlin/com/plcoding/chirp/infra/message_queue/NotificationChatEventListener.kt
package com.plcoding.chirp.infra.message_queue

import com.plcoding.chirp.domain.events.chat.ChatEvent
import com.plcoding.chirp.domain.events.user.UserEvent
import com.plcoding.chirp.service.EmailService
import com.plcoding.chirp.service.PushNotificationService
import org.springframework.amqp.rabbit.annotation.RabbitListener
import org.springframework.stereotype.Component
import java.time.Duration

@Component
class NotificationChatEventListener(
    private val pushNotificationService: PushNotificationService
) {

    @RabbitListener(queues = [MessageQueues.NOTIFICATION_CHAT_EVENTS])
    fun handleUserEvent(event: ChatEvent) {
        when(event) {
            is ChatEvent.NewMessage -> {
                pushNotificationService.sendNewMessageNotifications(
                    recipientUserIds = event.recipientIds.toList(),
                    senderUserId = event.senderId,
                    senderUsername = event.senderUsername,
                    message = event.message,
                    chatId = event.chatId
                )
            }
        }
    }
}

### FILE: notification/src/main/kotlin/com/plcoding/chirp/infra/message_queue/NotificationUserEventListener.kt
package com.plcoding.chirp.infra.message_queue

import com.plcoding.chirp.domain.events.user.UserEvent
import com.plcoding.chirp.service.EmailService
import org.springframework.amqp.rabbit.annotation.RabbitListener
import org.springframework.stereotype.Component
import java.time.Duration

@Component
class NotificationUserEventListener(private val emailService: EmailService) {

    @RabbitListener(queues = [MessageQueues.NOTIFICATION_USER_EVENTS])
    fun handleUserEvent(event: UserEvent) {
        when(event) {
            is UserEvent.Created -> {
                emailService.sendVerificationEmail(
                    email = event.email,
                    username = event.username,
                    userId = event.userId,
                    token = event.verificationToken
                )
            }
            is UserEvent.RequestResendVerification -> {
                emailService.sendVerificationEmail(
                    email = event.email,
                    username = event.username,
                    userId = event.userId,
                    token = event.verificationToken
                )
            }
            is UserEvent.RequestResetPassword -> {
                emailService.sendPasswordResetEmail(
                    email = event.email,
                    username = event.username,
                    userId = event.userId,
                    token = event.passwordResetToken,
                    expiresIn = Duration.ofMinutes(event.expiresInMinutes)
                )
            }
            else -> Unit
        }
    }
}

### FILE: notification/src/main/kotlin/com/plcoding/chirp/infra/push_notification/FirebasePushNotificationService.kt
package com.plcoding.chirp.infra.push_notification

import com.google.auth.oauth2.GoogleCredentials
import com.google.firebase.FirebaseApp
import com.google.firebase.FirebaseOptions
import com.google.firebase.messaging.AndroidConfig
import com.google.firebase.messaging.ApnsConfig
import com.google.firebase.messaging.Aps
import com.google.firebase.messaging.BatchResponse
import com.google.firebase.messaging.FirebaseMessaging
import com.google.firebase.messaging.FirebaseMessagingException
import com.google.firebase.messaging.Message
import com.google.firebase.messaging.MessagingErrorCode
import com.google.firebase.messaging.Notification
import com.plcoding.chirp.domain.model.DeviceToken
import com.plcoding.chirp.domain.model.PushNotification
import com.plcoding.chirp.domain.model.PushNotificationSendResult
import jakarta.annotation.PostConstruct
import org.slf4j.LoggerFactory
import org.springframework.beans.factory.annotation.Value
import org.springframework.core.io.ResourceLoader
import org.springframework.stereotype.Service

@Service
class FirebasePushNotificationService(
    @param:Value("\${firebase.credentials-path}")
    private val credentialsPath: String,
    private val resourceLoader: ResourceLoader
) {

    private val logger = LoggerFactory.getLogger(FirebasePushNotificationService::class.java)

    @PostConstruct
    fun initialize() {
        try {
            val serviceAccount = resourceLoader.getResource(credentialsPath)

            val options = FirebaseOptions.builder()
                .setCredentials(GoogleCredentials.fromStream(serviceAccount.inputStream))
                .build()

            FirebaseApp.initializeApp(options)
            logger.info("Firebase Admin SDK initialized successfully")
        } catch(e: Exception) {
            logger.error("Error initializing Firebase Admin SDK", e)
            throw e
        }
    }

    fun isValidToken(token: String): Boolean {
        val message = Message.builder()
            .setToken(token)
            .build()

        return try {
            FirebaseMessaging.getInstance().send(message, true)
            true
        } catch(e: FirebaseMessagingException) {
            logger.warn("Failed to validate Firebase token", e)
            false
        }
    }

    fun sendNotification(notification: PushNotification): PushNotificationSendResult {
        val messages = notification.recipients.map { recipient ->
            Message.builder()
                .setToken(recipient.token)
                .setNotification(
                    Notification.builder()
                        .setTitle(notification.title)
                        .setBody(notification.message)
                        .build()
                )
                .apply {
                    notification.data.forEach { (key, value) ->
                        putData(key, value)
                    }

                    when(recipient.platform) {
                        DeviceToken.Platform.ANDROID -> {
                            setAndroidConfig(
                                AndroidConfig.builder()
                                    .setPriority(AndroidConfig.Priority.HIGH)
                                    .setCollapseKey(notification.chatId.toString())
                                    .build()
                            )
                        }
                        DeviceToken.Platform.IOS -> {
                            setApnsConfig(
                                ApnsConfig.builder()
                                    .setAps(
                                        Aps.builder()
                                            .setSound("default")
                                            .setThreadId(notification.chatId.toString())
                                            .build()
                                    )
                                    .build()
                            )
                        }
                    }
                }
                .build()
        }

        return FirebaseMessaging
            .getInstance()
            .sendEach(messages)
            .toSendResult(notification.recipients)
    }

    private fun BatchResponse.toSendResult(
        allDeviceTokens: List<DeviceToken>
    ): PushNotificationSendResult {
        val succeeded = mutableListOf<DeviceToken>()
        val temporaryFailures = mutableListOf<DeviceToken>()
        val permanentFailures = mutableListOf<DeviceToken>()

        responses.forEachIndexed { index, sendResponse ->
            val deviceToken = allDeviceTokens[index]
            if(sendResponse.isSuccessful) {
                succeeded.add(deviceToken)
            } else {
                val errorCode = sendResponse.exception?.messagingErrorCode

                logger.warn("Failed to send notification to token ${deviceToken.token}: $errorCode")

                when(errorCode) {
                    MessagingErrorCode.UNREGISTERED,
                    MessagingErrorCode.SENDER_ID_MISMATCH,
                    MessagingErrorCode.INVALID_ARGUMENT,
                    MessagingErrorCode.THIRD_PARTY_AUTH_ERROR -> {
                        permanentFailures.add(deviceToken)
                    }
                    MessagingErrorCode.INTERNAL,
                    MessagingErrorCode.QUOTA_EXCEEDED,
                    MessagingErrorCode.UNAVAILABLE,
                    null -> {
                        temporaryFailures.add(deviceToken)
                    }
                }
            }
        }

        logger.debug("Push notifications sent. Succeeded: ${succeeded.size}, " +
                "temporary failures: ${temporaryFailures.size}, permanent failures: ${permanentFailures.size}")

        return PushNotificationSendResult(
            succeeded = succeeded.toList(),
            temporaryFailures = temporaryFailures.toList(),
            permanentFailures = permanentFailures.toList(),
        )
    }
}

### FILE: notification/src/main/kotlin/com/plcoding/chirp/service/EmailService.kt
package com.plcoding.chirp.service

import com.plcoding.chirp.domain.type.UserId
import org.slf4j.LoggerFactory
import org.springframework.beans.factory.annotation.Value
import org.springframework.mail.MailException
import org.springframework.mail.javamail.JavaMailSender
import org.springframework.mail.javamail.MimeMessageHelper
import org.springframework.stereotype.Service
import org.springframework.web.util.UriComponentsBuilder
import java.time.Duration

@Service
class EmailService(
    private val javaMailSender: JavaMailSender,
    private val templateService: EmailTemplateService,
    @param:Value("\${chirp.email.from}")
    private val emailFrom: String,
    @param:Value("\${chirp.email.url}")
    private val baseUrl: String,
) {

    private val logger = LoggerFactory.getLogger(javaClass)

    fun sendVerificationEmail(
        email: String,
        username: String,
        userId: UserId,
        token: String
    ) {
        logger.info("Sending verification email for user $userId")

        val verificationUrl = UriComponentsBuilder
            .fromUriString("$baseUrl/api/auth/verify")
            .queryParam("token", token)
            .build()
            .toUriString()

        // Same URL, but uses chirp:// scheme which allows easier testing of deep links
        // without having to verify them with Apple/Google
        val devVerificationUrl = UriComponentsBuilder
            .fromUriString("$baseUrl/api/auth/verify")
            .scheme("chirp")
            .queryParam("token", token)
            .build()
            .toUriString()

        val htmlContent = templateService.processTemplate(
            templateName = "emails/account-verification",
            variables = mapOf(
                "username" to username,
                "verificationUrl" to verificationUrl,
                "devVerificationUrl" to devVerificationUrl
            )
        )

        sendHtmlEmail(
            to = email,
            subject = "Verify your Chirp account",
            html = htmlContent
        )
    }

    fun sendPasswordResetEmail(
        email: String,
        username: String,
        userId: UserId,
        token: String,
        expiresIn: Duration
    ) {
        logger.info("Sending password reset email for user $userId")

        val resetPasswordUrl = UriComponentsBuilder
            .fromUriString("$baseUrl/api/auth/reset-password")
            .queryParam("token", token)
            .build()
            .toUriString()

        // Same URL, but uses chirp:// scheme which allows easier testing of deep links
        // without having to verify them with Apple/Google
        val devUrl = UriComponentsBuilder
            .fromUriString("$baseUrl/api/auth/reset-password")
            .scheme("chirp")
            .queryParam("token", token)
            .build()
            .toUriString()

        val htmlContent = templateService.processTemplate(
            templateName = "emails/reset-password",
            variables = mapOf(
                "username" to username,
                "resetPasswordUrl" to resetPasswordUrl,
                "devResetPasswordUrl" to devUrl,
                "expiresInMinutes" to expiresIn.toMinutes()
            )
        )

        sendHtmlEmail(
            to = email,
            subject = "Reset your Chirp password",
            html = htmlContent
        )
    }

    private fun sendHtmlEmail(
        to: String,
        subject: String,
        html: String
    ) {
        val message = javaMailSender.createMimeMessage()
        MimeMessageHelper(message, true, "UTF-8").apply {
            setFrom(emailFrom)
            setTo(to)
            setSubject(subject)
            setText(html, true)
        }

        try {
            javaMailSender.send(message)
        } catch(e: MailException) {
            logger.error("Could not send email", e)
        }
    }
}

### FILE: notification/src/main/kotlin/com/plcoding/chirp/service/EmailTemplateService.kt
package com.plcoding.chirp.service

import org.springframework.stereotype.Service
import org.thymeleaf.TemplateEngine
import org.thymeleaf.context.Context

@Service
class EmailTemplateService(
    private val templateEngine: TemplateEngine
) {

    fun processTemplate(
        templateName: String,
        variables: Map<String, Any> = emptyMap()
    ): String {
        val context = Context().apply {
            variables.forEach { (key, value) ->
                setVariable(key, value)
            }
        }

        return templateEngine.process(templateName, context)
    }
}

### FILE: notification/src/main/kotlin/com/plcoding/chirp/service/PushNotificationService.kt
package com.plcoding.chirp.service

import com.plcoding.chirp.domain.exception.InvalidDeviceTokenException
import com.plcoding.chirp.domain.model.DeviceToken
import com.plcoding.chirp.domain.model.PushNotification
import com.plcoding.chirp.domain.type.ChatId
import com.plcoding.chirp.domain.type.UserId
import com.plcoding.chirp.infra.database.DeviceTokenEntity
import com.plcoding.chirp.infra.database.DeviceTokenRepository
import com.plcoding.chirp.infra.mappers.toDeviceToken
import com.plcoding.chirp.infra.mappers.toPlatformEntity
import com.plcoding.chirp.infra.push_notification.FirebasePushNotificationService
import org.slf4j.LoggerFactory
import org.springframework.scheduling.annotation.Scheduled
import org.springframework.stereotype.Service
import org.springframework.transaction.annotation.Transactional
import java.time.Duration
import java.time.Instant
import java.util.concurrent.ConcurrentSkipListMap

@Service
class PushNotificationService(
    private val deviceTokenRepository: DeviceTokenRepository,
    private val firebasePushNotificationService: FirebasePushNotificationService
) {
    companion object {
        private val RETRY_DELAYS_SECONDS = listOf(
            30L,
            60L,
            120L,
            300L,
            600L
        )
        const val MAX_RETRY_AGE_MINUTES = 30L
    }

    private val retryQueue = ConcurrentSkipListMap<Long, MutableList<RetryData>>()

    private val logger = LoggerFactory.getLogger(javaClass)

    @Transactional
    fun registerDevice(
        userId: UserId,
        token: String,
        platform: DeviceToken.Platform
    ): DeviceToken {
        val existing = deviceTokenRepository.findByToken(token)

        val trimmedToken = token.trim()
        if(existing == null && !firebasePushNotificationService.isValidToken(trimmedToken)) {
            throw InvalidDeviceTokenException()
        }

        val entity = if(existing != null) {
            deviceTokenRepository.save(
                existing.apply {
                    this.userId = userId
                }
            )
        } else {
            deviceTokenRepository.save(
                DeviceTokenEntity(
                    userId = userId,
                    token = trimmedToken,
                    platform = platform.toPlatformEntity()
                )
            )
        }

        return entity.toDeviceToken()
    }

    @Transactional
    fun unregisterDevice(token: String) {
        deviceTokenRepository.deleteByToken(token.trim())
    }

    fun sendNewMessageNotifications(
        recipientUserIds: List<UserId>,
        senderUserId: UserId,
        senderUsername: String,
        message: String,
        chatId: ChatId
    ) {
        val deviceTokens = deviceTokenRepository.findByUserIdIn(recipientUserIds)
        if(deviceTokens.isEmpty()) {
            logger.info("No device tokens found for $recipientUserIds")
            return
        }

        val recipients = deviceTokens
            .filter { it.userId != senderUserId }
            .map { it.toDeviceToken() }

        val notification = PushNotification(
            title = "New message from $senderUsername",
            recipients = recipients,
            message = message,
            chatId = chatId,
            data = mapOf(
                "chatId" to chatId.toString(),
                "type" to "new_message"
            )
        )

        sendWithRetry(notification = notification)
    }

    fun sendWithRetry(
        notification: PushNotification,
        attempt: Int = 0
    ) {
        val result = firebasePushNotificationService.sendNotification(notification)

        result.permanentFailures.forEach {
            deviceTokenRepository.deleteByToken(it.token)
        }

        if(result.temporaryFailures.isNotEmpty() && attempt < RETRY_DELAYS_SECONDS.size) {
            val retryNotification = notification.copy(
                recipients = result.temporaryFailures
            )
            scheduleRetry(retryNotification, attempt + 1)
        }

        if(result.succeeded.isNotEmpty()) {
            logger.info("Successfully sent notification to ${result.succeeded.size} devices")
        }
    }

    private fun scheduleRetry(
        notification: PushNotification,
        attempt: Int
    ) {
        val delay = RETRY_DELAYS_SECONDS.getOrElse(attempt - 1) {
            RETRY_DELAYS_SECONDS.last()
        }
        val executeAt = Instant.now().plusSeconds(delay)
        val executeAtMillis = executeAt.toEpochMilli()

        val retryData = RetryData(
            notification = notification,
            attempt = attempt,
            createdAt = Instant.now()
        )

        retryQueue.compute(executeAtMillis) { _, retries ->
            (retries ?: mutableListOf()).apply { add(retryData) }
        }

        logger.info("Scheduled retry $attempt for ${notification.id} in $delay seconds")
    }

    @Scheduled(fixedDelay = 15_000L)
    fun processRetries() {
        val now = Instant.now()
        val nowMillis = now.toEpochMilli()

        val toProcess = retryQueue.headMap(nowMillis, true)

        if(toProcess.isEmpty()) {
            return
        }

        val entries = toProcess.entries.toList()
        entries.forEach { (timeMillis, retries) ->
            retryQueue.remove(timeMillis)

            retries.forEach { retry ->
                try {
                    val age = Duration.between(retry.createdAt, now)
                    if(age.toMinutes() > MAX_RETRY_AGE_MINUTES) {
                        logger.warn("Dropping old retry (${age.toMinutes()} old)")
                        return@forEach
                    }

                    sendWithRetry(
                        notification = retry.notification,
                        attempt = retry.attempt
                    )
                } catch(e: Exception) {
                    logger.warn("Error processing retry ${retry.notification.id}", e)
                }
            }
        }
    }

    private data class RetryData(
        val notification: PushNotification,
        val attempt: Int,
        val createdAt: Instant
    )
}

### FILE: README.md
# Chirp API

A real-time messaging API backend built with Kotlin and Spring Boot, part of the [**Building Industry-Level Kotlin Backends With Spring Boot**](https://pl-coding.com/kotlin-spring-boot?utm_source=github&utm_medium=readme&utm_campaign=default&cmc_strip=utm) course.

### System Architecture
<div align="center">
  <img width="800" alt="chirp-system-design" src="https://github.com/user-attachments/assets/27fec017-281c-424c-a2a5-f3060f783c8d" />
  <img width="2368" height="1776" alt="chirp-architecture" src="https://github.com/user-attachments/assets/112bdc07-12c8-4dba-a602-09f2c76d7b42" />
</div>

## KMP/CMP App
Check out the mobile implementation: [Chirp - Kotlin Multiplatform Project](https://github.com/philipplackner/Chirp/)

### Mobile View
<div align="center">
  <img width="800" alt="mobile-screens" src="https://github.com/user-attachments/assets/55ec2600-9ecb-4d5d-a8ad-1650dfe2dc17" />
</div>

### Desktop, Foldable & Tablet View
<div align="center">
  <img width="900" alt="tablet-chat" src="https://github.com/user-attachments/assets/df54ebfd-e7d7-4f14-9841-2abe75c630b3" />
</div>


## What's covered?

- Multi-module Spring Boot architecture
- JWT & API Key authentication
- Real-time messaging with WebSocket
- Push notifications with Firebase
- Email service integration
- Rate limiting & IP tracking
- RabbitMQ message queuing
- Redis caching
- Supabase storage integration
- Password reset & email verification flows

## Technology Stack

<table>
  <tr>
    <td align="center" width="120" height="120">
      <img src="https://github.com/user-attachments/assets/59036eab-e126-41f7-bf3d-29185d67f3b1" width="60" height="60" alt="Kotlin" />
      <br><strong>Kotlin</strong>
    </td>
    <td align="center" width="120" height="120">
      <img src="https://github.com/user-attachments/assets/51cc367e-bdbd-4018-ba9f-ed71841b8cf0" width="60" height="60" alt="Spring Boot" />
      <br><strong>Spring Boot</strong><br>3.x
    </td>
    <td align="center" width="120" height="120">
      <img src="https://github.com/user-attachments/assets/62c533f1-2bad-4e13-b781-b5439011e6c0" width="60" height="60" alt="PostgreSQL" />
      <br><strong>PostgreSQL</strong><br>Spring Data JPA
    </td>
    <td align="center" width="120" height="120">
      <img src="https://github.com/user-attachments/assets/e063b515-f336-487f-9626-7e4e5241653f" width="60" height="60" alt="Redis" />
      <br><strong>Redis</strong><br>Caching
    </td>
  </tr>
  <tr>
    <td align="center" width="120" height="120">
      <img src="https://github.com/user-attachments/assets/37087bf2-713b-44f7-bf24-08b417837340" width="60" height="60" alt="RabbitMQ" />
      <br><strong>RabbitMQ</strong><br>Message Queue
    </td>
    <td align="center" width="120" height="120">
      <img src="https://github.com/user-attachments/assets/d5889ebc-6de5-46ca-af06-9ec813af594f" width="60" height="60" alt="Firebase" />
      <br><strong>Firebase</strong><br>Cloud Messaging
    </td>
    <td align="center" width="120" height="120">
      <img src="https://github.com/user-attachments/assets/bbcc753d-b6b5-4462-98ec-3726ba12abce" width="60" height="60" alt="Supabase" />
      <br><strong>Supabase</strong><br>Backend Services
    </td>
    <td align="center" width="120" height="120">
      <!-- Empty cell for alignment -->
    </td>
  </tr>
</table>



---
## Learn to Build This App
<div align="center">
  <img width="2742" height="1508" alt="spring-boot-transparent-padded" src="https://github.com/user-attachments/assets/d62b23db-a51a-42f1-aa67-dd125e99ec0e" />
</div>

Learn more at [pl-coding.com/kotlin-spring-boot](https://pl-coding.com/kotlin-spring-boot?utm_source=github&utm_medium=readme&utm_campaign=default&cmc_strip=utm)


### FILE: settings.gradle.kts
pluginManagement {
    includeBuild("build-logic")
    repositories {
        maven { url = uri("https://repo.spring.io/milestone") }
        maven { url = uri("https://repo.spring.io/snapshot") }
        gradlePluginPortal()
    }
}

enableFeaturePreview("TYPESAFE_PROJECT_ACCESSORS")

rootProject.name = "chirp"

include("app")
include("user")
include("chat")
include("notification")
include("common")

### FILE: user/build.gradle.kts
plugins {
    id("java-library")
    id("chirp.spring-boot-service")
    kotlin("plugin.jpa")
}

group = "com.plcoding"
version = "unspecified"

repositories {
    mavenCentral()
    maven { url = uri("https://repo.spring.io/milestone") }
    maven { url = uri("https://repo.spring.io/snapshot") }
}

dependencies {
    implementation(projects.common)

    implementation(libs.spring.boot.starter.security)
    implementation(libs.spring.boot.starter.validation)

    implementation(libs.spring.boot.starter.data.redis)
    implementation(libs.spring.boot.starter.data.jpa)
    runtimeOnly(libs.postgresql)

    implementation(libs.jwt.api)
    runtimeOnly(libs.jwt.impl)
    runtimeOnly(libs.jwt.jackson)

    testImplementation(kotlin("test"))
}

tasks.test {
    useJUnitPlatform()
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/api/config/ApiKeyAuthFilter.kt
package com.plcoding.chirp.api.config

import com.plcoding.chirp.service.ApiKeyService
import jakarta.servlet.FilterChain
import jakarta.servlet.http.HttpServletRequest
import jakarta.servlet.http.HttpServletResponse
import org.springframework.http.HttpMethod
import org.springframework.http.HttpStatus
import org.springframework.stereotype.Component
import org.springframework.web.filter.OncePerRequestFilter

@Component
class ApiKeyAuthFilter(
    private val apiKeyService: ApiKeyService
) : OncePerRequestFilter() {

    companion object Companion {
        private const val API_KEY_HEADER = "X-API-Key"
        private const val AUTH_API_KEY_PATH = "/api/auth/apiKey"
    }

    override fun doFilterInternal(
        request: HttpServletRequest,
        response: HttpServletResponse,
        filterChain: FilterChain
    ) {
        if (shouldSkipAuthentication(request)) {
            filterChain.doFilter(request, response)
            return
        }

        val apiKey = request.getHeader(API_KEY_HEADER)

        if (apiKey.isNullOrBlank()) {
            sendUnauthorizedResponse(response, "Missing API key. Make sure to attach it as an X-API-Key header.")
            return
        }

        if (!apiKeyService.isValidKey(apiKey)) {
            sendUnauthorizedResponse(response, "Invalid API key")
            return
        }

        filterChain.doFilter(request, response)
    }

    private fun shouldSkipAuthentication(request: HttpServletRequest): Boolean {
        return request.method == HttpMethod.POST.name() &&
                request.servletPath == AUTH_API_KEY_PATH
    }

    private fun sendUnauthorizedResponse(response: HttpServletResponse, message: String) {
        response.status = HttpStatus.UNAUTHORIZED.value()
        response.contentType = "application/json"
        response.writer.write("""{"error": "$message"}""")
    }
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/api/config/IpRateLimit.kt
package com.plcoding.chirp.api.config

import java.util.concurrent.TimeUnit

annotation class IpRateLimit(
    val requests: Int = 60,
    val duration: Long = 1L,
    val unit: TimeUnit = TimeUnit.MINUTES
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/api/config/IpRateLimitInterceptor.kt
package com.plcoding.chirp.api.config

import com.plcoding.chirp.domain.exception.RateLimitException
import com.plcoding.chirp.infra.rate_limiting.IpRateLimiter
import com.plcoding.chirp.infra.rate_limiting.IpResolver
import jakarta.servlet.http.HttpServletRequest
import jakarta.servlet.http.HttpServletResponse
import org.springframework.beans.factory.annotation.Value
import org.springframework.stereotype.Component
import org.springframework.web.method.HandlerMethod
import org.springframework.web.servlet.HandlerInterceptor
import java.time.Duration

@Component
class IpRateLimitInterceptor(
    private val ipRateLimiter: IpRateLimiter,
    private val ipResolver: IpResolver,
    @param:Value("\${chirp.rate-limit.ip.apply-limit}")
    private val applyLimit: Boolean
): HandlerInterceptor {

    override fun preHandle(request: HttpServletRequest, response: HttpServletResponse, handler: Any): Boolean {
        if(handler is HandlerMethod && applyLimit) {
            val annotation = handler.getMethodAnnotation(IpRateLimit::class.java)
            if(annotation != null) {
                val clientIp = ipResolver.getClientIp(request)

                return try {
                    ipRateLimiter.withIpRateLimit(
                        ipAddress = clientIp,
                        resetsIn = Duration.of(
                            annotation.duration,
                            annotation.unit.toChronoUnit()
                        ),
                        maxRequestsPerIp = annotation.requests,
                        action = { true }
                    )
                } catch(e: RateLimitException) {
                    response.sendError(429)
                    false
                }
            }
        }

        return true
    }
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/api/config/JwtAuthFilter.kt
package com.plcoding.chirp.api.config

import com.plcoding.chirp.service.JwtService
import jakarta.servlet.FilterChain
import jakarta.servlet.http.HttpServletRequest
import jakarta.servlet.http.HttpServletResponse
import org.springframework.http.HttpHeaders
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken
import org.springframework.security.core.context.SecurityContextHolder
import org.springframework.stereotype.Component
import org.springframework.web.filter.OncePerRequestFilter

@Component
class JwtAuthFilter(
    private val jwtService: JwtService
): OncePerRequestFilter() {

    override fun doFilterInternal(
        request: HttpServletRequest,
        response: HttpServletResponse,
        filterChain: FilterChain
    ) {
        val authHeader = request.getHeader(HttpHeaders.AUTHORIZATION)
        if(authHeader != null && authHeader.startsWith("Bearer ")) {
            if(jwtService.validateAccessToken(authHeader)) {
                val userId = jwtService.getUserIdFromToken(authHeader)
                val auth = UsernamePasswordAuthenticationToken(
                    userId,
                    null,
                    emptyList()
                )
                SecurityContextHolder.getContext().authentication = auth
            }
        }
        filterChain.doFilter(request, response)
    }
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/api/config/WebMvcConfig.kt
package com.plcoding.chirp.api.config

import org.springframework.stereotype.Component
import org.springframework.web.servlet.config.annotation.InterceptorRegistry
import org.springframework.web.servlet.config.annotation.WebMvcConfigurer

@Component
class WebMvcConfig(
    private val ipRateLimitInterceptor: IpRateLimitInterceptor
): WebMvcConfigurer {

    override fun addInterceptors(registry: InterceptorRegistry) {
        registry
            .addInterceptor(ipRateLimitInterceptor)
            .addPathPatterns("/api/**")
    }
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/api/controllers/ApiKeyController.kt
package com.plcoding.chirp.api.controllers

import com.plcoding.chirp.api.dto.ApiKeyDto
import com.plcoding.chirp.api.dto.CreateApiKeyRequest
import com.plcoding.chirp.api.mappers.toApiKeyDto
import com.plcoding.chirp.service.ApiKeyService
import org.springframework.beans.factory.annotation.Value
import org.springframework.http.HttpStatus
import org.springframework.web.bind.annotation.PostMapping
import org.springframework.web.bind.annotation.RequestBody
import org.springframework.web.bind.annotation.RequestHeader
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RestController
import org.springframework.web.server.ResponseStatusException
import java.util.Base64

@RestController
@RequestMapping("/api/auth/apiKey")
class ApiKeyController(
    private val apiKeyService: ApiKeyService,
    @param:Value("\${chirp.api-key.admin.username}")
    private val adminUsername: String,
    @param:Value("\${chirp.api-key.admin.password}")
    private val adminPassword: String,
) {

    @PostMapping
    fun createApiKey(
        @RequestHeader("Authorization") authHeader: String,
        @RequestBody body: CreateApiKeyRequest
    ): ApiKeyDto {
        if(!isAuthorized(authHeader)) {
            throw ResponseStatusException(HttpStatus.UNAUTHORIZED)
        }

        return apiKeyService.createKey(body.email).toApiKeyDto()
    }

    private fun isAuthorized(authHeader: String?): Boolean {
        if (authHeader == null || !authHeader.startsWith("Basic ")) {
            return false
        }

        return try {
            val base64Credentials = authHeader.substringAfter("Basic ")
            val credentials = String(Base64.getDecoder().decode(base64Credentials))
            val parts = credentials.split(":", limit = 2)

            if (parts.size != 2) {
                return false
            }

            val username = parts[0]
            val password = parts[1]

            username == adminUsername && password == adminPassword
        } catch (e: Exception) {
            false
        }
    }
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/api/controllers/AuthController.kt
package com.plcoding.chirp.api.controllers

import com.plcoding.chirp.api.config.IpRateLimit
import com.plcoding.chirp.api.dto.AuthenticatedUserDto
import com.plcoding.chirp.api.dto.ChangePasswordRequest
import com.plcoding.chirp.api.dto.EmailRequest
import com.plcoding.chirp.api.dto.LoginRequest
import com.plcoding.chirp.api.dto.RefreshRequest
import com.plcoding.chirp.api.dto.RegisterRequest
import com.plcoding.chirp.api.dto.ResetPasswordRequest
import com.plcoding.chirp.api.dto.UserDto
import com.plcoding.chirp.api.mappers.toAuthenticatedUserDto
import com.plcoding.chirp.api.mappers.toUserDto
import com.plcoding.chirp.api.util.requestUserId
import com.plcoding.chirp.infra.rate_limiting.EmailRateLimiter
import com.plcoding.chirp.service.AuthService
import com.plcoding.chirp.service.EmailVerificationService
import com.plcoding.chirp.service.PasswordResetService
import jakarta.validation.Valid
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.PostMapping
import org.springframework.web.bind.annotation.RequestBody
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RequestParam
import org.springframework.web.bind.annotation.RestController
import java.util.concurrent.TimeUnit

@RestController
@RequestMapping("/api/auth")
class AuthController(
    private val authService: AuthService,
    private val emailVerificationService: EmailVerificationService,
    private val passwordResetService: PasswordResetService,
    private val emailRateLimiter: EmailRateLimiter
) {

    @PostMapping("/register")
    @IpRateLimit(
        requests = 50,
        duration = 1L,
        unit = TimeUnit.HOURS
    )
    fun register(
        @Valid @RequestBody body: RegisterRequest
    ): UserDto {
        return authService.register(
            email = body.email,
            username = body.username,
            password = body.password
        ).toUserDto()
    }

    @PostMapping("/login")
    @IpRateLimit(
        requests = 50,
        duration = 1L,
        unit = TimeUnit.HOURS
    )
    fun login(
        @RequestBody body: LoginRequest
    ): AuthenticatedUserDto {
        return authService.login(
            email = body.email,
            password = body.password
        ).toAuthenticatedUserDto()
    }

    @PostMapping("/refresh")
    @IpRateLimit(
        requests = 50,
        duration = 1L,
        unit = TimeUnit.HOURS
    )
    fun refresh(
        @RequestBody body: RefreshRequest
    ): AuthenticatedUserDto {
        return authService
            .refresh(body.refreshToken)
            .toAuthenticatedUserDto()
    }

    @PostMapping("/logout")
    fun logout(
        @RequestBody body: RefreshRequest
    ) {
        authService.logout(body.refreshToken)
    }

    @PostMapping("/resend-verification")
    @IpRateLimit(
        requests = 50,
        duration = 1L,
        unit = TimeUnit.HOURS
    )
    fun resendVerification(
        @Valid @RequestBody body: EmailRequest
    ) {
        emailRateLimiter.withRateLimit(
            email = body.email
        ) {
            emailVerificationService.resendVerificationEmail(body.email)
        }
    }

    @GetMapping("/verify")
    fun verifyEmail(
        @RequestParam token: String
    ) {
        emailVerificationService.verifyEmail(token)
    }

    @PostMapping("/forgot-password")
    @IpRateLimit(
        requests = 50,
        duration = 1L,
        unit = TimeUnit.HOURS
    )
    fun forgotPassword(
        @Valid @RequestBody body: EmailRequest
    ) {
        passwordResetService.requestPasswordReset(body.email)
    }

    @PostMapping("/reset-password")
    fun resetPassword(
        @Valid @RequestBody body: ResetPasswordRequest
    ) {
        passwordResetService.resetPassword(
            token = body.token,
            newPassword = body.newPassword
        )
    }

    @PostMapping("/change-password")
    fun changePassword(
        @Valid @RequestBody body: ChangePasswordRequest
    ) {
        passwordResetService.changePassword(
            userId = requestUserId,
            oldPassword = body.oldPassword,
            newPassword = body.newPassword
        )
    }
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/api/dto/ApiKeyDto.kt
package com.plcoding.chirp.api.dto

import java.time.Instant

data class ApiKeyDto(
    val key: String,
    val validFrom: Instant,
    val expiresAt: Instant,
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/api/dto/AuthenticatedUserDto.kt
package com.plcoding.chirp.api.dto

data class AuthenticatedUserDto(
    val user: UserDto,
    val accessToken: String,
    val refreshToken: String
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/api/dto/ChangePasswordRequest.kt
package com.plcoding.chirp.api.dto

import com.plcoding.chirp.api.util.Password
import jakarta.validation.constraints.NotBlank

data class ChangePasswordRequest(
    @field:NotBlank
    val oldPassword: String,
    @field:Password
    val newPassword: String
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/api/dto/CreateApiKeyRequest.kt
package com.plcoding.chirp.api.dto

data class CreateApiKeyRequest(
    val email: String
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/api/dto/EmailRequest.kt
package com.plcoding.chirp.api.dto

import jakarta.validation.constraints.Email

data class EmailRequest(
    @field:Email
    val email: String
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/api/dto/LoginRequest.kt
package com.plcoding.chirp.api.dto

data class LoginRequest(
    val email: String,
    val password: String
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/api/dto/RefreshRequest.kt
package com.plcoding.chirp.api.dto

data class RefreshRequest(
    val refreshToken: String
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/api/dto/RegisterRequest.kt
package com.plcoding.chirp.api.dto

import com.plcoding.chirp.api.util.Password
import jakarta.validation.constraints.Email
import jakarta.validation.constraints.Pattern
import org.hibernate.validator.constraints.Length

data class RegisterRequest(
    @field:Email(message = "Must be a valid email address")
    val email: String,
    @field:Length(min = 3, max = 20, message = "Username length must be between 3 and 20 characters")
    val username: String,
    @field:Password
    val password: String
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/api/dto/ResetPasswordRequest.kt
package com.plcoding.chirp.api.dto

import com.plcoding.chirp.api.util.Password
import jakarta.validation.constraints.NotBlank

data class ResetPasswordRequest(
    @field:NotBlank
    val token: String,
    @field:Password
    val newPassword: String
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/api/dto/UserDto.kt
package com.plcoding.chirp.api.dto

import com.plcoding.chirp.domain.type.UserId

data class UserDto(
    val id: UserId,
    val email: String,
    val username: String,
    val hasVerifiedEmail: Boolean,
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/api/exception_handling/AuthExceptionHandler.kt
package com.plcoding.chirp.api.exception_handling

import com.plcoding.chirp.domain.exception.EmailNotVerifiedException
import com.plcoding.chirp.domain.exception.InvalidCredentialsException
import com.plcoding.chirp.domain.exception.InvalidTokenException
import com.plcoding.chirp.domain.exception.RateLimitException
import com.plcoding.chirp.domain.exception.SamePasswordException
import com.plcoding.chirp.domain.exception.UnauthorizedException
import com.plcoding.chirp.domain.exception.UserAlreadyExistsException
import com.plcoding.chirp.domain.exception.UserNotFoundException
import org.springframework.http.HttpStatus
import org.springframework.http.ResponseEntity
import org.springframework.web.bind.MethodArgumentNotValidException
import org.springframework.web.bind.annotation.ExceptionHandler
import org.springframework.web.bind.annotation.ResponseStatus
import org.springframework.web.bind.annotation.RestControllerAdvice

@RestControllerAdvice
class AuthExceptionHandler {

    @ExceptionHandler(UserAlreadyExistsException::class)
    @ResponseStatus(HttpStatus.CONFLICT)
    fun onUserAlreadyExists(
        e: UserAlreadyExistsException
    ) = mapOf(
        "code" to "USER_EXISTS",
        "message" to e.message
    )

    @ExceptionHandler(UserNotFoundException::class)
    @ResponseStatus(HttpStatus.NOT_FOUND)
    fun onUserNotFound(
        e: UserNotFoundException
    ) = mapOf(
        "code" to "USER_NOT_FOUND",
        "message" to e.message
    )

    @ExceptionHandler(InvalidCredentialsException::class)
    @ResponseStatus(HttpStatus.UNAUTHORIZED)
    fun onInvalidCredentials(
        e: InvalidCredentialsException
    ) = mapOf(
        "code" to "INVALID_CREDENTIALS",
        "message" to e.message
    )

    @ExceptionHandler(InvalidTokenException::class)
    @ResponseStatus(HttpStatus.UNAUTHORIZED)
    fun onInvalidToken(
        e: InvalidTokenException
    ) = mapOf(
        "code" to "INVALID_TOKEN",
        "message" to e.message
    )

    @ExceptionHandler(EmailNotVerifiedException::class)
    @ResponseStatus(HttpStatus.FORBIDDEN)
    fun onEmailNotVerified(
        e: EmailNotVerifiedException
    ) = mapOf(
        "code" to "EMAIL_NOT_VERIFIED",
        "message" to e.message
    )

    @ExceptionHandler(UnauthorizedException::class)
    @ResponseStatus(HttpStatus.UNAUTHORIZED)
    fun onUnauthorized(
        e: UnauthorizedException
    ) = mapOf(
        "code" to "UNAUTHORIZED",
        "message" to e.message
    )

    @ExceptionHandler(SamePasswordException::class)
    @ResponseStatus(HttpStatus.CONFLICT)
    fun onSamePassword(
        e: SamePasswordException
    ) = mapOf(
        "code" to "SAME_PASSWORD",
        "message" to e.message
    )

    @ExceptionHandler(RateLimitException::class)
    @ResponseStatus(HttpStatus.TOO_MANY_REQUESTS)
    fun onRateLimitExceeded(
        e: RateLimitException
    ) = mapOf(
        "code" to "RATE_LIMIT_EXCEEDED",
        "message" to e.message
    )

    @ExceptionHandler(MethodArgumentNotValidException::class)
    fun onValidationException(
        e: MethodArgumentNotValidException
    ): ResponseEntity<Map<String, Any>> {
        val errors = e.bindingResult.allErrors.map {
            it.defaultMessage ?: "Invalid value"
        }
        return ResponseEntity
            .status(HttpStatus.BAD_REQUEST)
            .body(
                mapOf(
                    "code" to "VALIDATION_ERROR",
                    "errors" to errors
                )
            )
    }
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/api/mappers/ApiKeyMappers.kt
package com.plcoding.chirp.api.mappers

import com.plcoding.chirp.api.dto.ApiKeyDto
import com.plcoding.chirp.domain.model.ApiKey

fun ApiKey.toApiKeyDto(): ApiKeyDto {
    return ApiKeyDto(
        key = key,
        validFrom = validFrom,
        expiresAt = expiresAt,
    )
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/api/mappers/UserMappers.kt
package com.plcoding.chirp.api.mappers

import com.plcoding.chirp.api.dto.AuthenticatedUserDto
import com.plcoding.chirp.api.dto.UserDto
import com.plcoding.chirp.domain.model.AuthenticatedUser
import com.plcoding.chirp.domain.model.User

fun AuthenticatedUser.toAuthenticatedUserDto(): AuthenticatedUserDto {
    return AuthenticatedUserDto(
        user = user.toUserDto(),
        accessToken = accessToken,
        refreshToken = refreshToken
    )
}

fun User.toUserDto(): UserDto {
    return UserDto(
        id = id,
        email = email,
        username = username,
        hasVerifiedEmail = hasEmailVerified
    )
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/api/util/Password.kt
package com.plcoding.chirp.api.util

import jakarta.validation.Constraint
import jakarta.validation.Payload
import jakarta.validation.constraints.Pattern
import kotlin.reflect.KClass

@Target(AnnotationTarget.FIELD, AnnotationTarget.PROPERTY_GETTER)
@Retention(AnnotationRetention.RUNTIME)
@Constraint(validatedBy = [])
@Pattern(
    regexp = "^(?=.*[\\d!@#$%^&*()_+\\-=\\[\\]{};':\"\\\\|,.<>/?])(.{8,})$",
    message = "Password must be at least 8 characters and contain at least one digit or special character"
)
annotation class Password(
    val message: String = "Password must be at least 8 characters and contain at least one digit or special character",
    val groups: Array<KClass<out Any>> = [],
    val payload: Array<KClass<out Payload>> = []
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/domain/exception/EmailNotVerifiedException.kt
package com.plcoding.chirp.domain.exception

import java.lang.RuntimeException

class EmailNotVerifiedException: RuntimeException(
    "Email is not verified"
)

### FILE: user/src/main/kotlin/com/plcoding/chirp/domain/exception/InvalidCredentialsException.kt
package com.plcoding.chirp.domain.exception

class InvalidCredentialsException: RuntimeException(
    "The entered credentials aren't valid"
)

### FILE: user/src/main/kotlin/com/plcoding/chirp/domain/exception/RateLimitException.kt
package com.plcoding.chirp.domain.exception

class RateLimitException(
    val resetsInSeconds: Long
): RuntimeException(
    "Rate limit exceeded. Please try again in $resetsInSeconds seconds."
)

### FILE: user/src/main/kotlin/com/plcoding/chirp/domain/exception/SamePasswordException.kt
package com.plcoding.chirp.domain.exception

class SamePasswordException: RuntimeException(
    "The new password can't be equal to the old one."
)

### FILE: user/src/main/kotlin/com/plcoding/chirp/domain/exception/UserAlreadyExistsException.kt
package com.plcoding.chirp.domain.exception

import java.lang.RuntimeException

class UserAlreadyExistsException: RuntimeException(
    "A user with this username or email already exists."
)

### FILE: user/src/main/kotlin/com/plcoding/chirp/domain/exception/UserNotFoundException.kt
package com.plcoding.chirp.domain.exception

class UserNotFoundException: RuntimeException("User not found")

### FILE: user/src/main/kotlin/com/plcoding/chirp/domain/model/ApiKey.kt
package com.plcoding.chirp.domain.model

import java.security.SecureRandom
import java.time.Instant
import java.util.Base64

data class ApiKey(
    val key: String,
    val validFrom: Instant,
    val expiresAt: Instant,
) {
    companion object {
        private const val KEY_LENGTH = 20

        fun generateKey(): String {
            val bytes = ByteArray(KEY_LENGTH) { 0 }

            val secureRandom = SecureRandom()
            secureRandom.nextBytes(bytes)

            return Base64.getUrlEncoder()
                .withoutPadding()
                .encodeToString(bytes)
        }
    }
}


### FILE: user/src/main/kotlin/com/plcoding/chirp/domain/model/AuthenticatedUser.kt
package com.plcoding.chirp.domain.model

data class AuthenticatedUser(
    val user: User,
    val accessToken: String,
    val refreshToken: String
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/domain/model/EmailVerificationToken.kt
package com.plcoding.chirp.domain.model

data class EmailVerificationToken(
    val id: Long,
    val token: String,
    val user: User
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/domain/model/User.kt
package com.plcoding.chirp.domain.model

import com.plcoding.chirp.domain.type.UserId

data class User(
    val id: UserId,
    val username: String,
    val email: String,
    val hasEmailVerified: Boolean
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/config/NginxConfig.kt
package com.plcoding.chirp.infra.config

import org.springframework.boot.context.properties.ConfigurationProperties
import org.springframework.context.annotation.Configuration

@Configuration
@ConfigurationProperties(prefix = "nginx")
data class NginxConfig(
    var trustedIps: List<String> = emptyList(),
    var requireProxy: Boolean = true
)


### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/database/entities/ApiKeyEntity.kt
package com.plcoding.chirp.infra.database.entities

import jakarta.persistence.Column
import jakarta.persistence.Entity
import jakarta.persistence.Id
import jakarta.persistence.Table
import java.time.Instant

@Entity
@Table(
    name = "api_keys",
    schema = "user_service"
)
class ApiKeyEntity(
    @Id
    var key: String,
    @Column(nullable = false)
    var email: String,
    @Column(nullable = false)
    var validFrom: Instant,
    @Column(nullable = false)
    var expiresAt: Instant,
)

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/database/entities/EmailVerificationTokenEntity.kt
package com.plcoding.chirp.infra.database.entities

import com.plcoding.chirp.infra.security.TokenGenerator
import jakarta.persistence.Column
import jakarta.persistence.Entity
import jakarta.persistence.FetchType
import jakarta.persistence.GeneratedValue
import jakarta.persistence.GenerationType
import jakarta.persistence.Id
import jakarta.persistence.Index
import jakarta.persistence.JoinColumn
import jakarta.persistence.ManyToOne
import jakarta.persistence.OneToMany
import jakarta.persistence.Table
import org.hibernate.annotations.CreationTimestamp
import java.time.Instant

@Entity
@Table(
    name = "email_verification_tokens",
    schema = "user_service",
    indexes = [
        Index(name = "idx_email_verification_token_token", columnList = "token")
    ]
)
class EmailVerificationTokenEntity(
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    var id: Long = 0,
    @Column(nullable = false, unique = true)
    var token: String = TokenGenerator.generateSecureToken(),
    @Column(nullable = false)
    var expiresAt: Instant,
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "user_id", nullable = false)
    var user: UserEntity,
    @Column
    var usedAt: Instant? = null,
    @CreationTimestamp
    var createdAt: Instant = Instant.now(),
) {
    val isUsed: Boolean
        get() = usedAt != null

    val isExpired: Boolean
        get() = Instant.now() > expiresAt
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/database/entities/PasswordResetTokenEntity.kt
package com.plcoding.chirp.infra.database.entities

import com.plcoding.chirp.infra.security.TokenGenerator
import jakarta.persistence.Column
import jakarta.persistence.Entity
import jakarta.persistence.FetchType
import jakarta.persistence.GeneratedValue
import jakarta.persistence.GenerationType
import jakarta.persistence.Id
import jakarta.persistence.Index
import jakarta.persistence.JoinColumn
import jakarta.persistence.ManyToOne
import jakarta.persistence.Table
import org.hibernate.annotations.CreationTimestamp
import java.time.Instant

@Entity
@Table(
    name = "password_reset_tokens",
    schema = "user_service",
    indexes = [
        Index(name = "idx_password_reset_token_token", columnList = "token")
    ]
)
class PasswordResetTokenEntity(
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    var id: Long = 0,
    @Column(nullable = false, unique = true)
    var token: String = TokenGenerator.generateSecureToken(),
    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "user_id", nullable = false)
    var user: UserEntity,
    @Column(nullable = false)
    var expiresAt: Instant,
    @Column(nullable = true)
    var usedAt: Instant? = null,
    @CreationTimestamp
    var createdAt: Instant = Instant.now(),
) {
    val isUsed: Boolean
        get() = usedAt != null

    val isExpired: Boolean
        get() = Instant.now() > expiresAt
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/database/entities/RefreshTokenEntity.kt
package com.plcoding.chirp.infra.database.entities

import com.plcoding.chirp.domain.type.UserId
import jakarta.persistence.Column
import jakarta.persistence.Entity
import jakarta.persistence.GeneratedValue
import jakarta.persistence.GenerationType
import jakarta.persistence.Id
import jakarta.persistence.Index
import jakarta.persistence.Table
import org.hibernate.annotations.CreationTimestamp
import java.time.Instant

@Entity
@Table(
    name = "refresh_tokens",
    schema = "user_service",
    indexes = [
        Index(name = "idx_refresh_tokens_user_id", columnList = "user_id"),
        Index(name = "idx_refresh_tokens_user_token", columnList = "user_id,hashed_token"),
    ]
)
class RefreshTokenEntity(
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    var id: Long = 0,
    @Column(nullable = false)
    var userId: UserId,
    @Column(nullable = false)
    var expiresAt: Instant,
    @Column(nullable = false)
    var hashedToken: String,
    @CreationTimestamp
    var createdAt: Instant = Instant.now()
)

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/database/entities/UserEntity.kt
package com.plcoding.chirp.infra.database.entities

import com.plcoding.chirp.domain.type.UserId
import jakarta.persistence.Column
import jakarta.persistence.Entity
import jakarta.persistence.GeneratedValue
import jakarta.persistence.GenerationType
import jakarta.persistence.Id
import jakarta.persistence.Index
import jakarta.persistence.Table
import org.hibernate.annotations.CreationTimestamp
import org.hibernate.annotations.UpdateTimestamp
import java.time.Instant

@Entity
@Table(
    name = "users",
    schema = "user_service",
    indexes = [
        Index(name = "idx_users_email", columnList = "email"),
        Index(name = "idx_users_username", columnList = "username"),
    ]
)
class UserEntity(
    @Id
    @GeneratedValue(strategy = GenerationType.UUID)
    var id: UserId? = null,
    @Column(nullable = false, unique = true)
    var email: String,
    @Column(nullable = false, unique = true)
    var username: String,
    @Column(nullable = false)
    var hashedPassword: String,
    @Column(nullable = false)
    var hasVerifiedEmail: Boolean = false,
    @CreationTimestamp
    var createdAt: Instant = Instant.now(),
    @UpdateTimestamp
    var updatedAt: Instant = Instant.now(),
)

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/database/mappers/ApiKeyMappers.kt
package com.plcoding.chirp.infra.database.mappers

import com.plcoding.chirp.domain.model.ApiKey
import com.plcoding.chirp.infra.database.entities.ApiKeyEntity

fun ApiKeyEntity.toApiKey(): ApiKey {
    return ApiKey(
        key = key,
        validFrom = validFrom,
        expiresAt = expiresAt,
    )
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/database/mappers/EmailVerificationTokenMappers.kt
package com.plcoding.chirp.infra.database.mappers

import com.plcoding.chirp.domain.model.EmailVerificationToken
import com.plcoding.chirp.infra.database.entities.EmailVerificationTokenEntity

fun EmailVerificationTokenEntity.toEmailVerificationToken(): EmailVerificationToken {
    return EmailVerificationToken(
        id = id,
        token = token,
        user = user.toUser()
    )
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/database/mappers/UserMappers.kt
package com.plcoding.chirp.infra.database.mappers

import com.plcoding.chirp.domain.model.User
import com.plcoding.chirp.infra.database.entities.UserEntity

fun UserEntity.toUser(): User {
    return User(
        id = id!!,
        username = username,
        email = email,
        hasEmailVerified = hasVerifiedEmail
    )
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/database/repositories/ApiKeyRepository.kt
package com.plcoding.chirp.infra.database.repositories

import com.plcoding.chirp.infra.database.entities.ApiKeyEntity
import org.springframework.data.jpa.repository.JpaRepository

interface ApiKeyRepository: JpaRepository<ApiKeyEntity, String>

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/database/repositories/EmailVerificationTokenRepository.kt
package com.plcoding.chirp.infra.database.repositories

import com.plcoding.chirp.infra.database.entities.EmailVerificationTokenEntity
import com.plcoding.chirp.infra.database.entities.UserEntity
import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.data.jpa.repository.Modifying
import org.springframework.data.jpa.repository.Query
import java.time.Instant

interface EmailVerificationTokenRepository: JpaRepository<EmailVerificationTokenEntity, Long> {
    fun findByToken(token: String): EmailVerificationTokenEntity?
    fun deleteByExpiresAtLessThan(now: Instant)

    @Modifying
    @Query("""
        UPDATE EmailVerificationTokenEntity e
        SET e.usedAt = CURRENT_TIMESTAMP 
        WHERE e.user = :user
    """)
    fun invalidateActiveTokensForUser(user: UserEntity)
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/database/repositories/PasswordResetTokenRepository.kt
package com.plcoding.chirp.infra.database.repositories

import com.plcoding.chirp.infra.database.entities.PasswordResetTokenEntity
import com.plcoding.chirp.infra.database.entities.UserEntity
import org.springframework.data.jpa.repository.JpaRepository
import org.springframework.data.jpa.repository.Modifying
import org.springframework.data.jpa.repository.Query
import java.time.Instant

interface PasswordResetTokenRepository: JpaRepository<PasswordResetTokenEntity, Long> {
    fun findByToken(token: String): PasswordResetTokenEntity?
    fun deleteByExpiresAtLessThan(now: Instant)

    @Modifying
    @Query("""
        UPDATE PasswordResetTokenEntity p
        SET p.usedAt = CURRENT_TIMESTAMP
        WHERE p.user = :user
    """)
    fun invalidateActiveTokensForUser(user: UserEntity)
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/database/repositories/RefreshTokenRepository.kt
package com.plcoding.chirp.infra.database.repositories

import com.plcoding.chirp.domain.type.UserId
import com.plcoding.chirp.infra.database.entities.RefreshTokenEntity
import org.springframework.data.jpa.repository.JpaRepository

interface RefreshTokenRepository: JpaRepository<RefreshTokenEntity, Long> {
    fun findByUserIdAndHashedToken(userId: UserId, hashedToken: String): RefreshTokenEntity?
    fun deleteByUserIdAndHashedToken(userId: UserId, hashedToken: String)
    fun deleteByUserId(userId: UserId)
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/database/repositories/UserRepository.kt
package com.plcoding.chirp.infra.database.repositories

import com.plcoding.chirp.domain.type.UserId
import com.plcoding.chirp.infra.database.entities.UserEntity
import org.springframework.data.jpa.repository.JpaRepository

interface UserRepository: JpaRepository<UserEntity, UserId> {
    fun findByEmail(email: String): UserEntity?
    fun findByEmailOrUsername(email: String, username: String): UserEntity?
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/rate_limiting/EmailRateLimiter.kt
package com.plcoding.chirp.infra.rate_limiting

import com.plcoding.chirp.domain.exception.RateLimitException
import org.springframework.beans.factory.annotation.Value
import org.springframework.core.io.Resource
import org.springframework.data.redis.core.StringRedisTemplate
import org.springframework.data.redis.core.script.DefaultRedisScript
import org.springframework.stereotype.Component

@Component
class EmailRateLimiter(
    private val redisTemplate: StringRedisTemplate
) {

    companion object {
        private const val EMAIL_RATE_LIMIT_PREFIX = "rate_limit:email"
        private const val EMAIL_ATTEMPT_COUNT_PREFIX = "email_attempt_count"
    }

    @Value("classpath:email_rate_limit.lua")
    lateinit var rateLimitResource: Resource

    private val rateLimitScript by lazy {
        val script = rateLimitResource.inputStream.use {
            it.readBytes().decodeToString()
        }
        @Suppress("UNCHECKED_CAST")
        DefaultRedisScript(script, List::class.java as Class<List<Long>>)
    }

    fun withRateLimit(
        email: String,
        action: () -> Unit
    ) {
        val normalizedEmail = email.lowercase().trim()

        val rateLimitKey = "$EMAIL_RATE_LIMIT_PREFIX:$normalizedEmail"
        val attemptCountKey = "$EMAIL_ATTEMPT_COUNT_PREFIX:$normalizedEmail"

        val result = redisTemplate.execute(
            rateLimitScript,
            listOf(rateLimitKey, attemptCountKey)
        )

        val attemptCount = result[0]
        val ttl = result[1]

        if(attemptCount == -1L) {
            throw RateLimitException(resetsInSeconds = ttl)
        }

        action()
    }
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/rate_limiting/IpRateLimiter.kt
package com.plcoding.chirp.infra.rate_limiting

import com.plcoding.chirp.domain.exception.RateLimitException
import org.springframework.beans.factory.annotation.Value
import org.springframework.core.io.Resource
import org.springframework.data.redis.core.StringRedisTemplate
import org.springframework.data.redis.core.script.DefaultRedisScript
import org.springframework.stereotype.Component
import java.time.Duration

@Component
class IpRateLimiter(
    private val redisTemplate: StringRedisTemplate
) {
    companion object {
        private const val IP_RATE_LIMIT_PREFIX = "rate_limit:ip"
    }

    @Value("classpath:ip_rate_limit.lua")
    lateinit var rateLimitResource: Resource

    private val rateLimitScript by lazy {
        val script = rateLimitResource.inputStream.use {
            it.readBytes().decodeToString()
        }
        @Suppress("UNCHECKED_CAST")
        DefaultRedisScript(script, List::class.java as Class<List<Long>>)
    }

    fun <T> withIpRateLimit(
        ipAddress: String,
        resetsIn: Duration,
        maxRequestsPerIp: Int,
        action: () -> T
    ): T {
        val key = "$IP_RATE_LIMIT_PREFIX:$ipAddress"

        val result = redisTemplate.execute(
            rateLimitScript,
            listOf(key),
            maxRequestsPerIp.toString(),
            resetsIn.seconds.toString()
        )

        val currentCount = result[0]

        return if(currentCount <= maxRequestsPerIp) {
            action()
        } else {
            val ttl = result[1]
            throw RateLimitException(resetsInSeconds = ttl)
        }
    }
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/rate_limiting/IpResolver.kt
package com.plcoding.chirp.infra.rate_limiting

import com.plcoding.chirp.infra.config.NginxConfig
import jakarta.servlet.http.HttpServletRequest
import org.slf4j.LoggerFactory
import org.springframework.security.web.util.matcher.IpAddressMatcher
import org.springframework.stereotype.Component
import java.net.Inet4Address
import java.net.Inet6Address

@Component
class IpResolver(
    private val nginxConfig: NginxConfig
) {
    companion object {
        private val PRIVATE_IP_RANGES = listOf(
            "10.0.0.0/8",
            "172.16.0.0/12",
            "192.168.0.0/16",
            "127.0.0.0/8",
            "::1/128",
            "fc00::/7",
            "fe80::/10"
        ).map { IpAddressMatcher(it) }

        private val INVALID_IPS = listOf(
            "unknown",
            "unavailable",
            "0.0.0.0",
            "::"
        )
    }

    private val logger = LoggerFactory.getLogger(IpResolver::class.java)

    private val trustedMatchers: List<IpAddressMatcher> = nginxConfig
        .trustedIps
        .filter { it.isNotBlank() }
        .map { proxy ->
            val cidr = when {
                proxy.contains("/") -> proxy
                proxy.contains(":") -> "$proxy/128"
                else -> "$proxy/32"
            }
            IpAddressMatcher(cidr)
        }

    fun getClientIp(request: HttpServletRequest): String {
        val remoteAddr = request.remoteAddr

        if(!isFromTrustedProxy(remoteAddr)) {
            if(nginxConfig.requireProxy) {
                logger.warn("Direct connection attempt from $remoteAddr")
                throw SecurityException("No valid client IP in proxy headers")
            }

            return remoteAddr
        }

        val clientIp = extractFromXRealIp(request, remoteAddr)

        if(clientIp == null) {
            logger.warn("No valid client IP in proxy headers")
            if(nginxConfig.requireProxy) {
                throw SecurityException("No valid client IP in proxy headers")
            }
        }

        return clientIp ?: remoteAddr
    }

    private fun extractFromXRealIp(
        request: HttpServletRequest,
        proxyIp: String
    ): String? {
        return request.getHeader("X-Real-IP")?.let { header ->
            validateAndNormalizeIp(header, "X-Real-IP", proxyIp)
        }
    }

    private fun validateAndNormalizeIp(ip: String, headerName: String, proxyIp: String): String? {
        val trimmedIp = ip.trim()

        if(trimmedIp.isBlank() || INVALID_IPS.contains(trimmedIp)) {
            logger.debug("Invalid IP in $headerName: $ip from proxy $proxyIp")
            return null
        }

        return try {
            val inetAddr = when {
                trimmedIp.contains(":") -> Inet6Address.getByName(trimmedIp)
                trimmedIp.matches(Regex("\\d+\\.\\d+\\.\\d+\\.\\d+")) ->
                    Inet4Address.getByName(trimmedIp)
                else -> {
                    logger.warn("Invalid IP format in $headerName: $trimmedIp from proxy $proxyIp")
                    return null
                }
            }

            if(isPrivateIp(inetAddr.hostAddress)) {
                logger.debug("Private IP in $headerName: $trimmedIp from proxy $proxyIp")
            }

            inetAddr.hostAddress
        } catch(e: Exception) {
            logger.warn("Invalid IP format in $headerName: $trimmedIp from proxy $proxyIp", e)
            null
        }
    }

    private fun isPrivateIp(ip: String): Boolean {
        return PRIVATE_IP_RANGES.any { it.matches(ip) }
    }

    private fun isFromTrustedProxy(ip: String): Boolean {
        return trustedMatchers.any { matcher ->
            matcher.matches(ip)
        }
    }
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/security/PasswordEncoder.kt
package com.plcoding.chirp.infra.security

import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder
import org.springframework.stereotype.Component

@Component
class PasswordEncoder {

    private val bcrypt = BCryptPasswordEncoder()

    fun encode(rawPassword: String): String? = bcrypt.encode(rawPassword)

    fun matches(rawPassword: String, hashedPassword: String): Boolean {
        return bcrypt.matches(rawPassword, hashedPassword)
    }
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/infra/security/TokenGenerator.kt
package com.plcoding.chirp.infra.security

import java.security.SecureRandom
import java.util.Base64

object TokenGenerator {
    fun generateSecureToken(): String {
        val bytes = ByteArray(32) { 0 }

        val secureRandom = SecureRandom()
        secureRandom.nextBytes(bytes)

        return Base64.getUrlEncoder()
            .withoutPadding()
            .encodeToString(bytes)
    }
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/service/ApiKeyService.kt
package com.plcoding.chirp.service

import com.plcoding.chirp.domain.model.ApiKey
import com.plcoding.chirp.infra.database.entities.ApiKeyEntity
import com.plcoding.chirp.infra.database.mappers.toApiKey
import com.plcoding.chirp.infra.database.repositories.ApiKeyRepository
import org.springframework.beans.factory.annotation.Value
import org.springframework.data.repository.findByIdOrNull
import org.springframework.stereotype.Service
import org.springframework.transaction.annotation.Transactional
import java.time.Instant
import java.time.temporal.ChronoUnit

@Service
class ApiKeyService(
    private val apiKeyRepository: ApiKeyRepository,
    @param:Value("\${chirp.api-key.expires-in-days}") val expiresInDays: Long
) {

    @Transactional
    fun createKey(email: String): ApiKey {
        val key = ApiKey.generateKey()

        val now = Instant.now()
        val entity = ApiKeyEntity(
            key = key,
            email = email.trim(),
            validFrom = now,
            expiresAt = now.plus(expiresInDays, ChronoUnit.DAYS)
        )

        return apiKeyRepository.save(entity).toApiKey()
    }

    fun isValidKey(key: String): Boolean {
        return apiKeyRepository.findByIdOrNull(key) != null
    }
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/service/AuthService.kt
package com.plcoding.chirp.service

import com.plcoding.chirp.domain.events.user.UserEvent
import com.plcoding.chirp.domain.exception.EmailNotVerifiedException
import com.plcoding.chirp.domain.exception.InvalidCredentialsException
import com.plcoding.chirp.domain.exception.InvalidTokenException
import com.plcoding.chirp.domain.exception.UserAlreadyExistsException
import com.plcoding.chirp.domain.exception.UserNotFoundException
import com.plcoding.chirp.domain.model.AuthenticatedUser
import com.plcoding.chirp.domain.model.User
import com.plcoding.chirp.domain.type.UserId
import com.plcoding.chirp.infra.database.entities.RefreshTokenEntity
import com.plcoding.chirp.infra.database.entities.UserEntity
import com.plcoding.chirp.infra.database.mappers.toUser
import com.plcoding.chirp.infra.database.repositories.RefreshTokenRepository
import com.plcoding.chirp.infra.database.repositories.UserRepository
import com.plcoding.chirp.infra.message_queue.EventPublisher
import com.plcoding.chirp.infra.security.PasswordEncoder
import org.springframework.data.repository.findByIdOrNull
import org.springframework.stereotype.Service
import org.springframework.transaction.annotation.Transactional
import java.security.MessageDigest
import java.time.Instant
import java.util.Base64

@Service
class AuthService(
    private val userRepository: UserRepository,
    private val passwordEncoder: PasswordEncoder,
    private val jwtService: JwtService,
    private val refreshTokenRepository: RefreshTokenRepository,
    private val emailVerificationService: EmailVerificationService,
    private val eventPublisher: EventPublisher
) {

    @Transactional
    fun register(email: String, username: String, password: String): User {
        val trimmedEmail = email.trim()
        val user = userRepository.findByEmailOrUsername(
            email = trimmedEmail,
            username = username.trim()
        )
        if(user != null) {
            throw UserAlreadyExistsException()
        }

        val savedUser = userRepository.saveAndFlush(
            UserEntity(
                email = trimmedEmail,
                username = username.trim(),
                hashedPassword = passwordEncoder.encode(password)!!
            )
        ).toUser()

        val token = emailVerificationService.createVerificationToken(trimmedEmail)

        eventPublisher.publish(
            event = UserEvent.Created(
                userId = savedUser.id,
                email = savedUser.email,
                username = savedUser.username,
                verificationToken = token.token
            )
        )

        return savedUser
    }

    fun login(
        email: String,
        password: String
    ): AuthenticatedUser {
        val user = userRepository.findByEmail(email.trim())
            ?: throw InvalidCredentialsException()

        if(!passwordEncoder.matches(password, user.hashedPassword)) {
            throw InvalidCredentialsException()
        }

        if(!user.hasVerifiedEmail) {
            throw EmailNotVerifiedException()
        }

        return user.id?.let { userId ->
            val accessToken = jwtService.generateAccessToken(userId)
            val refreshToken = jwtService.generateRefreshToken(userId)

            storeRefreshToken(userId, refreshToken)

            AuthenticatedUser(
                user = user.toUser(),
                accessToken = accessToken,
                refreshToken = refreshToken
            )
        } ?: throw UserNotFoundException()
    }

    @Transactional
    fun refresh(refreshToken: String): AuthenticatedUser {
        if(!jwtService.validateRefreshToken(refreshToken)) {
            throw InvalidTokenException(
                message = "Invalid refresh token"
            )
        }

        val userId = jwtService.getUserIdFromToken(refreshToken)
        val user = userRepository.findByIdOrNull(userId)
            ?: throw UserNotFoundException()

        val hashed = hashToken(refreshToken)

        return user.id?.let { userId ->
            refreshTokenRepository.findByUserIdAndHashedToken(
                userId = userId,
                hashedToken = hashed
            ) ?: throw InvalidTokenException("Invalid refresh token")

            refreshTokenRepository.deleteByUserIdAndHashedToken(
                userId = userId,
                hashedToken = hashed
            )

            val newAccessToken = jwtService.generateAccessToken(userId)
            val newRefreshToken = jwtService.generateRefreshToken(userId)

            storeRefreshToken(userId, newRefreshToken)

            AuthenticatedUser(
                user = user.toUser(),
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            )
        } ?: throw UserNotFoundException()
    }

    @Transactional
    fun logout(refreshToken: String) {
        val userId = jwtService.getUserIdFromToken(refreshToken)
        val hashed = hashToken(refreshToken)
        refreshTokenRepository.deleteByUserIdAndHashedToken(userId, hashed)
    }

    private fun storeRefreshToken(userId: UserId, token: String) {
        val hashed = hashToken(token)
        val expiryMs = jwtService.refreshTokenValidityMs
        val expiresAt = Instant.now().plusMillis(expiryMs)

        refreshTokenRepository.save(
            RefreshTokenEntity(
                userId = userId,
                expiresAt = expiresAt,
                hashedToken = hashed
            )
        )
    }

    private fun hashToken(token: String): String {
        val digest = MessageDigest.getInstance("SHA-256")
        val hashBytes = digest.digest(token.encodeToByteArray())
        return Base64.getEncoder().encodeToString(hashBytes)
    }
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/service/EmailVerificationService.kt
package com.plcoding.chirp.service

import com.plcoding.chirp.domain.events.user.UserEvent
import com.plcoding.chirp.domain.exception.InvalidTokenException
import com.plcoding.chirp.domain.exception.UserNotFoundException
import com.plcoding.chirp.domain.model.EmailVerificationToken
import com.plcoding.chirp.infra.database.entities.EmailVerificationTokenEntity
import com.plcoding.chirp.infra.database.mappers.toEmailVerificationToken
import com.plcoding.chirp.infra.database.mappers.toUser
import com.plcoding.chirp.infra.database.repositories.EmailVerificationTokenRepository
import com.plcoding.chirp.infra.database.repositories.UserRepository
import com.plcoding.chirp.infra.message_queue.EventPublisher
import org.springframework.beans.factory.annotation.Value
import org.springframework.scheduling.annotation.Scheduled
import org.springframework.stereotype.Service
import org.springframework.transaction.annotation.Transactional
import java.time.Instant
import java.time.temporal.ChronoUnit

@Service
class EmailVerificationService(
    private val emailVerificationTokenRepository: EmailVerificationTokenRepository,
    private val userRepository: UserRepository,
    @param:Value("\${chirp.email.verification.expiry-hours}") private val expiryHours: Long,
    private val eventPublisher: EventPublisher
) {

    @Transactional
    fun resendVerificationEmail(email: String) {
        val token = createVerificationToken(email)

        if(token.user.hasEmailVerified) {
            return
        }

        eventPublisher.publish(
            event = UserEvent.RequestResendVerification(
                userId = token.user.id,
                email = token.user.email,
                username = token.user.username,
                verificationToken = token.token
            )
        )
    }

    @Transactional
    fun createVerificationToken(email: String): EmailVerificationToken {
        val userEntity = userRepository.findByEmail(email)
            ?: throw UserNotFoundException()

        emailVerificationTokenRepository.invalidateActiveTokensForUser(userEntity)

        val token = EmailVerificationTokenEntity(
            expiresAt = Instant.now().plus(expiryHours, ChronoUnit.HOURS),
            user = userEntity
        )

        return emailVerificationTokenRepository.save(token).toEmailVerificationToken()
    }

    @Transactional
    fun verifyEmail(token: String) {
        val verificationToken = emailVerificationTokenRepository.findByToken(token)
            ?: throw InvalidTokenException("Email verification token is invalid.")

        if(verificationToken.isUsed) {
            throw InvalidTokenException("Email verification token is already used.")
        }

        if(verificationToken.isExpired) {
            throw InvalidTokenException("Email verification token has already expired.")
        }

        emailVerificationTokenRepository.save(
            verificationToken.apply {
                this.usedAt = Instant.now()
            }
        )
        userRepository.save(
            verificationToken.user.apply {
                this.hasVerifiedEmail = true
            }
        ).toUser()

        eventPublisher.publish(
            event = UserEvent.Verified(
                userId = verificationToken.user.id!!,
                email = verificationToken.user.email,
                username = verificationToken.user.username,
            )
        )
    }

    @Scheduled(cron = "0 0 3 * * *")
    fun cleanupExpiredTokens() {
        emailVerificationTokenRepository.deleteByExpiresAtLessThan(
            now = Instant.now()
        )
    }
}

### FILE: user/src/main/kotlin/com/plcoding/chirp/service/PasswordResetService.kt
package com.plcoding.chirp.service

import com.plcoding.chirp.domain.events.user.UserEvent
import com.plcoding.chirp.domain.exception.InvalidCredentialsException
import com.plcoding.chirp.domain.exception.InvalidTokenException
import com.plcoding.chirp.domain.exception.SamePasswordException
import com.plcoding.chirp.domain.exception.UserNotFoundException
import com.plcoding.chirp.domain.type.UserId
import com.plcoding.chirp.infra.database.entities.PasswordResetTokenEntity
import com.plcoding.chirp.infra.database.repositories.PasswordResetTokenRepository
import com.plcoding.chirp.infra.database.repositories.RefreshTokenRepository
import com.plcoding.chirp.infra.database.repositories.UserRepository
import com.plcoding.chirp.infra.message_queue.EventPublisher
import com.plcoding.chirp.infra.security.PasswordEncoder
import org.springframework.beans.factory.annotation.Value
import org.springframework.data.repository.findByIdOrNull
import org.springframework.scheduling.annotation.Scheduled
import org.springframework.stereotype.Service
import org.springframework.transaction.annotation.Transactional
import java.time.Instant
import java.time.temporal.ChronoUnit

@Service
class PasswordResetService(
    private val userRepository: UserRepository,
    private val passwordResetTokenRepository: PasswordResetTokenRepository,
    private val passwordEncoder: PasswordEncoder,
    @param:Value("\${chirp.email.reset-password.expiry-minutes}")
    private val expiryMinutes: Long,
    private val refreshTokenRepository: RefreshTokenRepository,
    private val eventPublisher: EventPublisher
) {
    @Transactional
    fun requestPasswordReset(email: String) {
        val user = userRepository.findByEmail(email) ?: return

        passwordResetTokenRepository.invalidateActiveTokensForUser(user)

        val token = PasswordResetTokenEntity(
            user = user,
            expiresAt = Instant.now().plus(expiryMinutes, ChronoUnit.MINUTES),
        )
        passwordResetTokenRepository.save(token)

        eventPublisher.publish(
            event = UserEvent.RequestResetPassword(
                userId = user.id!!,
                email = user.email,
                username = user.username,
                passwordResetToken = token.token,
                expiresInMinutes = expiryMinutes
            )
        )
    }

    @Transactional
    fun resetPassword(token: String, newPassword: String) {
        val resetToken = passwordResetTokenRepository.findByToken(token)
            ?: throw InvalidTokenException("Invalid password reset token")

        if(resetToken.isUsed) {
            throw InvalidTokenException("Email verification token is already used.")
        }

        if(resetToken.isExpired) {
            throw InvalidTokenException("Email verification token has already expired.")
        }

        val user = resetToken.user

        if(passwordEncoder.matches(newPassword, user.hashedPassword)) {
            throw SamePasswordException()
        }

        val hashedNewPassword = passwordEncoder.encode(newPassword)
        userRepository.save(
            user.apply {
                this.hashedPassword = hashedNewPassword!!
            }
        )

        passwordResetTokenRepository.save(
            resetToken.apply {
                this.usedAt = Instant.now()
            }
        )

        refreshTokenRepository.deleteByUserId(user.id!!)
    }

    @Transactional
    fun changePassword(
        userId: UserId,
        oldPassword: String,
        newPassword: String,
    ) {
        val user = userRepository.findByIdOrNull(userId)
            ?: throw UserNotFoundException()

        if(!passwordEncoder.matches(oldPassword, user.hashedPassword)) {
            throw InvalidCredentialsException()
        }

        if(oldPassword == newPassword) {
            throw SamePasswordException()
        }

        refreshTokenRepository.deleteByUserId(user.id!!)

        val newHashedPassword = passwordEncoder.encode(newPassword)
        userRepository.save(
            user.apply {
                this.hashedPassword = newHashedPassword!!
            }
        )
    }

    @Scheduled(cron = "0 0 3 * * *")
    fun cleanupExpiredTokens() {
        passwordResetTokenRepository.deleteByExpiresAtLessThan(
            now = Instant.now()
        )
    }
} to Upgraded stack while preserving functional parity.
- Domain: generic_legacy
- Repo: https://github.com/philipplackner/chirp-api @ detached (unknown)
- Source language: (undetermined)
- Generated at: 2026-05-14T10:03:31.295883+00:00

## Decision Brief

| Category | Summary |
| --- | --- |
| Modernization readiness | n/a/100 |
| Risk tier | high |
| Headline | Full upgrade / translation + remediation recommended. |

## Recommended strategy

- Phased modernization recommended; specific track plan pending Discover refinement.

### Open Questions

- Are there existing operational constraints or integration dependencies not listed?
- What are target latency, throughput, and availability SLOs?

## Functional Requirements

### FR-001 — User Authentication
Implement user authentication using JWT and API Key methods in Go.

**Acceptance criteria**:
- Users can log in using JWT tokens.
- API Key authentication is supported.
- Authentication errors are logged and handled gracefully.

### FR-006 — Security Configuration
Replicate existing security configurations in the Go environment.

**Acceptance criteria**:
- Security settings match the legacy system.
- CSRF protection is implemented.
- Session management is stateless.

### FR-007 — Cache Management
Implement Redis caching in the Go backend.

**Acceptance criteria**:
- Cache is configured with custom serialization.
- Cache expiration policies are set.
- Cache performance is monitored.

## Non-Functional Requirements

- **NFR-002** _security_: Maintain robust security practices in the Go backend.

## QA Validation Summary

| Gate | Status | Detail |
| --- | --- | --- |
| gherkin_syntax | PASS | BDD syntax validation for Feature/Scenario/Given/When/Then. |
| requirements_completeness | FAIL | Checks minimum requirement volume, scenario coverage, and capability mapping presence. |
| compliance_constraints_applied | WARN | Verifies that regulatory/software controls are linked to requirements when applicable. |
| intake_classifier_alignment | PASS |  |
| knowledge_snapshot_pinned | PASS | Run is pinned to immutable knowledge source snapshots. |
| compliance_citation_grounding | PASS | Compliance controls have required citations. |
| source_influenced_qa_mandatory | PASS | No active knowledge sources in run context snapshot. |

## Evidence Appendix

### Discover Review Checklist

- Handler Inventory Completeness — PASS
- Report Model Reconciled — PASS
- Variant Resolution — PASS
- Variant Schema Divergence — PASS
- Key Safety Issues Identified — FAIL
- Schema Key Verification — PASS
- Identity & Access Model — WARN
- Database Archaeology & Mapping Readiness — WARN

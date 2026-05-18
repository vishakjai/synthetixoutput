package com.example.articleservice

import org.junit.jupiter.api.Test
import org.springframework.boot.test.context.SpringBootTest
import org.springframework.test.web.reactive.server.WebTestClient
import org.springframework.beans.factory.annotation.Autowired

@SpringBootTest(webEnvironment = SpringBootTest.WebEnvironment.RANDOM_PORT)
class ArticleServiceApplicationTests {

    @Autowired
    lateinit var webTestClient: WebTestClient

    @Test
    fun `test health endpoint`() {
        webTestClient.get().uri("/health")
            .exchange()
            .expectStatus().isOk
            .expectBody().jsonPath("$.status").isEqualTo("healthy")
    }

    @Test
    fun `test ready endpoint`() {
        webTestClient.get().uri("/ready")
            .exchange()
            .expectStatus().isOk
            .expectBody().jsonPath("$.status").isEqualTo("ready")
    }
}
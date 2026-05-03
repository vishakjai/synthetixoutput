# Kotlin JVM Service Modernization BRD

## Header
- Track: Reimagine — Kotlin JVM Service Upgrade
- Source language: Kotlin (JVM server target)
- Objective: Upgrade Kotlin application to Kotlin application (upgraded) while preserving functional parity.
- Repository: https://github.com/vishakjai/chirp-api @ master
- Generated at: 2026-05-01T19:32:20.026744+00:00

## Decision Brief

| Category | Summary |
| --- | --- |
| Modernization readiness | 100/100 |
| Risk tier | medium |
| Inventory | 74 Kotlin JVM modules across 178 files (4332 LOC) |
| Headline | Kotlin JVM upgrade recommended (JDK 21, Kotlin 2.x, Coroutines 1.8+, Spring Security 6 config DSL). |

## Current State

| Aspect | Detected value |
| --- | --- |
| Platform | Kotlin JVM server runtime |
| Primary language LOC mix | Kotlin 93.3% |
| Framework | (framework not detected) |
| HTTP stack | (HTTP stack not detected) |
| Dependency injection | (DI layer not detected) |
| Persistence | (persistence stack not detected) |
| Security | (security stack not detected) |
| Concurrency | (coroutines not detected; synchronous threads or reactive) |
| Observability | (no actuator / metrics endpoint detected) |
| Test tooling | (JUnit only — no Testcontainers detected) |
| Build system | Gradle |

## Target State

Modernization recommendations from the analyst track plan:

- **app Kotlin server modernization** → Kotlin 2.x / Spring Boot 3 or Ktor 3 / Coroutines 1.8+
- **build-logic Kotlin server modernization** → Kotlin 2.x / Spring Boot 3 or Ktor 3 / Coroutines 1.8+
- **root Kotlin server modernization** → Kotlin 2.x / Spring Boot 3 or Ktor 3 / Coroutines 1.8+
- **chat Kotlin server modernization** → Kotlin 2.x / Spring Boot 3 or Ktor 3 / Coroutines 1.8+
- **common Kotlin server modernization** → Kotlin 2.x / Spring Boot 3 or Ktor 3 / Coroutines 1.8+
- **notification Kotlin server modernization** → Kotlin 2.x / Spring Boot 3 or Ktor 3 / Coroutines 1.8+

## Recommended Strategy

- **app Kotlin server modernization**. Detected as kotlin_server_project with archetypes: none.
- **build-logic Kotlin server modernization**. Detected as kotlin_server_project with archetypes: none.
- **root Kotlin server modernization**. Detected as kotlin_server_project with archetypes: none.
- **chat Kotlin server modernization**. Detected as kotlin_server_project with archetypes: none.
- **common Kotlin server modernization**. Detected as kotlin_server_project with archetypes: none.
- **notification Kotlin server modernization**. Detected as kotlin_server_project with archetypes: none.
- **user Kotlin server modernization**. Detected as kotlin_server_project with archetypes: none.

### Open Questions

- Are there existing operational constraints or integration dependencies not listed?
- What are target latency, throughput, and availability SLOs?
- Confirm JDK target (17 or 21) and Kotlin / Gradle toolchain baseline to upgrade to.
- Confirm framework direction: Spring Boot 3.x, Ktor 3, or Micronaut 4 — and whether to stay on Spring MVC or move to WebFlux.
- Confirm Spring Security config migration path (5.x WebSecurityConfigurerAdapter → 6.x SecurityFilterChain DSL).
- Confirm persistence strategy: JPA + Hibernate 6.x, JetBrains Exposed, or jOOQ — and the Flyway / Liquibase migration baseline.
- Confirm concurrency model: Kotlin Coroutines 1.8+, Project Loom virtual threads, or reactive (WebFlux / Reactor / RxJava).
- Confirm observability baseline: Actuator + Micrometer + Prometheus / OTel wire-up, structured logging, trace propagation.
- Confirm test strategy upgrade: JUnit 5 + MockK, Testcontainers for integration tests, contract-testing (Pact / Spring Cloud Contract).

## Module Details



### MOD-001 — Root

**Type**: Build / root module
**Component ID**: `kotlin::root`
**Files**: 1 | **LOC**: 15

**Narrative Overview**

Root is the project / build configuration module. Gradle build files only.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- Root module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- Root module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-002 — App

**Type**: Kotlin JVM module
**Component ID**: `kotlin::app`
**Files**: 6 | **LOC**: 273

**Narrative Overview**

App is a Kotlin JVM module. See the Controller Endpoint Inventory and Service → Repository Binding Map appendices for cross-module behaviour.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

1. Module exposes beans / helpers consumed by upstream Controller/Service modules
2. See consuming modules for request-handling process flow

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- App module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- App module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-003 — Build-logic

**Type**: Kotlin JVM module
**Component ID**: `kotlin::build-logic`
**Files**: 5 | **LOC**: 95

**Narrative Overview**

Build-logic is a Kotlin JVM module. See the Controller Endpoint Inventory and Service → Repository Binding Map appendices for cross-module behaviour.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

1. Module exposes beans / helpers consumed by upstream Controller/Service modules
2. See consuming modules for request-handling process flow

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- Build-logic module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- Build-logic module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-004 — Chat

**Type**: Kotlin JVM module
**Component ID**: `kotlin::chat`
**Files**: 51 | **LOC**: 1791

**Narrative Overview**

Chat is a Kotlin JVM module. See the Controller Endpoint Inventory and Service → Repository Binding Map appendices for cross-module behaviour.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

1. Module exposes beans / helpers consumed by upstream Controller/Service modules
2. See consuming modules for request-handling process flow

**Features**

- Persistence access via `ChatMessageRepository`
- Persistence access via `ChatParticipantRepository`
- Persistence access via `ChatRepository`

**Entities & Data Model**

| Entity | Source path | Package |
| --- | --- | --- |
| ChatMessageEntity | chat/src/main/kotlin/com/plcoding/chirp/infra/database/entities/ChatMessageEntity.kt | com.plcoding.chirp.infra.database.entities |
| ChatParticipantEntity | chat/src/main/kotlin/com/plcoding/chirp/infra/database/entities/ChatParticipantEntity.kt | com.plcoding.chirp.infra.database.entities |

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- Chat module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- Chat module passes existing JUnit 5 + MockK test suite after dependency upgrades
- Flyway / Liquibase migration baseline exists for Chat entities; JPA schema validation passes at startup

### MOD-005 — Common

**Type**: Shared utility module
**Component ID**: `kotlin::common`
**Files**: 18 | **LOC**: 440

**Narrative Overview**

Common is a Kotlin JVM module. See the Controller Endpoint Inventory and Service → Repository Binding Map appendices for cross-module behaviour.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

1. Module exposes beans / helpers consumed by upstream Controller/Service modules
2. See consuming modules for request-handling process flow

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- Common module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- Common module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-006 — Notification

**Type**: Kotlin JVM module
**Component ID**: `kotlin::notification`
**Files**: 21 | **LOC**: 826

**Narrative Overview**

Notification is a Kotlin JVM module. See the Controller Endpoint Inventory and Service → Repository Binding Map appendices for cross-module behaviour.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

1. Module exposes beans / helpers consumed by upstream Controller/Service modules
2. See consuming modules for request-handling process flow

**Features**

- Persistence access via `DeviceTokenRepository`

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- Notification module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- Notification module passes existing JUnit 5 + MockK test suite after dependency upgrades
- Flyway / Liquibase migration baseline exists for Notification entities; JPA schema validation passes at startup

### MOD-007 — User

**Type**: Kotlin JVM module
**Component ID**: `kotlin::user`
**Files**: 55 | **LOC**: 1734

**Narrative Overview**

User is a Kotlin JVM module. See the Controller Endpoint Inventory and Service → Repository Binding Map appendices for cross-module behaviour.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

1. Module exposes beans / helpers consumed by upstream Controller/Service modules
2. See consuming modules for request-handling process flow

**Features**

- Persistence access via `ApiKeyRepository`
- Persistence access via `EmailVerificationTokenRepository`
- Persistence access via `PasswordResetTokenRepository`
- Persistence access via `RefreshTokenRepository`

**Entities & Data Model**

| Entity | Source path | Package |
| --- | --- | --- |
| EmailVerificationTokenEntity | user/src/main/kotlin/com/plcoding/chirp/infra/database/entities/EmailVerificationTokenEntity.kt | com.plcoding.chirp.infra.database.entities |
| PasswordResetTokenEntity | user/src/main/kotlin/com/plcoding/chirp/infra/database/entities/PasswordResetTokenEntity.kt | com.plcoding.chirp.infra.database.entities |
| RefreshTokenEntity | user/src/main/kotlin/com/plcoding/chirp/infra/database/entities/RefreshTokenEntity.kt | com.plcoding.chirp.infra.database.entities |
| UserEntity | user/src/main/kotlin/com/plcoding/chirp/infra/database/entities/UserEntity.kt | com.plcoding.chirp.infra.database.entities |

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- User module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- User module passes existing JUnit 5 + MockK test suite after dependency upgrades
- Flyway / Liquibase migration baseline exists for User entities; JPA schema validation passes at startup

### MOD-008 — Support Artifacts

**Type**: Kotlin JVM module
**Component ID**: `other::support`
**Files**: 7 | **LOC**: 220

**Narrative Overview**

Support Artifacts is a Kotlin JVM module. See the Controller Endpoint Inventory and Service → Repository Binding Map appendices for cross-module behaviour.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

1. Module exposes beans / helpers consumed by upstream Controller/Service modules
2. See consuming modules for request-handling process flow

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- Support Artifacts module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- Support Artifacts module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-009 — ChatController

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatController holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatController module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatController module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-010 — ChatMessageController

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatMessageController holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatMessageController module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatMessageController module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-011 — ChatParticipantController

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatParticipantController holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatParticipantController module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatParticipantController module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-012 — ChatCreatedEvent

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatCreatedEvent holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatCreatedEvent module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatCreatedEvent module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-013 — ChatParticipantLeftEvent

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatParticipantLeftEvent holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatParticipantLeftEvent module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatParticipantLeftEvent module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-014 — ChatParticipantsJoinedEvent

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatParticipantsJoinedEvent holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatParticipantsJoinedEvent module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatParticipantsJoinedEvent module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-015 — MessageDeletedEvent

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

MessageDeletedEvent holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- MessageDeletedEvent module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- MessageDeletedEvent module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-016 — ProfilePictureUpdatedEvent

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ProfilePictureUpdatedEvent holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ProfilePictureUpdatedEvent module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ProfilePictureUpdatedEvent module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-017 — ChatNotFoundException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatNotFoundException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatNotFoundException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatNotFoundException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-018 — ChatParticipantNotFoundException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatParticipantNotFoundException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatParticipantNotFoundException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatParticipantNotFoundException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-019 — InvalidChatSizeException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

InvalidChatSizeException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- InvalidChatSizeException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- InvalidChatSizeException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-020 — InvalidProfilePictureException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

InvalidProfilePictureException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- InvalidProfilePictureException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- InvalidProfilePictureException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-021 — MessageNotFoundException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

MessageNotFoundException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- MessageNotFoundException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- MessageNotFoundException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-022 — StorageException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

StorageException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- StorageException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- StorageException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-023 — ChatMessage

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatMessage holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatMessage module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatMessage module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-024 — ChatParticipant

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatParticipant holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatParticipant module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatParticipant module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-025 — ProfilePictureUploadCredentials

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ProfilePictureUploadCredentials holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ProfilePictureUploadCredentials module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ProfilePictureUploadCredentials module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-026 — ChatEntity

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatEntity holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatEntity module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatEntity module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-027 — ChatMessageRepository

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatMessageRepository holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatMessageRepository module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatMessageRepository module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-028 — ChatParticipantRepository

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatParticipantRepository holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatParticipantRepository module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatParticipantRepository module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-029 — ChatRepository

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatRepository holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatRepository module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatRepository module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-030 — SupabaseStorageService

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

SupabaseStorageService holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- SupabaseStorageService module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- SupabaseStorageService module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-031 — ChatMessageService

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatMessageService holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatMessageService module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatMessageService module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-032 — ChatParticipantService

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatParticipantService holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatParticipantService module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatParticipantService module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-033 — ChatService

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatService holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatService module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatService module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-034 — ProfilePictureService

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ProfilePictureService holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ProfilePictureService module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ProfilePictureService module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-035 — ChirpEvent

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChirpEvent holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChirpEvent module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChirpEvent module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-036 — ChatEvent

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatEvent holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatEvent module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatEvent module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-037 — ChatEventConstants

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ChatEventConstants holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ChatEventConstants module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ChatEventConstants module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-038 — UserEvent

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

UserEvent holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- UserEvent module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- UserEvent module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-039 — UserEventConstants

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

UserEventConstants holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- UserEventConstants module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- UserEventConstants module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-040 — ForbiddenException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ForbiddenException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ForbiddenException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ForbiddenException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-041 — InvalidTokenException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

InvalidTokenException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- InvalidTokenException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- InvalidTokenException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-042 — UnauthorizedException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

UnauthorizedException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- UnauthorizedException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- UnauthorizedException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-043 — JwtService

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

JwtService holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- JwtService module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- JwtService module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-044 — DeviceTokenController

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

DeviceTokenController holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- DeviceTokenController module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- DeviceTokenController module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-045 — InvalidDeviceTokenException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

InvalidDeviceTokenException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- InvalidDeviceTokenException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- InvalidDeviceTokenException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-046 — DeviceToken

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

DeviceToken holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- DeviceToken module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- DeviceToken module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-047 — PushNotification

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

PushNotification holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- PushNotification module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- PushNotification module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-048 — PushNotificationSendResult

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

PushNotificationSendResult holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- PushNotificationSendResult module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- PushNotificationSendResult module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-049 — DeviceTokenRepository

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

DeviceTokenRepository holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- DeviceTokenRepository module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- DeviceTokenRepository module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-050 — FirebasePushNotificationService

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

FirebasePushNotificationService holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- FirebasePushNotificationService module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- FirebasePushNotificationService module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-051 — EmailService

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

EmailService holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- EmailService module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- EmailService module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-052 — EmailTemplateService

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

EmailTemplateService holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- EmailTemplateService module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- EmailTemplateService module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-053 — PushNotificationService

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

PushNotificationService holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- PushNotificationService module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- PushNotificationService module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-054 — ApiKeyController

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ApiKeyController holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ApiKeyController module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ApiKeyController module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-055 — AuthController

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

AuthController holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- AuthController module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- AuthController module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-056 — EmailNotVerifiedException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

EmailNotVerifiedException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- EmailNotVerifiedException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- EmailNotVerifiedException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-057 — InvalidCredentialsException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

InvalidCredentialsException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- InvalidCredentialsException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- InvalidCredentialsException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-058 — RateLimitException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

RateLimitException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- RateLimitException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- RateLimitException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-059 — SamePasswordException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

SamePasswordException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- SamePasswordException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- SamePasswordException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-060 — UserAlreadyExistsException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

UserAlreadyExistsException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- UserAlreadyExistsException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- UserAlreadyExistsException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-061 — UserNotFoundException

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

UserNotFoundException holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- UserNotFoundException module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- UserNotFoundException module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-062 — ApiKey

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ApiKey holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ApiKey module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ApiKey module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-063 — AuthenticatedUser

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

AuthenticatedUser holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- AuthenticatedUser module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- AuthenticatedUser module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-064 — EmailVerificationToken

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

EmailVerificationToken holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- EmailVerificationToken module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- EmailVerificationToken module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-065 — ApiKeyEntity

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ApiKeyEntity holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ApiKeyEntity module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ApiKeyEntity module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-066 — ApiKeyRepository

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ApiKeyRepository holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ApiKeyRepository module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ApiKeyRepository module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-067 — EmailVerificationTokenRepository

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

EmailVerificationTokenRepository holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- EmailVerificationTokenRepository module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- EmailVerificationTokenRepository module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-068 — PasswordResetTokenRepository

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

PasswordResetTokenRepository holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- PasswordResetTokenRepository module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- PasswordResetTokenRepository module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-069 — RefreshTokenRepository

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

RefreshTokenRepository holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- RefreshTokenRepository module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- RefreshTokenRepository module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-070 — UserRepository

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

UserRepository holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- UserRepository module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- UserRepository module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-071 — ApiKeyService

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

ApiKeyService holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- ApiKeyService module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- ApiKeyService module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-072 — AuthService

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

AuthService holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- AuthService module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- AuthService module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-073 — EmailVerificationService

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

EmailVerificationService holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- EmailVerificationService module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- EmailVerificationService module passes existing JUnit 5 + MockK test suite after dependency upgrades

### MOD-074 — PasswordResetService

**Type**: Build configuration module
**Component ID**: ``
**Files**: 0 | **LOC**: ?

**Narrative Overview**

PasswordResetService holds configuration beans — security filter chains, `@Configuration` classes, application-scoped bean definitions, property bindings.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

_No request flow — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Entities & Data Model**

_No JPA entities detected in this module's symbol scope._

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- PasswordResetService module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- PasswordResetService module passes existing JUnit 5 + MockK test suite after dependency upgrades

## Appendix D — JPA Entities

Kotlin classes annotated with `@Entity` / `@MappedSuperclass` or residing in `model/` / `entity/` source paths.

| Entity | Package | Source path |
| --- | --- | --- |
| ChatMessageEntity |  | chat/src/main/kotlin/com/plcoding/chirp/infra/database/entities/ChatMessageEntity.kt |
| ChatParticipantEntity |  | chat/src/main/kotlin/com/plcoding/chirp/infra/database/entities/ChatParticipantEntity.kt |
| DeviceToken |  | notification/src/main/kotlin/com/plcoding/chirp/domain/model/DeviceToken.kt |
| PushNotification |  | notification/src/main/kotlin/com/plcoding/chirp/domain/model/PushNotification.kt |
| PushNotificationSendResult |  | notification/src/main/kotlin/com/plcoding/chirp/domain/model/PushNotificationSendResult.kt |
| ApiKey |  | user/src/main/kotlin/com/plcoding/chirp/domain/model/ApiKey.kt |
| AuthenticatedUser |  | user/src/main/kotlin/com/plcoding/chirp/domain/model/AuthenticatedUser.kt |
| EmailVerificationToken |  | user/src/main/kotlin/com/plcoding/chirp/domain/model/EmailVerificationToken.kt |
| User |  | user/src/main/kotlin/com/plcoding/chirp/domain/model/User.kt |
| EmailVerificationTokenEntity |  | user/src/main/kotlin/com/plcoding/chirp/infra/database/entities/EmailVerificationTokenEntity.kt |
| PasswordResetTokenEntity |  | user/src/main/kotlin/com/plcoding/chirp/infra/database/entities/PasswordResetTokenEntity.kt |
| RefreshTokenEntity |  | user/src/main/kotlin/com/plcoding/chirp/infra/database/entities/RefreshTokenEntity.kt |
| UserEntity |  | user/src/main/kotlin/com/plcoding/chirp/infra/database/entities/UserEntity.kt |

## QA Validation Summary

| Gate | Status | Detail |
| --- | --- | --- |
| requirements_pack_sections | PASS | Required sections are present. |
| bdd_presence | PASS | BDD features exist for downstream testing. |
| intake_classifier_alignment | PASS |  |

## Evidence

### Discover Review Checklist

- Handler Inventory Completeness — PASS
- Report Model Reconciled — PASS
- Variant Resolution — PASS
- Variant Schema Divergence — PASS
- Key Safety Issues Identified — FAIL
- Schema Key Verification — PASS
- Identity & Access Model — WARN
- Database Archaeology & Mapping Readiness — WARN

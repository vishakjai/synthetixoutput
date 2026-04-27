# Kotlin JVM + Vue Full-Stack Modernization BRD

## Header
- Track: Reimagine — Full-stack (Spring Boot backend + Vue frontend)
- Source language: Kotlin (backend) · JavaScript / TypeScript (frontend)
- Objective: Upgrade Vb6 application to Vb6 application (upgraded) while preserving functional parity.
- Repository: Synthetix @ codex-synthetix-ui-v2-claude (98f57e32079f)
- Generated at: 2026-04-27T22:08:03.677510+00:00

## System Topology

This repository hosts a full-stack system with a Kotlin JVM backend and a Vue single-page application served to end users. The two tracks ship as separate artifacts — the backend as a deployable JVM service, the frontend as static assets bundled and served by a CDN or the backend's static-resources handler — and are coupled at runtime via a REST API contract and an auth scheme.

| Tier | Technology | Responsibility |
| --- | --- | --- |
| Backend | Spring Boot (Kotlin JVM) | REST API, persistence, auth, business logic, observability |
| Frontend | Vue SPA | UI composition, client-side routing, API integration, state management |
| Cross-cutting | REST + auth token | API contract, session/session-token handoff, deployment topology |

Inventory snapshot: 58 files · 11611 LOC aggregate (per-tier breakdown in the Decision Brief sections below).

Unattributed to any module: ~9,800 LOC (node_modules / lockfiles / Gradle wrapper / generated resources). Per-module sum: 1,811 LOC across 2 components.

### Backend — Decision Brief

| Category | Summary |
| --- | --- |
| Modernization readiness | 75/100 |
| Risk tier | high |
| Readiness drivers | Spring Boot 2.1.3.RELEASE pre-3.x (EOL) (-10); JDK 1.8 (pre-11 EOL) (-5); Kotlin 1.2.71 (pre-1.5) (-5); legacy_adapter security config (-5); custom JWT implementation (-5) |
| Inventory | 2 Kotlin JVM modules across 58 files (11611 LOC) |
| Headline | Spring Boot upgrade recommended (JDK 21, Kotlin 2.x, Coroutines 1.8+, Spring Security 6 config DSL). |

### Frontend — Decision Brief

| Category | Summary |
| --- | --- |
| Modernization readiness | 60/100 |
| Risk tier | high |
| Readiness drivers | Vue 2 (EOL Dec 2023) (-10); Options API only (no Composition / <script setup>) (-5); Vuex-only (no Pinia) (-5); No frontend tests detected (-5); JWT persisted in localstorage (XSS surface; hydrated into Vuex at runtime) (-5) |
| Inventory | 1 Vue modules across 58 files (11611 LOC) |
| Headline | Vue upgrade recommended (Vue 3 + Composition API + Pinia + Vite baseline). |

### Backend — Current State

| Aspect | Detected value |
| --- | --- |
| Platform | Kotlin JVM server runtime |
| Primary language LOC mix | Kotlin 6.3% / Java 2.3% |
| Framework | Spring Boot 2.1.3.RELEASE |
| HTTP stack | Spring MVC |
| JVM target | 1.8 |
| Kotlin version | 1.2.71 |
| Dependency injection | Spring Context (stereotype scanning + constructor injection) |
| Persistence | Spring Data JPA / Hibernate, JDBC / PostgreSQL driver |
| Security | Spring Security |
| Concurrency | (coroutines not detected; synchronous threads or reactive) |
| Observability | Spring Actuator + Micrometer |
| Test tooling | (JUnit only — no Testcontainers detected) |
| Build system | Maven |

_Source: kotlin-spring skill brief v1.0.0 (deterministic extraction of build, framework surface, and per-module structure)._

### Frontend — Current State

| Aspect | Detected value |
| --- | --- |
| Framework | Vue 2.6.14 |
| Language | JavaScript |
| API style | Options API (legacy) (7/7 components) |
| State management | Vuex |
| Router | Vue Router 3.x |
| Build tool | Vue CLI (Webpack) 4.5.0 |

_Source: vue-comprehension skill brief v1.0.0 (deterministic package.json + component + router extraction)._

### Backend — Target State

Modernization recommendations from the analyst track plan:

- **backend Kotlin server modernization** → Kotlin 2.x / Spring Boot 3 or Ktor 3 / Coroutines 1.8+
- **root Kotlin server modernization** → Kotlin 2.x / Spring Boot 3 or Ktor 3 / Coroutines 1.8+

### Frontend — Target State

Modernization recommendations from the analyst track plan:

- **frontend Vue frontend modernization** → Vue 3.x / Composition API / Pinia / Vite
- **frontend web modernization** → Modern web stack / API + UI split

## Cross-Cutting Concerns

Topics that neither the backend nor the frontend track owns alone. Confirm each during Define Scope.

### Auth Handoff
- **Current** — likely a JWT issued by Spring Boot Security and held by the Vue client in an HTTP-only cookie or `Authorization` header. CSRF posture + SameSite attributes should be audited.
- **Target** — Spring Security 6.x SecurityFilterChain bean on the backend; axios / fetch interceptor on the Vue client that attaches the token and handles 401 refresh.

### API Contract
- **Current** — untyped REST exchange between Spring Boot controllers and the Vue client; any drift surfaces at runtime.
- **Target** — OpenAPI spec emitted by the backend (springdoc / ktor-openapi), typed client generated for the Vue app, breaking-change policy defined per endpoint.

### Deployment Topology
- **Current** — likely a single deployable that the backend serves static assets from, OR two separate deployables behind a reverse proxy. Confirm which and whether it changes in the target.
- **Target** — two-service topology (backend JAR + SPA bundle on CDN) is the modern default; keep single-service only if the operational simplicity is worth the coupling.

### Observability
- **Current** — unclear whether the SPA surfaces errors to the backend (Sentry / custom endpoint) or only logs locally. Trace context is likely NOT propagated from the client to the backend.
- **Target** — Actuator + Micrometer + Prometheus on the backend; Sentry (or equivalent) on the frontend; optional W3C Trace Context propagation so a single request traces across the boundary.

## Open Questions

- Confirm Vue 2 → Vue 3 migration strategy (full rewrite, `@vue/compat`, or stay).
- Confirm state management direction (Vuex → Pinia).
- Confirm build tool direction (Webpack / Vue CLI → Vite).
- Are there existing operational constraints or integration dependencies not listed?
- What are target latency, throughput, and availability SLOs?
- Confirm JDK target (17 or 21) and Kotlin / Gradle toolchain baseline to upgrade to.
- Confirm framework direction: Spring Boot 3.x, Ktor 3, or Micronaut 4 — and whether to stay on Spring MVC or move to WebFlux.
- Confirm Spring Security config migration path (5.x WebSecurityConfigurerAdapter → 6.x SecurityFilterChain DSL).
- Confirm persistence strategy: JPA + Hibernate 6.x, JetBrains Exposed, or jOOQ — and the Flyway / Liquibase migration baseline.
- Confirm concurrency model: Kotlin Coroutines 1.8+, Project Loom virtual threads, or reactive (WebFlux / Reactor / RxJava).
- Confirm observability baseline: Actuator + Micrometer + Prometheus / OTel wire-up, structured logging, trace propagation.
- Confirm test strategy upgrade: JUnit 5 + MockK, Testcontainers for integration tests, contract-testing (Pact / Spring Cloud Contract).
- Confirm Vue 2 → Vue 3 migration strategy: full rewrite, incremental with `@vue/compat`, or stay on Vue 2 end-of-life.
- Confirm target API style: Options API, Composition API, or `<script setup>` with TypeScript.
- Confirm state-management migration: Vuex → Pinia.
- Confirm build-tool direction: Webpack → Vite (dev-server + bundler), or stay on Vue CLI.
- Confirm Vue Router 3 → 4 upgrade and route-mode strategy.
- Confirm auth handoff: JWT in `Authorization` header, session cookie, or both — and the SameSite / CSRF posture for the SPA.
- Confirm API contract strategy: OpenAPI spec, typed client generation, and breaking-change policy between backend + SPA releases.
- Confirm deployment topology: two-service (backend + SPA) vs. embedded static-serving; reverse-proxy / CDN strategy.

## Backend Module Details



### MOD-001 — Backend

**Type**: Backend service module
**Component ID**: `kotlin::backend`
**Files**: 24 | **LOC**: 991

**Narrative Overview**

Backend hosts domain-service classes that encapsulate business logic for the backend subsystem. Services are constructor-injected into Controllers and compose Repositories for persistence.

**Process Flow** (Controller → Service → Repository → JPA/DB → Response)

1. HTTP request arrives at `AuthController` (Spring dispatcher routes by `@RequestMapping` / `@*Mapping`)
2. `AuthController` validates input and delegates to `ReCaptchaService` (constructor-injected)
3. `ReCaptchaService` applies business rules and calls `PersonRepository` for persistence
4. `PersonRepository` issues JPA / JPQL against the datasource (transaction-wrapped)
5. Response object returned up the chain; `AuthController` serializes to JSON via Jackson

**Features**

- **POST** `/signin` → `authenticateUser()`
- **POST** `/signup` → `registerUser()`
- **GET** `/greeting` → `greeting()`
- **GET** `/persons` → `getPersons()`
- **GET** `/usercontent` → `getUserContent()`
- **GET** `/admincontent` → `getAdminContent()`
- **GET** `/sendSimpleEmail` → `sendSimpleEmail()`
- **GET** `/sendTemplateEmail` → `sendTemplateEmail()`
- **GET** `/sendHtmlEmail` → `sendHtmlEmail()`

**Entities & Data Model**

| Entity | Source path | Package |
| --- | --- | --- |
| LoginUser | backend/src/main/kotlin/com/kotlinspringvue/backend/model/LoginUser.kt | com.kotlinspringvue.backend.model |
| NewUser | backend/src/main/kotlin/com/kotlinspringvue/backend/model/NewUser.kt | com.kotlinspringvue.backend.model |

**Configuration Beans**

- `WebSecurityConfig` — `backend/src/main/kotlin/com/kotlinspringvue/backend/config/WebSecurityConfig.kt`

**Acceptance Criteria** (Kotlin JVM-server upgrade)

- Backend module compiles on JDK 21 with Kotlin 2.x and the chosen framework's 3.x baseline
- Backend module passes existing JUnit 5 + MockK test suite after dependency upgrades
- Spring Security 6.x SecurityFilterChain bean replaces any WebSecurityConfigurerAdapter usage; endpoint auth posture preserved
- Controllers expose an `actuator` health endpoint and respond 200 on `/actuator/health`
- Flyway / Liquibase migration baseline exists for Backend entities; JPA schema validation passes at startup
- Coroutines-powered service methods pass an integration-level Testcontainers test against a real PostgreSQL instance

## Frontend Module Details



### MOD-001 — Frontend

**Type**: Vue frontend module
**Component ID**: `vue::frontend`
**Files**: 7 | **LOC**: 190

**Acceptance Criteria** (Vue upgrade)

- Frontend compiles under Vue 3.x with no `@vue/compat` warnings
- Frontend components use Composition API / `<script setup>` (Options-API only where legacy)
- State access for Frontend goes through Pinia stores (Vuex references removed)
- Build produced by Vite with dev-server start-time and bundle-size budgets met

## Appendix A — Controller Endpoint Inventory

Kotlin Spring controllers detected by the symbol index. Each row is a class-level `@RestController` / `@Controller` annotation. Endpoint-level mappings are listed in Appendix C.

| Stereotype | Class | Source path | Package |
| --- | --- | --- | --- |
| RestController | AuthController | backend/src/main/kotlin/com/kotlinspringvue/backend/controller/AuthController.kt |  |
| RestController | BackendController | backend/src/main/kotlin/com/kotlinspringvue/backend/controller/BackendController.kt |  |

## Appendix B — Service → Repository Binding Map

Best-effort binding derived from package proximity — each Service is listed with Repositories declared in the same (or parent) package.

| Service | Package | Likely repositories |
| --- | --- | --- |
| ReCaptchaService |  | PersonRepository, RoleRepository, UserRepository |
| UserDetailsServiceImpl |  | PersonRepository, RoleRepository, UserRepository |

## Appendix C — Exposed REST Endpoints

HTTP endpoints served by Kotlin controllers, derived from class-level `@RestController` + method-level HTTP mapping annotations.

| Method | Path | Handler | Package | Source |
| --- | --- | --- | --- | --- |
| POST | /signin | authenticateUser | com.kotlinspringvue.backend.controller.AuthController | backend/src/main/kotlin/com/kotlinspringvue/backend/controller/AuthController.kt |
| POST | /signup | registerUser | com.kotlinspringvue.backend.controller.AuthController | backend/src/main/kotlin/com/kotlinspringvue/backend/controller/AuthController.kt |
| GET | /greeting | greeting | com.kotlinspringvue.backend.controller.BackendController | backend/src/main/kotlin/com/kotlinspringvue/backend/controller/BackendController.kt |
| GET | /persons | getPersons | com.kotlinspringvue.backend.controller.BackendController | backend/src/main/kotlin/com/kotlinspringvue/backend/controller/BackendController.kt |
| GET | /usercontent | getUserContent | com.kotlinspringvue.backend.controller.BackendController | backend/src/main/kotlin/com/kotlinspringvue/backend/controller/BackendController.kt |
| GET | /admincontent | getAdminContent | com.kotlinspringvue.backend.controller.BackendController | backend/src/main/kotlin/com/kotlinspringvue/backend/controller/BackendController.kt |
| GET | /sendSimpleEmail | sendSimpleEmail | com.kotlinspringvue.backend.controller.BackendController | backend/src/main/kotlin/com/kotlinspringvue/backend/controller/BackendController.kt |
| GET | /sendTemplateEmail | sendTemplateEmail | com.kotlinspringvue.backend.controller.BackendController | backend/src/main/kotlin/com/kotlinspringvue/backend/controller/BackendController.kt |
| GET | /sendHtmlEmail | sendHtmlEmail | com.kotlinspringvue.backend.controller.BackendController | backend/src/main/kotlin/com/kotlinspringvue/backend/controller/BackendController.kt |

## Appendix D — JPA Entities

Kotlin classes annotated with `@Entity` / `@MappedSuperclass` or residing in `model/` / `entity/` source paths.

| Entity | Package | Source path |
| --- | --- | --- |
| Greeting |  | backend/src/main/kotlin/com/kotlinspringvue/backend/model/Greeting.kt |
| LoginUser |  | backend/src/main/kotlin/com/kotlinspringvue/backend/model/LoginUser.kt |
| NewUser |  | backend/src/main/kotlin/com/kotlinspringvue/backend/model/NewUser.kt |

## Appendix E — Maven Dependencies

JVM dependencies detected by the landscape router. Each row should be reviewed for upgrade implications during the Kotlin JVM-server modernization.

- `spring-boot-starter-actuator`
- `spring-boot-starter-web`
- `spring-boot-starter-data-jpa`
- `spring-boot-starter-mail`
- `spring-boot-starter-security`
- `postgresql`
- `spring-boot-starter-thymeleaf`
- `commons-io`

## Appendix — NPM Dependencies

Dependencies resolved from `package.json` + `package-lock.json` by the vue-comprehension skill:

| Package | Declared | Resolved | Scope |
| --- | --- | --- | --- |
| `vue` | `^2.6.11` | `2.6.14` | dependencies |
| `vue-router` | `^3.2.0` | `3.6.5` | dependencies |
| `vuex` | `^3.4.0` | `3.6.2` | dependencies |
| `bootstrap-vue` | `^2.21.2` | `2.23.1` | dependencies |
| `axios` | `^0.21.1` | `0.21.4` | dependencies |

# Kotlin to Go Modernization — Kotlin Modernization BRD

## 1. Executive Summary

**Source platform**: Kotlin Modernization Skill Pack  
**Primary language**: spring_boot  
**Build tool**: gradle  
**Project name**: realworld-spring-webflux-kt  
**Lines of code**: 4,217  
**Files scanned**: 66  
**Components**: 7  
**Gradle dependencies**: 0  
**Routes**: 19  
**Symbols indexed**: 253  
**Classes**: 8  
**Interfaces**: 6  
**Functions**: 188  
**Objects**: 2  

## 2. Module Topology

Kotlin applications are composed of Gradle modules — each module is a unit of build, testing, and deployment. The table below shows every module detected in the source tree, classified by architectural role.

| Module | Name | Type | Files | LOC | Stack |
|---|---|---|---|---|---|
| kotlin::domain | Domain and Model Layer | kotlin_domain | 19 | 597 | vb6 |
| kotlin::support | Shared Kotlin Support | kotlin_support | 18 | 1081 | vb6 |
| kotlin::controllers | Controllers and API Surface | kotlin_controllers | 5 | 368 | vb6 |
| kotlin::repositories | Repositories and Data Access | kotlin_repositories | 5 | 147 | vb6 |
| kotlin::config | Application and Configuration | kotlin_config | 4 | 85 | vb6 |
| kotlin::services | Service Layer | kotlin_services | 3 | 263 | vb6 |
| other::support | Support Artifacts | support | 1 | 1 | generic |

## 4. Domain Entities (Classes)

Classes extracted from the Kotlin source. These are the data structures and business objects that define the domain model — entities like `Account`, `Customer`, `Transaction` drive the persistence and API contracts of the modernized system.

| Class | Package | File | Line |
|---|---|---|---|
| SpringWebfluxKtApplication |  | src/main/kotlin/com/realworld/springmongo/SpringWebfluxKtApp… | 5 |
| Article |  | src/main/kotlin/com/realworld/springmongo/article/Article.kt | 9 |
| Comment |  | src/main/kotlin/com/realworld/springmongo/article/Comment.kt | 6 |
| InvalidRequestException |  | src/main/kotlin/com/realworld/springmongo/exceptions/Invalid… | 2 |
| OffsetBasedPageable |  | src/main/kotlin/com/realworld/springmongo/lib/OffsetBasedPag… | 5 |
| User |  | src/main/kotlin/com/realworld/springmongo/user/User.kt | 7 |
| NotBlankOrNull |  | src/main/kotlin/com/realworld/springmongo/validation/NotBlan… | 9 |
| NotBlankOrNullValidator |  | src/main/kotlin/com/realworld/springmongo/validation/NotBlan… | 18 |

## 5. Interfaces (API contracts)

Kotlin interfaces often define contracts at module boundaries — Retrofit API surfaces, repository contracts, DI module interfaces. Each interface is a seam where the target-platform equivalent must be selected during modernization.

| Interface | Package | File |
|---|---|---|
| ArticleManualRepository |  | src/main/kotlin/com/realworld/springmongo/article/repository… |
| ArticleRepository |  | src/main/kotlin/com/realworld/springmongo/article/repository… |
| EndpointsSecurityConfig | com.realworld.springmongo.security | src/main/kotlin/com/realworld/springmongo/security/SecurityC… |
| TagRepository |  | src/main/kotlin/com/realworld/springmongo/article/repository… |
| UserRepository |  | src/main/kotlin/com/realworld/springmongo/user/UserRepositor… |
| UserTokenProvider |  | src/main/kotlin/com/realworld/springmongo/user/UserTokenProv… |

## 6. Objects & Singletons

Kotlin `object` declarations (singletons, companion objects, DI components). These often hold global state or DI wiring — they need explicit handling during migration to ensure the target platform's DI model is respected.

| Object | Package | Kind | File |
|---|---|---|---|
| ArticleSamples |  | object | src/test/kotlin/helpers/ArticleSamples.kt |
| UserSamples |  | object | src/test/kotlin/helpers/UserSamples.kt |

## 7. Retrofit / HTTP Endpoints

Endpoints declared via Retrofit annotations (`@GET`, `@POST`, `@PUT`, `@DELETE`, `@PATCH`). Each row is an HTTP contract the app consumes — preserving these during migration is critical for wire-level parity with upstream services.

*No Retrofit endpoints detected. This codebase may not consume HTTP APIs, or annotations may not be captured by the extractor.*

## 8. Dependency Injection Modules

Hilt / Dagger DI modules (`@Module`, `@InstallIn`). Each module declares bindings that need an equivalent in the target-platform DI framework per DEC-KOTLIN-DI-001.

*No DI modules detected.*

## 9. Room Entities & DAOs

Room persistence: `@Entity` data classes and `@Dao` interfaces. Migration decisions around DEC-KOTLIN-DB-001 (persistence strategy) directly affect every row here.

*No Room entities or DAOs detected.*

## 10. API Surface — Routes (from java_route_inventory_v1)

HTTP routes exposed by controllers / Retrofit interfaces. These are the contract boundaries that must be preserved (or explicitly re-designed) during modernization.

| Method | Path | Handler | File |
|---|---|---|---|
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/ArticleControl… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/ArticleControl… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/ArticleControl… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/ArticleControl… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/ArticleControl… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/ArticleControl… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/ArticleControl… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/ArticleControl… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/ArticleControl… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/ArticleControl… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/ArticleControl… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/ArticleControl… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/UserController… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/UserController… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/UserController… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/UserController… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/UserController… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/UserController… |
|  |  |  | src/main/kotlin/com/realworld/springmongo/api/UserController… |

## 5. Business Flow & Calculations

Business logic extracted from Kotlin source — organized by module capability. Each flow documents its trigger, key steps, and data touchpoints.

*No data available.*

## 6. Business Rules

Business rules extracted from the Kotlin codebase. These must be preserved (or explicitly revised with stakeholder sign-off) during migration.

| ID | Type | Rule | Scope | Evidence |
|---|---|---|---|---|
| BR-001 | data_persistence | Articles must be stored in the database with a unique identifier. | Article Management | ArticleRepository.kt |
| BR-002 | input_validation | User input must be validated for registration and login. | User Management | UserController.kt |

## 7. Functional Requirements

### FR-001 — User Authentication and Authorization (P0)

Implement user authentication and authorization in Go, ensuring compatibility with existing JWT-based security.

**Acceptance Criteria:**
- User login and registration endpoints function as expected.
- JWT tokens are correctly generated and validated.
- Access control is enforced based on user roles.

### FR-002 — Article Management (P0)

Re-implement article management functionalities, including CRUD operations, in Go.

**Acceptance Criteria:**
- Users can create, read, update, and delete articles.
- Article data is correctly persisted and retrieved.
- API endpoints for articles match existing contracts.

### FR-003 — Comment Management (P1)

Migrate comment management features to Go, maintaining current functionality.

**Acceptance Criteria:**
- Users can add, edit, and delete comments on articles.
- Comments are correctly associated with articles.
- API endpoints for comments are consistent with existing ones.

### FR-004 — User Profile Management (P1)

Implement user profile management in Go, ensuring data integrity and consistency.

**Acceptance Criteria:**
- Users can view and update their profiles.
- Profile data is securely stored and retrieved.
- API contracts for user profiles remain unchanged.

### FR-005 — Persistent Storage Integration (P0)

Update persistent storage solutions to be compatible with Go.

**Acceptance Criteria:**
- Data is correctly stored and retrieved using the new storage solution.
- Migration scripts are provided for data transition.
- Performance benchmarks meet or exceed current levels.

## 8. Non-Functional Requirements

### NFR-001 — Performance Optimization (performance)

Ensure the Go application performs at least as well as the Kotlin version under load.

**Acceptance Criteria:**
- Performance tests show response times under 2 seconds.
- Load tests simulate peak user activity without degradation.
- Resource usage is optimized for Go.

### NFR-002 — Security Compliance (security)

Ensure the Go application adheres to security best practices.

**Acceptance Criteria:**
- Security scans show no critical vulnerabilities.
- All sensitive data is encrypted in transit and at rest.
- Access controls are verified and documented.

### NFR-003 — Scalability (scalability)

The application should scale to handle increased load without performance loss.

**Acceptance Criteria:**
- Scalability tests confirm handling of 1000 concurrent users.
- No significant increase in response time under load.
- Infrastructure supports horizontal scaling.

### NFR-004 — Observability and Monitoring (observability)

Implement observability features to monitor application health and performance.

**Acceptance Criteria:**
- All critical operations are logged.
- Monitoring dashboards provide real-time insights.
- Alerts are configured for critical failures.

### NFR-005 — Documentation Quality (usability)

Ensure all documentation is comprehensive and up-to-date.

**Acceptance Criteria:**
- Documentation is reviewed and approved by stakeholders.
- Guides are available for all major features.
- Documentation is accessible and easy to understand.

## 9. Blocking Decisions

Architectural decisions that must be resolved before migration can begin. Each choice has downstream impact on module extraction, API contracts, and delivery sequencing.

| ID | Question | Options | Impact if deferred |
|---|---|---|---|
| DEC-KOTLIN-TARGET-001 | Target platform for the modernized Kotlin codebase | Kotlin 2.x on JVM · Go domain service + retained Android client · Full client/backend decomposition · Language port (Kotlin → Java / TypeScript) | Every downstream decision (DI model, persistence, HTTP surface) depends on this choice. |
| DEC-KOTLIN-DI-001 | Dependency injection framework on the modernized stack | Retain Hilt/Dagger · Migrate to Koin · Replace with target-platform DI (Spring, Wire, etc.) · Manual constructor injection | Module wiring at scale is impractical without a DI strategy — legacy modules cannot be extracted cleanly. |
| DEC-KOTLIN-DB-001 | Persistence strategy for Kotlin modernization | Keep Room/SQLDelight (Android) · Keep Exposed/JPA (server) · Migrate to target-stack ORM · Hybrid — legacy keeps its DB, new modules use target stack | Database contracts drive every data-touching module — getting this wrong forces large rewrites. |
| DEC-KOTLIN-HTTP-001 | HTTP client / server replacement strategy | Keep Retrofit + OkHttp · Migrate to Ktor · Replace with target-platform HTTP library · Extract network layer to separate service | Network-layer decisions affect every API consumer and server endpoint. |
| DEC-KOTLIN-ASYNC-001 | Concurrency / async model on the target platform | Retain Kotlin coroutines · Port to target-platform async (CompletableFuture, async/await) · Replace with reactive streams · Redesign to synchronous + worker pool | Suspend functions and Flow pipelines have no automatic equivalent — every async boundary needs explicit decisions. |

## 10. Migration Strategy

Phased approach for modernizing the Kotlin application. Each phase has clear entry and exit criteria.

### Phase 0 — Dependency & Schema Freeze

Lock the Gradle dependency set (0 third-party libraries) and any persistence schemas. Document every external contract before migration begins — this becomes the parity baseline.

### Phase 1 — Module Extraction

Extract the 7 modules in dependency order: leaf modules first (no dependents), then inner modules. Each extraction includes a contract test proving no behavioral drift.

### Phase 2 — API Surface Migration

Migrate each of the 19 API routes behind a façade: old and new endpoints run side-by-side during cut-over, with shadow-traffic comparisons confirming parity.

### Phase 3 — Async & Coroutine Migration

Port suspend functions and Flow pipelines to the target concurrency model. Structural concurrency boundaries need explicit reasoning — not a mechanical rewrite.

### Phase 4 — Dependency Injection & Wiring

Replace Hilt/Dagger/Koin wiring per DEC-KOTLIN-DI-001. Modules should be constructor-injectable after this phase — no service-locator patterns.

### Phase 5 — Platform Primitives & Observability

Replace Kotlin-specific primitives (data classes, sealed classes, extension functions) with target-platform equivalents. Re-instrument logging, metrics, and tracing on the new stack.

## 11. Risks

| Risk | Impact | Mitigation |
|---|---|---|
| Potential data loss during migration to new storage solutions. | high | Implement thorough data migration testing and validation. |
| Security vulnerabilities during transition to new authentication mechanisms. | medium | Conduct security audits and implement best practices for Go. |
| Lack of familiarity with Go among current development team. | medium | Provide training and resources for Go development. |

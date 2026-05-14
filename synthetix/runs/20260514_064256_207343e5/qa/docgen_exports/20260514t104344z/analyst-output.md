# Chirp Backend Modernization — Kotlin Modernization BRD

## 1. Executive Summary

**Source platform**: Kotlin Modernization Skill Pack  
**Primary language**: —  
**Build tool**: —  
**Project name**: —  
**Lines of code**: —  
**Files scanned**: —  
**Components**: 8  
**Gradle dependencies**: —  
**Routes**: —  
**Symbols indexed**: 144  
**Classes**: 26  
**Interfaces**: 10  
**Functions**: —  
**Objects**: 5  

## 2. Module Topology

Kotlin applications are composed of Gradle modules — each module is a unit of build, testing, and deployment. The table below shows every module detected in the source tree, classified by architectural role.

| Module | Name | Type | Files | LOC | Stack |
|---|---|---|---|---|---|
| kotlin::user | user | kotlin_module | 55 | 1734 | kotlin, jvm |
| kotlin::chat | chat | kotlin_module | 51 | 1930 | kotlin, jvm |
| kotlin::notification | notification | kotlin_module | 21 | 826 | kotlin, jvm |
| kotlin::common | common | kotlin_module | 18 | 440 | kotlin, jvm |
| other::support | Support Artifacts | support | 7 | 220 | generic |
| kotlin::app | app | kotlin_module | 6 | 272 | kotlin, jvm |
| kotlin::build-logic | build-logic | kotlin_module | 5 | 95 | kotlin, jvm |
| kotlin::root | root | kotlin_module | 1 | 15 | kotlin, jvm |

## 4. Domain Entities (Classes)

Classes extracted from the Kotlin source. These are the data structures and business objects that define the domain model — entities like `Account`, `Customer`, `Transaction` drive the persistence and API contracts of the modernized system.

| Class | Package | File | Line |
|---|---|---|---|
| ChirpApplication |  | app/src/main/kotlin/com/plcoding/chirp/ChirpApplication.kt | 10 |
| ChatNotFoundException |  | chat/src/main/kotlin/com/plcoding/chirp/domain/exception/Cha… | 2 |
| ChatParticipantNotFoundException |  | chat/src/main/kotlin/com/plcoding/chirp/domain/exception/Cha… | 4 |
| InvalidChatSizeException |  | chat/src/main/kotlin/com/plcoding/chirp/domain/exception/Inv… | 2 |
| InvalidProfilePictureException |  | chat/src/main/kotlin/com/plcoding/chirp/domain/exception/Inv… | 2 |
| MessageNotFoundException |  | chat/src/main/kotlin/com/plcoding/chirp/domain/exception/Mes… | 4 |
| StorageException |  | chat/src/main/kotlin/com/plcoding/chirp/domain/exception/Sto… | 2 |
| ChatMessageEntity |  | chat/src/main/kotlin/com/plcoding/chirp/infra/database/entit… | 31 |
| ChatParticipantEntity |  | chat/src/main/kotlin/com/plcoding/chirp/infra/database/entit… | 21 |
| ForbiddenException |  | common/src/main/kotlin/com/plcoding/chirp/domain/exception/F… | 2 |
| InvalidTokenException |  | common/src/main/kotlin/com/plcoding/chirp/domain/exception/I… | 4 |
| UnauthorizedException |  | common/src/main/kotlin/com/plcoding/chirp/domain/exception/U… | 2 |
| InvalidDeviceTokenException |  | notification/src/main/kotlin/com/plcoding/chirp/domain/excep… | 2 |
| DeviceTokenEntity |  | notification/src/main/kotlin/com/plcoding/chirp/infra/databa… | 25 |
| IpRateLimit |  | user/src/main/kotlin/com/plcoding/chirp/api/config/IpRateLim… | 4 |
| Password |  | user/src/main/kotlin/com/plcoding/chirp/api/util/Password.kt | 15 |
| EmailNotVerifiedException |  | user/src/main/kotlin/com/plcoding/chirp/domain/exception/Ema… | 4 |
| InvalidCredentialsException |  | user/src/main/kotlin/com/plcoding/chirp/domain/exception/Inv… | 2 |
| RateLimitException |  | user/src/main/kotlin/com/plcoding/chirp/domain/exception/Rat… | 2 |
| SamePasswordException |  | user/src/main/kotlin/com/plcoding/chirp/domain/exception/Sam… | 2 |
| UserAlreadyExistsException |  | user/src/main/kotlin/com/plcoding/chirp/domain/exception/Use… | 4 |
| UserNotFoundException |  | user/src/main/kotlin/com/plcoding/chirp/domain/exception/Use… | 2 |
| EmailVerificationTokenEntity |  | user/src/main/kotlin/com/plcoding/chirp/infra/database/entit… | 26 |
| PasswordResetTokenEntity |  | user/src/main/kotlin/com/plcoding/chirp/infra/database/entit… | 25 |
| RefreshTokenEntity |  | user/src/main/kotlin/com/plcoding/chirp/infra/database/entit… | 23 |
| UserEntity |  | user/src/main/kotlin/com/plcoding/chirp/infra/database/entit… | 24 |

## 5. Interfaces (API contracts)

Kotlin interfaces often define contracts at module boundaries — Retrofit API surfaces, repository contracts, DI module interfaces. Each interface is a seam where the target-platform equivalent must be selected during modernization.

| Interface | Package | File |
|---|---|---|
| ApiKeyRepository |  | user/src/main/kotlin/com/plcoding/chirp/infra/database/repos… |
| ChatMessageRepository |  | chat/src/main/kotlin/com/plcoding/chirp/infra/database/repos… |
| ChatParticipantRepository |  | chat/src/main/kotlin/com/plcoding/chirp/infra/database/repos… |
| ChatRepository |  | chat/src/main/kotlin/com/plcoding/chirp/infra/database/repos… |
| ChirpEvent |  | common/src/main/kotlin/com/plcoding/chirp/domain/events/Chir… |
| DeviceTokenRepository |  | notification/src/main/kotlin/com/plcoding/chirp/infra/databa… |
| EmailVerificationTokenRepository |  | user/src/main/kotlin/com/plcoding/chirp/infra/database/repos… |
| PasswordResetTokenRepository |  | user/src/main/kotlin/com/plcoding/chirp/infra/database/repos… |
| RefreshTokenRepository |  | user/src/main/kotlin/com/plcoding/chirp/infra/database/repos… |
| UserRepository |  | user/src/main/kotlin/com/plcoding/chirp/infra/database/repos… |

## 6. Objects & Singletons

Kotlin `object` declarations (singletons, companion objects, DI components). These often hold global state or DI wiring — they need explicit handling during migration to ensure the target platform's DI model is respected.

| Object | Package | Kind | File |
|---|---|---|---|
| ChatEventConstants |  | object | common/src/main/kotlin/com/plcoding/chirp/domain/events/chat… |
| UserEventConstants |  | object | common/src/main/kotlin/com/plcoding/chirp/domain/events/user… |
| MessageQueues |  | object | common/src/main/kotlin/com/plcoding/chirp/infra/message_queu… |
| Companion |  | object | user/src/main/kotlin/com/plcoding/chirp/api/config/ApiKeyAut… |
| TokenGenerator |  | object | user/src/main/kotlin/com/plcoding/chirp/infra/security/Token… |

## 7. Retrofit / HTTP Endpoints

Endpoints declared via Retrofit annotations (`@GET`, `@POST`, `@PUT`, `@DELETE`, `@PATCH`). Each row is an HTTP contract the app consumes — preserving these during migration is critical for wire-level parity with upstream services.

*No Retrofit endpoints detected. This codebase may not consume HTTP APIs, or annotations may not be captured by the extractor.*

## 8. Dependency Injection Modules

Hilt / Dagger DI modules (`@Module`, `@InstallIn`). Each module declares bindings that need an equivalent in the target-platform DI framework per DEC-KOTLIN-DI-001.

*No DI modules detected.*

## 9. Room Entities & DAOs

Room persistence: `@Entity` data classes and `@Dao` interfaces. Migration decisions around DEC-KOTLIN-DB-001 (persistence strategy) directly affect every row here.

*No Room entities or DAOs detected.*

## 5. Business Flow & Calculations

Business logic extracted from Kotlin source — organized by module capability. Each flow documents its trigger, key steps, and data touchpoints.

*No data available.*

## 7. Functional Requirements

### FR-001 — Translate Kotlin Methods to Go (P0)

Use the MethodTranslatorSubAgent to convert all Kotlin methods to Go equivalents.

**Acceptance Criteria:**
- All Kotlin methods are successfully translated to Go.
- Translated methods pass all existing unit tests.
- No functional discrepancies between Kotlin and Go implementations.

### FR-002 — Verify Legacy Source File Population (P0)

Ensure all legacy source files are populated during the repo scan.

**Acceptance Criteria:**
- Repo scan identifies all legacy source files.
- All identified files are correctly populated.
- Verification logs confirm no missing files.

### FR-003 — Persist Architecture Output (P1)

Flatten and persist the architecture output as per the ARCH-V30 specification.

**Acceptance Criteria:**
- Architecture output is flattened according to specifications.
- Output is persisted in the designated storage.
- Verification checks confirm data integrity post-persistence.

### FR-004 — Backward Compatibility (P0)

Ensure the new Go implementation maintains backward compatibility with existing systems.

**Acceptance Criteria:**
- All existing APIs function as expected with the new backend.
- No changes are required in client applications.
- Legacy data formats are supported in the new system.

### FR-005 — Implement Error Handling (P1)

Define and implement error handling mechanisms in the Go backend.

**Acceptance Criteria:**
- All critical errors are logged with detailed information.
- Error handling follows defined patterns and practices.
- System recovers gracefully from expected error scenarios.

### FR-006 — Logging and Monitoring (P1)

Implement logging and monitoring for the Go backend to ensure observability.

**Acceptance Criteria:**
- All critical operations are logged.
- Monitoring dashboards reflect real-time system status.
- Alerts are configured for predefined thresholds.

### FR-007 — Data Migration Validation (P0)

Validate data migration from the Kotlin system to the Go system.

**Acceptance Criteria:**
- Data integrity is maintained post-migration.
- All data is accessible in the new system.
- Migration logs confirm successful data transfer.

### FR-008 — API Endpoint Consistency (P0)

Ensure API endpoints in the Go system are consistent with the legacy system.

**Acceptance Criteria:**
- All API endpoints are available in the Go system.
- Endpoint responses match legacy system outputs.
- API performance meets or exceeds legacy benchmarks.

## 8. Non-Functional Requirements

### NFR-AUTO-PERF-MIG — Migration Performance Preservation (performance)

Critical user flows across app, build-logic, chat, common must complete within 20% of pre-migration latency on the same Android device / API level combination throughout the phased Compose + StateFlow + Hilt migration. Regressions halt phase promotion until resolved.

**Acceptance Criteria:**
- Pre-migration p95 latency baseline captured on reference device matrix before cutover begins.
- Post-migration p95 measured per release candidate; threshold breach blocks phase promotion.
- Device-matrix evidence attached to the release checklist.

## 9. Blocking Decisions

Architectural decisions that must be resolved before migration can begin. Each choice has downstream impact on module extraction, API contracts, and delivery sequencing.

| ID | Question | Options | Impact if deferred |
|---|---|---|---|
| DEC-KOTLIN-TARGET-001 | Target platform for the modernized Kotlin codebase | Kotlin 2.x on JVM · Go domain service + retained Android client · Full client/backend decomposition · Language port (Kotlin → Java / TypeScript) | Every downstream decision (DI model, persistence, HTTP surface) depends on this choice. |
| DEC-KOTLIN-DI-001 | Dependency injection framework on the modernized stack | Retain Hilt/Dagger · Migrate to Koin · Replace with target-platform DI (Spring, Wire, etc.) · Manual constructor injection | Module wiring at scale is impractical without a DI strategy — legacy modules cannot be extracted cleanly. |
| DEC-KOTLIN-DB-001 | Persistence strategy for Kotlin modernization | Keep Room/SQLDelight (Android) · Keep Exposed/JPA (server) · Migrate to target-stack ORM · Hybrid — legacy keeps its DB, new modules use target stack | Database contracts drive every data-touching module — getting this wrong forces large rewrites. |
| DEC-KOTLIN-ASYNC-001 | Concurrency / async model on the target platform | Retain Kotlin coroutines · Port to target-platform async (CompletableFuture, async/await) · Replace with reactive streams · Redesign to synchronous + worker pool | Suspend functions and Flow pipelines have no automatic equivalent — every async boundary needs explicit decisions. |

## 10. Migration Strategy

Phased approach for modernizing the Kotlin application. Each phase has clear entry and exit criteria.

### Phase 0 — Dependency & Schema Freeze

Lock the Gradle dependency set ( third-party libraries) and any persistence schemas. Document every external contract before migration begins — this becomes the parity baseline.

### Phase 1 — Module Extraction

Extract the 8 modules in dependency order: leaf modules first (no dependents), then inner modules. Each extraction includes a contract test proving no behavioral drift.

### Phase 2 — API Surface Migration

Migrate each of the  API routes behind a façade: old and new endpoints run side-by-side during cut-over, with shadow-traffic comparisons confirming parity.

### Phase 3 — Async & Coroutine Migration

Port suspend functions and Flow pipelines to the target concurrency model. Structural concurrency boundaries need explicit reasoning — not a mechanical rewrite.

### Phase 4 — Dependency Injection & Wiring

Replace Hilt/Dagger/Koin wiring per DEC-KOTLIN-DI-001. Modules should be constructor-injectable after this phase — no service-locator patterns.

### Phase 5 — Platform Primitives & Observability

Replace Kotlin-specific primitives (data classes, sealed classes, extension functions) with target-platform equivalents. Re-instrument logging, metrics, and tracing on the new stack.

## 11. Risks

| Risk | Impact | Mitigation |
|---|---|---|
| Potential loss of functionality during translation. | high | Implement thorough testing and validation post-translation. |
| Performance degradation in the new Go system. | medium | Conduct performance benchmarking and optimization. |
| Security vulnerabilities in the new system. | medium | Perform security audits and penetration testing. |

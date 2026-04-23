# Kotlin / Android Modernization BRD

## Header
- Track: Reimagine — Kotlin Upgrade
- Source language: Kotlin (Android target)
- Objective: Upgrade Kotlin application to Kotlin application (upgraded) while preserving functional parity.
- Repository: Synthetix @ codex-synthetix-ui-v2-claude (90f3b99349e5)
- Generated at: 2026-04-23T12:16:49.240705+00:00

## Decision Brief

| Category | Summary |
| --- | --- |
| Modernization readiness | n/a/100 |
| Risk tier | medium |
| Inventory | 2 Kotlin/Android modules across 58 files (11611 LOC) |
| Headline | Kotlin / Android upgrade recommended (Compose UI, Hilt DI direction, SDK 34 target). |

## Current State

| Aspect | Detected value |
| --- | --- |
| Platform | Android — Kotlin/JVM |
| Primary language LOC mix | Kotlin 6.3% / XML ?% (XML = layouts + manifests + resources) |
| Architecture pattern | MVVM (ViewModel + Repository + DI) |
| Dependency injection | (DI framework not detected) |
| Networking | (networking stack not detected) |
| Concurrency | (coroutines not detected) |
| UI toolkit | XML layouts (legacy view system) |
| Build system | Gradle |

## Target State

Modernization recommendations from the analyst track plan:

- **backend Kotlin server modernization** → Kotlin 2.x / Spring Boot 3 or Ktor 3 / Coroutines 1.8+
- **frontend component assessment** → Target to be confirmed during Define Scope
- **root Kotlin server modernization** → Kotlin 2.x / Spring Boot 3 or Ktor 3 / Coroutines 1.8+
- **frontend web modernization** → Modern web stack / API + UI split

## Recommended Strategy

- **backend Kotlin server modernization**. Detected as kotlin_server_project with archetypes: none.
- **frontend component assessment**. Detected as java_project but no specific routing rule fired yet.
- **root Kotlin server modernization**. Detected as kotlin_server_project with archetypes: none.
- **frontend web modernization**. Detected as node_app with archetypes: none.

### Open Questions

- Confirm target framework: stay on Spring Boot / Ktor / Micronaut or migrate.
- Confirm structured concurrency strategy (coroutines vs Project Loom).
- Confirm persistence stack: JPA vs Exposed vs SQLDelight vs raw JDBC.
- Confirm the intended modernization lane for this component.
- Confirm runtime and package manager.
- Confirm whether this component is customer-facing UI or internal tooling.
- Are there existing operational constraints or integration dependencies not listed?
- What are target latency, throughput, and availability SLOs?
- Confirm DI framework direction: Hilt migration, Koin rewrite, or stay on the existing DI stack.
- Confirm minimum/target SDK levels (minSdk, targetSdk, compileSdk) and the Kotlin / AGP version baseline to upgrade to.
- Confirm Compose migration scope: full rewrite vs. incremental Compose-in-View interop.
- Confirm which modules are customer-facing (app), shared library, or build infrastructure.

## Module Details



### MOD-001 — Root

**Type**: Build configuration module
**Component ID**: `kotlin::root`
**Files**: 25 | **LOC**: ?

**Narrative Overview**

Root is the project / build configuration module. It contains Gradle build files only.

**Business Purpose**

Root is the build configuration module — Gradle settings only, no runtime behaviour.

**Process Flow** (Activity/Fragment → ViewModel → Repository → coroutine → UI)

_No screen lifecycle — module contains build configuration or test stubs only._

**Features**

- See consuming modules for behaviour

**Field Definitions** (entity data classes)

_No entity data classes detected in this module's symbol scope._

**Display Requirements**

_No Activity/Fragment screens detected for this module._

**Acceptance Criteria** (Kotlin / Android upgrade)

- Root module compiles against target SDK 34 with Kotlin 2.x
- Root module passes existing JUnit tests after dependency upgrades
- DI graph for Root module resolves cleanly under the chosen DI direction (Hilt or Koin)

### MOD-002 — Support Artifacts

**Type**: Kotlin / Android module
**Component ID**: `other::support`
**Files**: 13 | **LOC**: ?

**Narrative Overview**

Support Artifacts is a Kotlin / Android module within the Sweet Bank app. See the Activity/Fragment Inventory and ViewModel Binding Map appendices for cross-module behaviour.

**Business Purpose**

Support Artifacts is a Kotlin / Android module within the Sweet Bank app.

**Process Flow** (Activity/Fragment → ViewModel → Repository → coroutine → UI)

1. Module exposes data classes + helpers consumed by upstream UI/Repository modules
2. No standalone screen lifecycle — see consuming modules' process flows

**Features**

- See consuming modules for behaviour

**Field Definitions** (entity data classes)

_No entity data classes detected in this module's symbol scope._

**Display Requirements**

_No Activity/Fragment screens detected for this module._

**Acceptance Criteria** (Kotlin / Android upgrade)

- Support Artifacts module compiles against target SDK 34 with Kotlin 2.x
- Support Artifacts module passes existing JUnit tests after dependency upgrades
- DI graph for Support Artifacts module resolves cleanly under the chosen DI direction (Hilt or Koin)

## Appendix D — Data Entities

Kotlin data classes detected in `entities/` / `model/` source paths. Field-level extraction requires the kotlin-comprehension skill pack.

| Entity | Package | Source path |
| --- | --- | --- |
| LoginUser |  | backend/src/main/kotlin/com/kotlinspringvue/backend/model/LoginUser.kt |
| NewUser |  | backend/src/main/kotlin/com/kotlinspringvue/backend/model/NewUser.kt |

## Appendix E — Gradle Dependencies

JVM dependencies detected by the landscape router. Each row should be reviewed for upgrade implications during the Kotlin / Android modernization.

- `spring-boot-starter-actuator`
- `spring-boot-starter-web`
- `spring-boot-starter-data-jpa`
- `spring-boot-starter-mail`
- `spring-boot-starter-security`
- `postgresql`
- `spring-boot-starter-thymeleaf`
- `commons-io`

## QA Validation Summary

| Gate | Status | Detail |
| --- | --- | --- |
| gherkin_syntax | PASS | BDD syntax validation for Feature/Scenario/Given/When/Then. |
| requirements_completeness | FAIL | Checks minimum requirement volume, scenario coverage, and capability mapping presence. |
| compliance_constraints_applied | WARN | Verifies that regulatory/software controls are linked to requirements when applicable. |
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

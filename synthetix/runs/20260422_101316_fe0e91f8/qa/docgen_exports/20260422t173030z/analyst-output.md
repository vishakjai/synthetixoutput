# Kotlin / Android Modernization BRD

## Header
- Track: Reimagine — Kotlin Upgrade
- Source language: Kotlin (Android target)
- Objective: Migrate Kotlin to GO
- Repository: Synthetix @ codex-synthetix-ui-v2-claude (36526655edbf)
- Generated at: 2026-04-22T16:08:26.460136+00:00

## Decision Brief

| Category | Summary |
| --- | --- |
| Modernization readiness | n/a/100 |
| Risk tier | medium |
| Inventory | 9 Kotlin/Android modules across 227 files (4950 LOC) |
| Headline | Kotlin / Android upgrade recommended (Compose UI, Hilt DI direction, SDK 34 target). |

## Current State

| Aspect | Detected value |
| --- | --- |
| Platform | Android — Kotlin/JVM |
| Primary language LOC mix | Kotlin 42.0% / XML 49.9% (XML = layouts + manifests + resources) |
| Architecture pattern | MVVM (ViewModel + Repository + DI) |
| Dependency injection | Koin |
| Networking | Moshi |
| Concurrency | Kotlin Coroutines |
| UI toolkit | XML layouts (legacy view system) |
| Build system | Gradle |

## Target State

Modernization recommendations from the analyst track plan:

- **account Kotlin/Android modernization** → Android SDK 34 / Jetpack Compose / Hilt / Coroutines 1.8+
- **app Kotlin/Android modernization** → Android SDK 34 / Jetpack Compose / Hilt / Coroutines 1.8+
- **balance Kotlin/Android modernization** → Android SDK 34 / Jetpack Compose / Hilt / Coroutines 1.8+
- **root Kotlin/Android modernization** → Android SDK 34 / Jetpack Compose / Hilt / Coroutines 1.8+
- **dashboard Kotlin/Android modernization** → Android SDK 34 / Jetpack Compose / Hilt / Coroutines 1.8+
- **network Kotlin/Android modernization** → Android SDK 34 / Jetpack Compose / Hilt / Coroutines 1.8+

## Recommended Strategy

- **account Kotlin/Android modernization**. Detected as kotlin_android_project with archetypes: none.
- **app Kotlin/Android modernization**. Detected as kotlin_android_project with archetypes: none.
- **balance Kotlin/Android modernization**. Detected as kotlin_android_project with archetypes: none.
- **root Kotlin/Android modernization**. Detected as kotlin_android_project with archetypes: none.
- **dashboard Kotlin/Android modernization**. Detected as kotlin_android_project with archetypes: none.
- **network Kotlin/Android modernization**. Detected as kotlin_android_project with archetypes: none.
- **payees Kotlin/Android modernization**. Detected as kotlin_android_project with archetypes: none.
- **resources Kotlin/Android modernization**. Detected as kotlin_android_project with archetypes: none.

### Open Questions

- Confirm which modules are customer-facing (app) vs library vs shared.
- Confirm minimum/target SDK levels and AGP/Kotlin versions to upgrade to.
- Confirm dead/unlinked modules (e.g. modules on disk not listed in settings.gradle).
- Confirm DI framework direction (Hilt migration, Koin rewrite, or stay).
- Are there existing operational constraints or integration dependencies not listed?
- What are target latency, throughput, and availability SLOs?
- Confirm DI framework direction: Hilt migration, Koin rewrite, or stay on the existing DI stack.
- Confirm minimum/target SDK levels (minSdk, targetSdk, compileSdk) and the Kotlin / AGP version baseline to upgrade to.
- Confirm Compose migration scope: full rewrite vs. incremental Compose-in-View interop.
- Confirm which modules are customer-facing (app), shared library, or build infrastructure.
- Confirm scope decision for Balance module — flagged as `dead_module_not_in_settings` (directory exists with no main source). Scope in as placeholder, scope out, or investigate.

## Module Details



### MOD-001 — Root

**Type**: Build configuration module
**Component ID**: `kotlin::root`
**Files**: 1 | **LOC**: ?

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

### MOD-002 — Account

**Type**: Kotlin / Android module
**Component ID**: `kotlin::account`
**Files**: 20 | **LOC**: ?

**Narrative Overview**

Account is a Kotlin / Android module within the Sweet Bank app. See the Activity/Fragment Inventory and ViewModel Binding Map appendices for cross-module behaviour.

**Business Purpose**

Account is a Kotlin / Android module within the Sweet Bank app.

**Process Flow** (Activity/Fragment → ViewModel → Repository → coroutine → UI)

1. Module exposes data classes + helpers consumed by upstream UI/Repository modules
2. No standalone screen lifecycle — see consuming modules' process flows

**Features**

- Data fetch via `AccountRepository`
- Data fetch via `UserRepository`

**Field Definitions** (entity data classes)

| Entity | Source path | Container |
| --- | --- | --- |
| AcceptedOverdraft | account/src/main/java/com/davidm/account/entities/AcceptedOverdraft.kt | com.davidm.account.entities |
| Account | account/src/main/java/com/davidm/account/entities/Account.kt | com.davidm.account.entities |
| AccountBalance | account/src/main/java/com/davidm/account/entities/AccountBalance.kt | com.davidm.account.entities |
| AccountHolder | account/src/main/java/com/davidm/account/entities/AccountHolder.kt | com.davidm.account.entities |
| Accounts | account/src/main/java/com/davidm/account/entities/Accounts.kt | com.davidm.account.entities |
| Amount | account/src/main/java/com/davidm/account/entities/Amount.kt | com.davidm.account.entities |
| ClearedBalance | account/src/main/java/com/davidm/account/entities/ClearedBalance.kt | com.davidm.account.entities |
| EffectiveBalance | account/src/main/java/com/davidm/account/entities/EffectiveBalance.kt | com.davidm.account.entities |
| PendingTransactions | account/src/main/java/com/davidm/account/entities/PendingTransactions.kt | com.davidm.account.entities |
| User | account/src/main/java/com/davidm/account/entities/User.kt | com.davidm.account.entities |

**Display Requirements**

_No Activity/Fragment screens detected for this module._

**Acceptance Criteria** (Kotlin / Android upgrade)

- Account module compiles against target SDK 34 with Kotlin 2.x
- Account module passes existing JUnit tests after dependency upgrades
- DI graph for Account module resolves cleanly under the chosen DI direction (Hilt or Koin)

### MOD-003 — App

**Type**: Kotlin / Android module
**Component ID**: `kotlin::app`
**Files**: 7 | **LOC**: ?

**Narrative Overview**

App is a Kotlin / Android module within the Sweet Bank app. See the Activity/Fragment Inventory and ViewModel Binding Map appendices for cross-module behaviour.

**Business Purpose**

App is a Kotlin / Android module within the Sweet Bank app.

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

- App module compiles against target SDK 34 with Kotlin 2.x
- App module passes existing JUnit tests after dependency upgrades
- DI graph for App module resolves cleanly under the chosen DI direction (Hilt or Koin)

### MOD-004 — Balance

**Type**: Kotlin / Android module
**Component ID**: `kotlin::balance`
**Files**: 4 | **LOC**: ?

**Narrative Overview**

Balance is a Kotlin / Android module within the Sweet Bank app. See the Activity/Fragment Inventory and ViewModel Binding Map appendices for cross-module behaviour.

**Business Purpose**

Balance is a Kotlin / Android module within the Sweet Bank app.

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

- Balance module compiles against target SDK 34 with Kotlin 2.x
- Balance module passes existing JUnit tests after dependency upgrades
- DI graph for Balance module resolves cleanly under the chosen DI direction (Hilt or Koin)

### MOD-005 — Dashboard

**Type**: Kotlin / Android module
**Component ID**: `kotlin::dashboard`
**Files**: 21 | **LOC**: ?

**Narrative Overview**

Dashboard is a Kotlin / Android module within the Sweet Bank app. See the Activity/Fragment Inventory and ViewModel Binding Map appendices for cross-module behaviour.

**Business Purpose**

Dashboard is a Kotlin / Android module within the Sweet Bank app.

**Process Flow** (Activity/Fragment → ViewModel → Repository → coroutine → UI)

1. User launches `HomepageActivity`
2. `HomepageActivity` instantiates `DashboardViewModel` via DI; `init {}` triggers data load
3. `DashboardViewModel` invokes `TransactionsRepository.fetch...()` inside `viewModelScope.launch {}`
4. `TransactionsRepository` calls `TransactionsApi` (Retrofit interface) over the Starling Bank API
5. Result emitted via `LiveData` / `StateFlow`; `HomepageActivity` observes and renders state, errors propagate to a snackbar/error UI

**Features**

- `getPurchases()` — exposed by ViewModel in `com.davidm.ui.DashboardViewModel`
- `getUserInfo()` — exposed by ViewModel in `com.davidm.ui.DashboardViewModel`
- `getAccountBalance()` — exposed by ViewModel in `com.davidm.ui.DashboardViewModel`
- `getAccountHolderID()` — exposed by ViewModel in `com.davidm.ui.DashboardViewModel`
- `getProfilePicture()` — exposed by ViewModel in `com.davidm.ui.DashboardViewModel`
- `uploadProfilePicture()` — exposed by ViewModel in `com.davidm.ui.DashboardViewModel`
- `updateView()` — exposed by ViewModel in `com.davidm.ui.DashboardViewModel`

**Field Definitions** (entity data classes)

| Entity | Source path | Container |
| --- | --- | --- |
| DateInterval | dashboard/src/main/java/com/davidm/entities/DateInterval.kt | com.davidm.entities |
| Purchases | dashboard/src/main/java/com/davidm/entities/StarlingTransaction.kt | com.davidm.entities |
| StarlingTransaction | dashboard/src/main/java/com/davidm/entities/StarlingTransaction.kt | com.davidm.entities |

**Display Requirements**

- `HomepageActivity` (dashboard/src/main/java/com/davidm/ui/HomepageActivity.kt)
- `DashboardFragment` (dashboard/src/main/java/com/davidm/ui/DashboardFragment.kt)

**Acceptance Criteria** (Kotlin / Android upgrade)

- Dashboard module compiles against target SDK 34 with Kotlin 2.x
- Dashboard module passes existing JUnit tests after dependency upgrades
- DI graph for Dashboard module resolves cleanly under the chosen DI direction (Hilt or Koin)

### MOD-006 — Network

**Type**: Kotlin / Android module
**Component ID**: `kotlin::network`
**Files**: 5 | **LOC**: ?

**Narrative Overview**

Network is a Kotlin / Android module within the Sweet Bank app. See the Activity/Fragment Inventory and ViewModel Binding Map appendices for cross-module behaviour.

**Business Purpose**

Network is a Kotlin / Android module within the Sweet Bank app.

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

- Network module compiles against target SDK 34 with Kotlin 2.x
- Network module passes existing JUnit tests after dependency upgrades
- DI graph for Network module resolves cleanly under the chosen DI direction (Hilt or Koin)

### MOD-007 — Payees

**Type**: Kotlin / Android module
**Component ID**: `kotlin::payees`
**Files**: 29 | **LOC**: ?

**Narrative Overview**

Payees is a Kotlin / Android module within the Sweet Bank app. See the Activity/Fragment Inventory and ViewModel Binding Map appendices for cross-module behaviour.

**Business Purpose**

Payees is a Kotlin / Android module within the Sweet Bank app.

**Process Flow** (Activity/Fragment → ViewModel → Repository → coroutine → UI)

1. User launches `PayeeProfileActivity`
2. `PayeeProfileActivity` instantiates `PayeesViewModel` via DI; `init {}` triggers data load
3. `PayeesViewModel` invokes `PayeesRepository.fetch...()` inside `viewModelScope.launch {}`
4. `PayeesRepository` calls `PayeesApi` (Retrofit interface) over the Starling Bank API
5. Result emitted via `LiveData` / `StateFlow`; `PayeeProfileActivity` observes and renders state, errors propagate to a snackbar/error UI

**Features**

- `getPayees()` — exposed by ViewModel in `com.davidm.payees.ui.PayeesViewModel`
- `createPayee()` — exposed by ViewModel in `com.davidm.payees.ui.PayeesViewModel`

**Field Definitions** (entity data classes)

| Entity | Source path | Container |
| --- | --- | --- |
| Payee | payees/src/main/java/com/davidm/payees/entities/Payee.kt | com.davidm.payees.entities |
| Payees | payees/src/main/java/com/davidm/payees/entities/Payee.kt | com.davidm.payees.entities |
| PayeeAccount | payees/src/main/java/com/davidm/payees/entities/PayeeAccount.kt | com.davidm.payees.entities |
| PayeeCreationResponse | payees/src/main/java/com/davidm/payees/entities/PayeeCreationResponse.kt | com.davidm.payees.entities |
| ConsentInformation | payees/src/main/java/com/davidm/payees/entities/PayeeCreationResponse.kt | com.davidm.payees.entities |
| ErrorMessage | payees/src/main/java/com/davidm/payees/entities/PayeeCreationResponse.kt | com.davidm.payees.entities |

**Display Requirements**

- `PayeeProfileActivity` (payees/src/main/java/com/davidm/payees/ui/PayeeProfileActivity.kt)
- `PayeeCreationFragment` (payees/src/main/java/com/davidm/payees/ui/PayeeCreationFragment.kt)
- `PayeesFragment` (payees/src/main/java/com/davidm/payees/ui/PayeesFragment.kt)

**Acceptance Criteria** (Kotlin / Android upgrade)

- Payees module compiles against target SDK 34 with Kotlin 2.x
- Payees module passes existing JUnit tests after dependency upgrades
- DI graph for Payees module resolves cleanly under the chosen DI direction (Hilt or Koin)

### MOD-008 — Resources

**Type**: Shared UI component module
**Component ID**: `kotlin::resources`
**Files**: 3 | **LOC**: ?

**Narrative Overview**

Resources is a Kotlin / Android module within the Sweet Bank app. See the Activity/Fragment Inventory and ViewModel Binding Map appendices for cross-module behaviour.

**Business Purpose**

Resources is a Kotlin / Android module within the Sweet Bank app.

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

- Resources module compiles against target SDK 34 with Kotlin 2.x
- Resources module passes existing JUnit tests after dependency upgrades
- DI graph for Resources module resolves cleanly under the chosen DI direction (Hilt or Koin)

### MOD-009 — Support Artifacts

**Type**: Kotlin / Android module
**Component ID**: `other::support`
**Files**: 73 | **LOC**: ?

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

## Appendix A — Activity / Fragment Inventory

Replaces the legacy Java/PHP Controller-to-Model appendix. Each row is a Kotlin Activity or Fragment detected in the symbol index.

| Kind | Class | Source path | Package |
| --- | --- | --- | --- |
| Fragment | DashboardFragment | dashboard/src/main/java/com/davidm/ui/DashboardFragment.kt | com.davidm.ui |
| Activity | HomepageActivity | dashboard/src/main/java/com/davidm/ui/HomepageActivity.kt | com.davidm.ui |
| Fragment | PayeeCreationFragment | payees/src/main/java/com/davidm/payees/ui/PayeeCreationFragment.kt | com.davidm.payees.ui |
| Activity | PayeeProfileActivity | payees/src/main/java/com/davidm/payees/ui/PayeeProfileActivity.kt | com.davidm.payees.ui |
| Fragment | PayeesFragment | payees/src/main/java/com/davidm/payees/ui/PayeesFragment.kt | com.davidm.payees.ui |

## Appendix B — ViewModel Binding Map

Replaces the legacy Java/PHP Controller-to-View appendix. Each row links a Kotlin ViewModel to the Activities/Fragments that observe it (best-effort — derived from package proximity).

| ViewModel | Package | Likely consumers |
| --- | --- | --- |
| DashboardViewModel | com.davidm.ui | DashboardFragment, HomepageActivity |
| PayeesViewModel | com.davidm.payees.ui | PayeeCreationFragment, PayeeProfileActivity, PayeesFragment |

## Appendix C — External API Surface (consumed)

Replaces the legacy 'REST endpoints exposed' table. Mobile apps **consume** APIs rather than exposing them. Each row is a Retrofit interface (or API client class) detected in the symbol index.

| Interface / Class | Package | Source path |
| --- | --- | --- |
| AccountApi | com.davidm.account.network | account/src/main/java/com/davidm/account/network/AccountApi.kt |
| UserApi | com.davidm.account.network | account/src/main/java/com/davidm/account/network/UserApi.kt |
| TransactionsApi | com.davidm.network | dashboard/src/main/java/com/davidm/network/TransactionsApi.kt |
| PayeesApi | com.davidm.payees.network | payees/src/main/java/com/davidm/payees/network/PayeesApi.kt |

## Appendix D — Data Entities

Kotlin data classes detected in `entities/` / `model/` source paths. Field-level extraction requires the kotlin-comprehension skill pack.

| Entity | Package | Source path |
| --- | --- | --- |
| AcceptedOverdraft | com.davidm.account.entities | account/src/main/java/com/davidm/account/entities/AcceptedOverdraft.kt |
| Account | com.davidm.account.entities | account/src/main/java/com/davidm/account/entities/Account.kt |
| AccountBalance | com.davidm.account.entities | account/src/main/java/com/davidm/account/entities/AccountBalance.kt |
| AccountHolder | com.davidm.account.entities | account/src/main/java/com/davidm/account/entities/AccountHolder.kt |
| Accounts | com.davidm.account.entities | account/src/main/java/com/davidm/account/entities/Accounts.kt |
| Amount | com.davidm.account.entities | account/src/main/java/com/davidm/account/entities/Amount.kt |
| ClearedBalance | com.davidm.account.entities | account/src/main/java/com/davidm/account/entities/ClearedBalance.kt |
| EffectiveBalance | com.davidm.account.entities | account/src/main/java/com/davidm/account/entities/EffectiveBalance.kt |
| PendingTransactions | com.davidm.account.entities | account/src/main/java/com/davidm/account/entities/PendingTransactions.kt |
| User | com.davidm.account.entities | account/src/main/java/com/davidm/account/entities/User.kt |
| DateInterval | com.davidm.entities | dashboard/src/main/java/com/davidm/entities/DateInterval.kt |
| Purchases | com.davidm.entities | dashboard/src/main/java/com/davidm/entities/StarlingTransaction.kt |
| StarlingTransaction | com.davidm.entities | dashboard/src/main/java/com/davidm/entities/StarlingTransaction.kt |
| Payee | com.davidm.payees.entities | payees/src/main/java/com/davidm/payees/entities/Payee.kt |
| Payees | com.davidm.payees.entities | payees/src/main/java/com/davidm/payees/entities/Payee.kt |
| PayeeAccount | com.davidm.payees.entities | payees/src/main/java/com/davidm/payees/entities/PayeeAccount.kt |
| PayeeCreationResponse | com.davidm.payees.entities | payees/src/main/java/com/davidm/payees/entities/PayeeCreationResponse.kt |
| ConsentInformation | com.davidm.payees.entities | payees/src/main/java/com/davidm/payees/entities/PayeeCreationResponse.kt |
| ErrorMessage | com.davidm.payees.entities | payees/src/main/java/com/davidm/payees/entities/PayeeCreationResponse.kt |

## Appendix E — Gradle Dependencies

JVM dependencies detected by the landscape router. Each row should be reviewed for upgrade implications during the Kotlin / Android modernization.

- `kotlin-stdlib-jdk7`
- `core-ktx`
- `kotlinx-coroutines-core`
- `kotlinx-coroutines-android`
- `moshi`
- `moshi-kotlin`
- `converter-moshi`
- `koin-android`

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

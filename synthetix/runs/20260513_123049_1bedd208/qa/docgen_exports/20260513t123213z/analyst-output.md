# Legacy Modernization Scope — VB6 Modernization BRD

## 1. Executive Summary

**Application**: Legacy Modernization Scope  
**Source platform**: Visual Basic 6.0  
**Lines of code**: 0  
**Active forms**: 0  
**Standard modules (.bas)**: 0  
**Database files**: 0  
**Event handlers**: 0  
**Global variables**: 0  

**Modernization readiness**: 60/100  

## 2. Application Inventory

### 2.1 Forms

*No form dossiers produced by the pipeline.*

### 2.2 Standard Modules (.bas)

*No standard modules detected.*

### 2.3 Database Files

*No MDB/ACCDB database files detected.*

## 3. Event Flow & Business Logic

**Total event handlers**: 0  
**Handlers with SQL side effects**: 0  
**Handlers with navigation side effects**: 0  
**Handlers with risk flags**: 0  

## 4. Data Layer

## 4a. Functional Requirements

### FR-AUTO-001 — Implement Identity workflow contract (P1)

Implement deterministic workflow behavior for Identity with explicit input/output and error contracts.

**Acceptance criteria:**
- FR-AUTO-001: Inputs are validated against contract before processing.
- FR-AUTO-001: Outputs include deterministic status and correlation identifiers.
- FR-AUTO-001: Error paths are captured with actionable diagnostics.

### FR-AUTO-002 — Extend Identity workflow — phase 2 (P1)

Extend the Identity workflow with additional coverage scenarios (phase 2).

**Acceptance criteria:**
- FR-AUTO-002: Inputs are validated against contract before processing.
- FR-AUTO-002: Outputs include deterministic status and correlation identifiers.
- FR-AUTO-002: Error paths are captured with actionable diagnostics.

### FR-AUTO-003 — Extend Identity workflow — phase 3 (P1)

Extend the Identity workflow with additional coverage scenarios (phase 3).

**Acceptance criteria:**
- FR-AUTO-003: Inputs are validated against contract before processing.
- FR-AUTO-003: Outputs include deterministic status and correlation identifiers.
- FR-AUTO-003: Error paths are captured with actionable diagnostics.

### FR-AUTO-004 — Extend Identity workflow — phase 4 (P1)

Extend the Identity workflow with additional coverage scenarios (phase 4).

**Acceptance criteria:**
- FR-AUTO-004: Inputs are validated against contract before processing.
- FR-AUTO-004: Outputs include deterministic status and correlation identifiers.
- FR-AUTO-004: Error paths are captured with actionable diagnostics.

### FR-AUTO-005 — Extend Identity workflow — phase 5 (P1)

Extend the Identity workflow with additional coverage scenarios (phase 5).

**Acceptance criteria:**
- FR-AUTO-005: Inputs are validated against contract before processing.
- FR-AUTO-005: Outputs include deterministic status and correlation identifiers.
- FR-AUTO-005: Error paths are captured with actionable diagnostics.

## 4b. Non-Functional Requirements

| ID | Title | Category | Acceptance criteria |
|---|---|---|---|
| NFR-AUTO-001 | Auditability | security | NFR-AUTO-001: Metric is continuously measured in CI/CD or runtime dashboards.; NFR-AUTO-001: Threshold breach triggers alerting and remediation workflow.; NFR-AUTO-001: Compliance evidence is retained for release review. |
| NFR-AUTO-002 | Observability | reliability | NFR-AUTO-002: Metric is continuously measured in CI/CD or runtime dashboards.; NFR-AUTO-002: Threshold breach triggers alerting and remediation workflow.; NFR-AUTO-002: Compliance evidence is retained for release review. |
| NFR-AUTO-003 | Performance parity | performance | NFR-AUTO-003: Metric is continuously measured in CI/CD or runtime dashboards.; NFR-AUTO-003: Threshold breach triggers alerting and remediation workflow.; NFR-AUTO-003: Compliance evidence is retained for release review. |
| NFR-AUTO-004 | Operational reliability | reliability | NFR-AUTO-004: Metric is continuously measured in CI/CD or runtime dashboards.; NFR-AUTO-004: Threshold breach triggers alerting and remediation workflow.; NFR-AUTO-004: Compliance evidence is retained for release review. |

## 4c. Risks

| Risk | Impact | Mitigation |
|---|---|---|
| The primary Analyst LLM response could not be parsed into valid JSON, so Synthetix compiled the requirements pack from d | medium | Review the generated artifacts and rerun with a stricter output contract if additional narrative depth is required. |

## 5. Blocking Decisions

| ID | Decision | Options | Impact if deferred |
|---|---|---|---|
| DEC-VB6-UI-001 | Target UI framework for modernized application | WinForms (.NET) · WPF · Blazor · Web (React/Angular) | Form migration strategy, event-handler mapping, and control replacement all depend on this choice. |
| DEC-VB6-DATA-001 | Data access strategy replacing DAO/ADO recordsets | Keep ADO.NET · Migrate to Entity Framework Core · Hybrid (ADO.NET for legacy, EF Core for new) | Every form with SQL touchpoints is affected. Wrong choice means rework on recordset-heavy forms. |

## 6. Migration Strategy

### Phase 0 — Database Contract Lock
Freeze schema for 0 database file(s). Document all connection string variants. Establish a migration-safe baseline before touching any forms.

### Phase 1 — Data-Entry Form Migration (0 forms)
Migrate data-entry forms first — they have the highest business value and the most SQL touchpoints. Each form migrates as a unit: UI + event handlers + recordset operations.

### Phase 3 — Navigation Shell & Authentication (0 navigation forms)
Replace the MDI shell and navigation forms. If a login form exists, implement the identity model per DEC-VB6-IAM-001.

### Phase 4 — Shared Module Consolidation (0 .bas modules)
Consolidate global state from shared modules into proper dependency injection. Eliminate public variables and replace with configuration/service abstractions.

## 7. Quality Gates

| Gate | Metric | Value | Status |
|---|---|---|---|
| Form dossier coverage | Forms with coverage > 50% | 0/0 | WARN |
| Event handler coverage | Handlers with procedure summaries | 0/0 | WARN |
| SQL injection risk | Handlers with injection_risk flag | 0 | PASS |
| Dead form references | Unresolved form references | 0 | PASS |
| Orphan forms | Unused forms requiring disposition | 0 | PASS |
| Backlog completeness | Remediation items vs discovered scope | 0 items | WARN |

## 8. Remediation Backlog

| ID | Priority | Item | Acceptance criteria |
|---|---|---|---|
| RM-001 | P1 | No specific remediations identified | Review form dossiers for coverage gaps. |

## 9. Appendices

### Appendix A — Form Inventory

*No form LOC profile available.*

### Appendix B — Event Map

*No event map available.*

### Appendix C — Recordset Operations

*No recordset operations detected.*

### Appendix D — MDB Inventory

*No MDB files detected.*

### Appendix E — Connection String Variants

*No connection strings detected.*

### Appendix F — Module Global Inventory

*No global variables detected.*

### Appendix G — Dead Form References

*No dead form references detected.*

### Appendix H — DataEnvironment/Report Mapping

*No DataEnvironment mappings detected.*

### Appendix I — SQL Catalog

*No SQL catalog available.*

### Appendix J — Artifact Index

| Artifact | Status |
|---|---|
| event_map | produced |
| form_dossier | produced |
| recordset_ops | produced |
| mdb_inventory | produced |
| form_loc_profile | produced |
| connection_string_variants | produced |
| module_global_inventory | produced |
| dead_form_refs | produced |
| dataenvironment_report_mapping | produced |
| procedure_summary | produced |
| source_erd | not available |
| source_data_dictionary | not available |

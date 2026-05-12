# MagicBox Modernization — VB6 Modernization BRD

## 1. Executive Summary

**Application**: MagicBox Modernization  
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

### FR-001 — Validate Contract Owner Names (P0)

Ensure all contract owner names are canonical and match their declared containers.

**Acceptance criteria:**
- All contract owner names must follow the defined canonical naming convention.
- Mismatch reports should be generated for any inconsistencies found.
- Validation should occur during both data entry and batch processing.

*Grounding: matched 8 legacy terms*

### FR-002 — Generate Mismatch Reports (P1)

Create reports detailing any mismatches between contract owner names and their containers.

**Acceptance criteria:**
- Reports must include details of the mismatched names and their expected containers.
- Reports should be accessible to system administrators.
- Reports must be generated on-demand and scheduled intervals.

*Grounding: matched 3 legacy terms*

### FR-003 — Standardize Naming Conventions (P0)

Define and enforce a standard naming convention for contract owners.

**Acceptance criteria:**
- Naming conventions must be documented and approved by stakeholders.
- System must enforce naming conventions during data entry.
- Existing data must be audited and corrected to meet new standards.

*Grounding: matched 4 legacy terms*

### FR-004 — Container Mapping Verification (P1)

Verify that each contract owner is correctly mapped to its declared container.

**Acceptance criteria:**
- Mapping logic must be reviewed and approved by domain experts.
- Automated checks should flag incorrect mappings.
- Manual overrides must be logged and reviewed.

*Grounding: matched 2 legacy terms*

### FR-005 — Error Handling for Validation Failures (P1)

Implement error handling for validation failures in contract owner naming.

**Acceptance criteria:**
- Errors must be logged with sufficient detail for troubleshooting.
- Users must receive clear feedback on validation failures.
- System should allow retry after correcting errors.

*Grounding: matched 3 legacy terms*

### FR-006 — User Interface for Name Management (P2)

Provide a user interface for managing and reviewing contract owner names.

**Acceptance criteria:**
- UI must allow users to view and edit contract owner names.
- Changes must be logged with user and timestamp information.
- UI should provide search and filter capabilities for contract owners.

*Grounding: matched 3 legacy terms*

## 4b. Non-Functional Requirements

| ID | Title | Category | Acceptance criteria |
|---|---|---|---|
| NFR-002 | Security of Name Management | security | Access logs must be maintained for all name management actions.; System must enforce role-based access control.; Security audits must be conducted quarterly. |

## 4b1. Business Rules

Business rules extracted from the legacy codebase — these must be preserved in the modernized system.

| ID | Type | Rule | Scope | Evidence | Confidence |
|---|---|---|---|---|---|
| BR-001 | input_validation | Contract owner names must match the canonical naming convention. | Data entry | RecruitmentController.php | 90% |

## 4c. Risks

| Risk | Impact | Mitigation |
|---|---|---|
| Potential for mismatches between existing data and new naming conventions. | high | Conduct thorough data audit and provide tools for correction. |
| Performance degradation during validation under load. | medium | Optimize validation logic and conduct performance testing. |

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

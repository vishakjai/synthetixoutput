# MagicBox Contractor Modernization — VB6 Modernization BRD

## 1. Executive Summary

**Application**: MagicBox Contractor Modernization  
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

### FR-001 — Contractor Placement Submission (P0)

Enable recruiters to submit contractor placements for approval.

**Acceptance criteria:**
- Recruiters can submit placement details through the UI.
- Submitted placements trigger an approval workflow.
- Placement data is stored in the database upon submission.

*Grounding: matched 3 legacy terms*

### FR-003 — Contractor Background Check (P1)

Integrate background check process for contractors.

**Acceptance criteria:**
- System initiates background checks upon document submission.
- Background check status is updated in real-time.
- Notifications are sent to relevant parties upon status change.

*Grounding: matched 1 legacy term*

### FR-004 — Placement Approval Workflow (P1)

Implement a workflow for approving contractor placements.

**Acceptance criteria:**
- Placements move to 'Pending Approval' state after submission.
- Approvers can approve or request changes to placements.
- Approved placements are marked as 'Active'.

*Grounding: matched 5 legacy terms*

### FR-006 — Contractor Placement Search (P2)

Allow users to search for contractor placements.

**Acceptance criteria:**
- Users can search placements by contractor name or ID.
- Search results display relevant placement details.
- Search performance meets defined response time criteria.

*Grounding: matched 2 legacy terms*

## 4b. Non-Functional Requirements

| ID | Title | Category | Acceptance criteria |
|---|---|---|---|
| NFR-002 | Code Quality | reliability | All linting rules are active during code analysis.; No critical linting errors are present in the codebase.; Linting results are documented and reviewed. |

## 4b1. Business Rules

Business rules extracted from the legacy codebase — these must be preserved in the modernized system.

| ID | Type | Rule | Scope | Evidence | Confidence |
|---|---|---|---|---|---|
| BR-001 | workflow_orchestration | Placements must be approved before becoming active. | Placement approval process | ContractorController.php | 90% |

## 4c. Risks

| Risk | Impact | Mitigation |
|---|---|---|
| Potential data loss during document upload. | high | Implement robust error handling and retry mechanisms. |
| Performance degradation under concurrent load. | medium | Optimize database queries and use caching where applicable. |

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

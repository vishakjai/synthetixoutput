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

### FR-001 — User Authentication (P0)

Implement secure user authentication using Microsoft Entra ID (Azure AD) SSO.

**Acceptance criteria:**
- Users can log in using Azure AD credentials.
- Session management is handled securely.
- Unauthorized access attempts are logged.

*Grounding: matched 1 legacy term*

### FR-002 — Contractor Placement Management (P0)

Migrate contractor placement management functionality to .NET 8, maintaining existing workflows.

**Acceptance criteria:**
- Recruiters can create and edit contractor placements.
- Placements can be submitted for co-approval.
- Placement status updates are reflected in real-time.

*Grounding: matched 3 legacy terms*

### FR-003 — Document Management (P1)

Implement document upload and management using React and .NET 8.

**Acceptance criteria:**
- Users can upload documents securely.
- Documents are stored in a compliant manner.
- Access to documents is restricted based on user roles.

*Grounding: matched 1 legacy term*

### FR-007 — Payroll and Timesheet Tracking (P2)

Migrate payroll and timesheet tracking to .NET 8, ensuring data integrity and compliance.

**Acceptance criteria:**
- Payroll data is processed accurately.
- Timesheets can be submitted and approved.
- Data integrity is maintained during migration.

*Grounding: matched 1 legacy term*

### FR-008 — Compliance Management (P2)

Implement compliance management features to ensure regulatory adherence.

**Acceptance criteria:**
- Compliance checks are automated where possible.
- Non-compliance alerts are generated and logged.
- Compliance reports are available to authorized users.

*Grounding: matched 1 legacy term*

## 4b. Non-Functional Requirements

| ID | Title | Category | Acceptance criteria |
|---|---|---|---|
| NFR-001 | Performance Optimization | performance | Load testing shows response times within target.; Performance metrics are monitored continuously.; Bottlenecks are identified and resolved promptly. |
| NFR-002 | Security and Compliance | security | Security scans show no critical vulnerabilities.; Data encryption is applied to sensitive data.; Access controls are enforced consistently. |
| NFR-003 | Scalability | scalability | Scalability tests confirm system can handle increased load.; Database performance remains stable under load.; Application servers auto-scale based on demand. |
| NFR-004 | Usability | usability | Usability tests show high user satisfaction.; Navigation is intuitive and consistent.; User feedback is incorporated into design improvements. |
| NFR-005 | Reliability | reliability | Monitoring shows uptime within target.; Incident response times meet defined SLAs.; Redundancy is built into critical components. |

## 4b1. Business Rules

Business rules extracted from the legacy codebase — these must be preserved in the modernized system.

| ID | Type | Rule | Scope | Evidence | Confidence |
|---|---|---|---|---|---|
| BR-001 | workflow_orchestration | Placements must be approved by a co-approver before onboarding. | Placement management | Controller/RecruitmentController.php | 90% |
| BR-002 | input_validation | Candidate data must be validated before submission. | Candidate management | Model/Candidate.php | 80% |

## 4c. Risks

| Risk | Impact | Mitigation |
|---|---|---|
| Potential data loss during migration. | high | Implement thorough data backup and validation procedures. |
| Incompatibility with existing third-party services. | medium | Conduct compatibility testing with all external services. |

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

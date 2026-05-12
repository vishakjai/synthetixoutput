# MagicBox Service Name Modernization — VB6 Modernization BRD

## 1. Executive Summary

**Application**: MagicBox Service Name Modernization  
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

### FR-001 — Implement Canonical Service Name Verification (P0)

Develop a mechanism to verify that all service names adhere to the canonical naming standard.

**Acceptance criteria:**
- Service names are checked against a predefined canonical list.
- Non-compliant service names trigger an alert.
- Verification results are logged for audit purposes.

*Grounding: matched 2 legacy terms*

### FR-002 — Service Name Compliance Reporting (P1)

Generate reports on service name compliance status.

**Acceptance criteria:**
- Reports are generated weekly.
- Reports include a list of compliant and non-compliant services.
- Reports are accessible to authorized personnel.

*Grounding: matched 1 legacy term*

### FR-003 — Error Handling for Non-compliant Services (P0)

Implement error handling for services with non-canonical names.

**Acceptance criteria:**
- Non-compliant services return a standardized error message.
- Error messages include guidance for correction.
- Errors are logged with sufficient detail for troubleshooting.

*Grounding: matched 2 legacy terms*

### FR-004 — Integration with Existing Monitoring Tools (P1)

Ensure service name compliance checks are integrated with existing monitoring tools.

**Acceptance criteria:**
- Compliance checks are visible in the monitoring dashboard.
- Alerts are generated for non-compliance.
- Integration does not degrade system performance.

*Grounding: matched 1 legacy term*

### FR-005 — Automated Compliance Verification on Deployment (P0)

Automate the verification of service names during deployment.

**Acceptance criteria:**
- Deployment pipeline includes a compliance verification step.
- Deployments with non-compliant service names are blocked.
- Compliance checks do not significantly increase deployment time.

*Grounding: matched 2 legacy terms*

### FR-006 — User Interface for Managing Canonical Names (P2)

Provide a UI for managing and updating the list of canonical service names.

**Acceptance criteria:**
- UI allows authorized users to add, edit, and delete canonical names.
- Changes to the canonical list are logged.
- UI is accessible via the internal portal.

*Grounding: matched 2 legacy terms*

## 4b. Non-Functional Requirements

| ID | Title | Category | Acceptance criteria |
|---|---|---|---|
| NFR-001 | Performance of Compliance Checks | performance | Performance tests show compliance checks meet the target.; Checks do not impact overall system response time.; Performance is consistent under peak load conditions. |
| NFR-002 | Security of Compliance Data | security | Data is encrypted at rest and in transit.; Access logs are reviewed monthly.; Unauthorized access attempts trigger alerts. |
| NFR-003 | Scalability of Compliance Mechanisms | scalability | Scalability tests confirm support for target service volume.; Mechanisms maintain performance under increased load.; No additional infrastructure is required for scaling. |
| NFR-004 | Usability of Compliance Management UI | usability | Usability tests confirm ease of use.; UI feedback is positive from end users.; Training materials are provided for new users. |

## 4b1. Business Rules

Business rules extracted from the legacy codebase — these must be preserved in the modernized system.

| ID | Type | Rule | Scope | Evidence | Confidence |
|---|---|---|---|---|---|
| BR-001 | input_validation | Service names must match the canonical format. | Service Compliance | ServiceComplianceController.php | 90% |

## 4c. Risks

| Risk | Impact | Mitigation |
|---|---|---|
| Non-compliance with service naming standards could lead to integration issues. | high | Implement automated compliance checks and alerts. |
| Performance degradation due to compliance checks. | medium | Optimize compliance check algorithms and conduct performance testing. |

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

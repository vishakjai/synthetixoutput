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

### FR-001 — Skip Catchall Macros (P0)

Ensure that the system skips execution of catchall macros during processing.

**Acceptance criteria:**
- System logs show no execution of catchall macros.
- Catchall macros are identified and flagged in the codebase.
- Automated tests confirm macros are skipped in all scenarios.

### FR-002 — Implement Proportional Skew Check (P0)

Develop a mechanism to perform proportional skew checks to maintain balance in the system.

**Acceptance criteria:**
- Proportional skew checks are executed during each relevant transaction.
- Alerts are generated if skew exceeds defined thresholds.
- Reports provide a summary of skew checks and outcomes.

### FR-003 — Ensure No Orphan Contract Owners (P0)

Verify all contract owners are linked and no orphan entities exist.

**Acceptance criteria:**
- Database queries confirm no orphan contract owners.
- Reports list all contract owners and their associations.
- Automated checks ensure new contracts are linked correctly.

### FR-004 — Logging for Skipped Macros (P1)

Implement logging to track when macros are skipped.

**Acceptance criteria:**
- Logs capture every instance of a skipped macro.
- Logs are accessible and searchable by administrators.
- Alerts are sent if unexpected macros are executed.

### FR-005 — Automated Testing for Skew Checks (P1)

Create automated tests to validate proportional skew checks.

**Acceptance criteria:**
- Automated tests cover all scenarios for skew checks.
- Test results are logged and reviewed regularly.
- Failures in skew checks trigger alerts and logs.

### FR-006 — Contract Owner Verification Process (P1)

Establish a process to verify and correct contract owner links.

**Acceptance criteria:**
- Verification process is documented and accessible.
- Regular audits confirm no orphan contract owners.
- Discrepancies are resolved within a defined timeframe.

### FR-007 — User Interface for Skew Check Results (P2)

Provide a user interface to display results of skew checks.

**Acceptance criteria:**
- UI displays real-time skew check results.
- Users can filter and search skew check data.
- Access to UI is restricted to authorized personnel.

### FR-008 — Alert System for Orphan Contract Owners (P2)

Implement an alert system to notify administrators of orphan contract owners.

**Acceptance criteria:**
- Alerts are generated when orphan contract owners are detected.
- Administrators receive alerts via email and dashboard notifications.
- Alerts include details necessary for resolution.

## 4b. Non-Functional Requirements

| ID | Title | Category | Acceptance criteria |
|---|---|---|---|
| NFR-001 | System Performance | performance | Performance tests confirm skew checks execute within target time.; No degradation in system performance during peak loads.; Performance metrics are logged and reviewed monthly. |
| NFR-002 | Security of Contract Data | security | Security audits confirm no unauthorized data access.; Access logs are reviewed weekly by security team.; Data encryption is verified for all contract data. |
| NFR-003 | System Reliability | reliability | System monitoring confirms uptime targets are met.; Incident reports are reviewed and resolved within SLA.; Redundancy measures are in place and tested quarterly. |
| NFR-004 | Usability of Skew Check Interface | usability | User feedback sessions confirm ease of use.; Interface meets accessibility standards.; Training materials are available and up-to-date. |
| NFR-005 | Observability of Macro Skipping | observability | Logs capture all macro skipping events.; Logs are reviewed weekly by the operations team.; Anomalies in logs are flagged and investigated promptly. |

## 4b1. Business Rules

Business rules extracted from the legacy codebase — these must be preserved in the modernized system.

| ID | Type | Rule | Scope | Evidence | Confidence |
|---|---|---|---|---|---|
| BR-001 | workflow_orchestration | Placements must be approved by a co-approver before onboarding. | Contractor Placement | Contractor_placement.php | 90% |

## 4c. Risks

| Risk | Impact | Mitigation |
|---|---|---|
| Potential for missed orphan contract owners. | high | Implement thorough audits and automated checks. |
| Performance degradation during skew checks. | medium | Optimize skew check algorithms and conduct performance testing. |

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

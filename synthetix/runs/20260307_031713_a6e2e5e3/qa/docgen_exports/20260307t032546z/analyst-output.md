# Modernization Brief - VB6 to C# Modernization

## Header
- Objective: The objective is to analyze a public VB6 repository and produce various documentation artifacts to support modernization efforts.
- Domain: software
- Repo: https://github.com/vishakjai/TestVB6Project1 @ detached (unknown)
- SIL Versions: SCM scm-v1 / CP cp-v1 / HA ha-v1
- Generated At: 2026-03-07T03:18:39.068362+00:00

## Decision Brief

| Category | Summary |
|---|---|
| Modernization readiness | 66/100 |
| Risk tier | medium |
| Inventory | 1 project(s), 25 forms/usercontrols, 5 dependencies |
| Lines of code scanned | 9037 total LOC (5140 form LOC, 217 module LOC, 0 class LOC) across 48 files |
| Data touchpoints | tbltransaction, Date, tblCustomers, tblaccount |
| Headline | Phased UI migration recommended. |

### Recommended strategy
- Phased UI migration: Incremental form-by-form migration balances risk and delivery speed while preserving behavior.
- PH0 Baseline and equivalence harness: Capture golden flows and baseline outputs.
- PH1 Incremental migration and dependency replacement: Migrate forms/modules with dependency risk controls.
- PH2 Hardening and release evidence: Finalize quality gates and publish evidence pack.

### Decisions Required (Blocking)
- DEC-UI-001: Target UI framework selection for migrated forms
  - Recommendation: WinForms for lowest event-model delta from VB6 unless UX redesign is in scope.
- DEC-OCX-001: ActiveX/OCX replacement strategy by dependency
  - Recommendation: Replace common controls and isolate only high-risk dependencies behind adapters.
- DEC-DB-001: Database contract strategy during migration
  - Recommendation: Preserve contracts initially and modernize behind a compatibility layer.
- DEC-IAM-001: Confirm identity/access model (role model, multi-user assumptions, and credential handling).
  - Recommendation: Define target role model and credential policy before implementation.
- Q-001: Are there existing operational constraints or integration dependencies not listed?
  - Recommendation: Resolve with product/business owner before implementation commitment.
- Q-002: What are target latency, throughput, and availability SLOs?
  - Recommendation: Resolve with product/business owner before implementation commitment.

### Decisions Required (Non-blocking)
- DEC-OBS-001: Logging and observability stack for migrated runtime

## Delivery Spec

### Backlog
| ID | Pri | Type | Outcome | Acceptance |
|---|---|---|---|---|
| FR-001 | P0 | functional | Form Translation | All VB6 forms are present in the C# application with equivalent controls. / Event handlers in VB6 are correctly mapped to C# event handlers. |
| FR-002 | P0 | functional | ActiveX/OCX Replacement | All ActiveX components are replaced with equivalent C# libraries or controls. / OCX dependencies are removed and replaced with C# implementations. |
| FR-003 | P0 | functional | Business Logic Translation | All business logic from VB6 is implemented in C#. / Unit tests confirm that C# logic produces the same results as VB6 logic. |
| FR-004 | P1 | functional | Data Handling | Database connections in C# are equivalent to those in VB6. / Data retrieval and manipulation are functionally identical. |
| FR-005 | P1 | functional | User Authentication | User login functionality is present and operational in C#. / Authentication logic matches VB6 implementation. |
| FR-006 | P2 | functional | Report Generation | Reports generated in C# match those from VB6 in format and content. / All report generation logic is implemented in C#. |
| FR-007 | P2 | functional | Interest Calculation | Interest calculations in C# produce the same results as VB6. / All calculation logic is verified with unit tests. |
| FR-008 | P2 | functional | Account Management | Account creation, modification, and closure are functional in C#. / All account management workflows are preserved. |
| NFR-001 | P1 | non_functional | Performance | All operations complete within 2 seconds under normal load. / Performance tests show no degradation compared to VB6. |
| NFR-002 | P1 | non_functional | Security | Security audit finds no critical vulnerabilities. / All data is encrypted in transit and at rest. |
| RM-001 | P0 | risk_remediation | Parameterize SQL and secure credential handling | Remediation implemented and validated against affected legacy flow. / Evidence artifacts updated with before/after traceability. |
| RM-002 | P0 | risk_remediation | Define identity and access model for modernization scope | Remediation implemented and validated against affected legacy flow. / Evidence artifacts updated with before/after traceability. |

### Testing and Evidence
- Golden flows:
  - GF-001: BANK::frmWithinDate primary flow | entry=BANK::frmWithinDate::Form_Load
  - GF-002: BANK::frmWithinDate primary flow | entry=BANK::frmWithinDate::cmdPrint_Click
  - GF-003: BANK::frmdaily primary flow | entry=BANK::frmdaily::cmdGenerate_Click
  - GF-004: BANK::frmWithinDate primary flow | entry=BANK::frmWithinDate::cmdexit_Click
  - GF-005: shared_module primary flow | entry=shared_module::CheckDatabaseStatus
  - GF-006: shared_module primary flow | entry=shared_module::Lock_Form_Controls
  - GF-007: shared_module primary flow | entry=shared_module::Messager
  - GF-008: shared_module primary flow | entry=shared_module::MoveToFirst
  - GF-009: shared_module primary flow | entry=shared_module::MoveToLast
  - GF-010: shared_module primary flow | entry=shared_module::MoveToNext
- Quality gates:
  - gherkin_syntax: PASS | BDD syntax validation for Feature/Scenario/Given/When/Then.
  - requirements_completeness: PASS | Backlog grounded in discovered behavior (12 derived item(s), threshold 4).
  - compliance_constraints_applied: FAIL | No explicit compliance constraints were linked, but security/privacy risks were detected in legacy behavior.
  - bdd_flow_grounding: PASS | BDD scenarios are grounded in extracted legacy flows.
  - handler_inventory_completeness: PASS | All analyzed forms meet handler coverage threshold.
  - report_model_reconciled: PASS | Reporting model and entrypoints reconciled.
  - variant_resolution: PASS | Single project variant detected; no variant scope decision required.
  - variant_schema_divergence: PASS | No cross-variant schema naming divergence detected.
  - key_safety_issues_identified: PASS | Risk signals include SQL injection/credential handling issues (11 signal(s)).
  - schema_key_verification: PASS | No delete-by-customer transaction key hazard detected.
  - identity_access_model: WARN | Role model or credential handling requires confirmation.
  - database_archaeology_ready: WARN | DB QA detected blocking or warning issues in schema reconstruction/mapping.
  - qa_structural_integrity: WARN | QA structural checks: pass=15, warn=1, fail=0, blockers=0.
  - qa_semantic_plausibility: PASS | Semantic plausibility checks passed with no issues.
- QA summary:
  - Status: WARN
  - Structural: pass=15, warn=1, fail=0, blockers=0
  - QA Gate qa_structural_integrity: WARN | QA structural checks: pass=15, warn=1, fail=0, blockers=0.
  - QA Gate qa_semantic_plausibility: PASS | Semantic plausibility checks passed with no issues.
  - Structural checks: 16 total (0 blocking)
  - Rule consolidation notes are documented in Appendix Section E2 when duplicate rule templates are suppressed.

### Open Questions
- [HIGH] Q-001: Are there existing operational constraints or integration dependencies not listed? (owner: Client)
- [HIGH] Q-002: What are target latency, throughput, and availability SLOs? (owner: Client)

## QA Validation Summary
- Overall status: WARN
- Structural summary: pass=15, warn=1, fail=0, blockers=0
- Auto-fixes applied:
  - Normalized non-form rule scope references (module/project tokens) to non-blocking placeholders.
  - Aligned event handler count to event-map entry count for deterministic reconciliation.

## Evidence Appendix
- legacy_inventory_ref: artifact://legacy_inventory/1.0/art_legacy_inventory_6497943d0e994a18
- repo_landscape_ref: artifact://repo_landscape/1.0/art_repo_landscape_529be8ef2e524dc4
- scope_lock_ref: artifact://scope_lock/1.0/art_scope_lock_ee367f5339f447bf
- variant_inventory_ref: artifact://variant_inventory/1.0/art_variant_inventory_3d458518c0df4071
- event_map_ref: artifact://event_map/1.0/art_event_map_075b85a351b34e59
- sql_catalog_ref: artifact://sql_catalog/1.0/art_sql_catalog_91dda5b5cc9d4319
- sql_map_ref: artifact://sql_map/1.0/art_sql_map_1f882973513e42f0
- data_access_map_ref: artifact://data_access_map/1.0/art_data_access_map_0194a147d8484a33
- recordset_ops_ref: artifact://recordset_ops/1.0/art_recordset_ops_ddf728ac8eff486f
- procedure_summary_ref: artifact://procedure_summary/1.0/art_procedure_summary_625b8aa9fe034a9f
- form_dossier_ref: artifact://form_dossier/1.0/art_form_dossier_2180960dc3c3477e
- dependency_list_ref: artifact://dependency_inventory/1.0/art_dependency_inventory_2a14b5bff4164996
- dependency_inventory_ref: artifact://dependency_inventory/1.0/art_dependency_inventory_2a14b5bff4164996
- business_rules_ref: artifact://business_rule_catalog/1.0/art_business_rule_catalog_9d6d15fcec094883
- detector_findings_ref: artifact://detector_findings/1.0/art_detector_findings_aed6f266e6354ada
- risk_register_ref: artifact://risk_register/1.0/art_risk_register_b6df874a9ce94658
- orphan_analysis_ref: artifact://orphan_analysis/1.0/art_orphan_analysis_20bc082132654a47
- delivery_constitution_ref: artifact://delivery_constitution/1.0/art_delivery_constitution_a398a6466a894222
- source_db_profile_ref: artifact://source_db_profile/1.0/art_source_db_profile_79e6f5b526ac498b
- source_schema_model_ref: artifact://source_schema_model/1.0/art_source_schema_model_0628100daf0143a2
- source_query_catalog_ref: artifact://source_query_catalog/1.0/art_source_query_catalog_d91881bbf6de4764
- source_relationship_candidates_ref: artifact://source_relationship_candidates/1.0/art_source_relationship_candidates_7f5f9ee107b948d3
- source_data_dictionary_ref: artifact://source_data_dictionary/1.0/art_source_data_dictionary_7606e413b54f4a53
- source_data_dictionary_markdown_ref: artifact://source_data_dictionary_markdown/1.0/art_source_data_dictionary_markdown_6362505897044f6c
- source_erd_ref: artifact://source_erd/1.0/art_source_erd_01065c355f404f78
- source_hotspot_report_ref: artifact://source_hotspot_report/1.0/art_source_hotspot_report_d897a5aa2bb34b2c
- target_schema_model_ref: artifact://target_schema_model/1.0/art_target_schema_model_3d7a61d2a1274f4c
- target_erd_ref: artifact://target_erd/1.0/art_target_erd_1ebc5f53964c4fec
- target_data_dictionary_ref: artifact://target_data_dictionary/1.0/art_target_data_dictionary_7b73c0497906420d
- schema_mapping_matrix_ref: artifact://schema_mapping_matrix/1.0/art_schema_mapping_matrix_d55b1bb82e514eac
- migration_plan_ref: artifact://migration_plan/1.0/art_migration_plan_6ecba06998bd468d
- validation_harness_spec_ref: artifact://validation_harness_spec/1.0/art_validation_harness_spec_c85b2f8dc4374171
- db_qa_report_ref: artifact://db_qa_report/1.0/art_db_qa_report_278d170211b14b8a
- schema_approval_record_ref: artifact://schema_approval_record/1.0/art_schema_approval_record_c8809010527e4629
- schema_drift_report_ref: artifact://schema_drift_report/1.0/art_schema_drift_report_5c4f772381a545b1
- variant_diff_report_ref: artifact://variant_diff_report/1.0/art_variant_diff_report_13e2c11fb39f47ab
- reporting_model_ref: artifact://reporting_model/1.0/art_reporting_model_06daa13038294c0a
- identity_access_model_ref: artifact://identity_access_model/1.0/art_identity_access_model_c1d0546453af44ec
- discover_review_checklist_ref: artifact://discover_review_checklist/1.0/art_discover_review_checklist_8a204bc40e3647d3
- artifact_index_ref: artifact://artifact_index/1.0/art_artifact_index_77cc1913fbcb48b1
- qa_report_ref: embedded://analyst_report_v2/qa_report_v1
- knowledge_snapshot_ref: runctx://runctx-50fcec941d1ffc91/kctx-b2ff4c2dc6af0182
- run_delivery_constitution_ref: runctx://runctx-50fcec941d1ffc91/delivery_constitution/const-d92dd824c16e
- High-volume sections included in structured artifact (inventory, dependencies, event map, SQL catalog, business rules).

## Appendix Snapshot
- Legacy inventory: present
- Event map rows: 74
- SQL catalog rows: 45
- SQL map rows: 42
- Procedure summaries: 74
- Form dossiers: 24
- Dependency rows: 13
- Business rules: 23
- Risk register rows: 25
- Orphan analysis rows: 11
- Repo landscape variants: 1
- Variant inventory rows: 1
- Constitution principles: 3
- MDB inventory rows: 1
- Form LOC profile rows: 25
- Designer LOC rows: 16
- Connection string variants: 3
- Module global inventory rows: 20
- Dead form references: 4
- DataEnvironment report mappings: 6
- Static risk detector findings: 2
- Source data dictionary rows: 14
- Source LOC: 9037 total (forms=5140, modules=217, classes=0) across 48 file(s)

## Detailed Appendix

### A. Legacy Inventory
- Projects: 1
- Data touchpoints: tbltransaction, Date, tblCustomers, tblaccount
- Source LOC: 9037 total (forms=5140, modules=217, classes=0) across 48 file(s)
| Project | Type | Startup | Members | Forms | Reports | Dependencies | Source LOC | Shared tables |
|---|---|---|---:|---:|---:|---:|---:|---|
| BANK | Exe | frmSplash | 14 | 13 | 1 | 4 | 3383 | Date, TextBox, tblCustomers, tblaccount, tblcustomers, tbltransaction |

### B. Dependency Inventory
| Name | Kind | GUID / Reference | Risk | Recommended action | Forms mapped |
|---|---|---|---|---|---|
| MSCOMCT2.OCX | ocx | {86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0; MSCOMCT2.OCX | medium | Assess replacement/interop strategy. | BANK::frmLogin1, BANK::frmSplash, BANK::frmWithinDate, BANK::frmaddinterest, BANK::frmcloseacount, BANK::frmcustomer |
| MSCOMCTL.OCX | ocx | {831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0; MSCOMCTL.OCX | medium | Assess replacement/interop strategy. | BANK::frmLogin1, BANK::frmSplash, BANK::frmWithinDate, BANK::frmaddinterest, BANK::frmcloseacount, BANK::frmcustomer |
| MSComCtl2.DTPicker | com_typelib | n/a | medium | Assess replacement/interop strategy. | (unmapped)::Form1, (unmapped)::frmExpireItemsWithinDate, (unmapped)::frmcheckbalance, (unmapped)::frmreport, BANK::frmLogin1, BANK::frmSplash |
| MSComctlLib.ListView | com_typelib | n/a | medium | Assess replacement/interop strategy. | (unmapped)::frminterest, (unmapped)::frmtransaction |
| MSComctlLib.ProgressBar | com_typelib | n/a | medium | Assess replacement/interop strategy. | BANK::frmLogin1, BANK::frmSplash, BANK::frmWithinDate, BANK::frmaddinterest, BANK::frmcloseacount, BANK::frmcustomer |
| MSDBRPTR.DLL | dll | *\G{642AC760-AAB4-11D0-8494-00A0C90DC8A9}#1.0#0#C:\WINDOWS\system32\MSDBRPTR.DLL#Microsoft Data Report Designer v6.0 | medium | Assess replacement/interop strategy. | n/a |
| msstdfmt.dll | dll | *\G{6B263850-900B-11D0-9484-00A0C91110ED}#1.0#0#C:\WINDOWS\system32\msstdfmt.dll#Microsoft Data Formatting Object Library | medium | Assess replacement/interop strategy. | n/a |
| MSDERUN.DLL | dll | *\G{3D5C6BF0-69A3-11D0-B393-00A0C9055D8E}#1.0#0#C:\Program Files\Common Files\designer\MSDERUN.DLL#Microsoft Data Environment Instance 1.0 | medium | Assess replacement/interop strategy. | n/a |
| MSBIND.DLL | dll | *\G{56BF9020-7A2F-11D0-9482-00A0C91110ED}#1.0#0#C:\WINDOWS\system32\MSBIND.DLL#Microsoft Data Binding Collection | medium | Assess replacement/interop strategy. | n/a |
| msmask32.ocx | ocx | {C932BA88-4374-101B-A56C-00AA003668DC}#1.1#0; msmask32.ocx | medium | Assess replacement/interop strategy. | n/a |
| DBGRID32.OCX | ocx | {00028C01-0000-0000-0000-000000000046}#1.0#0; DBGRID32.OCX | medium | Assess replacement/interop strategy. | n/a |
| MSFLXGRD.OCX | ocx | {5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0; MSFLXGRD.OCX | medium | Assess replacement/interop strategy. | n/a |
| asctrls.ocx | ocx | {CC0918E0-EFE5-11CF-A044-00AA00B6015C}#1.0#0; asctrls.ocx | medium | Assess replacement/interop strategy. | n/a |

### C. Event Map
| Entry | Container | Trigger | Calls | Side effects |
|---|---|---|---|---|
| BANK::frmWithinDate:Load | BANK::frmWithinDate | Load | DTPicker2 | n/a |
| BANK::frmWithinDate:Click | BANK::frmWithinDate | Click | MsgBox, Exit, DonorReport | n/a |
| BANK::frmdaily:Click | BANK::frmdaily | Click | str1, deBank, rptdaily, Unload | n/a |
| BANK::frmWithinDate:Click | BANK::frmWithinDate | Click | Unload | n/a |
| shared_module:event | shared_module |  | MsgBox, Exit | n/a |
| shared_module:Form_Controls | shared_module | Form_Controls | ctrl | n/a |
| shared_module:event | shared_module |  | MsgBox | n/a |
| shared_module:event | shared_module |  | n/a | n/a |
| shared_module:event | shared_module |  | CheckDatabaseStatus, MsgBox, Exit | n/a |
| shared_module:event | shared_module |  | CheckDatabaseStatus, MsgBox, Exit | n/a |
| shared_module:event | shared_module |  | CheckDatabaseStatus, MsgBox, Exit | n/a |
| shared_module:event | shared_module |  | KeyAscii, MsgBox | n/a |
| shared_module:Form_Controls | shared_module | Form_Controls | ctrl | n/a |
| shared_module:event | shared_module |  | KeyAscii, MsgBox | n/a |
| shared_module:event | shared_module |  | KeyAscii, MsgBox | n/a |
| shared_module:form_controls | shared_module | form_controls | ctrl | n/a |
| shared_module:event | shared_module |  | rscustomers, rsTransaction, rsAccount | n/a |
| shared_module:event | shared_module |  | cnBank | n/a |
| shared_module:event | shared_module |  | txtCtrl | n/a |
| (unmapped)::frmcheckbalance:click | (unmapped)::frmcheckbalance | click | rstemp, txtcustomerid, txtcontacttitle, txtfirstname | tblCustomers |
| BANK::frmLogin1:Click | BANK::frmLogin1 | Click | fnd, rs, Exit, Load | n/a |
| BANK::frmwithdraw:Click | BANK::frmwithdraw | Click | Unload | n/a |
| BANK::frmSplash:KeyPress | BANK::frmSplash | KeyPress | frmlogin | n/a |
| BANK::frmwithdraw:Click | BANK::frmwithdraw | Click | frmLogin1 | n/a |
| BANK::frmSplash:Timer | BANK::frmSplash | Timer | ProgressBar, lbldisplay, Unload, frmLogin1 | n/a |
| BANK::frmSplash:Timer | BANK::frmSplash | Timer | n/a | n/a |
| BANK::frmcustomer:LostFocus | BANK::frmcustomer | LostFocus | MsgBox, txtcustomerid, Exit, find_str | tblcustomers |
| (unmapped)::frminterest:event | (unmapped)::frminterest |  | cnBank, sql, rs, TransactionID | tbltransaction |
| (unmapped)::frminterest:Click | (unmapped)::frminterest | Click | ans, Exit, TodayDate, mdate | tbltransaction, tblcustomers |
| BANK::menu:Load | BANK::menu | Load | connectDatabase | n/a |
| BANK::menu:QueryUnload | BANK::menu | QueryUnload | i, Cancel | n/a |
| BANK::menu:Click | BANK::menu | Click | frmdeposit | n/a |
| BANK::menu:Click | BANK::menu | Click | frmnewaccount | n/a |
| BANK::menu:Click | BANK::menu | Click | frmupdate | n/a |
| BANK::menu:Click | BANK::menu | Click | frmwithdraw | n/a |
| BANK::menu:Click | BANK::menu | Click | frmAccountdetails | n/a |
| BANK::menu:Click | BANK::menu | Click | frmaddinterest | n/a |
| BANK::menu:Click | BANK::menu | Click | frmaddinterest | n/a |
| BANK::menu:Click | BANK::menu | Click | frmcloseacount | n/a |
| BANK::menu:Click | BANK::menu | Click | frmcustomer | n/a |
| BANK::menu:Click | BANK::menu | Click | rpttransaction | n/a |
| BANK::menu:Click | BANK::menu | Click | i, Cancel | n/a |
| BANK::menu:Click | BANK::menu | Click | frminterest | n/a |
| BANK::menu:Click | BANK::menu | Click | frmWithinDate | n/a |
| BANK::menu:Click | BANK::menu | Click | frmstatement | n/a |
| BANK::menu:Click | BANK::menu | Click | rpttransaction | n/a |
| BANK::menu:Click | BANK::menu | Click | frmtransaction | n/a |
| BANK::menu:Click | BANK::menu | Click | frmtransaction | n/a |
| BANK::menu:Click | BANK::menu | Click | rptWithdrawals | n/a |
| BANK::frmmonthlyreport:KeyPress | BANK::frmmonthlyreport | KeyPress | Numeric | n/a |
| BANK::frmmonthlyreport:KeyPress | BANK::frmmonthlyreport | KeyPress | ValidNumeric | n/a |
| BANK::frmmonthlyreport:Click | BANK::frmmonthlyreport | Click | MsgBox, cmbcustomerid, Exit, DTPTo | Date, tblcustomers |
| BANK::frmsettings:event | BANK::frmsettings |  | txtaccountid, txtaccounttype, txtcheque, txtnocheque | n/a |
| BANK::frmsettings:event | BANK::frmsettings |  | cheque, rsfind, txtaccountid, txtaccounttype | tblaccount |
| BANK::frmwithdraw:Click | BANK::frmwithdraw | Click | cmdedit, cmdsave, rsfind, NewRecord | n/a |
| BANK::frmsettings:Click | BANK::frmsettings | Click | cmdedit, cmdsave, cmdcancel, Control | n/a |
| BANK::frmsettings:Click | BANK::frmsettings | Click | MsgBox, txtaccountid, Exit, txtcheque | n/a |
| BANK::frmsettings:KeyPress | BANK::frmsettings | KeyPress | ValidNumeric | n/a |
| BANK::frmsettings:KeyPress | BANK::frmsettings | KeyPress | ValidNumeric | n/a |
| BANK::frmsettings:KeyPress | BANK::frmsettings | KeyPress | ValidNumeric | n/a |
| BANK::frmwithdraw:Click | BANK::frmwithdraw | Click | Unload | n/a |
| (unmapped)::frmwith:Click | (unmapped)::frmwith | Click | rstemp, lvwTransactions, LoadListView, MsgBox | tbltransaction |
| BANK::menu:Click | BANK::menu | Click | Unload | n/a |
| BANK::menu:Click | BANK::menu | Click | frmWithinDate | n/a |
| BANK::menu:Click | BANK::menu | Click | frmmonthlyreport | n/a |
| BANK::menu:Click | BANK::menu | Click | frmaddinterest | n/a |
| BANK::menu:Click | BANK::menu | Click | frmsettings | n/a |
| (unmapped)::frmtransaction:Click | (unmapped)::frmtransaction | Click | n/a | n/a |
| (unmapped)::frmtransaction:Click | (unmapped)::frmtransaction | Click | rsTemp, cboAccNo, cboCustomerID, cboFirst | tblcustomers, tblTransaction |
| (unmapped)::frmtransaction:Click | (unmapped)::frmtransaction | Click | fradate, rsTemp, cboAccNo, cboCustomerID | tblCustomers, tblTransaction |
| (unmapped)::frmtransaction:Click | (unmapped)::frmtransaction | Click | fradate, rsTemp, cboAccNo, cboCustomerID | tblCustomers, tbltransaction |
| (unmapped)::frmtransaction:Click | (unmapped)::frmtransaction | Click | rptstatement | n/a |
| BANK::frmcloseacount:Click | BANK::frmcloseacount | Click | Unload | n/a |
| (unmapped)::frmtransaction:Click | (unmapped)::frmtransaction | Click | lvwTransactions | n/a |

### D. SQL Catalog
| SQL ID | Kind | Tables | Query |
|---|---|---|---|
| sql:1 | unknown | n/a | .Update |
| sql:2 | unknown | n/a | End Select |
| sql:3 | insert | tbltransaction | Insert into tbltransaction (AccountNO, TransactionType, Date, Amount, Status) values ( |
| sql:4 | unknown | n/a | MsgBox "Please Select 'To Date' Todays Date", vbInformation + vbOKOnly |
| sql:5 | unknown | Date | MsgBox "Please Select From Date Less Than To Date ", vbInformation + vbOK |
| sql:6 | unknown | n/a | MsgBox "Please select 'To Date' Todays date", vbInformation + vbOKOnly |
| sql:7 | unknown | n/a | MsgBox "select 2nd Date as Todays Date" |
| sql:8 | unknown | n/a | Please Select 'To Date' Todays Date |
| sql:9 | unknown | Date | Please Select From Date Less Than To Date |
| sql:10 | unknown | n/a | Please check Selected Dates |
| sql:11 | unknown | n/a | Please select 'To Date' Todays date |
| sql:12 | select | tblCustomers | SELECT * FROM tblCustomers WHERE AccountNo=' |
| sql:13 | select | tblCustomers | SELECT * FROM tblCustomers WHERE AccountNo=':expr' |
| sql:14 | select | n/a | SELECT tblcustomers.FirstName,Sum(tbltransaction.Balance) as Balance,sum(tbltransaction.Amount) as amount, tbltransaction.Date |
| sql:15 | select | tbltransaction | SELECT tblcustomers.FirstName,Sum(tbltransaction.Balance) as Balance,sum(tbltransaction.Amount) as amount, tbltransaction.Date From tbltransaction, tblcustomers Where (((tbltransaction.AccountNo) = [tblcustomers].[AccountNo])) GROUP BY tblcustomers.Firstname, tbltransaction.Date HAVING(((tbltransaction.Date) Between #:expr# And #:expr#)) |
| sql:16 | select | n/a | SELECT tbltransaction.AccountNo, tbltransaction.TransactionID,tbltransaction.Date,tbltransaction.transactiontype, tbltransaction.Amount,tbltransaction.Balance, tbltransaction.date |
| sql:17 | select | tbltransaction | SELECT tbltransaction.AccountNo, tbltransaction.TransactionID,tbltransaction.Date,tbltransaction.transactiontype, tbltransaction.Amount,tbltransaction.Balance, tbltransaction.date From tbltransaction WHERE (((tbltransaction.date)Between #:expr# And #:expr#)) |
| sql:18 | select | n/a | SELECT tbltransaction.TransactionID, tbltransaction.CustomerID, tbltransaction.ACountNO, tbltransaction.Date |
| sql:19 | select | tbltransaction | SELECT tbltransaction.TransactionID, tbltransaction.CustomerID, tbltransaction.ACountNO, tbltransaction.Date FROM tbltransaction INNER JOIN tbltransaction ON tbltransaction.transactionid=tbltransaction.transactionid WHERE (((tbltransaction.Date) Between #:expr# And #:expr#)) |
| sql:20 | select | tblCustomers | Select * FROM tblCustomers WHERE AccountNo= |
| sql:21 | select | tblCustomers | Select * FROM tblCustomers WHERE AccountNo=:expr"", cnBank, adOpenKeyset, adLockOptimistic |
| sql:22 | select | tblCustomers | Select * from tblCustomers Where CustomerID= |
| sql:23 | select | tblCustomers | Select * from tblCustomers Where CustomerID=:expr"", cnBank, adOpenKeyset, adLockOptimistic |
| sql:24 | select | tblCustomers | Select * from tblCustomers Where FirstName=' |
| sql:25 | select | tblCustomers | Select * from tblCustomers Where FirstName=':expr"'", cnBank, adOpenKeyset, adLockOptimistic |
| sql:26 | select | tblTransaction | Select * from tblTransaction Where AccountNo= |
| sql:27 | select | tblTransaction | Select * from tblTransaction Where AccountNo=:expr"", cnBank, adOpenKeyset, adLockOptimistic |
| sql:28 | select | tblTransaction | Select * from tblTransaction Where CustomerID= |
| sql:29 | select | tblTransaction | Select * from tblTransaction Where CustomerID=:expr"", cnBank, adOpenKeyset, adLockOptimistic |
| sql:30 | select | tblcustomers | Select * from tblcustomers Where AccountNo= |
| sql:31 | select | tblcustomers | Select * from tblcustomers Where AccountNo=:expr" ", cnBank, adOpenKeyset, adLockOptimistic |
| sql:32 | select | tbltransaction | Select * from tbltransaction Where Date BETWEEN # |
| sql:33 | select | tbltransaction | Select * from tbltransaction Where Date BETWEEN #:expr# AND #:expr"#", cnBank, adOpenKeyset, adLockOptimistic |
| sql:34 | select | n/a | Select Case KeyAscii |
| sql:35 | select | tblcustomers | Select CustomerID from tblcustomers |
| sql:36 | select | tblaccount | select * from tblaccount |
| sql:37 | select | tblcustomers | select * from tblcustomers |
| sql:38 | select | tblcustomers | select * from tblcustomers where CustomerID = |
| sql:39 | select | tblcustomers | select * from tblcustomers where CustomerID = :expr; |
| sql:40 | select | tbltransaction | select * from tbltransaction |
| sql:41 | select | tbltransaction | select * from tbltransaction where AccountNO = |
| sql:42 | select | tbltransaction | select * from tbltransaction where AccountNO = rsAccNum!AccountNo order by Date ; |
| sql:43 | select | n/a | select 2nd Date as Todays Date |
| sql:44 | select | tbltransaction | select max(transactionid) from tbltransaction |
| sql:45 | select | n/a | select... |

### D1. Source DB Column Schema
| Table | Column | Type | FK Ref | Confidence | Access Evidence | Business Meaning | Evidence Ref |
|---|---|---|---|---:|---|---|---|
| tblCustomers | CustomerID | integer | n/a | 0.78 | SQL refs=1 | Identifier key used to reference a business entity. | sql:35 |
| tbltransaction | AccountNO | integer | n/a | 0.74 | SQL refs=2 | Business meaning inferred from query usage; verify with SME. | sql:3, sql:17 |
| tbltransaction | ACountNO | integer | n/a | 0.74 | SQL refs=1 | Business meaning inferred from query usage; verify with SME. | sql:19 |
| tbltransaction | Amount | numeric(18,2) | n/a | 0.74 | SQL refs=2 | Monetary amount captured for debit/credit operations. | sql:3, sql:17 |
| tbltransaction | Amount) | numeric(18,2) | n/a | 0.74 | SQL refs=1 | Monetary amount captured for debit/credit operations. | sql:15 |
| tbltransaction | Balance | numeric(18,2) | n/a | 0.74 | SQL refs=1 | Running or current balance used for account state. | sql:17 |
| tbltransaction | Balance) | numeric(18,2) | n/a | 0.74 | SQL refs=1 | Running or current balance used for account state. | sql:15 |
| tbltransaction | CustomerID | integer | n/a | 0.78 | SQL refs=1 | Identifier key used to reference a business entity. | sql:19 |
| tbltransaction | Date | timestamp | n/a | 0.74 | SQL refs=4 | Business date captured for transaction timing. | sql:3, sql:15, sql:17, sql:19 |
| tbltransaction | FirstName | varchar(255) | n/a | 0.74 | SQL refs=1 | Human-readable name used in UI and reports. | sql:15 |
| tbltransaction | max(transactionid) | text | n/a | 0.74 | SQL refs=1 | Business meaning inferred from query usage; verify with SME. | sql:44 |
| tbltransaction | Status | varchar(255) | n/a | 0.74 | SQL refs=1 | Record lifecycle or approval state. | sql:3 |
| tbltransaction | TransactionID | integer | n/a | 0.78 | SQL refs=2 | Identifier key used to reference a business entity. | sql:17, sql:19 |
| tbltransaction | TransactionType | varchar(255) | n/a | 0.74 | SQL refs=2 | Business meaning inferred from query usage; verify with SME. | sql:3, sql:17 |

### E. Business Rules
| Rule ID | Form | Layer | Category | Business Meaning | Implementation Evidence | Risk links |
|---|---|---|---|---|---|---|
| BR-001 | n/a | Presentation | business_objective | User authentication is required before entering the workflow. | BANK objective inference | none |
| BR-002 | n/a | Presentation | workflow_orchestration | Workflow is orchestrated through UI event handlers and internal procedures. | BANK procedure map | none |
| BR-003 | n/a | Data | data_persistence | Form persists and retrieves records from the listed tables. | BANK SQL/table hints | none |
| BR-004 | Form1 | Data | threshold_rule | The workflow continues only when this condition is true: rs.RecordCount < 1. | BankApp1/Form1.frm:148 | none |
| BR-005 | n/a | Shared | input_validation | Input validation rule detected (IsNumeric/IsDate/Len). | BankApp1/Mdl.bas:89 | none |
| BR-006 | n/a | Shared | decision_branching | Keyboard input routing determines which action path is executed. | BankApp1/Mdl.bas:95 | none |
| BR-007 | n/a | Shared | threshold_rule | Input is restricted to numeric digits only. | BankApp1/Mdl.bas:179 | none |
| BR-008 | n/a | Shared | threshold_rule | The workflow continues only when this condition is true: rsTemp.RecordCount <> 0. | BankApp1/Mdl.bas:191 | none |
| BR-009 | account | Data | threshold_rule | The action proceeds only when matching records are found. | BankApp1/close account.frm:203 | none |
| BR-010 | frm8 | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | BankApp1/frm8.frm:39 | none |
| BR-012 | frmSplash | Data | calculation_logic | Splash/loading behavior advances progress state before opening workflow screens. | BankApp1/frmSplash.frm:210 | none |
| BR-013 | frmSplash | Data | threshold_rule | The workflow continues only when this condition is true: ProgressBar.Value = 1. | BankApp1/frmSplash.frm:211 | none |
| BR-014 | frmSplash | Data | threshold_rule | The workflow continues only when this condition is true: ProgressBar.Value = 100. | BankApp1/frmSplash.frm:218 | none |
| BR-021 | frmwith | Data | threshold_rule | The action proceeds only when matching records are found. | BankApp1/frmwith.frm:22 | RISK-017, RISK-018 |
| BR-023 | transaction | Data | threshold_rule | The action proceeds only when matching records are found. | BankApp1/transaction.frm:207 | none |
| BR-025 | BANK::frmSplash | Data | threshold_rule | The workflow continues only when this condition is true: ProgressBar.Value = 1. | mirrored_from_variant_mapping (source=BR-013) | none |
| BR-026 | BANK::frmSplash | Data | threshold_rule | The workflow continues only when this condition is true: ProgressBar.Value = 100. | mirrored_from_variant_mapping (source=BR-014) | none |
| BR-035 | (unmapped)::Form1 | Data | threshold_rule | The workflow continues only when this condition is true: rs.RecordCount < 1. | mirrored_from_variant_mapping (source=BR-004) | none |
| BR-052 | (unmapped)::frmtransaction | Data | threshold_rule | The action proceeds only when matching records are found. | mirrored_from_variant_mapping (source=BR-021) | RISK-007, RISK-008, RISK-009, RISK-010, RISK-011, RISK-012 |
| BR-053 | (unmapped)::frmwith | Data | threshold_rule | The action proceeds only when matching records are found. | mirrored_from_variant_mapping (source=BR-021) | RISK-017, RISK-018 |
| BR-056 | BANK::frmaddinterest | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | variant_backfill_for_eq_sync (source=BR-011); source_rule=BR-011 | RISK-001 |
| BR-057 | BANK::frmcloseacount | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | variant_backfill_for_eq_sync (source=BR-011); source_rule=BR-011 | none |
| BR-058 | BANK::frmcustomer | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | variant_backfill_for_eq_sync (source=BR-011); source_rule=BR-011 | RISK-021, RISK-022 |
| BR-059 | BANK::frmmonthlyreport | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | variant_backfill_for_eq_sync (source=BR-020); source_rule=BR-020 | RISK-021, RISK-022 |
| BR-060 | BANK::frmsettings | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | variant_backfill_for_eq_sync (source=BR-011); source_rule=BR-011 | RISK-019 |
| BR-061 | (unmapped)::frmcheckbalance | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | variant_backfill_for_eq_sync (source=BR-011); source_rule=BR-011 | RISK-006, RISK-015 |
| BR-062 | (unmapped)::frmcloseaccount | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | variant_backfill_for_eq_sync (source=BR-011); source_rule=BR-011 | none |
| BR-063 | (unmapped)::frminterest | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | variant_backfill_for_eq_sync (source=BR-015); source_rule=BR-015 | RISK-020, RISK-023, RISK-024, RISK-025 |
| BR-064 | (unmapped)::frmreport | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | variant_backfill_for_eq_sync (source=BR-011); source_rule=BR-011 | none |
| BR-065 | (unmapped)::Mdi | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | variant_backfill_for_eq_sync (source=BR-011); source_rule=BR-011 | none |

### E1. Rule Cross-Reference by Form
- a: rule_ids=[BR-001, BR-002, BR-003, BR-005, BR-006, BR-007, BR-008]; summary=User authentication is required before entering the workflow. / Workflow is orchestrated through UI event handlers and internal procedures. / Form persists and retrieves records from the listed tables.
- account: rule_ids=[BR-009]; summary=The action proceeds only when matching records are found.
- form1: rule_ids=[BR-004, BR-035]; summary=Captures date 1, date 2. / The workflow continues only when this condition is true: rs.RecordCount < 1.
- frm8: rule_ids=[BR-010]; summary=Balance is recalculated using the entered amount and current account value.
- frmaddinterest: rule_ids=[BR-010, BR-056]; summary=Customer profile onboarding and maintenance workflow Captures account no, currentdate, date, month, year. Business outcome: Customer profile created or updated.; Navigation routes the user to selected module screens.. / Balance is recalculated using the entered amount and current account value.
- frmcheckbalance: rule_ids=[BR-010, BR-061]; summary=Customer profile onboarding and maintenance workflow Captures ac no, account no, contacttitle, customer id, date 1, firstname. Business outcome: Customer profile created or updated.; Navigation routes the user to selected module screens.. / Balance is recalculated using the entered amount and current account value.
- frmcloseaccount: rule_ids=[BR-010, BR-062]; summary=Customer profile onboarding and maintenance workflow Business outcome: Customer profile created or updated.; Navigation routes the user to selected module screens.. / Balance is recalculated using the entered amount and current account value.
- frmcloseacount: rule_ids=[BR-010, BR-057]; summary=Customer profile onboarding and maintenance workflow Captures account no, address, balance, cust id, customer id, dateofopen. Business outcome: Customer profile created or updated.; Navigation routes the user to selected module screens.. / Balance is recalculated using the entered amount and current account value.
- frmcustomer: rule_ids=[BR-010, BR-058]; summary=Customer profile onboarding and maintenance workflow Captures account no, address, balance, customer id, dateofopen, dob. Business outcome: Customer profile created or updated.; Matching records displayed to the user.; Navigation routes the user to selected module screens.. / Balance is recalculated using the entered amount and current account value.
- frminterest: rule_ids=[BR-010, BR-063]; summary=Transaction ledger management and adjustment workflow Business outcome: Transaction history updated.. / Balance is recalculated using the entered amount and current account value.
- frmmonthlyreport: rule_ids=[BR-010, BR-059]; summary=Operational reporting and statement generation workflow Captures customer id, from, to. Business outcome: Customer profile created or updated.. / Balance is recalculated using the entered amount and current account value.
- frmreport: rule_ids=[BR-010, BR-064]; summary=Operational reporting and statement generation workflow Captures account, account no, balance, customer id, first name, from date. Business outcome: Customer profile created or updated.. / Balance is recalculated using the entered amount and current account value.
- frmsettings: rule_ids=[BR-010, BR-060]; summary=Customer profile onboarding and maintenance workflow Captures account id, accounttype, cheque, interestrate, nocheque. Business outcome: Customer profile created or updated.; Account type master data maintained.; Navigation routes the user to selected module screens.. / Balance is recalculated using the entered amount and current account value.
- frmsplash: rule_ids=[BR-010, BR-012, BR-013, BR-014, BR-025, BR-026]; summary=Splash/loading behavior advances progress state before opening workflow screens. / The workflow continues only when this condition is true: ProgressBar.Value = 1. / The workflow continues only when this condition is true: ProgressBar.Value = 100.
- frmtransaction: rule_ids=[BR-010, BR-052]; summary=Transaction ledger management and adjustment workflow Captures acc no, option 1, option 2. Business outcome: Transaction history updated.. / Balance is recalculated using the entered amount and current account value. / The action proceeds only when matching records are found.
- frmwith: rule_ids=[BR-021, BR-053]; summary=Transaction ledger management and adjustment workflow Business outcome: Transaction history updated.. / The action proceeds only when matching records are found.
- mdi: rule_ids=[BR-010, BR-065]; summary=Customer profile onboarding and maintenance workflow Business outcome: Customer profile created or updated.; Navigation routes the user to selected module screens.. / Balance is recalculated using the entered amount and current account value.
- transaction: rule_ids=[BR-010, BR-023]; summary=Balance is recalculated using the entered amount and current account value. / The action proceeds only when matching records are found.

### E2. Shared Rule Consolidation
- BR-010: consolidated 35 duplicate row(s); applies to 17 form(s): (unmapped)::Mdi, (unmapped)::frmcheckbalance, (unmapped)::frmcloseaccount, (unmapped)::frminterest, (unmapped)::frmreport, (unmapped)::frmtransaction, BANK::frmSplash, BANK::frmaddinterest, BANK::frmcloseacount, BANK::frmcustomer, BANK::frmmonthlyreport, BANK::frmsettings(+5 more)
  - Canonical meaning: Balance is recalculated using the entered amount and current account value.

### F. Detector Findings
| Detector | Severity | Count | Summary | Required actions |
|---|---|---:|---|---|
| VB6-OOP-007 | medium | 7 | BankApp1/frmADDINTEREST.frm: default instance references | default_instance_refactor_plan |
| VB6-UI-002 | medium | 12 | BankApp1/frmLogin.frm: control array index markers | ui_migration_strategy |

### G. Artifact Index
| Type | Ref |
|---|---|
| legacy_inventory | artifact://legacy_inventory/1.0/art_legacy_inventory_6497943d0e994a18 |
| repo_landscape | artifact://repo_landscape/1.0/art_repo_landscape_529be8ef2e524dc4 |
| scope_lock | artifact://scope_lock/1.0/art_scope_lock_ee367f5339f447bf |
| variant_inventory | artifact://variant_inventory/1.0/art_variant_inventory_3d458518c0df4071 |
| dependency_inventory | artifact://dependency_inventory/1.0/art_dependency_inventory_2a14b5bff4164996 |
| event_map | artifact://event_map/1.0/art_event_map_075b85a351b34e59 |
| sql_catalog | artifact://sql_catalog/1.0/art_sql_catalog_91dda5b5cc9d4319 |
| sql_map | artifact://sql_map/1.0/art_sql_map_1f882973513e42f0 |
| data_access_map | artifact://data_access_map/1.0/art_data_access_map_0194a147d8484a33 |
| recordset_ops | artifact://recordset_ops/1.0/art_recordset_ops_ddf728ac8eff486f |
| procedure_summary | artifact://procedure_summary/1.0/art_procedure_summary_625b8aa9fe034a9f |
| form_dossier | artifact://form_dossier/1.0/art_form_dossier_2180960dc3c3477e |
| business_rule_catalog | artifact://business_rule_catalog/1.0/art_business_rule_catalog_9d6d15fcec094883 |
| detector_findings | artifact://detector_findings/1.0/art_detector_findings_aed6f266e6354ada |
| risk_register | artifact://risk_register/1.0/art_risk_register_b6df874a9ce94658 |
| orphan_analysis | artifact://orphan_analysis/1.0/art_orphan_analysis_20bc082132654a47 |
| project_metrics | artifact://project_metrics/1.0/art_project_metrics_aa7a31f1477b4b3a |
| static_forensics_layer | artifact://static_forensics_layer/1.0/art_static_forensics_layer_09c2423885e8425b |
| type_metrics | artifact://type_metrics/1.0/art_type_metrics_4ff28beadc454d71 |
| type_dependency_matrix | artifact://type_dependency_matrix/1.0/art_type_dependency_matrix_51b646f5c8fb4e72 |
| runtime_dependency_matrix | artifact://runtime_dependency_matrix/1.0/art_runtime_dependency_matrix_ac5d62dfb91b4608 |
| dead_code_report | artifact://dead_code_report/1.0/art_dead_code_report_ded81e0642264cb2 |
| third_party_usage | artifact://third_party_usage/1.0/art_third_party_usage_4080a17fa1f34089 |
| code_quality_rules | artifact://code_quality_rules/1.0/art_code_quality_rules_498213c519a5440d |
| quality_violation_report | artifact://quality_violation_report/1.0/art_quality_violation_report_23554efe86a14830 |
| trend_snapshot | artifact://trend_snapshot/1.0/art_trend_snapshot_db969eb8c7d14bca |
| trend_series | artifact://trend_series/1.0/art_trend_series_ef815f8517974ec1 |
| mdb_inventory | artifact://mdb_inventory/1.0/art_mdb_inventory_35c453e614db4fd6 |
| form_loc_profile | artifact://form_loc_profile/1.0/art_form_loc_profile_d24aedcd5ca64e4d |
| connection_string_variants | artifact://connection_string_variants/1.0/art_connection_string_variants_551e9ee4b182459b |
| module_global_inventory | artifact://module_global_inventory/1.0/art_module_global_inventory_759545a35a704225 |
| dead_form_refs | artifact://dead_form_refs/1.0/art_dead_form_refs_9ef7af5f57034ae7 |
| dataenvironment_report_mapping | artifact://dataenvironment_report_mapping/1.0/art_dataenvironment_report_mapping_e76acd11c6c8419e |
| static_risk_detectors | artifact://static_risk_detectors/1.0/art_static_risk_detectors_a815612b3c314a24 |
| delivery_constitution | artifact://delivery_constitution/1.0/art_delivery_constitution_a398a6466a894222 |
| source_db_profile | artifact://source_db_profile/1.0/art_source_db_profile_79e6f5b526ac498b |
| source_schema_model | artifact://source_schema_model/1.0/art_source_schema_model_0628100daf0143a2 |
| source_query_catalog | artifact://source_query_catalog/1.0/art_source_query_catalog_d91881bbf6de4764 |
| source_relationship_candidates | artifact://source_relationship_candidates/1.0/art_source_relationship_candidates_7f5f9ee107b948d3 |
| source_data_dictionary | artifact://source_data_dictionary/1.0/art_source_data_dictionary_7606e413b54f4a53 |
| source_data_dictionary_markdown | artifact://source_data_dictionary_markdown/1.0/art_source_data_dictionary_markdown_6362505897044f6c |
| source_erd | artifact://source_erd/1.0/art_source_erd_01065c355f404f78 |
| source_hotspot_report | artifact://source_hotspot_report/1.0/art_source_hotspot_report_d897a5aa2bb34b2c |
| target_schema_model | artifact://target_schema_model/1.0/art_target_schema_model_3d7a61d2a1274f4c |
| target_erd | artifact://target_erd/1.0/art_target_erd_1ebc5f53964c4fec |
| target_data_dictionary | artifact://target_data_dictionary/1.0/art_target_data_dictionary_7b73c0497906420d |
| schema_mapping_matrix | artifact://schema_mapping_matrix/1.0/art_schema_mapping_matrix_d55b1bb82e514eac |
| migration_plan | artifact://migration_plan/1.0/art_migration_plan_6ecba06998bd468d |
| validation_harness_spec | artifact://validation_harness_spec/1.0/art_validation_harness_spec_c85b2f8dc4374171 |
| db_qa_report | artifact://db_qa_report/1.0/art_db_qa_report_278d170211b14b8a |
| schema_approval_record | artifact://schema_approval_record/1.0/art_schema_approval_record_c8809010527e4629 |
| schema_drift_report | artifact://schema_drift_report/1.0/art_schema_drift_report_5c4f772381a545b1 |
| variant_diff_report | artifact://variant_diff_report/1.0/art_variant_diff_report_13e2c11fb39f47ab |
| reporting_model | artifact://reporting_model/1.0/art_reporting_model_06daa13038294c0a |
| identity_access_model | artifact://identity_access_model/1.0/art_identity_access_model_c1d0546453af44ec |
| discover_review_checklist | artifact://discover_review_checklist/1.0/art_discover_review_checklist_8a204bc40e3647d3 |

### H. SQL Map
| Form | Procedure | Operation | Tables | Risks | activex_trigger | trace_complete |
|---|---|---|---|---|---|---|
| BANK::frmWithinDate | cmdPrint_Click | unknown | n/a | none | n/a | no |
| BANK::frmWithinDate | cmdPrint_Click | unknown | n/a | none | n/a | no |
| BANK::frmWithinDate | cmdPrint_Click | select | n/a | none | n/a | no |
| shared_module | Numeric | select | n/a | none | n/a | no |
| shared_module | Numeric | unknown | n/a | none | n/a | no |
| shared_module | ValidNonNumeric | select | n/a | none | n/a | no |
| shared_module | ValidNonNumeric | unknown | n/a | none | n/a | no |
| shared_module | ValidNumeric | select | n/a | none | n/a | no |
| shared_module | ValidNumeric | unknown | n/a | none | n/a | no |
| (unmapped)::frmcheckbalance [Customer Management] | cboaccountno_click | select | tblCustomers | select_star | n/a | yes |
| (unmapped)::frmcheckbalance [Customer Management] | cboaccountno_click | select | tblcustomers | select_star, string_concatenation, possible_injection | n/a | yes |
| BANK::frmcustomer [Customer Management] | txtcustomerid_LostFocus | select | tblcustomers | select_star | n/a | yes |
| BANK::frmcustomer [Customer Management] | txtcustomerid_LostFocus | select | tblcustomers | select_star, string_concatenation, possible_injection | n/a | yes |
| (unmapped)::frminterest [Transaction Ledger] | GenerateNewTransactCode | select | tbltransaction | none | n/a | yes |
| (unmapped)::frminterest [Transaction Ledger] | cmdCalculateInterest_Click | select | tbltransaction | select_star | n/a | yes |
| (unmapped)::frminterest [Transaction Ledger] | cmdCalculateInterest_Click | select | tblcustomers | select_star | n/a | yes |
| (unmapped)::frminterest [Transaction Ledger] | cmdCalculateInterest_Click | select | tbltransaction | select_star | n/a | yes |
| (unmapped)::frminterest [Transaction Ledger] | cmdCalculateInterest_Click | select | tbltransaction | select_star, string_concatenation, possible_injection | n/a | yes |
| (unmapped)::frminterest [Transaction Ledger] | cmdCalculateInterest_Click | insert | tbltransaction | none | n/a | yes |
| (unmapped)::frminterest [Transaction Ledger] | cmdCalculateInterest_Click | unknown | n/a | none | n/a | no |
| BANK::frmmonthlyreport [Customer Management] | cmdShow_Click | unknown | n/a | none | n/a | no |
| BANK::frmmonthlyreport [Customer Management] | cmdShow_Click | unknown | n/a | none | n/a | no |
| BANK::frmmonthlyreport [Customer Management] | cmdShow_Click | unknown | Date | none | n/a | yes |
| BANK::frmmonthlyreport [Customer Management] | cmdShow_Click | unknown | Date | none | n/a | yes |
| BANK::frmmonthlyreport [Customer Management] | cmdShow_Click | select | tblcustomers | select_star | n/a | yes |
| BANK::frmmonthlyreport [Customer Management] | cmdShow_Click | select | tblcustomers | select_star, string_concatenation, possible_injection | n/a | yes |
| BANK::frmsettings [Customer Management] | DisplayCustomers | select | tblaccount | select_star | n/a | yes |
| BANK::frmsettings [Customer Management] | cmdsave_Click | unknown | n/a | none | n/a | no |
| (unmapped)::frmwith [Transaction Ledger] | cmdOk_Click | select | tbltransaction | select_star | n/a | yes |
| (unmapped)::frmwith [Transaction Ledger] | cmdOk_Click | select | tbltransaction | select_star | n/a | yes |
| (unmapped)::frmtransaction [Transaction Entry] | cboAccNo_Click | select | tblcustomers | select_star | n/a | yes |
| (unmapped)::frmtransaction [Transaction Entry] | cboAccNo_Click | select | tblcustomers | select_star, string_concatenation, possible_injection | n/a | yes |
| (unmapped)::frmtransaction [Transaction Entry] | cboAccNo_Click | select | tblTransaction | select_star | n/a | yes |
| (unmapped)::frmtransaction [Transaction Entry] | cboAccNo_Click | select | tblTransaction | select_star, string_concatenation, possible_injection | n/a | yes |
| (unmapped)::frmtransaction [Transaction Entry] | cboCustomerID_Click | select | tblCustomers | select_star | n/a | yes |
| (unmapped)::frmtransaction [Transaction Entry] | cboCustomerID_Click | select | tblCustomers | select_star, string_concatenation, possible_injection | n/a | yes |
| (unmapped)::frmtransaction [Transaction Entry] | cboCustomerID_Click | select | tblTransaction | select_star | n/a | yes |
| (unmapped)::frmtransaction [Transaction Entry] | cboCustomerID_Click | select | tblTransaction | select_star, string_concatenation, possible_injection | n/a | yes |
| (unmapped)::frmtransaction [Transaction Entry] | cboFirst_Click | select | tblCustomers | select_star, string_concatenation, possible_injection | n/a | yes |
| (unmapped)::frmtransaction [Transaction Entry] | cboFirst_Click | select | tblCustomers | select_star, string_concatenation, possible_injection | n/a | yes |
| (unmapped)::frmtransaction [Transaction Entry] | cboFirst_Click | select | tblTransaction | select_star | n/a | yes |
| (unmapped)::frmtransaction [Transaction Entry] | cboFirst_Click | select | tblTransaction | select_star, string_concatenation, possible_injection | n/a | yes |

### I. Handler and Procedure Summaries
| Callable | Kind | Form | SQL IDs | Steps | Risks | Source line refs |
|---|---|---|---|---|---|---|
| Form_Load | event_handler | BANK::frmWithinDate | n/a | Triggered from Form Load. / Invokes procedures: DTPicker2. | none | BankApp1/Form1.frm:178 |
| cmdPrint_Click | event_handler | BANK::frmWithinDate | sql:10, sql:7, sql:43 | Triggered from cmdPrint Click. / Invokes procedures: MsgBox, Exit, DonorReport. | none | BankApp1/Form1.frm:125 |
| cmdGenerate_Click | event_handler | BANK::frmdaily | n/a | Triggered from cmdGenerate Click. / Invokes procedures: str1, deBank, rptdaily, Unload. | none | BankApp1/Form9.frm:60 |
| cmdexit_Click | event_handler | BANK::frmWithinDate | n/a | Triggered from cmdexit Click. / Invokes procedures: Unload. | none | BankApp1/Form9.frm:56 |
| CheckDatabaseStatus | shared_function | shared_module | n/a | Triggered from CheckDatabaseStatus. / Invokes procedures: MsgBox, Exit. | none | BankApp1/Mdl.bas:126 |
| Lock_Form_Controls | shared_function | shared_module | n/a | Triggered from Lock Form_Controls. / Invokes procedures: ctrl. | none | BankApp1/Mdl.bas:59, BankApp1/Mdl.bas:70 |
| Messager | shared_function | shared_module | n/a | Triggered from Messager. / Invokes procedures: MsgBox. | none | BankApp1/Mdl.bas:175 |
| MoveToFirst | shared_function | shared_module | n/a | Triggered from MoveToFirst. / Runs in form context shared_module. | none | BankApp1/Mdl.bas:134 |
| MoveToLast | shared_function | shared_module | n/a | Triggered from MoveToLast. / Invokes procedures: CheckDatabaseStatus, MsgBox, Exit. | none | BankApp1/Mdl.bas:162 |
| MoveToNext | shared_function | shared_module | n/a | Triggered from MoveToNext. / Invokes procedures: CheckDatabaseStatus, MsgBox, Exit. | none | BankApp1/Mdl.bas:150 |
| MoveToPrev | shared_function | shared_module | n/a | Triggered from MoveToPrev. / Invokes procedures: CheckDatabaseStatus, MsgBox, Exit. | none | BankApp1/Mdl.bas:137 |
| Numeric | shared_function | shared_module | sql:34, sql:2 | Triggered from Numeric. / Invokes procedures: KeyAscii, MsgBox. | none | BankApp1/Mdl.bas:201, BankApp1/Mdl.bas:93, BankApp1/Mdl.bas:108 |
| UnLock_Form_Controls | shared_function | shared_module | n/a | Triggered from UnLock Form_Controls. / Invokes procedures: ctrl. | none | BankApp1/Mdl.bas:70 |
| ValidNonNumeric | shared_function | shared_module | sql:34, sql:2 | Triggered from ValidNonNumeric. / Invokes procedures: KeyAscii, MsgBox. | none | BankApp1/Mdl.bas:93 |
| ValidNumeric | shared_function | shared_module | sql:34, sql:2 | Triggered from ValidNumeric. / Invokes procedures: KeyAscii, MsgBox. | none | BankApp1/Mdl.bas:108 |
| clear_form_controls | shared_function | shared_module | n/a | Triggered from clear form_controls. / Invokes procedures: ctrl. | none | BankApp1/Mdl.bas:45 |
| connectDatabase | shared_function | shared_module | n/a | Triggered from connectDatabase. / Invokes procedures: rscustomers, rsTransaction, rsAccount. | none | BankApp1/Mdl.bas:21, BankApp1/Mdl.bas:84 |
| disconnectDatabase | shared_function | shared_module | n/a | Triggered from disconnectDatabase. / Invokes procedures: cnBank. | none | BankApp1/Mdl.bas:84 |
| selectTextControl | shared_function | shared_module | n/a | Triggered from selectTextControl. / Invokes procedures: txtCtrl. | none | BankApp1/Mdl.bas:87 |
| cboaccountno_click | event_handler | (unmapped)::frmcheckbalance | sql:21, sql:30 | Triggered from cboaccountno click. / Invokes procedures: rstemp, txtcustomerid, txtcontacttitle, txtfirstname, txtlastname, txttypeofaccount. | select_star, string_concatenation, possible_injection | BankApp1/close account.frm:195 |
| cmdOK_Click | event_handler | BANK::frmLogin1 | n/a | Triggered from cmdOK Click. / Invokes procedures: fnd, rs, Exit, Load, menu, Visible. | none | BankApp1/frmLogin.frm:87 |
| cmdcancel_Click | event_handler | BANK::frmwithdraw | n/a | Triggered from cmdcancel Click. / Invokes procedures: Unload. | none | BankApp1/frmLogin.frm:83 |
| Form_KeyPress | event_handler | BANK::frmSplash | n/a | Triggered from Form KeyPress. / Invokes procedures: frmlogin. | none | BankApp1/frmSplash.frm:191 |
| Frame1_Click | event_handler | BANK::frmwithdraw | n/a | Triggered from Frame1 Click. / Invokes procedures: frmLogin1. | none | BankApp1/frmSplash.frm:204 |
| Timer1_Timer | event_handler | BANK::frmSplash | n/a | Triggered from Timer1 Timer. / Invokes procedures: ProgressBar, lbldisplay, Unload, frmLogin1. | none | BankApp1/frmSplash.frm:209 |
| Timer_Timer | event_handler | BANK::frmSplash | n/a | Triggered from Timer Timer. / Runs in form context BANK::frmSplash. | none | BankApp1/frmSplash.frm:200 |
| txtcustomerid_LostFocus | event_handler | BANK::frmcustomer | sql:39, sql:38 | Triggered from txtcustomerid LostFocus. / Invokes procedures: MsgBox, txtcustomerid, Exit, find_str, rscustomers, txtaccountno. | select_star, string_concatenation, possible_injection | BankApp1/frmcloseaccount.frm:32 |
| GenerateNewTransactCode | procedure | (unmapped)::frminterest | sql:44 | Triggered from GenerateNewTransactCode. / Invokes procedures: cnBank, sql, rs, TransactionID. | none | BankApp1/frminterest.frm:150 |
| cmdCalculateInterest_Click | event_handler | (unmapped)::frminterest | sql:40, sql:37, sql:42, sql:41, sql:3, sql:1 | Triggered from cmdCalculateInterest Click. / Invokes procedures: ans, Exit, TodayDate, mdate, transaction, findAccStr. | select_star, string_concatenation, possible_injection | BankApp1/frminterest.frm:49 |
| MDIForm_Load | event_handler | BANK::menu | n/a | Triggered from MDIForm Load. / Invokes procedures: connectDatabase. | none | BankApp1/frmmdi.frm:70 |
| MDIForm_QueryUnload | event_handler | BANK::menu | n/a | Triggered from MDIForm QueryUnload. / Invokes procedures: i, Cancel. | none | BankApp1/frmmdi.frm:155 |
| mnuDepositAmount_Click | event_handler | BANK::menu | n/a | Triggered from mnuDepositAmount Click. / Invokes procedures: frmdeposit. | none | BankApp1/frmmdi.frm:90 |
| mnuNewAccount_Click | event_handler | BANK::menu | n/a | Triggered from mnuNewAccount Click. / Invokes procedures: frmnewaccount. | none | BankApp1/frmmdi.frm:97 |
| mnuUpdatedetails_Click | event_handler | BANK::menu | n/a | Triggered from mnuUpdatedetails Click. / Invokes procedures: frmupdate. | none | BankApp1/frmmdi.frm:101 |
| mnuWithdrawAmount_Click | event_handler | BANK::menu | n/a | Triggered from mnuWithdrawAmount Click. / Invokes procedures: frmwithdraw. | none | BankApp1/frmmdi.frm:137 |
| mnuaccountdetails_Click | event_handler | BANK::menu | n/a | Triggered from mnuaccountdetails Click. / Invokes procedures: frmAccountdetails. | none | BankApp1/frmmdi.frm:62 |
| mnuaddinterest_Click | event_handler | BANK::menu | n/a | Triggered from mnuaddinterest Click. / Invokes procedures: frmaddinterest. | none | BankApp1/frmmdi.frm:78 |
| mnucheckbalance_Click | event_handler | BANK::menu | n/a | Triggered from mnucheckbalance Click. / Invokes procedures: frmaddinterest. | none | BankApp1/frmmdi.frm:66 |
| mnuclose_Click | event_handler | BANK::menu | n/a | Triggered from mnuclose Click. / Invokes procedures: frmcloseacount. | none | BankApp1/frmmdi.frm:82 |
| mnucustomerdetails_Click | event_handler | BANK::menu | n/a | Triggered from mnucustomerdetails Click. / Invokes procedures: frmcustomer. | none | BankApp1/frmmdi.frm:86 |
| mnudeposits_Click | event_handler | BANK::menu | n/a | Triggered from mnudeposits Click. / Invokes procedures: rpttransaction. | none | BankApp1/frmmdi.frm:117 |
| mnuexitmdifrm_Click | event_handler | BANK::menu | n/a | Triggered from mnuexitmdifrm Click. / Invokes procedures: i, Cancel. | none | BankApp1/frmmdi.frm:142 |
| mnuinterest_Click | event_handler | BANK::menu | n/a | Triggered from mnuinterest Click. / Invokes procedures: frminterest. | none | BankApp1/frmmdi.frm:124 |
| mnumonthly_Click | event_handler | BANK::menu | n/a | Triggered from mnumonthly Click. / Invokes procedures: frmWithinDate. | none | BankApp1/frmmdi.frm:105 |
| mnustatement_Click | event_handler | BANK::menu | n/a | Triggered from mnustatement Click. / Invokes procedures: frmstatement. | none | BankApp1/frmmdi.frm:128 |
| mnutransaction_Click | event_handler | BANK::menu | n/a | Triggered from mnutransaction Click. / Invokes procedures: rpttransaction. | none | BankApp1/frmmdi.frm:109 |
| mnutransactionreport_Click | event_handler | BANK::menu | n/a | Triggered from mnutransactionreport Click. / Invokes procedures: frmtransaction. | none | BankApp1/frmmdi.frm:113 |
| mnuviewtransaction_Click | event_handler | BANK::menu | n/a | Triggered from mnuviewtransaction Click. / Invokes procedures: frmtransaction. | none | BankApp1/frmmdi.frm:133 |
| mnuwithdrawn_Click | event_handler | BANK::menu | n/a | Triggered from mnuwithdrawn Click. / Invokes procedures: rptWithdrawals. | none | BankApp1/frmmdi.frm:168 |
| cmbaccountno_KeyPress | event_handler | BANK::frmmonthlyreport | n/a | Triggered from cmbaccountno KeyPress. / Invokes procedures: Numeric. | none | BankApp1/frmmonthlyreport.frm:139 |
| cmbcustomerid_KeyPress | event_handler | BANK::frmmonthlyreport | n/a | Triggered from cmbcustomerid KeyPress. / Invokes procedures: ValidNumeric. | none | BankApp1/frmmonthlyreport.frm:143 |
| cmdShow_Click | event_handler | BANK::frmmonthlyreport | sql:6, sql:11, sql:5, sql:9, sql:39, sql:38 | Triggered from cmdShow Click. / Invokes procedures: MsgBox, cmbcustomerid, Exit, DTPTo, DTPFrom, find_str. | select_star, string_concatenation, possible_injection | BankApp1/frmmonthlyreport.frm:151 |
| Control | procedure | BANK::frmsettings | n/a | Triggered from Control. / Invokes procedures: txtaccountid, txtaccounttype, txtcheque, txtnocheque, txtinterestrate. | none | BankApp1/frmsettings.frm:327 |
| DisplayCustomers | procedure | BANK::frmsettings | sql:36 | Triggered from DisplayCustomers. / Invokes procedures: cheque, rsfind, txtaccountid, txtaccounttype, txtcheque, txtnocheque. | select_star | BankApp1/frmsettings.frm:314 |
| cmdCancel_Click | event_handler | BANK::frmwithdraw | n/a | Triggered from cmdCancel Click. / Invokes procedures: cmdedit, cmdsave, rsfind, NewRecord, DisplayCustomers, Lock_Form_Controls. | none | BankApp1/frmsettings.frm:227 |
| cmdedit_Click | event_handler | BANK::frmsettings | n/a | Triggered from cmdedit Click. / Invokes procedures: cmdedit, cmdsave, cmdcancel, Control. | none | BankApp1/frmsettings.frm:246 |
| cmdsave_Click | event_handler | BANK::frmsettings | sql:1 | Triggered from cmdsave Click. / Invokes procedures: MsgBox, txtaccountid, Exit, txtcheque, txtnocheque, txtinterestrate. | none | BankApp1/frmsettings.frm:258 |
| txtcheque_KeyPress | event_handler | BANK::frmsettings | n/a | Triggered from txtcheque KeyPress. / Invokes procedures: ValidNumeric. | none | BankApp1/frmsettings.frm:340 |
| txtinterestrate_KeyPress | event_handler | BANK::frmsettings | n/a | Triggered from txtinterestrate KeyPress. / Invokes procedures: ValidNumeric. | none | BankApp1/frmsettings.frm:344 |
| txtnocheque_KeyPress | event_handler | BANK::frmsettings | n/a | Triggered from txtnocheque KeyPress. / Invokes procedures: ValidNumeric. | none | BankApp1/frmsettings.frm:348 |
| cmdExit_Click | event_handler | BANK::frmwithdraw | n/a | Triggered from cmdExit Click. / Invokes procedures: Unload. | none | BankApp1/frmstatement.frm:22 |
| cmdOk_Click | event_handler | (unmapped)::frmwith | sql:33, sql:32 | Triggered from cmdOk Click. / Invokes procedures: rstemp, lvwTransactions, LoadListView, MsgBox, Exit. | select_star | BankApp1/frmwith.frm:18 |
| mnuExit_Click | event_handler | BANK::menu | n/a | Triggered from mnuExit Click. / Invokes procedures: Unload. | none | BankApp1/menu.frm:115 |
| mnubetween_Click | event_handler | BANK::menu | n/a | Triggered from mnubetween Click. / Invokes procedures: frmWithinDate. | none | BankApp1/menu.frm:80 |
| mnucustomermonthly_Click | event_handler | BANK::menu | n/a | Triggered from mnucustomermonthly Click. / Invokes procedures: frmmonthlyreport. | none | BankApp1/menu.frm:92 |
| mnugiveinterest_Click | event_handler | BANK::menu | n/a | Triggered from mnugiveinterest Click. / Invokes procedures: frmaddinterest. | none | BankApp1/menu.frm:119 |
| mnusettings_Click | event_handler | BANK::menu | n/a | Triggered from mnusettings Click. / Invokes procedures: frmsettings. | none | BankApp1/menu.frm:123 |
| Command1_Click | event_handler | (unmapped)::frmtransaction | n/a | Triggered from Command1 Click. / Runs in form context (unmapped)::frmtransaction. | none | BankApp1/transaction.frm:371 |
| cboAccNo_Click | event_handler | (unmapped)::frmtransaction | sql:31, sql:30, sql:27, sql:26 | Triggered from cboAccNo Click. / Invokes procedures: rsTemp, cboAccNo, cboCustomerID, cboFirst, MsgBox, Exit. | select_star, string_concatenation, possible_injection | BankApp1/transaction.frm:203 |
| cboCustomerID_Click | event_handler | (unmapped)::frmtransaction | sql:23, sql:22, sql:29, sql:28 | Triggered from cboCustomerID Click. / Invokes procedures: fradate, rsTemp, cboAccNo, cboCustomerID, cboFirst, MsgBox. | select_star, string_concatenation, possible_injection | BankApp1/transaction.frm:238 |
| cboFirst_Click | event_handler | (unmapped)::frmtransaction | sql:25, sql:24, sql:29, sql:28 | Triggered from cboFirst Click. / Invokes procedures: fradate, rsTemp, cboAccNo, cboCustomerID, cboFirst, MsgBox. | select_star, string_concatenation, possible_injection | BankApp1/transaction.frm:268 |
| cmdPrintAll_Click | event_handler | (unmapped)::frmtransaction | n/a | Triggered from cmdPrintAll Click. / Invokes procedures: rptstatement. | none | BankApp1/transaction.frm:355 |
| cmdQuit_Click | event_handler | BANK::frmcloseacount | n/a | Triggered from cmdQuit Click. / Invokes procedures: Unload. | none | BankApp1/transaction.frm:361 |
| cmdRefresh_Click | event_handler | (unmapped)::frmtransaction | n/a | Triggered from cmdRefresh Click. / Invokes procedures: lvwTransactions. | none | BankApp1/transaction.frm:366 |

### J. Delivery Constitution
- Preserve critical legacy behavior first; modernization must prove functional equivalence.
- Every modernization decision must map to explicit evidence (code, query, event, or rule).
- No breaking change to data contracts without approved migration path and rollback evidence.

### K. Form Dossiers
| Form | Display Name | Project | form_type | Status | Purpose | Inputs (data) | Outputs (effects) | ActiveX used | DB tables | Actions | Coverage | Confidence | Exclusion reason |
|---|---|---|---|---|---|---|---|---|---|---:|---:|---:|---|
| Form1 | Form1 [Navigation/Menu] | n/a | Child | mapped | Business workflow executed through event-driven UI controls. | date 1, date 2 | n/a | MSComCtl2.DTPicker | n/a | 0 | 1.00 | 0.31 | none |
| frmcheckbalance | frmcheckbalance [Customer Management] | n/a | Child | mapped | Customer profile onboarding and maintenance workflow. | ac no, account no, contacttitle, customer id, date 1, firstname, lastname, typeofaccount | Customer details displayed for review. | MSComCtl2.DTPicker | tblCustomers, tblcustomers | 1 | 1.00 | 0.92 | none |
| frmcloseaccount | frmcloseaccount [Customer Management] | n/a | Child | mapped | Customer profile onboarding and maintenance workflow. | n/a | Customer profile created or updated., Navigation routes the user to selected module screens. | n/a | n/a | 0 | 1.00 | 0.47 | none |
| frmdep | frmdep | n/a | Child | mapped | Business workflow executed through event-driven UI controls. | n/a | n/a | n/a | n/a | 0 | 1.00 | 0.26 | none |
| frmExpireItemsWithinDate | frmExpireItemsWithinDate | n/a | Child | mapped | Business workflow executed through event-driven UI controls. | n/a | n/a | MSComCtl2.DTPicker | n/a | 0 | 1.00 | 0.26 | none |
| frminterest | frminterest [Transaction Ledger] | n/a | Child | mapped | Transaction ledger management and adjustment workflow. | n/a | Customer details displayed for review., Transaction history displayed for review., Transaction ledger updated. | MSComctlLib.ListView | tblcustomers, tbltransaction | 2 | 1.00 | 0.90 | none |
| frmMonthly | frmMonthly [Reporting] | n/a | Child | mapped | Operational reporting and statement generation workflow. | report | n/a | n/a | n/a | 0 | 1.00 | 0.51 | none |
| frmreport | frmreport [Customer Management] | n/a | Child | mapped | Operational reporting and statement generation workflow. | account, account no, balance, customer id, first name, from date, last name, to date | Customer profile created or updated. | MSComCtl2.DTPicker | n/a | 0 | 1.00 | 0.51 | none |
| frmtransaction | frmtransaction [Transaction Entry] | n/a | Child | mapped | Transaction ledger management and adjustment workflow. | acc no, customer id, first, option 1, option 2 | Customer details displayed for review., Transaction history displayed for review. | MSComctlLib.ListView | tblCustomers, tblTransaction, tblcustomers | 6 | 1.00 | 0.98 | none |
| frmwith | frmwith [Transaction Ledger] | n/a | Child | mapped | Transaction ledger management and adjustment workflow. | n/a | Transaction history displayed for review. | n/a | tbltransaction | 1 | 1.00 | 0.84 | none |
| Mdi | Mdi [Customer Management] | n/a | MDI_Host | mapped | Customer profile onboarding and maintenance workflow. | n/a | Customer profile created or updated., Navigation routes the user to selected module screens. | n/a | n/a | 0 | 1.00 | 0.46 | none |
| Form1 | Form1 [Navigation/Menu] | (unmapped) | Child | mapped | Business workflow executed through event-driven UI controls. | date 1, date 2 | n/a | MSComCtl2.DTPicker | n/a | 0 | 1.00 | 0.31 | none |
| frmcheckbalance | frmcheckbalance [Customer Management] | (unmapped) | Child | mapped | Customer profile onboarding and maintenance workflow. | ac no, account no, contacttitle, customer id, date 1, firstname, lastname, typeofaccount | Customer details displayed for review. | MSComCtl2.DTPicker | tblCustomers, tblcustomers | 1 | 1.00 | 0.92 | none |
| frmcloseaccount | frmcloseaccount [Customer Management] | (unmapped) | Child | mapped | Customer profile onboarding and maintenance workflow. | n/a | Customer profile created or updated., Navigation routes the user to selected module screens. | n/a | n/a | 0 | 1.00 | 0.47 | none |
| frmdep | frmdep | (unmapped) | Child | mapped | Business workflow executed through event-driven UI controls. | n/a | n/a | n/a | n/a | 0 | 1.00 | 0.26 | none |
| frmExpireItemsWithinDate | frmExpireItemsWithinDate | (unmapped) | Child | mapped | Business workflow executed through event-driven UI controls. | n/a | n/a | MSComCtl2.DTPicker | n/a | 0 | 1.00 | 0.26 | none |
| frminterest | frminterest [Transaction Ledger] | (unmapped) | Child | mapped | Transaction ledger management and adjustment workflow. | n/a | Customer details displayed for review., Transaction history displayed for review., Transaction ledger updated. | MSComctlLib.ListView | tblcustomers, tbltransaction | 2 | 1.00 | 0.90 | none |
| frmMonthly | frmMonthly [Reporting] | (unmapped) | Child | mapped | Operational reporting and statement generation workflow. | report | n/a | n/a | n/a | 0 | 1.00 | 0.51 | none |
| frmreport | frmreport [Customer Management] | (unmapped) | Child | mapped | Operational reporting and statement generation workflow. | account, account no, balance, customer id, first name, from date, last name, to date | Customer profile created or updated. | MSComCtl2.DTPicker | n/a | 0 | 1.00 | 0.51 | none |
| frmtransaction | frmtransaction [Transaction Entry] | (unmapped) | Child | mapped | Transaction ledger management and adjustment workflow. | acc no, customer id, first, option 1, option 2 | Customer details displayed for review., Transaction history displayed for review. | MSComctlLib.ListView | tblCustomers, tblTransaction, tblcustomers | 6 | 1.00 | 0.98 | none |
| frmwith | frmwith [Transaction Ledger] | (unmapped) | Child | mapped | Transaction ledger management and adjustment workflow. | n/a | Transaction history displayed for review. | n/a | tbltransaction | 1 | 1.00 | 0.84 | none |
| Mdi | Mdi [Customer Management] | (unmapped) | MDI_Host | mapped | Customer profile onboarding and maintenance workflow. | n/a | Customer profile created or updated., Navigation routes the user to selected module screens. | n/a | n/a | 0 | 1.00 | 0.46 | none |
| closeacount.frm | closeacount.frm | BANK [BankApp1/BANK.vbp] | Child | excluded | n/a | n/a | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| Form9.frm | Form9.frm | BANK [BankApp1/BANK.vbp] | Child | excluded | n/a | n/a | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| frmaddinterest | frmaddinterest [Customer Management] | BANK [BankApp1/BANK.vbp] | Child | mapped | Customer profile onboarding and maintenance workflow. | account no, currentdate, date, month, year | Customer profile created or updated., Navigation routes the user to selected module screens. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | n/a | 0 | 1.00 | 0.51 | none |
| frmcloseacount | frmcloseacount [Customer Management] | BANK [BankApp1/BANK.vbp] | Child | mapped | Customer profile onboarding and maintenance workflow. | account no, address, balance, cust id, customer id, dateofopen, dob, firstname | Customer profile created or updated., Navigation routes the user to selected module screens. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | n/a | 1 | 1.00 | 0.71 | none |
| frmcustomer | frmcustomer [Customer Management] | BANK [BankApp1/BANK.vbp] | Child | mapped | Customer profile onboarding and maintenance workflow. | account no, address, balance, customer id, dateofopen, dob, firstname, lastname | Customer details displayed for review. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | tblcustomers | 1 | 1.00 | 0.90 | none |
| frmdaily | frmdaily | BANK [BankApp1/BANK.vbp] | Child | mapped | Business workflow executed through event-driven UI controls. | daily | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | n/a | 1 | 1.00 | 0.51 | none |
| frmdeposit | frmdeposit [Deposit Capture] | BANK [BankApp1/BANK.vbp] | Child | mapped | Deposit capture and balance posting workflow. | bankname, cash, cheque, cheque no, dateoftransaction, no, searchaccount no, yes | Account balance recalculated., Deposit transaction recorded., Matching records displayed to the user., Transaction history updated. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | n/a | 0 | 1.00 | 0.51 | none |
| frmLogin.frm | frmLogin.frm [Authentication] | BANK [BankApp1/BANK.vbp] | Login | excluded | Authentication workflow. | n/a | User access is validated before workflow continuation. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| frmLogin1 | frmLogin1 [Password Management] | BANK [BankApp1/BANK.vbp] | Login | mapped | Authentication and credential validation workflow. | pass, un | User access is validated before workflow continuation. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | n/a | 1 | 1.00 | 0.71 | none |
| frmmonthlyreport | frmmonthlyreport [Customer Management] | BANK [BankApp1/BANK.vbp] | Child | mapped | Operational reporting and statement generation workflow. | account no, customer id, from, to | Customer details displayed for review. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | Date, tblcustomers | 3 | 1.00 | 0.98 | none |
| frmsettings | frmsettings [Customer Management] | BANK [BankApp1/BANK.vbp] | Child | mapped | Customer profile onboarding and maintenance workflow. | account id, accounttype, cheque, interestrate, nocheque | Account type master data maintained., Customer profile created or updated., Navigation routes the user to selected module screens. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | tblaccount | 7 | 1.00 | 0.98 | none |
| frmSplash | frmSplash [Splash/Loading] | BANK [BankApp1/BANK.vbp] | Splash | mapped | Business workflow executed through event-driven UI controls. | n/a | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | n/a | 3 | 1.00 | 0.53 | none |
| frmstatement | frmstatement | BANK [BankApp1/BANK.vbp] | Child | mapped | Business workflow executed through event-driven UI controls. | n/a | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | n/a | 0 | 1.00 | 0.28 | none |
| frmwithdraw | frmwithdraw [Withdrawal Processing] | BANK [BankApp1/BANK.vbp] | Child | mapped | Withdrawal processing and balance deduction workflow. | account no, dateoftransaction, no, transaction id, withdrawn, yes | Account balance recalculated., Transaction history updated., Withdrawal transaction recorded. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | n/a | 4 | 1.00 | 0.81 | none |
| frmWithinDate | frmWithinDate | BANK [BankApp1/BANK.vbp] | Child | mapped | Business workflow executed through event-driven UI controls. | n/a | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | n/a | 3 | 1.00 | 0.70 | none |
| menu | menu [Deposit Capture] | BANK [BankApp1/BANK.vbp] | Child | mapped | Deposit capture and balance posting workflow. | n/a | Account balance recalculated., Deposit transaction recorded. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar | n/a | 20 | 1.00 | 0.84 | none |

#### K1. Excluded/Unresolved Forms
| Form | Reason | Source |
|---|---|---|
| BANK::closeacount.frm | missing_from_form_dossier | project.members |
| BANK::Form9.frm | missing_from_form_dossier | project.members |
| BANK::frmLogin.frm | missing_from_form_dossier | project.members |

### L. Risk Register
| Risk ID | Severity | Description | Recommended action |
|---|---|---|---|
| RISK-001 | medium | BankApp1/frmADDINTEREST.frm: default instance references | default_instance_refactor_plan |
| RISK-002 | medium | BankApp1/frmLogin.frm: control array index markers | ui_migration_strategy |
| RISK-003 | high | SQL risk flags for sql:12: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-004 | high | SQL risk flags for sql:13: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-005 | high | SQL risk flags for sql:20: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-006 | medium | SQL risk flags for sql:21: select_star | Parameterize query and align dialect/validation rules before migration. |
| RISK-007 | high | SQL risk flags for sql:22: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-008 | medium | SQL risk flags for sql:23: select_star | Parameterize query and align dialect/validation rules before migration. |
| RISK-009 | high | SQL risk flags for sql:24: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-010 | high | SQL risk flags for sql:25: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-011 | high | SQL risk flags for sql:26: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-012 | medium | SQL risk flags for sql:27: select_star | Parameterize query and align dialect/validation rules before migration. |
| RISK-013 | high | SQL risk flags for sql:28: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-014 | medium | SQL risk flags for sql:29: select_star | Parameterize query and align dialect/validation rules before migration. |
| RISK-015 | high | SQL risk flags for sql:30: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-016 | medium | SQL risk flags for sql:31: select_star | Parameterize query and align dialect/validation rules before migration. |
| RISK-017 | medium | SQL risk flags for sql:32: select_star | Parameterize query and align dialect/validation rules before migration. |
| RISK-018 | medium | SQL risk flags for sql:33: select_star | Parameterize query and align dialect/validation rules before migration. |
| RISK-019 | medium | SQL risk flags for sql:36: select_star | Parameterize query and align dialect/validation rules before migration. |
| RISK-020 | medium | SQL risk flags for sql:37: select_star | Parameterize query and align dialect/validation rules before migration. |
| RISK-021 | high | SQL risk flags for sql:38: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-022 | medium | SQL risk flags for sql:39: select_star | Parameterize query and align dialect/validation rules before migration. |
| RISK-023 | medium | SQL risk flags for sql:40: select_star | Parameterize query and align dialect/validation rules before migration. |
| RISK-024 | high | SQL risk flags for sql:41: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-025 | medium | SQL risk flags for sql:42: select_star | Parameterize query and align dialect/validation rules before migration. |

### M. Orphan Analysis
| Path | SQL IDs | Tables touched | Recommendation |
|---|---|---|---|
| Form1 | n/a | n/a | exclude_or_defer |
| frmExpireItemsWithinDate | sql:5, sql:9 | Date | prioritize_migration |
| frmMonthly | n/a | n/a | exclude_or_defer |
| frmcheckbalance | sql:21, sql:30 | tblCustomers, tblcustomers | prioritize_migration |
| frmcloseaccount | sql:36 | tblaccount | prioritize_migration |
| frmdep | n/a | n/a | exclude_or_defer |
| frminterest | sql:1, sql:3, sql:37, sql:40, sql:41, sql:42 | tblcustomers, tbltransaction | prioritize_migration |
| frmreport | n/a | n/a | exclude_or_defer |
| frmtransaction | sql:22, sql:23, sql:24, sql:25, sql:26, sql:27 | tblCustomers, tblTransaction, tblcustomers | prioritize_migration |
| frmwith | sql:32, sql:33 | tbltransaction | prioritize_migration |
| Mdi | n/a | n/a | exclude_or_defer |

### N. Repository Landscape and Variant Inventory
| Variant | Path | Startup | Forms | Members | Dependencies |
|---|---|---|---:|---:|---:|
| BANK | BankApp1/BANK.vbp | frmSplash | 13 | 14 | 4 |

| Variant | Forms | Modules | Tables touched | Dependency summary |
|---|---:|---:|---:|---|
| BANK | 13 | 1 | 6 | total=4, ocx=2, dll=0 |

### O. Project Dependency Map
| From | To | Type | Evidence | Blocks Sprint |
|---|---|---|---|---|
| shared_module | CheckDatabaseStatus | shared_module_call | shared_module::MoveToLast | Sprint 1 |
| shared_module | CheckDatabaseStatus | shared_module_call | shared_module::MoveToNext | Sprint 1 |
| shared_module | CheckDatabaseStatus | shared_module_call | shared_module::MoveToPrev | Sprint 1 |
| BANK::menu | connectDatabase | shared_module_call | BANK::menu::MDIForm_Load | Sprint 1 |
| BANK::frmmonthlyreport | Numeric | shared_module_call | BANK::frmmonthlyreport::cmbaccountno_KeyPress | Sprint 1 |
| BANK::frmmonthlyreport | ValidNumeric | shared_module_call | BANK::frmmonthlyreport::cmbcustomerid_KeyPress | Sprint 1 |
| BANK::frmwithdraw | Lock_Form_Controls | shared_module_call | BANK::frmwithdraw::cmdCancel_Click | Sprint 1 |
| BANK::frmsettings | ValidNumeric | shared_module_call | BANK::frmsettings::txtcheque_KeyPress | Sprint 1 |
| BANK::frmsettings | ValidNumeric | shared_module_call | BANK::frmsettings::txtinterestrate_KeyPress | Sprint 1 |
| BANK::frmsettings | ValidNumeric | shared_module_call | BANK::frmsettings::txtnocheque_KeyPress | Sprint 1 |

### O1. Form User Flow (Spec-Kit Style)
(unmapped)::frmtransaction
  '- -> rptstatement [via cmdPrintAll]

BANK::frmdaily
  '- -> rptdaily [via cmdGenerate]

BANK::frmLogin1
  '- -> menu [via cmdOK]

BANK::frmmonthlyreport
  '- -> rptmonthlystatement [via cmdShow]

BANK::frmSplash
  |- -> frmlogin [via Form]
  '- -> frmLogin1 [via Timer1]

BANK::frmwithdraw
  '- -> frmLogin1 [via Frame1]

BANK::menu
  |- -> frmAccountdetails [via mnuaccountdetails]
  |- -> frmaddinterest [via mnuaddinterest]
  |- -> frmcloseacount [via mnuclose]
  |- -> frmcustomer [via mnucustomerdetails]
  |- -> frmdeposit [via mnuDepositAmount]
  |- -> frminterest [via mnuinterest]
  |- -> frmmonthlyreport [via mnucustomermonthly]
  |- -> frmnewaccount [via mnuNewAccount]
  |- -> frmsettings [via mnusettings]
  |- -> frmstatement [via mnustatement]
  |- -> frmtransaction [via mnutransactionreport]
  |- -> frmupdate [via mnuUpdatedetails]
  |- -> frmwithdraw [via mnuWithdrawAmount]
  |- -> frmWithinDate [via mnubetween]
  |- -> rpttransaction [via mnudeposits]
  '- -> rptWithdrawals [via mnuwithdrawn]


### P. Form Flow Traces
#### frmLogin1 (BANK [BankApp1/BANK.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| cmdOK_Click | event_handler | BANK::frmLogin1::cmdOK_Click | n/a | n/a | n/a | BankApp1/frmLogin.frm:87 | TRACE_GAP |
#### frmSplash (BANK [BankApp1/BANK.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| Form_KeyPress | event_handler | BANK::frmSplash::Form_KeyPress | n/a | n/a | n/a | BankApp1/frmSplash.frm:191 | TRACE_GAP |
| Timer1_Timer | procedure | BANK::frmSplash::Timer1_Timer | n/a | n/a | n/a | BankApp1/frmSplash.frm:209 | TRACE_GAP |
| Timer_Timer | procedure | BANK::frmSplash::Timer_Timer | n/a | n/a | n/a | BankApp1/frmSplash.frm:200 | TRACE_GAP |
#### frmWithinDate (BANK [BankApp1/BANK.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| Form_Load | event_handler | BANK::frmWithinDate::Form_Load | n/a | n/a | n/a | BankApp1/Form1.frm:178 | TRACE_GAP |
| cmdPrint_Click | event_handler | BANK::frmWithinDate::cmdPrint_Click | n/a | sql:10, sql:43, sql:7 | n/a | BankApp1/Form1.frm:125 | TRACE_GAP |
| cmdexit_Click | event_handler | BANK::frmWithinDate::cmdexit_Click | n/a | n/a | n/a | BankApp1/Form9.frm:56 | TRACE_GAP |
#### frmaddinterest (BANK [BankApp1/BANK.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### frmcloseacount (BANK [BankApp1/BANK.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| cmdQuit_Click | event_handler | BANK::frmcloseacount::cmdQuit_Click | n/a | n/a | n/a | BankApp1/transaction.frm:361 | TRACE_GAP |
#### frmcustomer (BANK [BankApp1/BANK.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| txtcustomerid_LostFocus | event_handler | BANK::frmcustomer::txtcustomerid_LostFocus | n/a | sql:38, sql:39 | tblcustomers | BankApp1/frmcloseaccount.frm:32 | OK |
#### frmdaily (BANK [BankApp1/BANK.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| cmdGenerate_Click | event_handler | BANK::frmdaily::cmdGenerate_Click | n/a | n/a | n/a | BankApp1/Form9.frm:60 | TRACE_GAP |
#### frmdeposit (BANK [BankApp1/BANK.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### frmmonthlyreport (BANK [BankApp1/BANK.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| cmbaccountno_KeyPress | event_handler | BANK::frmmonthlyreport::cmbaccountno_KeyPress | n/a | n/a | n/a | BankApp1/frmmonthlyreport.frm:139 | TRACE_GAP |
| cmbcustomerid_KeyPress | event_handler | BANK::frmmonthlyreport::cmbcustomerid_KeyPress | n/a | n/a | n/a | BankApp1/frmmonthlyreport.frm:143 | TRACE_GAP |
| cmdShow_Click | event_handler | BANK::frmmonthlyreport::cmdShow_Click | n/a | sql:11, sql:38, sql:39, sql:5, sql:6, sql:9 | Date, tblcustomers | BankApp1/frmmonthlyreport.frm:151 | OK |
#### frmsettings (BANK [BankApp1/BANK.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| Control | procedure | BANK::frmsettings::Control | n/a | n/a | n/a | BankApp1/frmsettings.frm:327 | TRACE_GAP |
| DisplayCustomers | procedure | BANK::frmsettings::DisplayCustomers | n/a | sql:36 | tblaccount | BankApp1/frmsettings.frm:314 | OK |
| cmdedit_Click | event_handler | BANK::frmsettings::cmdedit_Click | n/a | n/a | n/a | BankApp1/frmsettings.frm:246 | TRACE_GAP |
| cmdsave_Click | event_handler | BANK::frmsettings::cmdsave_Click | n/a | sql:1 | n/a | BankApp1/frmsettings.frm:258 | TRACE_GAP |
| txtcheque_KeyPress | event_handler | BANK::frmsettings::txtcheque_KeyPress | n/a | n/a | n/a | BankApp1/frmsettings.frm:340 | TRACE_GAP |
| txtinterestrate_KeyPress | event_handler | BANK::frmsettings::txtinterestrate_KeyPress | n/a | n/a | n/a | BankApp1/frmsettings.frm:344 | TRACE_GAP |
| txtnocheque_KeyPress | event_handler | BANK::frmsettings::txtnocheque_KeyPress | n/a | n/a | n/a | BankApp1/frmsettings.frm:348 | TRACE_GAP |
#### frmstatement (BANK [BankApp1/BANK.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### frmwithdraw (BANK [BankApp1/BANK.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| Frame1_Click | event_handler | BANK::frmwithdraw::Frame1_Click | n/a | n/a | n/a | BankApp1/frmSplash.frm:204 | TRACE_GAP |
| cmdCancel_Click | event_handler | BANK::frmwithdraw::cmdCancel_Click | n/a | n/a | n/a | BankApp1/frmsettings.frm:227 | TRACE_GAP |
| cmdExit_Click | event_handler | BANK::frmwithdraw::cmdExit_Click | n/a | n/a | n/a | BankApp1/frmstatement.frm:22 | TRACE_GAP |
| cmdcancel_Click | event_handler | BANK::frmwithdraw::cmdcancel_Click | n/a | n/a | n/a | BankApp1/frmLogin.frm:83 | TRACE_GAP |
#### menu (BANK [BankApp1/BANK.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| MDIForm_Load | event_handler | BANK::menu::MDIForm_Load | n/a | n/a | n/a | BankApp1/frmmdi.frm:70 | TRACE_GAP |
| MDIForm_QueryUnload | procedure | BANK::menu::MDIForm_QueryUnload | n/a | n/a | n/a | BankApp1/frmmdi.frm:155 | TRACE_GAP |
| mnuDepositAmount_Click | event_handler | BANK::menu::mnuDepositAmount_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:90 | TRACE_GAP |
| mnuExit_Click | event_handler | BANK::menu::mnuExit_Click | n/a | n/a | n/a | BankApp1/menu.frm:115 | TRACE_GAP |
| mnuNewAccount_Click | event_handler | BANK::menu::mnuNewAccount_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:97 | TRACE_GAP |
| mnuUpdatedetails_Click | event_handler | BANK::menu::mnuUpdatedetails_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:101 | TRACE_GAP |
| mnuWithdrawAmount_Click | event_handler | BANK::menu::mnuWithdrawAmount_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:137 | TRACE_GAP |
| mnuaccountdetails_Click | event_handler | BANK::menu::mnuaccountdetails_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:62 | TRACE_GAP |
| mnuaddinterest_Click | event_handler | BANK::menu::mnuaddinterest_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:78 | TRACE_GAP |
| mnubetween_Click | event_handler | BANK::menu::mnubetween_Click | n/a | n/a | n/a | BankApp1/menu.frm:80 | TRACE_GAP |
| mnucheckbalance_Click | event_handler | BANK::menu::mnucheckbalance_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:66 | TRACE_GAP |
| mnuclose_Click | event_handler | BANK::menu::mnuclose_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:82 | TRACE_GAP |
| mnucustomerdetails_Click | event_handler | BANK::menu::mnucustomerdetails_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:86 | TRACE_GAP |
| mnucustomermonthly_Click | event_handler | BANK::menu::mnucustomermonthly_Click | n/a | n/a | n/a | BankApp1/menu.frm:92 | TRACE_GAP |
| mnudeposits_Click | event_handler | BANK::menu::mnudeposits_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:117 | TRACE_GAP |
| mnuexitmdifrm_Click | event_handler | BANK::menu::mnuexitmdifrm_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:142 | TRACE_GAP |
| mnugiveinterest_Click | event_handler | BANK::menu::mnugiveinterest_Click | n/a | n/a | n/a | BankApp1/menu.frm:119 | TRACE_GAP |
| mnuinterest_Click | event_handler | BANK::menu::mnuinterest_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:124 | TRACE_GAP |
| mnumonthly_Click | event_handler | BANK::menu::mnumonthly_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:105 | TRACE_GAP |
| mnusettings_Click | event_handler | BANK::menu::mnusettings_Click | n/a | n/a | n/a | BankApp1/menu.frm:123 | TRACE_GAP |
| mnustatement_Click | event_handler | BANK::menu::mnustatement_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:128 | TRACE_GAP |
| mnutransaction_Click | event_handler | BANK::menu::mnutransaction_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:109 | TRACE_GAP |
| mnutransactionreport_Click | event_handler | BANK::menu::mnutransactionreport_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:113 | TRACE_GAP |
| mnuviewtransaction_Click | event_handler | BANK::menu::mnuviewtransaction_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:133 | TRACE_GAP |
| mnuwithdrawn_Click | event_handler | BANK::menu::mnuwithdrawn_Click | n/a | n/a | n/a | BankApp1/frmmdi.frm:168 | TRACE_GAP |
#### Form1 ((unmapped))
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### frmExpireItemsWithinDate ((unmapped))
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### frmMonthly ((unmapped))
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### frmcheckbalance ((unmapped))
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| cboaccountno_click | event_handler | (unmapped)::frmcheckbalance::cboaccountno_click | n/a | sql:21, sql:30 | tblCustomers, tblcustomers | BankApp1/close account.frm:195 | OK |
#### frmcloseaccount ((unmapped))
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### frmdep ((unmapped))
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### frminterest ((unmapped))
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| GenerateNewTransactCode | procedure | (unmapped)::frminterest::GenerateNewTransactCode | n/a | sql:44 | tbltransaction | BankApp1/frminterest.frm:150 | OK |
| cmdCalculateInterest_Click | event_handler | (unmapped)::frminterest::cmdCalculateInterest_Click | n/a | sql:1, sql:3, sql:37, sql:40, sql:41, sql:42 | tblcustomers, tbltransaction | BankApp1/frminterest.frm:49 | OK |
#### frmreport ((unmapped))
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### frmtransaction ((unmapped))
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| Command1_Click | event_handler | (unmapped)::frmtransaction::Command1_Click | n/a | n/a | n/a | BankApp1/transaction.frm:371 | TRACE_GAP |
| cboAccNo_Click | event_handler | (unmapped)::frmtransaction::cboAccNo_Click | n/a | sql:26, sql:27, sql:30, sql:31 | tblTransaction, tblcustomers | BankApp1/transaction.frm:203 | OK |
| cboCustomerID_Click | event_handler | (unmapped)::frmtransaction::cboCustomerID_Click | n/a | sql:22, sql:23, sql:28, sql:29 | tblCustomers, tblTransaction | BankApp1/transaction.frm:238 | OK |
| cboFirst_Click | event_handler | (unmapped)::frmtransaction::cboFirst_Click | n/a | sql:24, sql:25, sql:28, sql:29 | tblCustomers, tblTransaction | BankApp1/transaction.frm:268 | OK |
| cmdPrintAll_Click | event_handler | (unmapped)::frmtransaction::cmdPrintAll_Click | n/a | n/a | n/a | BankApp1/transaction.frm:355 | TRACE_GAP |
| cmdRefresh_Click | event_handler | (unmapped)::frmtransaction::cmdRefresh_Click | n/a | n/a | n/a | BankApp1/transaction.frm:366 | TRACE_GAP |
#### frmwith ((unmapped))
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| cmdOk_Click | event_handler | (unmapped)::frmwith::cmdOk_Click | n/a | sql:32, sql:33 | tbltransaction | BankApp1/frmwith.frm:18 | OK |
#### Mdi ((unmapped))
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Source line refs | Trace status |
|---|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |

### Q. Form Traceability Matrix
| Form | Project | Source LOC | has_event_map | has_sql_map | has_business_rules | has_risk_entry | completeness_score | missing_links |
|---|---|---:|---|---|---|---|---:|---|
| BANK::frmLogin1 | BANK [BankApp1/BANK.vbp] | 0 | yes | no | no | no | 40 | sql_map, business_rules, risk_register |
| BANK::frmSplash | BANK [BankApp1/BANK.vbp] | 223 | yes | no | yes | no | 60 | sql_map, risk_register |
| BANK::frmWithinDate | BANK [BankApp1/BANK.vbp] | 179 | yes | yes | no | no | 60 | business_rules, risk_register |
| BANK::frmaddinterest | BANK [BankApp1/BANK.vbp] | 330 | no | no | yes | yes | 40 | event_map, sql_map, procedure_summary |
| BANK::frmcloseacount | BANK [BankApp1/BANK.vbp] | 0 | yes | no | yes | no | 60 | sql_map, risk_register |
| BANK::frmcustomer | BANK [BankApp1/BANK.vbp] | 370 | yes | yes | yes | yes | 100 | none |
| BANK::frmdaily | BANK [BankApp1/BANK.vbp] | 0 | yes | no | no | no | 40 | sql_map, business_rules, risk_register |
| BANK::frmdeposit | BANK [BankApp1/BANK.vbp] | 329 | no | no | no | no | 0 | event_map, sql_map, business_rules, risk_register, procedure_summary |
| BANK::frmmonthlyreport | BANK [BankApp1/BANK.vbp] | 217 | yes | yes | yes | yes | 100 | none |
| BANK::frmsettings | BANK [BankApp1/BANK.vbp] | 350 | yes | yes | yes | yes | 100 | none |
| BANK::frmstatement | BANK [BankApp1/BANK.vbp] | 73 | no | no | no | no | 0 | event_map, sql_map, business_rules, risk_register, procedure_summary |
| BANK::frmwithdraw | BANK [BankApp1/BANK.vbp] | 338 | yes | no | no | no | 40 | sql_map, business_rules, risk_register |
| BANK::menu | BANK [BankApp1/BANK.vbp] | 188 | yes | no | no | no | 40 | sql_map, business_rules, risk_register |
| (unmapped)::Form1 | (unmapped) | 180 | no | no | yes | no | 20 | event_map, sql_map, risk_register, procedure_summary |
| (unmapped)::frmExpireItemsWithinDate | (unmapped) | 168 | no | no | no | no | 0 | event_map, sql_map, business_rules, risk_register, procedure_summary |
| (unmapped)::frmMonthly | (unmapped) | 0 | no | no | no | no | 0 | event_map, sql_map, business_rules, risk_register, procedure_summary |
| (unmapped)::frmcheckbalance | (unmapped) | 0 | yes | yes | yes | yes | 100 | none |
| (unmapped)::frmcloseaccount | (unmapped) | 70 | no | no | yes | no | 20 | event_map, sql_map, risk_register, procedure_summary |
| (unmapped)::frmdep | (unmapped) | 17 | no | no | no | no | 0 | event_map, sql_map, business_rules, risk_register, procedure_summary |
| (unmapped)::frminterest | (unmapped) | 158 | yes | yes | yes | yes | 100 | none |
| (unmapped)::frmreport | (unmapped) | 320 | no | no | yes | no | 20 | event_map, sql_map, risk_register, procedure_summary |
| (unmapped)::frmtransaction | (unmapped) | 0 | yes | yes | yes | yes | 100 | none |
| (unmapped)::frmwith | (unmapped) | 31 | yes | yes | yes | yes | 100 | none |
| (unmapped)::Mdi | (unmapped) | 0 | no | no | yes | no | 20 | event_map, sql_map, risk_register, procedure_summary |

### R. Sprint Dependency Map
| Form | Suggested sprint | Depends on | Shared Components Required | Rationale |
|---|---|---|---|---|
| BANK::frmstatement | Sprint 0 (Discovery closure) | Q.sql_map, Q.event_map, Q.business_rules | none | Close traceability gaps before modernization changes. |
| BANK::frmdeposit | Sprint 0 (Discovery closure) | Q.sql_map, Q.event_map, Q.business_rules | none | Close traceability gaps before modernization changes. |
| (unmapped)::frmdep | Sprint 0 (Discovery closure) | Q.sql_map, Q.event_map, Q.business_rules | none | Close traceability gaps before modernization changes. |
| (unmapped)::frmMonthly | Sprint 0 (Discovery closure) | Q.sql_map, Q.event_map, Q.business_rules | none | Close traceability gaps before modernization changes. |
| (unmapped)::frmExpireItemsWithinDate | Sprint 0 (Discovery closure) | Q.sql_map, Q.event_map, Q.business_rules | none | Close traceability gaps before modernization changes. |
| (unmapped)::frmreport | Sprint 0 (Discovery closure) | Q.sql_map, Q.event_map | none | Close traceability gaps before modernization changes. |
| (unmapped)::frmcloseaccount | Sprint 0 (Discovery closure) | Q.sql_map, Q.event_map | none | Close traceability gaps before modernization changes. |
| (unmapped)::Mdi | Sprint 0 (Discovery closure) | Q.sql_map, Q.event_map | none | Close traceability gaps before modernization changes. |
| (unmapped)::Form1 | Sprint 0 (Discovery closure) | Q.sql_map, Q.event_map | none | Close traceability gaps before modernization changes. |
| BANK::menu | Sprint 0 (Discovery closure) | Q.sql_map, Q.business_rules | connectDatabase | Close traceability gaps before modernization changes. |
| BANK::frmwithdraw | Sprint 0 (Discovery closure) | Q.sql_map, Q.business_rules | Lock_Form_Controls | Close traceability gaps before modernization changes. |
| BANK::frmdaily | Sprint 0 (Discovery closure) | Q.sql_map, Q.business_rules | none | Close traceability gaps before modernization changes. |
| BANK::frmLogin1 | Sprint 0 (Discovery closure) | Q.sql_map, Q.business_rules | none | Close traceability gaps before modernization changes. |
| BANK::frmaddinterest | Sprint 0 (Discovery closure) | Q.sql_map, Q.event_map, RISK-001 | none | Close traceability gaps before modernization changes. |
| BANK::frmcloseacount | Sprint 0 (Discovery closure) | Q.sql_map | none | Close traceability gaps before modernization changes. |
| BANK::frmWithinDate | Sprint 2 (Parity hardening) | Q.business_rules | none | Complete hardening, regression validation, and release evidence for production readiness. |
| BANK::frmSplash | Sprint 0 (Discovery closure) | Q.sql_map | none | Close traceability gaps before modernization changes. |
| BANK::frmsettings | Sprint 1 (Risk-first modernization) | RISK-019 | ValidNumeric | Implement remediation-first changes for high-risk legacy behavior. |
| BANK::frmmonthlyreport | Sprint 1 (Risk-first modernization) | RISK-021, RISK-022 | Numeric, ValidNumeric | Implement remediation-first changes for high-risk legacy behavior. |
| BANK::frmcustomer | Sprint 1 (Risk-first modernization) | RISK-021, RISK-022 | none | Implement remediation-first changes for high-risk legacy behavior. |
| (unmapped)::frmwith | Sprint 1 (Risk-first modernization) | RISK-017, RISK-018 | none | Implement remediation-first changes for high-risk legacy behavior. |
| (unmapped)::frmtransaction | Sprint 1 (Risk-first modernization) | RISK-007, RISK-008 | none | Implement remediation-first changes for high-risk legacy behavior. |
| (unmapped)::frminterest | Sprint 1 (Risk-first modernization) | RISK-020, RISK-023 | none | Implement remediation-first changes for high-risk legacy behavior. |
| (unmapped)::frmcheckbalance | Sprint 1 (Risk-first modernization) | RISK-006, RISK-015 | none | Implement remediation-first changes for high-risk legacy behavior. |

### S. MDB Inventory
- Databases detected: 1 | forms referenced: 2 | module refs: 1
| DB ID | Path | Name | Ext | LOC proxy | Detected from | Referenced by forms | Referenced by modules | Evidence refs |
|---|---|---|---|---:|---|---|---|---|
| mdb:1 | dbBank.mdb | dbBank.mdb | .mdb | 0 | db_reference, connection_string | frmlogin1, frminterest | Mdl | BankApp1/Mdl.bas, BankApp1/frmLogin.frm, BankApp1/frminterest.frm, conn:1, conn:2, conn:3 |

### T. Form LOC Profile
- Forms discovered: 25 | active: 13 | orphan: 12 | forms LOC total: 5140 | designer LOC total: 3612
| Form ID | Form | Base form | Project | Source file | LOC | In VBP | Active/Orphan | Confidence | Evidence |
|---|---|---|---|---|---:|---|---|---:|---|
| form_loc:26 | (unmapped)::close account | close account | (unmapped) | BankApp1/close account.frm | 273 | no | orphan | 0.45 | form_loc:26 \| conf 0.45 |
| form_loc:27 | (unmapped)::closeacount | closeacount | (unmapped) | BankApp1/closeacount.frm | 377 | yes | active | 0.45 | form_loc:27 \| conf 0.45 |
| form_loc:14 | (unmapped)::Form1 | form1 | (unmapped) | BankApp1/Form1.frm | 180 | no | orphan | 0.92 | form_loc:14 \| conf 0.92 |
| form_loc:25 | (unmapped)::form9 | form9 | (unmapped) | BankApp1/Form9.frm | 71 | yes | active | 0.45 | form_loc:25 \| conf 0.45 |
| form_loc:28 | (unmapped)::frm8 | frm8 | (unmapped) | BankApp1/frm8.frm | 44 | no | orphan | 0.45 | form_loc:28 \| conf 0.45 |
| form_loc:18 | (unmapped)::frmcloseaccount | frmcloseaccount | (unmapped) | BankApp1/frmcloseaccount.frm | 70 | no | orphan | 0.86 | form_loc:18 \| conf 0.86 |
| form_loc:29 | (unmapped)::frmdate | frmdate | (unmapped) | BankApp1/frmDate.frm | 168 | no | orphan | 0.45 | form_loc:29 \| conf 0.45 |
| form_loc:19 | (unmapped)::frmdep | frmdep | (unmapped) | BankApp1/frmdep.frm | 17 | no | orphan | 0.86 | form_loc:19 \| conf 0.86 |
| form_loc:15 | (unmapped)::frmExpireItemsWithinDate | frmexpireitemswithindate | (unmapped) | BankApp1/frmExpireItemsWithinDate.frm | 168 | no | orphan | 0.92 | form_loc:15 \| conf 0.92 |
| form_loc:20 | (unmapped)::frminterest | frminterest | (unmapped) | BankApp1/frminterest.frm | 158 | no | orphan | 0.92 | form_loc:20 \| conf 0.92 |
| form_loc:30 | (unmapped)::frmlogin | frmlogin | (unmapped) | BankApp1/frmLogin.frm | 121 | yes | active | 0.45 | form_loc:30 \| conf 0.45 |
| form_loc:31 | (unmapped)::frmmdi | frmmdi | (unmapped) | BankApp1/frmmdi.frm | 170 | no | orphan | 0.45 | form_loc:31 \| conf 0.45 |
| form_loc:21 | (unmapped)::frmreport | frmreport | (unmapped) | BankApp1/frmreport.frm | 320 | no | orphan | 0.92 | form_loc:21 \| conf 0.92 |
| form_loc:23 | (unmapped)::frmwith | frmwith | (unmapped) | BankApp1/frmwith.frm | 31 | no | orphan | 0.86 | form_loc:23 \| conf 0.86 |
| form_loc:32 | (unmapped)::transaction | transaction | (unmapped) | BankApp1/transaction.frm | 375 | no | orphan | 0.45 | form_loc:32 \| conf 0.45 |
| form_loc:4 | BANK::frmaddinterest | frmaddinterest | BANK | BankApp1/frmADDINTEREST.frm | 330 | yes | active | 0.92 | form_loc:4 \| conf 0.92 |
| form_loc:6 | BANK::frmcustomer | frmcustomer | BANK | BankApp1/frmcustomer.frm | 370 | yes | active | 0.92 | form_loc:6 \| conf 0.92 |
| form_loc:8 | BANK::frmdeposit | frmdeposit | BANK | BankApp1/frmdeposit.frm | 329 | yes | active | 0.92 | form_loc:8 \| conf 0.92 |
| form_loc:9 | BANK::frmmonthlyreport | frmmonthlyreport | BANK | BankApp1/frmmonthlyreport.frm | 217 | yes | active | 0.92 | form_loc:9 \| conf 0.92 |
| form_loc:10 | BANK::frmsettings | frmsettings | BANK | BankApp1/frmsettings.frm | 350 | yes | active | 0.92 | form_loc:10 \| conf 0.92 |
| form_loc:2 | BANK::frmSplash | frmsplash | BANK | BankApp1/frmSplash.frm | 223 | yes | active | 0.92 | form_loc:2 \| conf 0.92 |
| form_loc:11 | BANK::frmstatement | frmstatement | BANK | BankApp1/frmstatement.frm | 73 | yes | active | 0.86 | form_loc:11 \| conf 0.86 |
| form_loc:12 | BANK::frmwithdraw | frmwithdraw | BANK | BankApp1/frmwithdraw.frm | 338 | yes | active | 0.92 | form_loc:12 \| conf 0.92 |
| form_loc:3 | BANK::frmWithinDate | frmwithindate | BANK | BankApp1/frmWithinDate.frm | 179 | yes | active | 0.92 | form_loc:3 \| conf 0.92 |
| form_loc:13 | BANK::menu | menu | BANK | BankApp1/menu.frm | 188 | yes | active | 0.92 | form_loc:13 \| conf 0.92 |

### T1. Designer LOC Profile
| File | Kind | LOC |
|---|---|---:|
| BankApp1/DataReport1.DCA | designer_definition | 253 |
| BankApp1/DataReport2.DCA | designer_definition | 241 |
| BankApp1/DataReport3.DCA | designer_definition | 218 |
| BankApp1/DataReport31.DCA | designer_definition | 254 |
| BankApp1/DataReport4.DCA | designer_definition | 205 |
| BankApp1/Date.DCA | designer_definition | 241 |
| BankApp1/ExpiredItemsWithinDate.DCA | designer_definition | 244 |
| BankApp1/Month.DCA | designer_definition | 215 |
| BankApp1/daily.DCA | designer_definition | 207 |
| BankApp1/deBank.DCA | designer_definition | 92 |
| BankApp1/rptCustomers.DCA | designer_definition | 250 |
| BankApp1/rptDeposits.DCA | designer_definition | 232 |
| BankApp1/rptStatement.DCA | designer_definition | 258 |
| BankApp1/rptWithdrawals.DCA | designer_definition | 257 |
| BankApp1/rptmonthlystatement.DCA | designer_definition | 212 |
| BankApp1/withindate.DCA | designer_definition | 233 |

### U. Connection String Variants
- Variants: 3 | relative-path risks: 3 | embedded-credential risks: 0
| Variant ID | Normalized pattern | Risk flags | Source refs | Example |
|---|---|---|---|---|
| conn:1 | <db-file> | relative_db_path | conn:1 | \dbBank.mdb |
| conn:2 | Provider=Microsoft.Jet.Oledb.:num.:num;Data Source=<db-file> | legacy_access_provider, relative_db_path | conn:2 | Provider=Microsoft.Jet.Oledb.4.0;Data Source=dbBank.mdb |
| conn:3 | Provider=Microsoft.Jet.OLEDB.:num.:num;Data Source=<db-file>;Persist Security Info=False | legacy_access_provider, relative_db_path | conn:3 | Provider=Microsoft.Jet.OLEDB.4.0;Data Source=dbbank.mdb;Persist Security Info=False |

### V. Module Global Inventory
- Modules: 1 | global candidates: 20 | extraction status: declared_plus_inferred
| Symbol | Declared type | Scope | Inferred purpose | Evidence refs |
|---|---|---|---|---|
| cmd | ADODB.Command | module_shared_candidate | Shared state or helper object referenced from UI events. | cmdCalculateInterest_Click |
| cnBank | Connection | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:4, disconnectDatabase, GenerateNewTransactCode |
| con | ADODB.Connection | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:18 |
| ctrl | Control | dim | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:46, BankApp1/Mdl.bas:60, BankApp1/Mdl.bas:71 |
| dBConnection | ADODB.Connection | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:19 |
| NewRecord | Boolean | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:17 |
| p_AccDetails | Variant | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:20 |
| p_SQL | String | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:15 |
| rs | New ADODB.Recordset | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:10, cmdOK_Click, GenerateNewTransactCode |
| rsAccount | Recordset | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:9 |
| rscustomers | Recordset | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:5 |
| rsDeposit | Recordset | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:6 |
| rsStat | Recordset | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:14 |
| rstem | Recordset | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:12 |
| rsTemp | Recordset | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:11 |
| rsTemp2 | Recordset | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:13 |
| rsTransaction | Recordset | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:8 |
| rsWithdrawal | Recordset | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:7 |
| StrSQl | String | dim | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:3 |
| X | Integer | public | Global/module-level declaration extracted from .bas module. | BankApp1/Mdl.bas:16 |

### V1. Module Inventory
| Module |
|---|
| BankApp1/Mdl.bas |

### W. Dead Form References
- Unresolved references: 4 | callers impacted: 2
| Ref ID | Caller form | Caller handler | Target token | Status | Rationale | Evidence ref |
|---|---|---|---|---|---|---|
| dead_form_ref:1 | BANK::frmSplash | Form_KeyPress | frmlogin | unresolved | Target form token was referenced but no matching discovered form dossier exists. | event:23 |
| dead_form_ref:2 | BANK::menu | mnuNewAccount_Click | frmnewaccount | unresolved | Target form token was referenced but no matching discovered form dossier exists. | event:33 |
| dead_form_ref:3 | BANK::menu | mnuUpdatedetails_Click | frmupdate | unresolved | Target form token was referenced but no matching discovered form dossier exists. | event:34 |
| dead_form_ref:4 | BANK::menu | mnuaccountdetails_Click | frmAccountdetails | unresolved | Target form token was referenced but no matching discovered form dossier exists. | event:36 |

### X. DataEnvironment Report Mapping
- DataEnvironments: 1 | reports: 1 | mapped calls: 6
| Mapping ID | Caller form | Caller handler | Report object | DataEnvironment | Kind | Confidence | Evidence ref |
|---|---|---|---|---|---|---:|---|
| de_map:1 | BANK::frmdaily | cmdGenerate_Click | rptdaily | deBank | command_to_report | 0.72 | event:3 |
| de_map:2 | BANK::menu | mnudeposits_Click | rpttransaction | deBank | command_to_report | 0.72 | event:41 |
| de_map:3 | BANK::menu | mnutransaction_Click | rpttransaction | deBank | command_to_report | 0.72 | event:46 |
| de_map:4 | BANK::menu | mnuwithdrawn_Click | rptWithdrawals | deBank | command_to_report | 0.72 | event:49 |
| de_map:5 | BANK::frmmonthlyreport | cmdShow_Click | rptmonthlystatement | deBank | command_to_report | 0.72 | event:52 |
| de_map:6 | (unmapped)::frmtransaction | cmdPrintAll_Click | rptstatement | deBank | command_to_report | 0.72 | event:72 |

### Y. Static Risk Detectors
- Detector checks: 4 | findings: 2 | high severity: 1
| Detector ID | Severity | Summary | Evidence |
|---|---|---|---|
| no_rollback_on_multi_write | high | Multiple write operations were detected without explicit transaction/rollback guards. | {"write_statement_count": 1, "write_table_count": 1, "write_tables": ["tbltransaction"], "rule_signals": 0, "event_write_signals": 7, "event_write_signal_samples": ["cmdsave", "frmdeposit", "frmupdate", "frmwithdraw", "insert", "rptwithdrawals", "save_operation"]} |
| manual_id_generation_concurrency_risk | medium | Manual ID generation pattern (SELECT MAX(...)) detected; this can cause concurrency collisions. | {"sql_ids": ["sql:44"]} |

### Y1. Raw UI Control Inventory
- Controls discovered from raw form dossiers. Selection/list controls are preserved even when list values are not statically recoverable.
| Project | Form | Control Name | Control Type | Role | Values / Notes |
|---|---|---|---|---|---|
| BANK [BankApp1/BANK.vbp] | frmLogin1 | cmdCancel | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmLogin1 | cmdOK | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmLogin1 | lblLabels | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmLogin1 | txtPass | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmLogin1 | txtUn | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmSplash | ProgressBar | MSComctlLib.ProgressBar | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmSplash | ProgressBar1 | MSComctlLib.ProgressBar | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmSplash | frasplash | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmSplash | Image1 | VB.Image | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmSplash | lblCompany | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmSplash | lblCompanyProduct | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmSplash | lblCopyright | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmSplash | lblLicenseTo | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmSplash | lblWarning | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmSplash | lbldisplay | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmSplash | Timer1 | VB.Timer | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmWithinDate | DTFrom | MSComCtl2.DTPicker | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmWithinDate | DTTo | MSComCtl2.DTPicker | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmWithinDate | cmdExit | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmWithinDate | cmdPrint | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmWithinDate | Frame1 | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmWithinDate | Frame2 | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmWithinDate | frawithindate | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmWithinDate | Label1 | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmWithinDate | Label2 | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | txtdate | MSComCtl2.DTPicker | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | cbomonth | VB.ComboBox | selection | designer list values not statically recovered |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | cboyear | VB.ComboBox | selection | designer list values not statically recovered |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | cmdclear | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | cmdexit | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | cmdinterest | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | cmdsearch | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | Frame1 | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | Frame2 | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | fra | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | Label1 | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | Label5 | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | lblamount | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | lblbal | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | lblbalance | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | lblcurrentbalance | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | lblcustomerid | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | lblfirst | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | lblfirstname | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | lblid | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | lblinterest | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | lbllast | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | lbllastname | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | lbltransaction | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | lbltransactionid | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | lbltype | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | txtaccountno | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmaddinterest | txtcurrentdate | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | txtdateofopen | MSComCtl2.DTPicker | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | cbosex | VB.ComboBox | selection | designer list values not statically recovered |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | cmdDelete | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | cmdQuit | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | cmdsearch | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | Frame1 | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | Frame2 | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | fracheque | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | franominee | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | frasearch | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lblCheque | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lblDateOfOpen | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lblNominee | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lblPhoneNo | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lblaccountno | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lbladdress | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lblbalance | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lblcustid | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lblcustomerid | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lbldateofbirth | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lblfirstname | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lbllastname | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lblmiddlename | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lblrelationship | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lblsex | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | lbltype | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | optmajor | VB.OptionButton | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | optminor | VB.OptionButton | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | optno | VB.OptionButton | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | optyes | VB.OptionButton | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | txtaccountno | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | txtaddress | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | txtbalance | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | txtcustid | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | txtcustomerid | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | txtdob | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | txtfirstname | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | txtlastname | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | txtmiddlename | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | txtmobileno | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | txtnominee | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | txtphoneno | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | txtpincode | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcloseacount | txtrelationship | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | txtdateofopen | MSComCtl2.DTPicker | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | txtdob | MSComCtl2.DTPicker | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | cbosex | VB.ComboBox | selection | designer list values not statically recovered |
| BANK [BankApp1/BANK.vbp] | frmcustomer | cmdsearch | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | Frame1 | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | Frame2 | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | fracheque | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | franominee | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | frasearch | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | Label2 | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | Label3 | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | lblCheque | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | lblDateOfOpen | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | lblNominee | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | lblPhoneNo | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | lblPincode | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | lbladdress | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | lblbalance | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | lbldateofbirth | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | lblmiddlename | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | lblrelationship | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | lblsex | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | lbltype | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | lbltypeofaccount | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | optmajor | VB.OptionButton | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | optminor | VB.OptionButton | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | optno | VB.OptionButton | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | optyes | VB.OptionButton | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | txtaccountno | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | txtaddress | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | txtbalance | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | txtcustomerid | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | txtfirstname | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | txtlastname | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | txtmiddlename | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | txtmobileno | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | txtnominee | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | txtphoneno | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | txtpincode | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | txtrelationship | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmcustomer | txtsearch | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmdaily | txtdaily | MSComCtl2.DTPicker | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmdaily | cmdGenerate | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmdaily | cmdexit | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmdaily | fradaily | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | txtdateoftransaction | MSComCtl2.DTPicker | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | cmdsearch | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | Frame1 | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | Frame2 | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | Frame3 | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | fracheque | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | fraext | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | framode | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | Label2 | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | lblFieldLabel | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | lblaccount | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | lblbalance | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | lblbankname | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | lblchequeissued | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | lblcustomer | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | lblcustomerid | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | lbldate | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | lblfirst | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | lblfirstname | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | lbllast | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | lbllastname | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | lbltypeofaccount | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | optcash | VB.OptionButton | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | optcheque | VB.OptionButton | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | optno | VB.OptionButton | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | optyes | VB.OptionButton | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | txtbankname | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | txtchequeno | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmdeposit | txtsearchaccountno | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmmonthlyreport | DTPFrom | MSComCtl2.DTPicker | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmmonthlyreport | DTPTo | MSComCtl2.DTPicker | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmmonthlyreport | cmbcustomerid | VB.ComboBox | selection | designer list values not statically recovered |
| BANK [BankApp1/BANK.vbp] | frmmonthlyreport | cmdShow | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmmonthlyreport | cmdexit | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmmonthlyreport | Frame1 | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmmonthlyreport | Label1 | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmmonthlyreport | lblFrom | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmmonthlyreport | lblTo | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmsettings | cmdcancel | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmsettings | cmdedit | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmsettings | cmdexit | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmsettings | cmdsave | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmsettings | frasettings | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmsettings | Label1 | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmsettings | lblFieldLabel | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmsettings | lblaccountid | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmsettings | txtaccountid | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmsettings | txtaccounttype | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmsettings | txtcheque | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmsettings | txtinterestrate | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmsettings | txtnocheque | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | txtdateoftransaction | MSComCtl2.DTPicker | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | cmdCancel | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | cmdexit | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | cmdsearch | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | cmdwithdraw | VB.CommandButton | action | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | Frame1 | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | Frame3 | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | fracheque | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | frawithdrawn | VB.Frame | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | Label2 | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | lblFieldLabel | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | lblaccountno | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | lblaccounttype | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | lblbalance | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | lblcheque | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | lblchequeissued | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | lblcustid | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | lblcustomerid | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | lbldate | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | lblfirst | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | lblfirstname | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | lbllast | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | lbllastname | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | lbltag | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | lbltypeofaccount | VB.Label | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | optno | VB.OptionButton | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | optyes | VB.OptionButton | display | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | txtaccountno | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | txttransactionid | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | frmwithdraw | txtwithdrawn | VB.TextBox | data_input | n/a |
| BANK [BankApp1/BANK.vbp] | menu | mnuDepositAmount | VB.Menu | display | n/a |
| BANK [BankApp1/BANK.vbp] | menu | mnuExit | VB.Menu | display | n/a |
| BANK [BankApp1/BANK.vbp] | menu | mnuReports | VB.Menu | display | n/a |
| BANK [BankApp1/BANK.vbp] | menu | mnuWithdrawAmount | VB.Menu | display | n/a |
| BANK [BankApp1/BANK.vbp] | menu | mnubetween | VB.Menu | display | n/a |
| BANK [BankApp1/BANK.vbp] | menu | mnuclose | VB.Menu | display | n/a |
| BANK [BankApp1/BANK.vbp] | menu | mnucustomerdetails | VB.Menu | display | n/a |
| BANK [BankApp1/BANK.vbp] | menu | mnucustomermonthly | VB.Menu | display | n/a |
| BANK [BankApp1/BANK.vbp] | menu | mnugiveinterest | VB.Menu | display | n/a |
| BANK [BankApp1/BANK.vbp] | menu | mnumaster | VB.Menu | display | n/a |
| BANK [BankApp1/BANK.vbp] | menu | mnusettings | VB.Menu | display | n/a |
| BANK [BankApp1/BANK.vbp] | menu | mnutransaction | VB.Menu | display | n/a |
| BANK [BankApp1/BANK.vbp] | menu | mnutransactions | VB.Menu | display | n/a |
| (unmapped) | Form1 | DTPicker1 | MSComCtl2.DTPicker | data_input | n/a |
| (unmapped) | Form1 | DTPicker2 | MSComCtl2.DTPicker | data_input | n/a |
| (unmapped) | Form1 | cmdPrint | VB.CommandButton | action | n/a |
| (unmapped) | Form1 | Frame1 | VB.Frame | display | n/a |
| (unmapped) | Form1 | Frame2 | VB.Frame | display | n/a |
| (unmapped) | Form1 | Label1 | VB.Label | display | n/a |
| (unmapped) | Form1 | Label2 | VB.Label | display | n/a |
| (unmapped) | Form1 | Shape5 | VB.Shape | display | n/a |
| (unmapped) | frmExpireItemsWithinDate | DTFrom | MSComCtl2.DTPicker | display | n/a |
| (unmapped) | frmExpireItemsWithinDate | DTTo | MSComCtl2.DTPicker | display | n/a |
| (unmapped) | frmExpireItemsWithinDate | cmdExit | VB.CommandButton | action | n/a |
| (unmapped) | frmExpireItemsWithinDate | cmdPrint | VB.CommandButton | action | n/a |
| (unmapped) | frmExpireItemsWithinDate | Frame1 | VB.Frame | display | n/a |
| (unmapped) | frmExpireItemsWithinDate | Frame2 | VB.Frame | display | n/a |
| (unmapped) | frmExpireItemsWithinDate | Label1 | VB.Label | display | n/a |
| (unmapped) | frmExpireItemsWithinDate | Label2 | VB.Label | display | n/a |
| (unmapped) | frmMonthly | cmbReport | VB.ComboBox | selection | designer list values not statically recovered |
| (unmapped) | frmMonthly | cmdGenerate | VB.CommandButton | action | n/a |
| (unmapped) | frmcheckbalance | DTPicker1 | MSComCtl2.DTPicker | data_input | n/a |
| (unmapped) | frmcheckbalance | cboaccountno | VB.ComboBox | selection | designer list values not statically recovered |
| (unmapped) | frmcheckbalance | cmdsearch | VB.CommandButton | action | n/a |
| (unmapped) | frmcheckbalance | Frame1 | VB.Frame | display | n/a |
| (unmapped) | frmcheckbalance | Frame2 | VB.Frame | display | n/a |
| (unmapped) | frmcheckbalance | Label5 | VB.Label | display | n/a |
| (unmapped) | frmcheckbalance | lblaccno | VB.Label | display | n/a |
| (unmapped) | frmcheckbalance | lblaccountno | VB.Label | display | n/a |
| (unmapped) | frmcheckbalance | lblbal | VB.Label | display | n/a |
| (unmapped) | frmcheckbalance | lblbalance | VB.Label | display | n/a |
| (unmapped) | frmcheckbalance | lblcontacttitle | VB.Label | display | n/a |
| (unmapped) | frmcheckbalance | lblcustomerid | VB.Label | display | n/a |
| (unmapped) | frmcheckbalance | lbldate | VB.Label | display | n/a |
| (unmapped) | frmcheckbalance | lblfirstname | VB.Label | display | n/a |
| (unmapped) | frmcheckbalance | lbllastname | VB.Label | display | n/a |
| (unmapped) | frmcheckbalance | txtacno | VB.TextBox | data_input | n/a |
| (unmapped) | frmcheckbalance | txtcontacttitle | VB.TextBox | data_input | n/a |
| (unmapped) | frmcheckbalance | txtcustomerid | VB.TextBox | data_input | n/a |
| (unmapped) | frmcheckbalance | txtfirstname | VB.TextBox | data_input | n/a |
| (unmapped) | frmcheckbalance | txtlastname | VB.TextBox | data_input | n/a |
| (unmapped) | frmcheckbalance | txttypeofaccount | VB.TextBox | data_input | n/a |
| (unmapped) | frminterest | ListView1 | MSComctlLib.ListView | selection | designer list values not statically recovered |
| (unmapped) | frminterest | cmdCalculateInterest | VB.CommandButton | action | n/a |
| (unmapped) | frmreport | dtpFromDate | MSComCtl2.DTPicker | data_input | n/a |
| (unmapped) | frmreport | dtpToDate | MSComCtl2.DTPicker | data_input | n/a |
| (unmapped) | frmreport | cmdcancel | VB.CommandButton | action | n/a |
| (unmapped) | frmreport | cmdpreview | VB.CommandButton | action | n/a |
| (unmapped) | frmreport | cmdsearch | VB.CommandButton | action | n/a |
| (unmapped) | frmreport | Frame3 | VB.Frame | display | n/a |
| (unmapped) | frmreport | Frame4 | VB.Frame | display | n/a |
| (unmapped) | frmreport | Frame5 | VB.Frame | display | n/a |
| (unmapped) | frmreport | Frame7 | VB.Frame | display | n/a |
| (unmapped) | frmreport | frareport | VB.Frame | display | n/a |
| (unmapped) | frmreport | frasearch | VB.Frame | display | n/a |
| (unmapped) | frmreport | Label1 | VB.Label | display | n/a |
| (unmapped) | frmreport | Label2 | VB.Label | display | n/a |
| (unmapped) | frmreport | Label5 | VB.Label | display | n/a |
| (unmapped) | frmreport | Label6 | VB.Label | display | n/a |
| (unmapped) | frmreport | Label7 | VB.Label | display | n/a |
| (unmapped) | frmreport | Label8 | VB.Label | display | n/a |
| (unmapped) | frmreport | lblcustomerid | VB.Label | display | n/a |
| (unmapped) | frmreport | txtBalance | VB.TextBox | data_input | n/a |
| (unmapped) | frmreport | txtFirstName | VB.TextBox | data_input | n/a |
| (unmapped) | frmreport | txtLastName | VB.TextBox | data_input | n/a |
| (unmapped) | frmreport | txtaccount | VB.TextBox | data_input | n/a |
| (unmapped) | frmreport | txtaccountno | VB.TextBox | data_input | n/a |
| (unmapped) | frmreport | txtcustomerid | VB.TextBox | data_input | n/a |
| (unmapped) | frmreport | txttypeofaccount | VB.TextBox | data_input | n/a |
| (unmapped) | frmtransaction | lvwTransactions | MSComctlLib.ListView | selection | designer list values not statically recovered |
| (unmapped) | frmtransaction | cboAccNo | VB.ComboBox | selection | designer list values not statically recovered |
| (unmapped) | frmtransaction | cmdQuit | VB.CommandButton | action | n/a |
| (unmapped) | frmtransaction | cmdRefresh | VB.CommandButton | action | n/a |
| (unmapped) | frmtransaction | Frame1 | VB.Frame | display | n/a |
| (unmapped) | frmtransaction | Frame3 | VB.Frame | display | n/a |
| (unmapped) | frmtransaction | fraaccountno | VB.Frame | display | n/a |
| (unmapped) | frmtransaction | Label2 | VB.Label | display | n/a |
| (unmapped) | frmtransaction | Label5 | VB.Label | display | n/a |
| (unmapped) | frmtransaction | Option1 | VB.OptionButton | display | n/a |
| (unmapped) | frmtransaction | Option2 | VB.OptionButton | display | n/a |
| (unmapped) | Mdi | mnuDepositAmount | VB.Menu | display | n/a |
| (unmapped) | Mdi | mnuExit | VB.Menu | display | n/a |
| (unmapped) | Mdi | mnuReports | VB.Menu | display | n/a |
| (unmapped) | Mdi | mnuWithdrawAmount | VB.Menu | display | n/a |
| (unmapped) | Mdi | mnuaddinterest | VB.Menu | display | n/a |
| (unmapped) | Mdi | mnuclose | VB.Menu | display | n/a |
| (unmapped) | Mdi | mnucustomerdetails | VB.Menu | display | n/a |
| (unmapped) | Mdi | mnuinterest | VB.Menu | display | n/a |
| (unmapped) | Mdi | mnumaster | VB.Menu | display | n/a |
| (unmapped) | Mdi | mnumonthly | VB.Menu | display | n/a |
| (unmapped) | Mdi | mnustatement | VB.Menu | display | n/a |
| (unmapped) | Mdi | mnutransaction | VB.Menu | display | n/a |
| (unmapped) | Mdi | mnutransactions | VB.Menu | display | n/a |
| (unmapped) | Mdi | mnuviewtransaction | VB.Menu | display | n/a |
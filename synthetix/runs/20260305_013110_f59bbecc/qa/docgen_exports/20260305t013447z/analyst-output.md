# Modernization Brief - Banking Application Modernization

## Header
- Objective: Modernize a legacy VB6 banking application into C#, ensuring backward compatibility and documenting current functionality.
- Domain: software
- Repo: https://github.com/vishakjai/TestVBProject1 @ detached (unknown)
- SIL Versions: SCM 1.0 / CP 1.0 / HA 1.0
- Generated At: 2026-03-05T01:33:01.674003+00:00

## Decision Brief

| Category | Summary |
|---|---|
| Modernization readiness | 54/100 |
| Risk tier | medium |
| Inventory | 3 project(s), 26 forms/usercontrols, 10 dependencies |
| Data touchpoints | transctions, customer, accounttype, deposit, withdrawal, transactions, balancedt, LOGIN, logi, tblBalances, tblCustomers, tblTransactions |
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
- DEC-VARIANT-001: Resolve legacy project variant scope before planning execution and address cross-variant schema divergence.
  - Recommendation: Select canonical variant and capture explicit merge/out-of-scope decision in Review.
- DEC-IAM-001: Confirm identity/access model (role model, multi-user assumptions, and credential handling).
  - Recommendation: Define target role model and credential policy before implementation.
- DEC-SCHEMA-KEY-001: Transaction delete-key behavior requires explicit decision.
  - Recommendation: Adopt explicit transaction key with migration plan and backward-compatibility checks.
- Q-001: Are there existing operational constraints or integration dependencies not listed?
  - Recommendation: Resolve with product/business owner before implementation commitment.

### Decisions Required (Non-blocking)
- DEC-OBS-001: Logging and observability stack for migrated runtime

## Delivery Spec

### Backlog
| ID | Pri | Type | Outcome | Acceptance |
|---|---|---|---|---|
| FR-001 | P0 | functional | Modernize Account Types Form | The C# form replicates the layout and controls of the VB6 frmAccTypes form. / All functionalities, including account operations, are preserved and functional. |
| FR-002 | P0 | functional | Modernize Customer Management Form | The C# form matches the VB6 frmCustomers form in appearance and behavior. / Customer profile lookup and maintenance functionalities are operational. |
| FR-003 | P1 | functional | Modernize Deposits Form | The C# form accurately reflects the VB6 frmDeposits form's UI and controls. / Deposit operations are fully functional in the modernized form. |
| FR-004 | P1 | functional | Modernize Search Functionality | The C# form provides the same search capabilities as the VB6 frmSearch form. / Search results are accurate and displayed correctly. |
| FR-005 | P0 | functional | Modernize Transaction Management | The C# form replicates the VB6 frmTransactions form's layout and controls. / Transaction operations are fully functional and accurate. |
| FR-006 | P2 | functional | Modernize Withdrawal Form | The C# form matches the VB6 frmWithdrawal form in appearance and behavior. / Withdrawal operations are operational and accurate. |
| FR-007 | P0 | functional | Database Connection Modernization | All database connections are established using Entity Framework or ADO.NET. / Data operations are consistent with the legacy application. |
| FR-008 | P1 | functional | ActiveX Control Replacement | All ActiveX controls are replaced with equivalent .NET components. / The application maintains functional parity with the legacy version. |
| NFR-002 | P1 | non_functional | Security Compliance | No critical vulnerabilities are present in the application. / All data is encrypted in transit and at rest. |
| RM-001 | P0 | risk_remediation | Parameterize SQL and secure credential handling | Remediation implemented and validated against affected legacy flow. / Evidence artifacts updated with before/after traceability. |
| RM-002 | P0 | risk_remediation | Replace UI-caption-based balance arithmetic | Remediation implemented and validated against affected legacy flow. / Evidence artifacts updated with before/after traceability. |
| RM-003 | P0 | risk_remediation | Define identity and access model for modernization scope | Remediation implemented and validated against affected legacy flow. / Evidence artifacts updated with before/after traceability. |
| RM-004 | P0 | risk_remediation | Resolve cross-variant schema naming divergence | Remediation implemented and validated against affected legacy flow. / Evidence artifacts updated with before/after traceability. |
| RM-005 | P0 | risk_remediation | Remediate transaction schema key hazard | Remediation implemented and validated against affected legacy flow. / Evidence artifacts updated with before/after traceability. |

### Testing and Evidence
- Golden flows:
  - GF-001: Project1 (STUDENT BANKING/BANKING.vbp)::main primary flow | entry=Project1 (STUDENT BANKING/BANKING.vbp)::main::Command1_Click
  - GF-002: Project1 (STUDENT BANKING/BANKING.vbp)::Form1 primary flow | entry=Project1 (STUDENT BANKING/BANKING.vbp)::Form1::Label3_Click
  - GF-003: Project1 (STUDENT BANKING/BANKING.vbp)::Form1 primary flow | entry=Project1 (STUDENT BANKING/BANKING.vbp)::Form1::Label4_Click
  - GF-004: Project1 (STUDENT BANKING/BANKING.vbp)::Form6 primary flow | entry=Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdAdd_Click
  - GF-005: Project1 (STUDENT BANKING/BANKING.vbp)::Form6 primary flow | entry=Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdEdit_Click
  - GF-006: Project1 (STUDENT BANKING/BANKING.vbp)::Form6 primary flow | entry=Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdFirst_Click
  - GF-007: Project1 (STUDENT BANKING/BANKING.vbp)::Form6 primary flow | entry=Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdLast_Click
  - GF-008: Project1 (STUDENT BANKING/BANKING.vbp)::Form6 primary flow | entry=Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdNext_Click
  - GF-009: shared_module primary flow | entry=shared_module::CheckDatabaseStatus
  - GF-010: shared_module primary flow | entry=shared_module::Lock_Form_Controls
- Quality gates:
  - gherkin_syntax: PASS | BDD syntax validation for Feature/Scenario/Given/When/Then.
  - requirements_completeness: PASS | Backlog grounded in discovered behavior (14 derived item(s), threshold 4).
  - compliance_constraints_applied: FAIL | No explicit compliance constraints were linked, but security/privacy risks were detected in legacy behavior.
  - bdd_flow_grounding: PASS | BDD scenarios are grounded in extracted legacy flows.
  - handler_inventory_completeness: PASS | All analyzed forms meet handler coverage threshold.
  - report_model_reconciled: PASS | Reporting model and entrypoints reconciled.
  - variant_resolution: FAIL | Variant scope unresolved and schema divergence detected across project variants. Resolve DEC-VARIANT-001 before planning.
  - variant_schema_divergence: FAIL | Schema naming divergence detected in 3 variant pair(s); 3 pair(s) include transaction-like table conflicts.
  - key_safety_issues_identified: PASS | Risk signals include SQL injection/credential handling issues (16 signal(s)).
  - schema_key_verification: FAIL | Delete-by-customer pattern detected in transaction scope without explicit transaction key.
  - identity_access_model: FAIL | Role model or credential handling requires confirmation.
  - qa_structural_integrity: PASS | QA structural checks: pass=9, warn=0, fail=0, blockers=0.
  - qa_semantic_plausibility: PASS | Semantic plausibility checks passed with no issues.
- QA summary:
  - Status: PASS
  - Structural: pass=9, warn=0, fail=0, blockers=0
  - QA Gate qa_structural_integrity: PASS | QA structural checks: pass=9, warn=0, fail=0, blockers=0.
  - QA Gate qa_semantic_plausibility: PASS | Semantic plausibility checks passed with no issues.
  - Structural checks: 9 total (0 blocking)
  - Rule consolidation notes are documented in Appendix Section E2 when duplicate rule templates are suppressed.

### Open Questions
- [HIGH] Q-001: Are there existing operational constraints or integration dependencies not listed? (owner: Client)

## QA Validation Summary
- Overall status: PASS
- Structural summary: pass=9, warn=0, fail=0, blockers=0
- Auto-fixes applied:
  - Aligned event handler count to event-map entry count for deterministic reconciliation.
  - Aligned rendered form count with discovered form dossier count.
  - Canonicalized saturated rule template 'threshold decision rule: if rs.state = 1 then ...' into one shared rule with 10 occurrences.

## Evidence Appendix
- legacy_inventory_ref: artifact://legacy_inventory/1.0/art_legacy_inventory_86dccc60a28243cf
- repo_landscape_ref: artifact://repo_landscape/1.0/art_repo_landscape_05ffd21f535b4d54
- scope_lock_ref: artifact://scope_lock/1.0/art_scope_lock_6d828ec0a21f4b90
- variant_inventory_ref: artifact://variant_inventory/1.0/art_variant_inventory_ec4a249c13c64e9a
- event_map_ref: artifact://event_map/1.0/art_event_map_cca0b661c9c1497d
- sql_catalog_ref: artifact://sql_catalog/1.0/art_sql_catalog_c93c948671a74f04
- sql_map_ref: artifact://sql_map/1.0/art_sql_map_bec81a6a723e489c
- data_access_map_ref: artifact://data_access_map/1.0/art_data_access_map_c94e2f2a56f64a46
- recordset_ops_ref: artifact://recordset_ops/1.0/art_recordset_ops_307836d9d38f4319
- procedure_summary_ref: artifact://procedure_summary/1.0/art_procedure_summary_f6b6ea7f704543f8
- form_dossier_ref: artifact://form_dossier/1.0/art_form_dossier_e7f2fda265c741d9
- dependency_list_ref: artifact://dependency_inventory/1.0/art_dependency_inventory_5dfc18df03614e74
- dependency_inventory_ref: artifact://dependency_inventory/1.0/art_dependency_inventory_5dfc18df03614e74
- business_rules_ref: artifact://business_rule_catalog/1.0/art_business_rule_catalog_72a7b9b12bbd4876
- detector_findings_ref: artifact://detector_findings/1.0/art_detector_findings_e87fee329f794483
- risk_register_ref: artifact://risk_register/1.0/art_risk_register_9c6499d243fc4c5a
- orphan_analysis_ref: artifact://orphan_analysis/1.0/art_orphan_analysis_b48e7998b7234ed9
- delivery_constitution_ref: artifact://delivery_constitution/1.0/art_delivery_constitution_3462c9c3bfa640aa
- variant_diff_report_ref: artifact://variant_diff_report/1.0/art_variant_diff_report_184359648ae84b97
- reporting_model_ref: artifact://reporting_model/1.0/art_reporting_model_2e6343596f4d42cf
- identity_access_model_ref: artifact://identity_access_model/1.0/art_identity_access_model_99e81951c7674563
- discover_review_checklist_ref: artifact://discover_review_checklist/1.0/art_discover_review_checklist_8b160b2ea93c469d
- artifact_index_ref: artifact://artifact_index/1.0/art_artifact_index_1905f4cd09864d56
- qa_report_ref: embedded://analyst_report_v2/qa_report_v1
- knowledge_snapshot_ref: runctx://runctx-fe02f94508769f61/kctx-b5bbe5ad54a31f2d
- run_delivery_constitution_ref: runctx://runctx-fe02f94508769f61/delivery_constitution/const-a51cbbbde910
- High-volume sections included in structured artifact (inventory, dependencies, event map, SQL catalog, business rules).

## Appendix Snapshot
- Legacy inventory: present
- Event map rows: 60
- SQL catalog rows: 62
- SQL map rows: 29
- Procedure summaries: 60
- Form dossiers: 26
- Dependency rows: 24
- Business rules: 35
- Risk register rows: 26
- Orphan analysis rows: 1
- Repo landscape variants: 3
- Variant inventory rows: 3
- Constitution principles: 3

## Detailed Appendix

### A. Legacy Inventory
- Projects: 3
- Data touchpoints: transctions, customer, accounttype, deposit, withdrawal, transactions, balancedt, LOGIN, logi, tblBalances, tblCustomers, tblTransactions, fragmented
| Project | Type | Startup | Members | Forms | Reports | Dependencies | Shared tables |
|---|---|---|---:|---:|---:|---:|---|
| BANK_SYSTEM | Exe | Main | 9 | 8 | 0 | 7 | tblBalances, tblCustomers, tblTransactions |
| Project1 (BANKING.vbp) | Exe | main | 10 | 8 | 0 | 6 | accounttype, balancedt, customer, deposit, logi, transactions, transctions, withdrawal |
| Project1 (STUDENT BANKING/BANKING.vbp) | Exe | main | 11 | 10 | 1 | 6 | LOGIN, accounttype, customer, deposit, logi, tblBalances, tblCustomers, transctions |

### B. Dependency Inventory
| Name | Kind | GUID / Reference | Risk | Recommended action | Forms mapped |
|---|---|---|---|---|---|
| MSCOMCT2.OCX | ocx | {86CF1D34-0C5F-11D2-A9FC-0000F8754DA1}#2.0#0; MSCOMCT2.OCX | medium | Assess replacement/interop strategy. | BANK_SYSTEM::Main, BANK_SYSTEM::frmAccTypes, BANK_SYSTEM::frmCustomers, BANK_SYSTEM::frmDeposits, BANK_SYSTEM::frmSearch, BANK_SYSTEM::frmTransaction |
| MSCOMCTL.OCX | ocx | {831FDD16-0C5C-11D2-A9FC-0000F8754DA1}#2.0#0; MSCOMCTL.OCX | medium | Assess replacement/interop strategy. | BANK_SYSTEM::Main, BANK_SYSTEM::frmAccTypes, BANK_SYSTEM::frmCustomers, BANK_SYSTEM::frmDeposits, BANK_SYSTEM::frmSearch, BANK_SYSTEM::frmTransaction |
| MSComCtl2.DTPicker | com_typelib | n/a | medium | Assess replacement/interop strategy. | BANK_SYSTEM::Main, BANK_SYSTEM::frmAccTypes, BANK_SYSTEM::frmCustomers, BANK_SYSTEM::frmDeposits, BANK_SYSTEM::frmSearch, BANK_SYSTEM::frmTransaction |
| MSComctlLib.ListView | com_typelib | n/a | medium | Assess replacement/interop strategy. | BANK_SYSTEM::Main, BANK_SYSTEM::frmAccTypes, BANK_SYSTEM::frmCustomers, BANK_SYSTEM::frmDeposits, BANK_SYSTEM::frmSearch, BANK_SYSTEM::frmTransaction |
| MSComctlLib.ProgressBar | com_typelib | n/a | medium | Assess replacement/interop strategy. | Project1 (BANKING.vbp)::Form1, Project1 (BANKING.vbp)::Form2, Project1 (BANKING.vbp)::Form3, Project1 (BANKING.vbp)::Form4, Project1 (BANKING.vbp)::Form6, Project1 (BANKING.vbp)::Form7 |
| MSComctlLib.Toolbar | com_typelib | n/a | medium | Assess replacement/interop strategy. | BANK_SYSTEM::Main, BANK_SYSTEM::frmAccTypes, BANK_SYSTEM::frmCustomers, BANK_SYSTEM::frmDeposits, BANK_SYSTEM::frmSearch, BANK_SYSTEM::frmTransaction |
| MSFLXGRD.OCX | ocx | {5E9E78A0-531B-11CF-91F6-C2863C385E30}#1.0#0; MSFLXGRD.OCX | medium | Assess replacement/interop strategy. | Project1 (BANKING.vbp)::Form1, Project1 (BANKING.vbp)::Form2, Project1 (BANKING.vbp)::Form3, Project1 (BANKING.vbp)::Form4, Project1 (BANKING.vbp)::Form6, Project1 (BANKING.vbp)::Form7 |
| MSFlexGridLib.MSFlexGrid | other | n/a | medium | Assess replacement/interop strategy. | Project1 (BANKING.vbp)::Form1, Project1 (BANKING.vbp)::Form2, Project1 (BANKING.vbp)::Form3, Project1 (BANKING.vbp)::Form4, Project1 (BANKING.vbp)::Form6, Project1 (BANKING.vbp)::Form7 |
| MSMask.MaskEdBox | other | n/a | medium | Assess replacement/interop strategy. | BANK_SYSTEM::Main, BANK_SYSTEM::frmAccTypes, BANK_SYSTEM::frmCustomers, BANK_SYSTEM::frmDeposits, BANK_SYSTEM::frmSearch, BANK_SYSTEM::frmTransaction |
| msmask32.ocx | ocx | {C932BA88-4374-101B-A56C-00AA003668DC}#1.1#0; msmask32.ocx | medium | Assess replacement/interop strategy. | BANK_SYSTEM::Main, BANK_SYSTEM::frmAccTypes, BANK_SYSTEM::frmCustomers, BANK_SYSTEM::frmDeposits, BANK_SYSTEM::frmSearch, BANK_SYSTEM::frmTransaction |
| DAO350.DLL | dll | *\G{00025E01-0000-0000-C000-000000000046}#4.0#0#C:\Program Files\Common Files\Microsoft Shared\DAO\DAO350.DLL#Microsoft DAO 3.51 Object Library | medium | Assess replacement/interop strategy. | n/a |
| MSDERUN.DLL | dll | *\G{3D5C6BF0-69A3-11D0-B393-00A0C9055D8E}#1.0#0#C:\Program Files\Common Files\designer\MSDERUN.DLL#Microsoft Data Environment Instance 1.0; *\G{3D5C6BF0-69A3-11D0-B393-00A0C9055D8E}#1.0#0#..\..\..\..\..\Program Files\Common Files\designer\MSDERUN.DLL#Microsoft Data Environment Instance 1.0 | medium | Assess replacement/interop strategy. | n/a |
| MSDBRPTR.DLL | dll | *\G{642AC760-AAB4-11D0-8494-00A0C90DC8A9}#1.0#0#C:\WINDOWS\system32\MSDBRPTR.DLL#Microsoft Data Report Designer v6.0; *\G{642AC760-AAB4-11D0-8494-00A0C90DC8A9}#1.0#0#..\..\..\..\..\WINDOWS\system32\MSDBRPTR.DLL#Microsoft Data Report Designer v6.0 | medium | Assess replacement/interop strategy. | n/a |
| msstdfmt.dll | dll | *\G{6B263850-900B-11D0-9484-00A0C91110ED}#1.0#0#c:\WINDOWS\system32\msstdfmt.dll#Microsoft Data Formatting Object Library 6.0 (SP4); *\G{6B263850-900B-11D0-9484-00A0C91110ED}#1.0#0#..\..\..\..\..\WINDOWS\system32\msstdfmt.dll#Microsoft Data Formatting Object Library | medium | Assess replacement/interop strategy. | n/a |
| MSBIND.DLL | dll | *\G{56BF9020-7A2F-11D0-9482-00A0C91110ED}#1.0#0#C:\WINDOWS\system32\MSBIND.DLL#Microsoft Data Binding Collection; *\G{56BF9020-7A2F-11D0-9482-00A0C91110ED}#1.0#0#..\..\..\..\..\WINDOWS\system32\MSBIND.DLL#Microsoft Data Binding Collection | medium | Assess replacement/interop strategy. | n/a |
| COMCT232.OCX | ocx | {FE0065C0-1B7B-11CF-9D53-00AA003C9CB6}#1.1#0; COMCT232.OCX | medium | Assess replacement/interop strategy. | n/a |
| COMCTL32.OCX | ocx | {6B7E6392-850A-101B-AFC0-4210102A8DA7}#1.3#0; COMCTL32.OCX | medium | Assess replacement/interop strategy. | n/a |
| MSWINSCK.OCX | ocx | {248DD890-BB45-11CF-9ABC-0080C7E7B78D}#1.0#0; MSWINSCK.OCX | medium | Assess replacement/interop strategy. | n/a |
| ieinfo5.ocx | ocx | {25959BEC-E700-11D2-A7AF-00C04F806200}#1.0#0; ieinfo5.ocx | medium | Assess replacement/interop strategy. | n/a |
| COMCT332.OCX | ocx | {38911DA0-E448-11D0-84A3-00DD01104159}#1.1#0; COMCT332.OCX | medium | Assess replacement/interop strategy. | n/a |
| MSADODC.OCX | ocx | {67397AA1-7FB1-11D0-B148-00A0C922E820}#6.0#0; MSADODC.OCX | medium | Assess replacement/interop strategy. | n/a |
| msvidctl.dll | dll | {B0EDF154-910A-11D2-B632-00C04F79498E}#1.0#0; msvidctl.dll | medium | Assess replacement/interop strategy. | n/a |
| FLEXWIZ.OCX | ocx | {BEC61919-E6C4-11D1-BE7D-C63815000000}#1.0#0; FLEXWIZ.OCX | medium | Assess replacement/interop strategy. | n/a |
| agentctl.dll | dll | {F5BE8BC2-7DE6-11D0-91FE-00C04FD701A5}#2.0#0; agentctl.dll | medium | Assess replacement/interop strategy. | n/a |

### C. Event Map
| Entry | Container | Trigger | Calls | Side effects |
|---|---|---|---|---|
| Project1 (STUDENT BANKING/BANKING.vbp)::main:Click | Project1 (STUDENT BANKING/BANKING.vbp)::main | Click | frm, RS, Form9, MsgBox | logi |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form1:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form1 | Click | Form7 | n/a |
| shared_module:event | shared_module |  | CON | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form1:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form1 | Click | Form7 | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | Click | frm, RS, txtAccountID, txtAccountName | accounttype |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | Click | CON, MsgBox | accounttype |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | Click | RS, txtAccountID, txtAccountName, txtDescription | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | Click | RS, txtAccountID, txtAccountName, txtDescription | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | Click | RS, txtAccountID, txtAccountName, txtDescription | n/a |
| shared_module:event | shared_module |  | MsgBox, Exit | n/a |
| shared_module:Form_Controls | shared_module | Form_Controls | ctrl | n/a |
| shared_module:event | shared_module |  | MsgBox | n/a |
| shared_module:event | shared_module |  | CheckDatabaseStatus, MsgBox, Exit | n/a |
| shared_module:event | shared_module |  | CheckDatabaseStatus, MsgBox, Exit | n/a |
| shared_module:event | shared_module |  | CheckDatabaseStatus, MsgBox, Exit | n/a |
| shared_module:event | shared_module |  | CheckDatabaseStatus, MsgBox, Exit | n/a |
| shared_module:Form_Controls | shared_module | Form_Controls | ctrl | n/a |
| shared_module:event | shared_module |  | KeyAscii, MsgBox | n/a |
| shared_module:event | shared_module |  | MsgBox, KeyAscii | n/a |
| shared_module:Form_Controls | shared_module | Form_Controls | ctrl | n/a |
| shared_module:event | shared_module |  | rsCustomers, rsDeposit, rsWithdrawal, rsBalances | n/a |
| shared_module:event | shared_module |  | cnBank | n/a |
| shared_module:event | shared_module |  | maskCtrl | n/a |
| shared_module:event | shared_module |  | txtCtrl | n/a |
| BANK_SYSTEM::Main:Load | BANK_SYSTEM::Main | Load | connectDatabase | n/a |
| BANK_SYSTEM::Main:ButtonClick | BANK_SYSTEM::Main | ButtonClick | frmCustomers, frmDeposits, frmWithdrawal, frmTransactions | n/a |
| BANK_SYSTEM::Main:ButtonMenuClick | BANK_SYSTEM::Main | ButtonMenuClick | frmAccTypes, rptCustomers, rptDeposits, rptWithdrawals | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | Click | n/a | n/a |
| BANK_SYSTEM::frmAccTypes:event | BANK_SYSTEM::frmAccTypes |  | txtAccountID, txtAccountName, txtDescription, txtInterestRate | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form8:Load | Project1 (STUDENT BANKING/BANKING.vbp)::Form8 | Load | connectDatabase, cmdAdd, cmdSave, cmdCancel | n/a |
| BANK_SYSTEM::frmAccTypes:event | BANK_SYSTEM::frmAccTypes |  | lastnumber, newnumber, txtAccountID | n/a |
| BANK_SYSTEM::frmCustomers:Click | BANK_SYSTEM::frmCustomers | Click | cmdAdd, cmdSave, cmdCancel, cmdEdit | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | Click | MoveToPrev, DisplayaccTypes, lblStatus | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | Click | n/a | n/a |
| BANK_SYSTEM::frmWithdrawal:event | BANK_SYSTEM::frmWithdrawal |  | lastnumber, newnumber, txtTransactionID | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form3:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | Click | rsTemp, txtAccountNo, txtNarration, MsgBox | tblCustomers, tblBalances |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form3:KeyPress | Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | KeyPress | n/a | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form3:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | Click | NewRecord, clear_Form_Controls, GenerateNewTransactCode, cboCustomerNo | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form3:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | Click | txtCheckNo, txtMode | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form3:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | Click | n/a | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form3:Change | Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | Change | n/a | n/a |
| BANK_SYSTEM::frmTransaction:event | BANK_SYSTEM::frmTransaction |  | cboCustomerNo, txtAccountNo, txtNarration, txtCheckNo | n/a |
| BANK_SYSTEM::frmTransaction:Click | BANK_SYSTEM::frmTransaction | Click | Unload | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | Click | NewRecord, clear_Form_Controls, GenerateNewTransactCode, cboCustomerNo | n/a |
| BANK_SYSTEM::frmWithdrawal:Change | BANK_SYSTEM::frmWithdrawal | Change | txtNarration | n/a |
| BANK_SYSTEM::frmWithdrawal:KeyPress | BANK_SYSTEM::frmWithdrawal | KeyPress | ValidNumeric | n/a |
| BANK_SYSTEM::frmWithdrawal:KeyPress | BANK_SYSTEM::frmWithdrawal | KeyPress | KeyAscii | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4:event | Project1 (STUDENT BANKING/BANKING.vbp)::Form4 |  | frm, RS, cboCustomerno | customer |
| Project1 (STUDENT BANKING/BANKING.vbp)::main:Click | Project1 (STUDENT BANKING/BANKING.vbp)::main | Click | Form3 | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::main:Click | Project1 (STUDENT BANKING/BANKING.vbp)::main | Click | Form4 | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::main:Click | Project1 (STUDENT BANKING/BANKING.vbp)::main | Click | Form9 | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::main:Click | Project1 (STUDENT BANKING/BANKING.vbp)::main | Click | DataReport4 | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form8:Timer | Project1 (STUDENT BANKING/BANKING.vbp)::Form8 | Timer | main, Timer1, ProgressBar1, Unload | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6:event | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 |  | msfgroom, RS, RNO | transctions |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | Click | z, txtCustomerID, txtAccountNo, txtNarration | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4:Click | Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | Click | RS, txtaccountno, balance | customer |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4:click | Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | click | frm, RS, cboCustomerno, Exit | withdrawal |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6:event | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 |  | txtAccountID, txtAccountName, txtDescription, txtInterestRate | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::main:Click | Project1 (STUDENT BANKING/BANKING.vbp)::main | Click | Form6 | n/a |
| Project1 (STUDENT BANKING/BANKING.vbp)::main:Click | Project1 (STUDENT BANKING/BANKING.vbp)::main | Click | n/a | n/a |

### D. SQL Catalog
| SQL ID | Kind | Tables | Query |
|---|---|---|---|
| sql:1 | unknown | n/a | "Delete" |
| sql:2 | unknown | transctions | "select * from transctions", CON |
| sql:3 | unknown | transctions | "select count(*) from transctions", CON |
| sql:4 | unknown | customer | "select customerid from customer", CON |
| sql:5 | unknown | accounttype | "select max(AccountID) from accounttype", CON |
| sql:6 | unknown | deposit | "select max(TransactionID) from deposit", CON |
| sql:7 | unknown | accounttype | "select max(accountid) from accounttype", CON |
| sql:8 | unknown | deposit | "select max(transactionid) from deposit", CON |
| sql:9 | unknown | withdrawal | "select max(transactionid) from withdrawal", CON |
| sql:10 | unknown | n/a | &Delete |
| sql:11 | unknown | deposit | ("insert into deposit values(":expr,:expr,:expr,':expr',:expr,':expr','0',':expr"')") |
| sql:12 | unknown | deposit | ("insert into deposit values(":expr,:expr,:expr,':expr',:expr,':expr',0,'CDate(txtDated.Value)"')") |
| sql:13 | unknown | deposit | ("insert into deposit values(":expr,:expr,:expr,':expr',:expr,':expr',:expr,':expr"')") |
| sql:14 | unknown | transactions | ("insert into transactions values(":expr,:expr,':expr','00','CDate(txtDated.Value)',:expr,'0',:expr",'cash')") |
| sql:15 | unknown | transactions | ("insert into transactions values(":expr,:expr,':expr',:expr,':expr',:expr,'0',:expr",'cheque')") |
| sql:16 | unknown | withdrawal | ("insert into withdrawal values(":expr,:expr,:expr,':expr',:expr,:expr")") |
| sql:17 | unknown | accounttype | ("update accounttype set AccountID=":expr,AccountName=':expr',Description=':expr',InterestRate=:expr,MinBalance=:expr where AccountID=:expr"") |
| sql:18 | unknown | balancedt | ("update balancedt set balance=":expr where accno=:expr"") |
| sql:19 | unknown | n/a | .Update |
| sql:20 | delete | n/a | DELETE |
| sql:21 | unknown | n/a | End Select |
| sql:22 | unknown | n/a | MsgBox "Please select Transaction Mode.", vbInformation |
| sql:23 | unknown | n/a | Please select Transaction Mode. |
| sql:24 | select | LOGIN | SELECT * FROM LOGIN WHERE USERNAME <> ' |
| sql:25 | select | LOGIN | SELECT * FROM LOGIN WHERE USERNAME <> ':expr' AND PASSWORD <> :expr"", CON |
| sql:26 | select | LOGIN | SELECT * FROM LOGIN WHERE USERNAME=' |
| sql:27 | select | LOGIN | SELECT * FROM LOGIN WHERE USERNAME=':expr' AND PASSWORD=:expr"", CON |
| sql:28 | select | logi | SELECT * FROM logi WHERE user1=' |
| sql:29 | select | logi | SELECT * FROM logi WHERE user1=':expr' AND pass=':expr"'", CON |
| sql:30 | select | tblBalances | Select * FROM tblBalances WHERE CustomerID=' |
| sql:31 | select | tblBalances | Select * FROM tblBalances WHERE CustomerID=':expr"'", cnBank, adOpenKeyset, adLockOptimistic |
| sql:32 | select | tblCustomers | Select * FROM tblCustomers WHERE CustomerID=' |
| sql:33 | select | tblCustomers | Select * FROM tblCustomers WHERE CustomerID=':expr"'", cnBank, adOpenKeyset, adLockOptimistic |
| sql:34 | select | tblTransactions | Select * FROM tblTransactions WHERE Code= |
| sql:35 | select | tblTransactions | Select * FROM tblTransactions WHERE Code=:expr" ", cnBank, adOpenKeyset, adLockOptimistic |
| sql:36 | select | n/a | Select Case Button.Index |
| sql:37 | select | n/a | Select Case ButtonMenu.Key |
| sql:38 | select | n/a | Select Case KeyAscii |
| sql:39 | select | n/a | Select... |
| sql:40 | insert | deposit | insert into deposit values( |
| sql:41 | insert | transactions | insert into transactions values( |
| sql:42 | insert | withdrawal | insert into withdrawal values( |
| sql:43 | unknown | n/a | record updated |
| sql:44 | select | customer | select * from customer where customerid= |
| sql:45 | select | customer | select * from customer where customerid= :expr"", CON |
| sql:46 | select | transctions | select * from transctions |
| sql:47 | select | balancedt | select balance from balancedt where accno= |
| sql:48 | select | balancedt | select balance from balancedt where accno= :expr"", CON |
| sql:49 | select | transctions | select count(*) from transctions |
| sql:50 | select | customer | select customerid from customer |
| sql:51 | select | accounttype | select max(AccountID) from accounttype |
| sql:52 | select | deposit | select max(TransactionID) from deposit |
| sql:53 | select | accounttype | select max(accountid) from accounttype |
| sql:54 | select | deposit | select max(transactionid) from deposit |
| sql:55 | select | withdrawal | select max(transactionid) from withdrawal |
| sql:56 | update | LOGIN | update LOGIN set USERNAME=' |
| sql:57 | update | LOGIN | update LOGIN set USERNAME=':expr',PASSWORD=:expr where USERNAME=':expr' |
| sql:58 | update | accounttype | update accounttype set AccountID= |
| sql:59 | update | accounttype | update accounttype set accountid= |
| sql:60 | update | accounttype | update accounttype set accountid=:expr,accountname=':expr',description=':expr',interestRate=':expr',minbalance=':expr' where accountid=:expr |
| sql:61 | update | balancedt | update balancedt set balance= |
| sql:62 | delete | transactions | delete from transactions where CustomerID=:expr /* inferred from fragmented legacy SQL */ |

### E. Business Rules
| Rule ID | Form | Layer | Category | Business Meaning | Implementation Evidence | Risk links |
|---|---|---|---|---|---|---|
| BR-001 | n/a | Presentation | business_objective | Manage customer profile lookup and maintenance workflows. | BANK_SYSTEM objective inference | none |
| BR-002 | n/a | Presentation | workflow_orchestration | Workflow is orchestrated through UI event handlers and internal procedures. | BANK_SYSTEM procedure map | none |
| BR-003 | n/a | Data | data_persistence | Form persists and retrieves records from the listed tables. | BANK_SYSTEM SQL/table hints | RISK-026 |
| BR-004 | n/a | Presentation | business_objective | Manage customer profile lookup and maintenance workflows. | Project1 (BANKING.vbp) objective inference | none |
| BR-005 | n/a | Presentation | workflow_orchestration | Workflow is orchestrated through UI event handlers and internal procedures. | Project1 (BANKING.vbp) procedure map | none |
| BR-006 | n/a | Data | data_persistence | Form persists and retrieves records from the listed tables. | Project1 (BANKING.vbp) SQL/table hints | RISK-026 |
| BR-007 | n/a | Presentation | business_objective | User authentication is required before entering the workflow. | Project1 (STUDENT BANKING/BANKING.vbp) objective inference | none |
| BR-008 | n/a | Presentation | workflow_orchestration | Workflow is orchestrated through UI event handlers and internal procedures. | Project1 (STUDENT BANKING/BANKING.vbp) procedure map | none |
| BR-009 | n/a | Data | data_persistence | Form persists and retrieves records from the listed tables. | Project1 (STUDENT BANKING/BANKING.vbp) SQL/table hints | none |
| BR-010 | LOGIN | Data | threshold_rule | The action proceeds only when the recordset/connection is active. | LOGIN.frm:158 | none |
| BR-016 | accounttype | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | STUDENT BANKING/accounttype.frm:318 | none |
| BR-017 | n/a | Shared | input_validation | Input validation rule detected (IsNumeric/IsDate/Len). | STUDENT BANKING/banking finish/BANKING/Modules/Mdl.bas:95 | none |
| BR-018 | n/a | Shared | decision_branching | Keyboard input routing determines which action path is executed. | STUDENT BANKING/banking finish/BANKING/Modules/Mdl.bas:104 | none |
| BR-019 | main | Presentation | decision_branching | User menu selection routes the workflow to the corresponding module. | STUDENT BANKING/banking finish/BANKING/UserInterface/Main.frm:167 | none |
| BR-020 | main | Presentation | decision_branching | User menu selection routes the workflow to the corresponding module. | STUDENT BANKING/banking finish/BANKING/UserInterface/Main.frm:180 | none |
| BR-022 | frmDeposits | Data | threshold_rule | The action proceeds only when matching records are found. | STUDENT BANKING/banking finish/BANKING/UserInterface/frmDeposits.frm:284 | RISK-026 |
| BR-023 | frmDeposits | Data | threshold_rule | Pressing Enter triggers the same action flow as the primary button. | STUDENT BANKING/banking finish/BANKING/UserInterface/frmDeposits.frm:311 | RISK-026 |
| BR-024 | frmDeposits | Data | calculation_logic | Balance is recalculated from the displayed balance label and entered amount (UI-derived source). | STUDENT BANKING/banking finish/BANKING/UserInterface/frmDeposits.frm:382 | RISK-026 |
| BR-027 | frmWithdrawal | Data | threshold_rule | The action proceeds only when matching records are found. | STUDENT BANKING/banking finish/BANKING/UserInterface/frmWithdrawal.frm:200 | none |
| BR-028 | frmWithdrawal | Data | threshold_rule | Pressing Enter triggers the same action flow as the primary button. | STUDENT BANKING/banking finish/BANKING/UserInterface/frmWithdrawal.frm:227 | none |
| BR-029 | frmWithdrawal | Data | calculation_logic | Balance is recalculated from the displayed balance label and entered amount (UI-derived source). | STUDENT BANKING/banking finish/BANKING/UserInterface/frmWithdrawal.frm:280 | RISK-026 |
| BR-034 | splash | Data | calculation_logic | Splash/loading behavior advances progress state before opening workflow screens. | STUDENT BANKING/splash.frm:47 | none |
| BR-046 | BANK_SYSTEM::frmDeposits | Data | threshold_rule | The action proceeds only when matching records are found. | mirrored_from_variant_mapping (source=BR-022) | RISK-026 |
| BR-047 | BANK_SYSTEM::frmDeposits | Data | threshold_rule | Pressing Enter triggers the same action flow as the primary button. | mirrored_from_variant_mapping (source=BR-023) | RISK-026 |
| BR-048 | BANK_SYSTEM::frmDeposits | Data | calculation_logic | Balance is recalculated from the displayed balance label and entered amount (UI-derived source). | mirrored_from_variant_mapping (source=BR-024) | RISK-026 |
| BR-052 | BANK_SYSTEM::frmWithdrawal | Data | threshold_rule | The action proceeds only when matching records are found. | mirrored_from_variant_mapping (source=BR-027) | none |
| BR-053 | BANK_SYSTEM::frmWithdrawal | Data | threshold_rule | Pressing Enter triggers the same action flow as the primary button. | mirrored_from_variant_mapping (source=BR-028) | none |
| BR-054 | BANK_SYSTEM::frmWithdrawal | Data | calculation_logic | Balance is recalculated from the displayed balance label and entered amount (UI-derived source). | mirrored_from_variant_mapping (source=BR-029) | none |
| BR-056 | BANK_SYSTEM::main | Presentation | decision_branching | User menu selection routes the workflow to the corresponding module. | mirrored_from_variant_mapping (source=BR-019) | none |
| BR-057 | BANK_SYSTEM::main | Presentation | decision_branching | User menu selection routes the workflow to the corresponding module. | mirrored_from_variant_mapping (source=BR-020) | none |
| BR-058 | Project1 (BANKING.vbp)::Form3 | Data | threshold_rule | The action proceeds only when matching records are found. | mirrored_from_variant_mapping (source=BR-022) | RISK-010, RISK-011, RISK-012, RISK-013 |
| BR-059 | Project1 (BANKING.vbp)::Form3 | Data | threshold_rule | Pressing Enter triggers the same action flow as the primary button. | mirrored_from_variant_mapping (source=BR-023) | RISK-010, RISK-011, RISK-012, RISK-013 |
| BR-060 | Project1 (BANKING.vbp)::Form3 | Data | calculation_logic | Balance is recalculated from the displayed balance label and entered amount (UI-derived source). | mirrored_from_variant_mapping (source=BR-024) | RISK-010, RISK-011, RISK-012, RISK-013 |
| BR-062 | Project1 (BANKING.vbp)::Form4 | Data | threshold_rule | The action proceeds only when matching records are found. | mirrored_from_variant_mapping (source=BR-027) | RISK-016, RISK-017 |
| BR-063 | Project1 (BANKING.vbp)::Form4 | Data | threshold_rule | Pressing Enter triggers the same action flow as the primary button. | mirrored_from_variant_mapping (source=BR-028) | RISK-016, RISK-017 |
| BR-064 | Project1 (BANKING.vbp)::Form4 | Data | calculation_logic | Balance is recalculated from the displayed balance label and entered amount (UI-derived source). | mirrored_from_variant_mapping (source=BR-029) | RISK-016, RISK-017 |
| BR-068 | Project1 (BANKING.vbp)::main | Presentation | decision_branching | User menu selection routes the workflow to the corresponding module. | mirrored_from_variant_mapping (source=BR-019) | none |
| BR-069 | Project1 (BANKING.vbp)::main | Presentation | decision_branching | User menu selection routes the workflow to the corresponding module. | mirrored_from_variant_mapping (source=BR-020) | none |
| BR-070 | Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | Data | threshold_rule | The action proceeds only when matching records are found. | mirrored_from_variant_mapping (source=BR-022) | RISK-010, RISK-011, RISK-012, RISK-013 |
| BR-071 | Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | Data | threshold_rule | Pressing Enter triggers the same action flow as the primary button. | mirrored_from_variant_mapping (source=BR-023) | RISK-010, RISK-011, RISK-012, RISK-013 |
| BR-072 | Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | Data | calculation_logic | Balance is recalculated from the displayed balance label and entered amount (UI-derived source). | mirrored_from_variant_mapping (source=BR-024) | RISK-010, RISK-011, RISK-012, RISK-013 |
| BR-074 | Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | Data | threshold_rule | The action proceeds only when matching records are found. | mirrored_from_variant_mapping (source=BR-027) | RISK-016, RISK-017 |
| BR-075 | Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | Data | threshold_rule | Pressing Enter triggers the same action flow as the primary button. | mirrored_from_variant_mapping (source=BR-028) | RISK-016, RISK-017 |
| BR-076 | Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | Data | calculation_logic | Balance is recalculated from the displayed balance label and entered amount (UI-derived source). | mirrored_from_variant_mapping (source=BR-029) | RISK-016, RISK-017 |
| BR-080 | Project1 (STUDENT BANKING/BANKING.vbp)::main | Presentation | decision_branching | User menu selection routes the workflow to the corresponding module. | mirrored_from_variant_mapping (source=BR-019) | none |
| BR-081 | Project1 (STUDENT BANKING/BANKING.vbp)::main | Presentation | decision_branching | User menu selection routes the workflow to the corresponding module. | mirrored_from_variant_mapping (source=BR-020) | none |
| BR-082 | BANK_SYSTEM::frmAccTypes | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | variant_backfill_for_eq_sync (source=BR-021); source_rule=BR-021 | none |
| BR-083 | BANK_SYSTEM::frmTransaction | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | variant_backfill_for_eq_sync (source=BR-026); source_rule=BR-026 | none |
| BR-084 | BANK_SYSTEM::frmTransactions | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | variant_backfill_for_eq_sync (source=BR-026); source_rule=BR-026 | none |
| BR-085 | Project1 (BANKING.vbp)::Form6 | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | variant_backfill_for_eq_sync (source=BR-026); source_rule=BR-026 | RISK-003, RISK-018, RISK-023 |
| BR-086 | Project1 (BANKING.vbp)::Form7 | Data | threshold_rule | The action proceeds only when the recordset/connection is active. | variant_backfill_for_eq_sync (source=BR-010); source_rule=BR-010 | none |
| BR-087 | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | Data | calculation_logic | Balance is recalculated using the entered amount and current account value. | variant_backfill_for_eq_sync (source=BR-026); source_rule=BR-026 | RISK-003, RISK-018, RISK-023 |
| BR-088 | Project1 (STUDENT BANKING/BANKING.vbp)::Form7 | Data | threshold_rule | The action proceeds only when the recordset/connection is active. | variant_backfill_for_eq_sync (source=BR-010); source_rule=BR-010 | none |

### E1. Rule Cross-Reference by Form
- a: rule_ids=[BR-001, BR-002, BR-003, BR-004, BR-005, BR-006, BR-007, BR-008]; summary=Manage customer profile lookup and maintenance workflows. / Workflow is orchestrated through UI event handlers and internal procedures. / Form persists and retrieves records from the listed tables.
- accounttype: rule_ids=[BR-016]; summary=Balance is recalculated using the entered amount and current account value.
- deposit: rule_ids=[BR-016]; summary=Balance is recalculated using the entered amount and current account value.
- form3: rule_ids=[BR-016, BR-058, BR-059, BR-060, BR-070, BR-071, BR-072]; summary=Deposit capture and balance posting workflow Captures account no, amount deposited, cash, check no, cheque, customer no. Business outcome: Deposit transaction recorded.; Account balance recalculated.; Transaction history updated.. / The action proceeds only when matching records are found. / Pressing Enter triggers the same action flow as the primary button.
- form4: rule_ids=[BR-016, BR-062, BR-063, BR-064, BR-074, BR-075, BR-076]; summary=Withdrawal processing and balance deduction workflow Captures account no, amount withdrawn, customer no, dated, narration, transaction id. Business outcome: Withdrawal transaction recorded.; Account balance recalculated.; Transaction history updated.. / The action proceeds only when matching records are found. / Pressing Enter triggers the same action flow as the primary button.
- form6: rule_ids=[BR-016, BR-085, BR-087]; summary=Transaction ledger management and adjustment workflow Captures account id, account name, account no, balance, check no, credit. Business outcome: Deposit transaction recorded.; Account balance recalculated.; Withdrawal transaction recorded.. / Balance is recalculated using the entered amount and current account value.
- form7: rule_ids=[BR-010, BR-086, BR-088]; summary=Captures name, name 1, pass, pass 1. Business outcome: User access is validated before workflow continuation.. / The action proceeds only when the recordset/connection is active.
- frmacctypes: rule_ids=[BR-016, BR-082]; summary=Account type maintenance and account setup workflow Captures account id, account name, description, interest rate, min balance. Business outcome: Account type master data maintained.; Navigation routes the user to selected module screens.. / Balance is recalculated using the entered amount and current account value.
- frmdeposits: rule_ids=[BR-016, BR-022, BR-023, BR-024, BR-046, BR-047, BR-048]; summary=Deposit capture and balance posting workflow Captures account no, amount deposited, cash, check no, cheque, customer no. Business outcome: Deposit transaction recorded.; Account balance recalculated.; Transaction history updated.. / The action proceeds only when matching records are found. / Pressing Enter triggers the same action flow as the primary button.
- frmtransaction: rule_ids=[BR-016, BR-083]; summary=Transaction ledger management and adjustment workflow Captures account no, balance, check no, code, credit, customer id. Business outcome: Deposit transaction recorded.; Account balance recalculated.; Withdrawal transaction recorded.. / Balance is recalculated using the entered amount and current account value.
- frmtransactions: rule_ids=[BR-016, BR-084]; summary=Transaction ledger management and adjustment workflow Captures acc no, customer id, first, option 1, option 2. Business outcome: Transaction history updated.. / Balance is recalculated using the entered amount and current account value.
- frmwithdrawal: rule_ids=[BR-016, BR-027, BR-028, BR-029, BR-052, BR-053, BR-054]; summary=Withdrawal processing and balance deduction workflow Captures account no, amount withdrawn, customer no, dated, narration, transaction id. Business outcome: Withdrawal transaction recorded.; Account balance recalculated.; Transaction history updated.. / The action proceeds only when matching records are found. / Pressing Enter triggers the same action flow as the primary button.
- login: rule_ids=[BR-010]; summary=The action proceeds only when the recordset/connection is active.
- main: rule_ids=[BR-019, BR-020, BR-056, BR-057, BR-068, BR-069, BR-080, BR-081]; summary=Application navigation and module routing workflow Business outcome: User access is validated before workflow continuation.. / User menu selection routes the workflow to the corresponding module.
- splash: rule_ids=[BR-034]; summary=Splash/loading behavior advances progress state before opening workflow screens.
- transaction: rule_ids=[BR-016]; summary=Balance is recalculated using the entered amount and current account value.
- withdrawal: rule_ids=[BR-016]; summary=Balance is recalculated using the entered amount and current account value.

### E2. Shared Rule Consolidation
- BR-016: consolidated 22 duplicate row(s); applies to 19 form(s): BANK_SYSTEM::frmAccTypes, BANK_SYSTEM::frmDeposits, BANK_SYSTEM::frmTransaction, BANK_SYSTEM::frmTransactions, BANK_SYSTEM::frmWithdrawal, Project1 (BANKING.vbp)::Form3, Project1 (BANKING.vbp)::Form4, Project1 (BANKING.vbp)::Form6, Project1 (STUDENT BANKING/BANKING.vbp)::Form3, Project1 (STUDENT BANKING/BANKING.vbp)::Form4, Project1 (STUDENT BANKING/BANKING.vbp)::Form6, accounttype(+7 more)
  - Canonical meaning: Balance is recalculated using the entered amount and current account value.
- BR-010: consolidated 4 duplicate row(s); applies to 4 form(s): LOGIN, Project1 (BANKING.vbp)::Form7, Project1 (STUDENT BANKING/BANKING.vbp)::Form7, n/a
  - Canonical meaning: The action proceeds only when the recordset/connection is active.

### F. Detector Findings
| Detector | Severity | Count | Summary | Required actions |
|---|---|---:|---|---|
| VB6-OOP-007 | medium | 20 | Form5.frm: default instance references | default_instance_refactor_plan |
| VB6-UI-002 | medium | 78 | LOGIN.frm: control array index markers | ui_migration_strategy |

### G. Artifact Index
| Type | Ref |
|---|---|
| legacy_inventory | artifact://legacy_inventory/1.0/art_legacy_inventory_86dccc60a28243cf |
| repo_landscape | artifact://repo_landscape/1.0/art_repo_landscape_05ffd21f535b4d54 |
| scope_lock | artifact://scope_lock/1.0/art_scope_lock_6d828ec0a21f4b90 |
| variant_inventory | artifact://variant_inventory/1.0/art_variant_inventory_ec4a249c13c64e9a |
| dependency_inventory | artifact://dependency_inventory/1.0/art_dependency_inventory_5dfc18df03614e74 |
| event_map | artifact://event_map/1.0/art_event_map_cca0b661c9c1497d |
| sql_catalog | artifact://sql_catalog/1.0/art_sql_catalog_c93c948671a74f04 |
| sql_map | artifact://sql_map/1.0/art_sql_map_bec81a6a723e489c |
| data_access_map | artifact://data_access_map/1.0/art_data_access_map_c94e2f2a56f64a46 |
| recordset_ops | artifact://recordset_ops/1.0/art_recordset_ops_307836d9d38f4319 |
| procedure_summary | artifact://procedure_summary/1.0/art_procedure_summary_f6b6ea7f704543f8 |
| form_dossier | artifact://form_dossier/1.0/art_form_dossier_e7f2fda265c741d9 |
| business_rule_catalog | artifact://business_rule_catalog/1.0/art_business_rule_catalog_72a7b9b12bbd4876 |
| detector_findings | artifact://detector_findings/1.0/art_detector_findings_e87fee329f794483 |
| risk_register | artifact://risk_register/1.0/art_risk_register_9c6499d243fc4c5a |
| orphan_analysis | artifact://orphan_analysis/1.0/art_orphan_analysis_b48e7998b7234ed9 |
| delivery_constitution | artifact://delivery_constitution/1.0/art_delivery_constitution_3462c9c3bfa640aa |
| variant_diff_report | artifact://variant_diff_report/1.0/art_variant_diff_report_184359648ae84b97 |
| reporting_model | artifact://reporting_model/1.0/art_reporting_model_2e6343596f4d42cf |
| identity_access_model | artifact://identity_access_model/1.0/art_identity_access_model_99e81951c7674563 |
| discover_review_checklist | artifact://discover_review_checklist/1.0/art_discover_review_checklist_8b160b2ea93c469d |

### H. SQL Map
| Form | Procedure | Operation | Tables | Risks | activex_trigger | trace_complete |
|---|---|---|---|---|---|---|
| Project1 (STUDENT BANKING/BANKING.vbp)::main [Authentication] | Command1_Click | select | logi | select_star, string_concatenation, possible_injection, sensitive_credential_query | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::main [Authentication] | Command1_Click | select | logi | select_star, string_concatenation, possible_injection | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6 [Transaction Ledger] | cmdAdd_Click | unknown | accounttype | none | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6 [Transaction Ledger] | cmdAdd_Click | select | accounttype | none | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6 [Transaction Ledger] | cmdEdit_Click | update | accounttype | none | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6 [Transaction Ledger] | cmdEdit_Click | update | accounttype | missing_where_clause | n/a | yes |
| shared_module | ValidNonNumeric | select | n/a | none | n/a | no |
| shared_module | ValidNonNumeric | unknown | n/a | none | n/a | no |
| shared_module | ValidNumeric | select | n/a | none | n/a | no |
| shared_module | ValidNumeric | unknown | n/a | none | n/a | no |
| BANK_SYSTEM::Main [Authentication] | Toolbar1_ButtonClick | select | n/a | none | Toolbar1:MSComctlLib.Toolbar | no |
| BANK_SYSTEM::Main [Authentication] | Toolbar1_ButtonClick | unknown | n/a | none | Toolbar1:MSComctlLib.Toolbar | no |
| BANK_SYSTEM::Main [Authentication] | Toolbar1_ButtonMenuClick | select | n/a | none | Toolbar1:MSComctlLib.Toolbar | no |
| BANK_SYSTEM::Main [Authentication] | Toolbar1_ButtonMenuClick | unknown | n/a | none | Toolbar1:MSComctlLib.Toolbar | no |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4 [Withdrawal Processing] | cmdSave_Click | unknown | n/a | none | n/a | no |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form3 [Deposit Capture] | cboCustomerNo_Click | select | tblCustomers | select_star, string_concatenation, possible_injection | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form3 [Deposit Capture] | cboCustomerNo_Click | select | tblCustomers | select_star, string_concatenation, possible_injection | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form3 [Deposit Capture] | cboCustomerNo_Click | select | tblBalances | select_star, string_concatenation, possible_injection | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form3 [Deposit Capture] | cboCustomerNo_Click | select | tblBalances | select_star, string_concatenation, possible_injection | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4 [Withdrawal Processing] | fill | unknown | customer | none | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4 [Withdrawal Processing] | fill | select | customer | none | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6 [Transaction Ledger] | display | unknown | transctions | none | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6 [Transaction Ledger] | display | select | transctions | none | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6 [Transaction Ledger] | display | unknown | transctions | select_star | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6 [Transaction Ledger] | display | select | transctions | select_star | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4 [Withdrawal Processing] | cboCustomerno_Click | select | customer | select_star | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4 [Withdrawal Processing] | cboCustomerno_Click | select | customer | select_star, string_concatenation, possible_injection | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4 [Withdrawal Processing] | cmdwithdrawn_click | unknown | withdrawal | none | n/a | yes |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4 [Withdrawal Processing] | cmdwithdrawn_click | select | withdrawal | none | n/a | yes |

### I. Handler and Procedure Summaries
| Callable | Kind | Form | SQL IDs | Steps | Risks |
|---|---|---|---|---|---|
| Command1_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main | sql:29, sql:28 | Triggered from Command1 Click. / Invokes procedures: frm, RS, Form9, MsgBox, Text1, Text2. | select_star, string_concatenation, possible_injection, sensitive_credential_query |
| Label3_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form1 | n/a | Triggered from Label3 Click. / Invokes procedures: Form7. | none |
| frm | shared_function | shared_module | n/a | Triggered from frm. / Invokes procedures: CON. | none |
| Label4_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form1 | n/a | Triggered from Label4 Click. / Invokes procedures: Form7. | none |
| cmdAdd_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | sql:7, sql:53 | Triggered from cmdAdd Click. / Invokes procedures: frm, RS, txtAccountID, txtAccountName, txtDescription, txtInterestRate. | none |
| cmdEdit_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | sql:60, sql:59 | Triggered from cmdEdit Click. / Invokes procedures: CON, MsgBox. | missing_where_clause |
| cmdFirst_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | n/a | Triggered from cmdFirst Click. / Invokes procedures: RS, txtAccountID, txtAccountName, txtDescription, txtInterestRate, txtMinBalance. | none |
| cmdLast_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | n/a | Triggered from cmdLast Click. / Invokes procedures: RS, txtAccountID, txtAccountName, txtDescription, txtInterestRate, txtMinBalance. | none |
| cmdNext_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | n/a | Triggered from cmdNext Click. / Invokes procedures: RS, txtAccountID, txtAccountName, txtDescription. | none |
| CheckDatabaseStatus | shared_function | shared_module | n/a | Triggered from CheckDatabaseStatus. / Invokes procedures: MsgBox, Exit. | none |
| Lock_Form_Controls | shared_function | shared_module | n/a | Triggered from Lock Form_Controls. / Invokes procedures: ctrl. | none |
| Messager | shared_function | shared_module | n/a | Triggered from Messager. / Invokes procedures: MsgBox. | none |
| MoveToFirst | shared_function | shared_module | n/a | Triggered from MoveToFirst. / Invokes procedures: CheckDatabaseStatus, MsgBox, Exit. | none |
| MoveToLast | shared_function | shared_module | n/a | Triggered from MoveToLast. / Invokes procedures: CheckDatabaseStatus, MsgBox, Exit. | none |
| MoveToNext | shared_function | shared_module | n/a | Triggered from MoveToNext. / Invokes procedures: CheckDatabaseStatus, MsgBox, Exit. | none |
| MoveToPrev | shared_function | shared_module | n/a | Triggered from MoveToPrev. / Invokes procedures: CheckDatabaseStatus, MsgBox, Exit. | none |
| UnLock_Form_Controls | shared_function | shared_module | n/a | Triggered from UnLock Form_Controls. / Invokes procedures: ctrl. | none |
| ValidNonNumeric | shared_function | shared_module | sql:38, sql:21 | Triggered from ValidNonNumeric. / Invokes procedures: KeyAscii, MsgBox. | none |
| ValidNumeric | shared_function | shared_module | sql:38, sql:21 | Triggered from ValidNumeric. / Invokes procedures: MsgBox, KeyAscii. | none |
| clear_Form_Controls | shared_function | shared_module | n/a | Triggered from clear Form_Controls. / Invokes procedures: ctrl. | none |
| connectDatabase | shared_function | shared_module | n/a | Triggered from connectDatabase. / Invokes procedures: rsCustomers, rsDeposit, rsWithdrawal, rsBalances, rsTransactions, rsAccTypes. | none |
| disconnectDatabase | shared_function | shared_module | n/a | Triggered from disconnectDatabase. / Invokes procedures: cnBank. | none |
| selectMaskControl | shared_function | shared_module | n/a | Triggered from selectMaskControl. / Invokes procedures: maskCtrl. | none |
| selectTextControl | shared_function | shared_module | n/a | Triggered from selectTextControl. / Invokes procedures: txtCtrl. | none |
| MDIForm_Load | event_handler | BANK_SYSTEM::Main | n/a | Triggered from MDIForm Load. / Invokes procedures: connectDatabase. | none |
| Toolbar1_ButtonClick | event_handler | BANK_SYSTEM::Main | sql:36, sql:21 | Triggered from Toolbar1 ButtonClick. / Invokes procedures: frmCustomers, frmDeposits, frmWithdrawal, frmTransactions. | none |
| Toolbar1_ButtonMenuClick | event_handler | BANK_SYSTEM::Main | sql:37, sql:21 | Triggered from Toolbar1 ButtonMenuClick. / Invokes procedures: frmAccTypes, rptCustomers, rptDeposits, rptWithdrawals. | none |
| cmdQuit_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | n/a | Triggered from cmdQuit Click. / Runs in form context Project1 (STUDENT BANKING/BANKING.vbp)::Form4. | none |
| DisplayaccTypes | procedure | BANK_SYSTEM::frmAccTypes | n/a | Triggered from DisplayaccTypes. / Invokes procedures: txtAccountID, txtAccountName, txtDescription, txtInterestRate, txtMinBalance. | none |
| Form_Load | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form8 | n/a | Triggered from Form Load. / Invokes procedures: connectDatabase, cmdAdd, cmdSave, cmdCancel, cmdEdit, cmdQuit. | none |
| GenerateNewAccountCode | procedure | BANK_SYSTEM::frmAccTypes | n/a | Triggered from GenerateNewAccountCode. / Invokes procedures: lastnumber, newnumber, txtAccountID. | none |
| cmdCancel_Click | event_handler | BANK_SYSTEM::frmCustomers | n/a | Triggered from cmdCancel Click. / Invokes procedures: cmdAdd, cmdSave, cmdCancel, cmdEdit, cmdQuit, NewRecord. | none |
| cmdPrevious_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | n/a | Triggered from cmdPrevious Click. / Invokes procedures: MoveToPrev, DisplayaccTypes, lblStatus. | none |
| cmdSave_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | sql:19 | Triggered from cmdSave Click. / Executes 1 SQL statement(s) affecting transactional state. | none |
| GenerateNewTransactCode | procedure | BANK_SYSTEM::frmWithdrawal | n/a | Triggered from GenerateNewTransactCode. / Invokes procedures: lastnumber, newnumber, txtTransactionID. | none |
| cboCustomerNo_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | sql:33, sql:32, sql:31, sql:30 | Triggered from cboCustomerNo Click. / Invokes procedures: rsTemp, txtAccountNo, txtNarration, MsgBox, Exit, lblBalance. | select_star, string_concatenation, possible_injection |
| cboCustomerNo_KeyPress | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | n/a | Triggered from cboCustomerNo KeyPress. / Runs in form context Project1 (STUDENT BANKING/BANKING.vbp)::Form3. | none |
| cmdDeposit_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | n/a | Triggered from cmdDeposit Click. / Invokes procedures: NewRecord, clear_Form_Controls, GenerateNewTransactCode, cboCustomerNo. | none |
| optCash_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | n/a | Triggered from optCash Click. / Invokes procedures: txtCheckNo, txtMode. | none |
| optCheque_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | n/a | Triggered from optCheque Click. / Runs in form context Project1 (STUDENT BANKING/BANKING.vbp)::Form3. | none |
| Text1_Change | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | n/a | Triggered from Text1 Change. / Runs in form context Project1 (STUDENT BANKING/BANKING.vbp)::Form3. | none |
| DisplayTransact | procedure | BANK_SYSTEM::frmTransaction | n/a | Triggered from DisplayTransact. / Invokes procedures: cboCustomerNo, txtAccountNo, txtNarration, txtCheckNo, txtDated, txtAmountDeposited. | none |
| cmdClose_Click | event_handler | BANK_SYSTEM::frmTransaction | n/a | Triggered from cmdClose Click. / Invokes procedures: Unload. | none |
| cmdWithdraw_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | n/a | Triggered from cmdWithdraw Click. / Invokes procedures: NewRecord, clear_Form_Controls, GenerateNewTransactCode, cboCustomerNo. | none |
| txtAccountNo_Change | event_handler | BANK_SYSTEM::frmWithdrawal | n/a | Triggered from txtAccountNo Change. / Invokes procedures: txtNarration. | none |
| txtAmountwithdrawn_KeyPress | event_handler | BANK_SYSTEM::frmWithdrawal | n/a | Triggered from txtAmountwithdrawn KeyPress. / Invokes procedures: ValidNumeric. | none |
| txtNarration_KeyPress | event_handler | BANK_SYSTEM::frmWithdrawal | n/a | Triggered from txtNarration KeyPress. / Invokes procedures: KeyAscii. | none |
| fill | procedure | Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | sql:4, sql:50 | Triggered from fill. / Invokes procedures: frm, RS, cboCustomerno. | none |
| Command2_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main | n/a | Triggered from Command2 Click. / Invokes procedures: Form3. | none |
| Command3_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main | n/a | Triggered from Command3 Click. / Invokes procedures: Form4. | none |
| Command6_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main | n/a | Triggered from Command6 Click. / Invokes procedures: Form9. | none |
| Command4_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main | n/a | Triggered from Command4 Click. / Invokes procedures: DataReport4. | none |
| Timer1_Timer | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form8 | n/a | Triggered from Timer1 Timer. / Invokes procedures: main, Timer1, ProgressBar1, Unload. | none |
| display | procedure | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | sql:3, sql:49, sql:2, sql:46 | Triggered from display. / Invokes procedures: msfgroom, RS, RNO. | select_star |
| msfgroom_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | n/a | Triggered from msfgroom Click. / Invokes procedures: z, txtCustomerID, txtAccountNo, txtNarration, txtCheckNo, txtDated. | none |
| cboCustomerno_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | sql:45, sql:44 | Triggered from cboCustomerno Click. / Invokes procedures: RS, txtaccountno, balance. | select_star, string_concatenation, possible_injection |
| cmdwithdrawn_click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | sql:9, sql:55 | Triggered from cmdwithdrawn click. / Invokes procedures: frm, RS, cboCustomerno, Exit. | none |
| clr | procedure | Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | n/a | Triggered from clr. / Invokes procedures: txtAccountID, txtAccountName, txtDescription, txtInterestRate, txtMinBalance. | none |
| Command5_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main | n/a | Triggered from Command5 Click. / Invokes procedures: Form6. | none |
| Command7_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main | n/a | Triggered from Command7 Click. / Runs in form context Project1 (STUDENT BANKING/BANKING.vbp)::main. | none |

### J. Delivery Constitution
- Preserve critical legacy behavior first; modernization must prove functional equivalence.
- Every modernization decision must map to explicit evidence (code, query, event, or rule).
- No breaking change to data contracts without approved migration path and rollback evidence.

### K. Form Dossiers
| Form | Display Name | Project | form_type | Status | Purpose | Inputs (data) | Outputs (effects) | ActiveX used | DB tables | Actions | Coverage | Confidence | Exclusion reason |
|---|---|---|---|---|---|---|---|---|---|---:|---:|---:|---|
| frmAccTypes | frmAccTypes [Account Type Maintenance] | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | Child | mapped | Account type maintenance and account setup workflow. | account id, account name, description, interest rate, min balance | Account type master data maintained., Navigation routes the user to selected module screens. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ListView, MSComctlLib.Toolbar, msmask32.ocx | n/a | 2 | 1.00 | 0.74 | none |
| frmCustomers | frmCustomers [Customer Management] | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | Child | mapped | Customer profile onboarding and maintenance workflow. | acc type, account no, email, mobile no, opening bal, phone no | Customer profile created or updated., Navigation routes the user to selected module screens. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ListView, MSComctlLib.Toolbar, MSMask.MaskEdBox | n/a | 1 | 1.00 | 0.71 | none |
| frmDeposits | frmDeposits [Deposit Capture] | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | Child | mapped | Deposit capture and balance posting workflow. | account no, amount deposited, cash, check no, cheque, customer no, dated, mode | Account balance recalculated., Deposit transaction recorded., Transaction history updated. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ListView, MSComctlLib.Toolbar, msmask32.ocx | n/a | 0 | 1.00 | 0.51 | none |
| frmSearch | frmSearch [Record Search] | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | Child | mapped | Record search and retrieval workflow. | n/a | Matching records displayed to the user. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ListView, MSComctlLib.Toolbar, msmask32.ocx | n/a | 0 | 1.00 | 0.46 | none |
| frmTransaction | frmTransaction [Transaction Entry] | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | Child | mapped | Transaction ledger management and adjustment workflow. | account no, balance, check no, code, credit, customer id, dated, debit | Account balance recalculated., Deposit transaction recorded., Transaction history updated., Withdrawal transaction recorded. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ListView, MSComctlLib.Toolbar, msmask32.ocx | n/a | 2 | 1.00 | 0.74 | none |
| frmTransactions | frmTransactions [Transaction History] | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | Child | mapped | Transaction ledger management and adjustment workflow. | acc no, customer id, first, option 1, option 2 | Transaction history updated. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ListView, MSComctlLib.Toolbar, msmask32.ocx | n/a | 0 | 1.00 | 0.51 | none |
| frmWithdrawal | frmWithdrawal [Withdrawal Processing] | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | Child | mapped | Withdrawal processing and balance deduction workflow. | account no, amount withdrawn, amountwithdrawn, customer no, dated, narration, transaction id | Account balance recalculated., Transaction history updated., Withdrawal transaction recorded. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ListView, MSComctlLib.Toolbar, msmask32.ocx | n/a | 4 | 1.00 | 0.81 | none |
| Main | Main [Authentication] | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | MDI_Host | mapped | Application navigation and module routing workflow. | n/a | User access is validated before workflow continuation. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ListView, MSComctlLib.Toolbar, msmask32.ocx | logi | 10 | 1.00 | 0.98 | none |
| accounttype.frm | accounttype.frm [Account Type Maintenance] | Project1 (BANKING.vbp) [BANKING.vbp] | Child | excluded | Account Type Maintenance workflow. | n/a | Account type master data maintained., Navigation routes the user to selected module screens. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| deposit.frm | deposit.frm [Deposit Capture] | Project1 (BANKING.vbp) [BANKING.vbp] | Child | excluded | Deposit Capture workflow. | n/a | Account balance recalculated., Deposit transaction recorded. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| Form1 | Form1 [Navigation/Menu] | Project1 (BANKING.vbp) [BANKING.vbp] | Child | mapped | Business workflow executed through event-driven UI controls. | n/a | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 2 | 1.00 | 0.49 | none |
| Form2 | Form2 [Date/Period Entry] | Project1 (BANKING.vbp) [BANKING.vbp] | Child | mapped | Business workflow executed through event-driven UI controls. | date 1 | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 1.00 | 0.31 | none |
| Form3 | Form3 [Deposit Capture] | Project1 (BANKING.vbp) [BANKING.vbp] | Child | mapped | Deposit capture and balance posting workflow. | account no, amount deposited, cash, check no, cheque, customer no, dated, narration | Customer balance and account details displayed for review., Customer details displayed for review. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | tblBalances, tblCustomers | 6 | 1.00 | 0.98 | none |
| Form4 | Form4 [Withdrawal Processing] | Project1 (BANKING.vbp) [BANKING.vbp] | Child | mapped | Withdrawal processing and balance deduction workflow. | account no, amount withdrawn, customer no, dated, narration, transaction id | Customer details displayed for review., Withdrawal transaction recorded. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | customer, withdrawal | 6 | 1.00 | 0.98 | none |
| Form6 | Form6 [Transaction Ledger] | Project1 (BANKING.vbp) [BANKING.vbp] | Child | mapped | Transaction ledger management and adjustment workflow. | account id, account name, account no, balance, check no, credit, customer id, dated | Account type configuration updated., Account type details displayed for selection., Transaction history displayed for review., Transaction ledger updated. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | accounttype, transctions | 9 | 1.00 | 0.98 | none |
| Form7 | Form7 [Password Management] | Project1 (BANKING.vbp) [BANKING.vbp] | Child | mapped | Business workflow executed through event-driven UI controls. | name, name 1, pass, pass 1 | User access is validated before workflow continuation. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 1.00 | 0.31 | none |
| Form8 | Form8 [Splash/Loading] | Project1 (BANKING.vbp) [BANKING.vbp] | Child | mapped | Business workflow executed through event-driven UI controls. | n/a | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 2 | 1.00 | 0.49 | none |
| frmcustomer.frm | frmcustomer.frm [Customer Management] | Project1 (BANKING.vbp) [BANKING.vbp] | Child | excluded | Customer Management workflow. | n/a | Customer profile created or updated. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| LOGIN.frm | LOGIN.frm [Authentication] | Project1 (BANKING.vbp) [BANKING.vbp] | Login | excluded | Authentication workflow. | n/a | User access is validated before workflow continuation. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| main | main [Authentication] | Project1 (BANKING.vbp) [BANKING.vbp] | MDI_Host | mapped | Application navigation and module routing workflow. | n/a | User credentials validated against stored records. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | logi | 10 | 1.00 | 0.98 | none |
| newlogin.frm | newlogin.frm [Authentication] | Project1 (BANKING.vbp) [BANKING.vbp] | Login | excluded | Authentication workflow. | n/a | User access is validated before workflow continuation. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| splash.frm | splash.frm [Splash/Loading] | Project1 (BANKING.vbp) [BANKING.vbp] | Splash | excluded | Splash/Loading workflow. | n/a | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| transaction.frm | transaction.frm | Project1 (BANKING.vbp) [BANKING.vbp] | Child | excluded | n/a | n/a | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| withdrawal.frm | withdrawal.frm [Withdrawal Processing] | Project1 (BANKING.vbp) [BANKING.vbp] | Child | excluded | Withdrawal Processing workflow. | n/a | Account balance recalculated., Withdrawal transaction recorded. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| accounttype.frm | accounttype.frm [Account Type Maintenance] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Child | excluded | Account Type Maintenance workflow. | n/a | Account type master data maintained., Navigation routes the user to selected module screens. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| deposit.frm | deposit.frm [Deposit Capture] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Child | excluded | Deposit Capture workflow. | n/a | Account balance recalculated., Deposit transaction recorded. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| Form1 | Form1 [Navigation/Menu] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Child | mapped | Business workflow executed through event-driven UI controls. | n/a | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 2 | 1.00 | 0.49 | none |
| Form2 | Form2 [Date/Period Entry] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Child | mapped | Business workflow executed through event-driven UI controls. | date 1 | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 1.00 | 0.31 | none |
| Form3 | Form3 [Deposit Capture] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Child | mapped | Deposit capture and balance posting workflow. | account no, amount deposited, cash, check no, cheque, customer no, dated, narration | Customer balance and account details displayed for review., Customer details displayed for review. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | tblBalances, tblCustomers | 6 | 1.00 | 0.98 | none |
| Form4 | Form4 [Withdrawal Processing] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Child | mapped | Withdrawal processing and balance deduction workflow. | account no, amount withdrawn, customer no, dated, narration, transaction id | Customer details displayed for review., Withdrawal transaction recorded. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | customer, withdrawal | 6 | 1.00 | 0.98 | none |
| Form5 | Form5 [Customer Management] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Child | mapped | Customer profile onboarding and maintenance workflow. | acc no, account id, account name, customer id, description, first, interest rate, min balance | Customer profile created or updated., Navigation routes the user to selected module screens. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 1.00 | 0.51 | none |
| Form6 | Form6 [Transaction Ledger] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Child | mapped | Transaction ledger management and adjustment workflow. | account id, account name, account no, balance, check no, credit, customer id, dated | Account type configuration updated., Account type details displayed for selection., Transaction history displayed for review., Transaction ledger updated. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | accounttype, transctions | 9 | 1.00 | 0.98 | none |
| Form7 | Form7 [Password Management] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Child | mapped | Business workflow executed through event-driven UI controls. | name, name 1, pass, pass 1 | User access is validated before workflow continuation. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 1.00 | 0.31 | none |
| Form8 | Form8 [Splash/Loading] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Child | mapped | Business workflow executed through event-driven UI controls. | n/a | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 2 | 1.00 | 0.49 | none |
| Form9 | Form9 [Authentication Entry] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Child | mapped | Business workflow executed through event-driven UI controls. | n/a | User access is validated before workflow continuation. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 1.00 | 0.26 | none |
| frmcustomer.frm | frmcustomer.frm [Customer Management] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Child | excluded | Customer Management workflow. | n/a | Customer profile created or updated. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| LOGIN.frm | LOGIN.frm [Authentication] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Login | excluded | Authentication workflow. | n/a | User access is validated before workflow continuation. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| main | main [Authentication] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | MDI_Host | mapped | Application navigation and module routing workflow. | n/a | User credentials validated against stored records. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | logi | 10 | 1.00 | 0.98 | none |
| newlogin.frm | newlogin.frm [Authentication] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Login | excluded | Authentication workflow. | n/a | User access is validated before workflow continuation. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| report.frm | report.frm [Reporting] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Child | excluded | Reporting workflow. | n/a | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| splash.frm | splash.frm [Splash/Loading] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Splash | excluded | Splash/Loading workflow. | n/a | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| transaction.frm | transaction.frm | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Child | excluded | n/a | n/a | n/a | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |
| withdrawal.frm | withdrawal.frm [Withdrawal Processing] | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | Child | excluded | Withdrawal Processing workflow. | n/a | Account balance recalculated., Withdrawal transaction recorded. | MSCOMCT2.OCX, MSCOMCTL.OCX, MSComCtl2.DTPicker, MSComctlLib.ProgressBar, MSFLXGRD.OCX, MSFlexGridLib.MSFlexGrid | n/a | 0 | 0.00 | 0.10 | missing_from_form_dossier |

#### K1. Excluded/Unresolved Forms
| Form | Reason | Source |
|---|---|---|
| Project1 (BANKING.vbp)::accounttype.frm | missing_from_form_dossier | project.members |
| Project1 (BANKING.vbp)::deposit.frm | missing_from_form_dossier | project.members |
| Project1 (BANKING.vbp)::frmcustomer.frm | missing_from_form_dossier | project.members |
| Project1 (BANKING.vbp)::LOGIN.frm | missing_from_form_dossier | project.members |
| Project1 (BANKING.vbp)::newlogin.frm | missing_from_form_dossier | project.members |
| Project1 (BANKING.vbp)::splash.frm | missing_from_form_dossier | project.members |
| Project1 (BANKING.vbp)::transaction.frm | missing_from_form_dossier | project.members |
| Project1 (BANKING.vbp)::withdrawal.frm | missing_from_form_dossier | project.members |
| Project1 (STUDENT BANKING/BANKING.vbp)::accounttype.frm | missing_from_form_dossier | project.members |
| Project1 (STUDENT BANKING/BANKING.vbp)::deposit.frm | missing_from_form_dossier | project.members |
| Project1 (STUDENT BANKING/BANKING.vbp)::frmcustomer.frm | missing_from_form_dossier | project.members |
| Project1 (STUDENT BANKING/BANKING.vbp)::LOGIN.frm | missing_from_form_dossier | project.members |
| Project1 (STUDENT BANKING/BANKING.vbp)::newlogin.frm | missing_from_form_dossier | project.members |
| Project1 (STUDENT BANKING/BANKING.vbp)::report.frm | missing_from_form_dossier | project.members |
| Project1 (STUDENT BANKING/BANKING.vbp)::splash.frm | missing_from_form_dossier | project.members |
| Project1 (STUDENT BANKING/BANKING.vbp)::transaction.frm | missing_from_form_dossier | project.members |
| Project1 (STUDENT BANKING/BANKING.vbp)::withdrawal.frm | missing_from_form_dossier | project.members |

### L. Risk Register
| Risk ID | Severity | Description | Recommended action |
|---|---|---|---|
| RISK-001 | medium | Form5.frm: default instance references | default_instance_refactor_plan |
| RISK-002 | medium | LOGIN.frm: control array index markers | ui_migration_strategy |
| RISK-003 | medium | SQL risk flags for sql:2: select_star | Parameterize query and align dialect/validation rules before migration. |
| RISK-004 | high | SQL risk flags for sql:24: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-005 | high | SQL risk flags for sql:25: select_star, sensitive_credential_query | Parameterize query and align dialect/validation rules before migration. |
| RISK-006 | high | SQL risk flags for sql:26: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-007 | high | SQL risk flags for sql:27: select_star, string_concatenation, possible_injection, sensitive_credential_query | Parameterize query and align dialect/validation rules before migration. |
| RISK-008 | high | SQL risk flags for sql:28: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-009 | high | SQL risk flags for sql:29: select_star, string_concatenation, possible_injection, sensitive_credential_query | Parameterize query and align dialect/validation rules before migration. |
| RISK-010 | high | SQL risk flags for sql:30: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-011 | high | SQL risk flags for sql:31: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-012 | high | SQL risk flags for sql:32: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-013 | high | SQL risk flags for sql:33: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-014 | high | SQL risk flags for sql:34: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-015 | medium | SQL risk flags for sql:35: select_star | Parameterize query and align dialect/validation rules before migration. |
| RISK-016 | high | SQL risk flags for sql:44: select_star, string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-017 | medium | SQL risk flags for sql:45: select_star | Parameterize query and align dialect/validation rules before migration. |
| RISK-018 | medium | SQL risk flags for sql:46: select_star | Parameterize query and align dialect/validation rules before migration. |
| RISK-019 | high | SQL risk flags for sql:47: string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-020 | high | SQL risk flags for sql:56: string_concatenation, possible_injection, missing_where_clause | Parameterize query and align dialect/validation rules before migration. |
| RISK-021 | high | SQL risk flags for sql:57: string_concatenation, possible_injection, sensitive_credential_query | Parameterize query and align dialect/validation rules before migration. |
| RISK-022 | medium | SQL risk flags for sql:58: missing_where_clause | Parameterize query and align dialect/validation rules before migration. |
| RISK-023 | medium | SQL risk flags for sql:59: missing_where_clause | Parameterize query and align dialect/validation rules before migration. |
| RISK-024 | medium | SQL risk flags for sql:61: missing_where_clause | Parameterize query and align dialect/validation rules before migration. |
| RISK-025 | high | SQL risk flags for sql:62: string_concatenation, possible_injection | Parameterize query and align dialect/validation rules before migration. |
| RISK-026 | high | Balance calculation depends on UI caption value instead of persisted balance source. | Refactor balance calculations to use persisted/accounting source of truth and add parity tests covering caption/display mismatch scenarios. |

### M. Orphan Analysis
| Path | SQL IDs | Tables touched | Recommendation |
|---|---|---|---|
| LOGIN.frm | n/a | n/a | reconcile_project_membership |

### N. Repository Landscape and Variant Inventory
| Variant | Path | Startup | Forms | Members | Dependencies |
|---|---|---|---:|---:|---:|
| BANK_SYSTEM | STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp | Main | 8 | 9 | 7 |
| Project1 (BANKING.vbp) | BANKING.vbp | main | 8 | 10 | 6 |
| Project1 (STUDENT BANKING/BANKING.vbp) | STUDENT BANKING/BANKING.vbp | main | 10 | 11 | 6 |

| Variant | Forms | Modules | Tables touched | Dependency summary |
|---|---:|---:|---:|---|
| BANK_SYSTEM | 8 | 1 | 3 | total=7, ocx=3, dll=0 |
| Project1 (BANKING.vbp) | 8 | 1 | 8 | total=6, ocx=3, dll=0 |
| Project1 (STUDENT BANKING/BANKING.vbp) | 10 | 1 | 6 | total=6, ocx=3, dll=0 |

### O. Project Dependency Map
| From | To | Type | Evidence | Blocks Sprint |
|---|---|---|---|---|
| Project1 (STUDENT BANKING/BANKING.vbp)::main | frm | shared_module_call | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command1_Click | Sprint 1 |
| Project1 (STUDENT BANKING/BANKING.vbp)::main | Form9 | mdi_navigation | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command1_Click | Sprint 1 |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | frm | shared_module_call | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdAdd_Click | Sprint 1 |
| shared_module | CheckDatabaseStatus | shared_module_call | shared_module::MoveToFirst | Sprint 1 |
| shared_module | CheckDatabaseStatus | shared_module_call | shared_module::MoveToLast | Sprint 1 |
| shared_module | CheckDatabaseStatus | shared_module_call | shared_module::MoveToNext | Sprint 1 |
| shared_module | CheckDatabaseStatus | shared_module_call | shared_module::MoveToPrev | Sprint 1 |
| BANK_SYSTEM::Main | connectDatabase | shared_module_call | BANK_SYSTEM::Main::MDIForm_Load | Sprint 1 |
| BANK_SYSTEM::Main | frmCustomers | mdi_navigation | BANK_SYSTEM::Main::Toolbar1_ButtonClick | Sprint 1 |
| BANK_SYSTEM::Main | frmDeposits | mdi_navigation | BANK_SYSTEM::Main::Toolbar1_ButtonClick | Sprint 1 |
| BANK_SYSTEM::Main | frmWithdrawal | mdi_navigation | BANK_SYSTEM::Main::Toolbar1_ButtonClick | Sprint 1 |
| BANK_SYSTEM::Main | frmTransactions | mdi_navigation | BANK_SYSTEM::Main::Toolbar1_ButtonClick | Sprint 1 |
| BANK_SYSTEM::Main | frmAccTypes | mdi_navigation | BANK_SYSTEM::Main::Toolbar1_ButtonMenuClick | Sprint 1 |
| BANK_SYSTEM::Main | rptCustomers | mdi_navigation | BANK_SYSTEM::Main::Toolbar1_ButtonMenuClick | Sprint 1 |
| BANK_SYSTEM::Main | rptDeposits | mdi_navigation | BANK_SYSTEM::Main::Toolbar1_ButtonMenuClick | Sprint 1 |
| BANK_SYSTEM::Main | rptWithdrawals | mdi_navigation | BANK_SYSTEM::Main::Toolbar1_ButtonMenuClick | Sprint 1 |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form8 | connectDatabase | shared_module_call | Project1 (STUDENT BANKING/BANKING.vbp)::Form8::Form_Load | Sprint 1 |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form8 | Lock_Form_Controls | shared_module_call | Project1 (STUDENT BANKING/BANKING.vbp)::Form8::Form_Load | Sprint 1 |
| BANK_SYSTEM::frmCustomers | Lock_Form_Controls | shared_module_call | BANK_SYSTEM::frmCustomers::cmdCancel_Click | Sprint 1 |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | MoveToPrev | shared_module_call | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdPrevious_Click | Sprint 1 |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | clear_Form_Controls | shared_module_call | Project1 (STUDENT BANKING/BANKING.vbp)::Form3::cmdDeposit_Click | Sprint 1 |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | clear_Form_Controls | shared_module_call | Project1 (STUDENT BANKING/BANKING.vbp)::Form4::cmdWithdraw_Click | Sprint 1 |
| BANK_SYSTEM::frmWithdrawal | ValidNumeric | shared_module_call | BANK_SYSTEM::frmWithdrawal::txtAmountwithdrawn_KeyPress | Sprint 1 |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | frm | shared_module_call | Project1 (STUDENT BANKING/BANKING.vbp)::Form4::fill | Sprint 1 |
| Project1 (STUDENT BANKING/BANKING.vbp)::main | Form3 | mdi_navigation | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command2_Click | Sprint 1 |
| Project1 (STUDENT BANKING/BANKING.vbp)::main | Form4 | mdi_navigation | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command3_Click | Sprint 1 |
| Project1 (STUDENT BANKING/BANKING.vbp)::main | Form9 | mdi_navigation | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command6_Click | Sprint 1 |
| Project1 (STUDENT BANKING/BANKING.vbp)::main | DataReport4 | mdi_navigation | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command4_Click | Sprint 1 |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | frm | shared_module_call | Project1 (STUDENT BANKING/BANKING.vbp)::Form4::cmdwithdrawn_click | Sprint 1 |
| Project1 (STUDENT BANKING/BANKING.vbp)::main | Form6 | mdi_navigation | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command5_Click | Sprint 1 |
| BANK_SYSTEM | Project1 (BANKING.vbp) | cross_variant_schema_conflict | alias_mismatches=2, near_miss=2, transaction_conflict=yes | Sprint 0 |
| BANK_SYSTEM | Project1 (STUDENT BANKING/BANKING.vbp) | cross_variant_schema_conflict | alias_mismatches=1, near_miss=1, transaction_conflict=yes | Sprint 0 |
| Project1 (BANKING.vbp) | Project1 (STUDENT BANKING/BANKING.vbp) | cross_variant_schema_conflict | alias_mismatches=0, near_miss=2, transaction_conflict=yes | Sprint 0 |

### P. Form Flow Traces
#### frmAccTypes (BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| DisplayaccTypes | procedure | BANK_SYSTEM::frmAccTypes::DisplayaccTypes | n/a | n/a | n/a | TRACE_GAP |
| GenerateNewAccountCode | procedure | BANK_SYSTEM::frmAccTypes::GenerateNewAccountCode | n/a | n/a | n/a | TRACE_GAP |
#### frmCustomers (BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| cmdCancel_Click | event_handler | BANK_SYSTEM::frmCustomers::cmdCancel_Click | n/a | n/a | n/a | TRACE_GAP |
#### frmDeposits (BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### frmSearch (BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### frmTransaction (BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| DisplayTransact | procedure | BANK_SYSTEM::frmTransaction::DisplayTransact | n/a | n/a | n/a | TRACE_GAP |
| cmdClose_Click | event_handler | BANK_SYSTEM::frmTransaction::cmdClose_Click | n/a | n/a | n/a | TRACE_GAP |
#### frmTransactions (BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### frmWithdrawal (BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| GenerateNewTransactCode | procedure | BANK_SYSTEM::frmWithdrawal::GenerateNewTransactCode | n/a | n/a | n/a | TRACE_GAP |
| txtAccountNo_Change | event_handler | BANK_SYSTEM::frmWithdrawal::txtAccountNo_Change | n/a | n/a | n/a | TRACE_GAP |
| txtAmountwithdrawn_KeyPress | event_handler | BANK_SYSTEM::frmWithdrawal::txtAmountwithdrawn_KeyPress | n/a | n/a | n/a | TRACE_GAP |
| txtNarration_KeyPress | event_handler | BANK_SYSTEM::frmWithdrawal::txtNarration_KeyPress | n/a | n/a | n/a | TRACE_GAP |
#### Main (BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| MDIForm_Load | event_handler | BANK_SYSTEM::Main::MDIForm_Load | n/a | n/a | n/a | TRACE_GAP |
| Toolbar1_ButtonClick | procedure | BANK_SYSTEM::Main::Toolbar1_ButtonClick | Toolbar1:MSComctlLib.Toolbar | sql:21, sql:36 | n/a | TRACE_GAP |
| Toolbar1_ButtonMenuClick | procedure | BANK_SYSTEM::Main::Toolbar1_ButtonMenuClick | Toolbar1:MSComctlLib.Toolbar | sql:21, sql:37 | n/a | TRACE_GAP |
#### Form1 (Project1 (BANKING.vbp) [BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| Label3_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form1::Label3_Click | n/a | n/a | n/a | TRACE_GAP |
| Label4_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form1::Label4_Click | n/a | n/a | n/a | TRACE_GAP |
#### Form2 (Project1 (BANKING.vbp) [BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### Form3 (Project1 (BANKING.vbp) [BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| Text1_Change | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3::Text1_Change | n/a | n/a | n/a | TRACE_GAP |
| cboCustomerNo_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3::cboCustomerNo_Click | n/a | sql:30, sql:31, sql:32, sql:33 | tblBalances, tblCustomers | OK |
| cboCustomerNo_KeyPress | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3::cboCustomerNo_KeyPress | n/a | n/a | n/a | TRACE_GAP |
| cmdDeposit_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3::cmdDeposit_Click | n/a | n/a | n/a | TRACE_GAP |
| optCash_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3::optCash_Click | n/a | n/a | n/a | TRACE_GAP |
| optCheque_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3::optCheque_Click | n/a | n/a | n/a | TRACE_GAP |
#### Form4 (Project1 (BANKING.vbp) [BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| cboCustomerno_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form4::cboCustomerno_Click | n/a | sql:44, sql:45 | customer | OK |
| cmdQuit_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form4::cmdQuit_Click | n/a | n/a | n/a | TRACE_GAP |
| cmdSave_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form4::cmdSave_Click | n/a | sql:19 | n/a | TRACE_GAP |
| cmdWithdraw_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form4::cmdWithdraw_Click | n/a | n/a | n/a | TRACE_GAP |
| cmdwithdrawn_click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form4::cmdwithdrawn_click | n/a | sql:55, sql:9 | withdrawal | OK |
| fill | procedure | Project1 (STUDENT BANKING/BANKING.vbp)::Form4::fill | n/a | sql:4, sql:50 | customer | OK |
#### Form6 (Project1 (BANKING.vbp) [BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| clr | procedure | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::clr | n/a | n/a | n/a | TRACE_GAP |
| cmdAdd_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdAdd_Click | n/a | sql:53, sql:7 | accounttype | OK |
| cmdEdit_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdEdit_Click | n/a | sql:59, sql:60 | accounttype | OK |
| cmdFirst_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdFirst_Click | n/a | n/a | n/a | TRACE_GAP |
| cmdLast_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdLast_Click | n/a | n/a | n/a | TRACE_GAP |
| cmdNext_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdNext_Click | n/a | n/a | n/a | TRACE_GAP |
| cmdPrevious_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdPrevious_Click | n/a | n/a | n/a | TRACE_GAP |
| display | procedure | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::display | n/a | sql:2, sql:3, sql:46, sql:49 | transctions | OK |
| msfgroom_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::msfgroom_Click | msfgroom:MSFlexGridLib.MSFlexGrid | n/a | n/a | TRACE_GAP |
#### Form7 (Project1 (BANKING.vbp) [BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### Form8 (Project1 (BANKING.vbp) [BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| Form_Load | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form8::Form_Load | n/a | n/a | n/a | TRACE_GAP |
| Timer1_Timer | procedure | Project1 (STUDENT BANKING/BANKING.vbp)::Form8::Timer1_Timer | n/a | n/a | n/a | TRACE_GAP |
#### main (Project1 (BANKING.vbp) [BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| Command1_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command1_Click | n/a | sql:28, sql:29 | logi | OK |
| Command2_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command2_Click | n/a | n/a | n/a | TRACE_GAP |
| Command3_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command3_Click | n/a | n/a | n/a | TRACE_GAP |
| Command4_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command4_Click | n/a | n/a | n/a | TRACE_GAP |
| Command5_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command5_Click | n/a | n/a | n/a | TRACE_GAP |
| Command6_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command6_Click | n/a | n/a | n/a | TRACE_GAP |
| Command7_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command7_Click | n/a | n/a | n/a | TRACE_GAP |
| MDIForm_Load | event_handler | BANK_SYSTEM::Main::MDIForm_Load | n/a | n/a | n/a | TRACE_GAP |
| Toolbar1_ButtonClick | procedure | BANK_SYSTEM::Main::Toolbar1_ButtonClick | n/a | sql:21, sql:36 | n/a | TRACE_GAP |
| Toolbar1_ButtonMenuClick | procedure | BANK_SYSTEM::Main::Toolbar1_ButtonMenuClick | n/a | sql:21, sql:37 | n/a | TRACE_GAP |
#### Form1 (Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| Label3_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form1::Label3_Click | n/a | n/a | n/a | TRACE_GAP |
| Label4_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form1::Label4_Click | n/a | n/a | n/a | TRACE_GAP |
#### Form2 (Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### Form3 (Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| Text1_Change | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3::Text1_Change | n/a | n/a | n/a | TRACE_GAP |
| cboCustomerNo_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3::cboCustomerNo_Click | n/a | sql:30, sql:31, sql:32, sql:33 | tblBalances, tblCustomers | OK |
| cboCustomerNo_KeyPress | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3::cboCustomerNo_KeyPress | n/a | n/a | n/a | TRACE_GAP |
| cmdDeposit_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3::cmdDeposit_Click | n/a | n/a | n/a | TRACE_GAP |
| optCash_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3::optCash_Click | n/a | n/a | n/a | TRACE_GAP |
| optCheque_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form3::optCheque_Click | n/a | n/a | n/a | TRACE_GAP |
#### Form4 (Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| cboCustomerno_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form4::cboCustomerno_Click | n/a | sql:44, sql:45 | customer | OK |
| cmdQuit_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form4::cmdQuit_Click | n/a | n/a | n/a | TRACE_GAP |
| cmdSave_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form4::cmdSave_Click | n/a | sql:19 | n/a | TRACE_GAP |
| cmdWithdraw_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form4::cmdWithdraw_Click | n/a | n/a | n/a | TRACE_GAP |
| cmdwithdrawn_click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form4::cmdwithdrawn_click | n/a | sql:55, sql:9 | withdrawal | OK |
| fill | procedure | Project1 (STUDENT BANKING/BANKING.vbp)::Form4::fill | n/a | sql:4, sql:50 | customer | OK |
#### Form5 (Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### Form6 (Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| clr | procedure | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::clr | n/a | n/a | n/a | TRACE_GAP |
| cmdAdd_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdAdd_Click | n/a | sql:53, sql:7 | accounttype | OK |
| cmdEdit_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdEdit_Click | n/a | sql:59, sql:60 | accounttype | OK |
| cmdFirst_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdFirst_Click | n/a | n/a | n/a | TRACE_GAP |
| cmdLast_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdLast_Click | n/a | n/a | n/a | TRACE_GAP |
| cmdNext_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdNext_Click | n/a | n/a | n/a | TRACE_GAP |
| cmdPrevious_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::cmdPrevious_Click | n/a | n/a | n/a | TRACE_GAP |
| display | procedure | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::display | n/a | sql:2, sql:3, sql:46, sql:49 | transctions | OK |
| msfgroom_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form6::msfgroom_Click | msfgroom:MSFlexGridLib.MSFlexGrid | n/a | n/a | TRACE_GAP |
#### Form7 (Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### Form8 (Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| Form_Load | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::Form8::Form_Load | n/a | n/a | n/a | TRACE_GAP |
| Timer1_Timer | procedure | Project1 (STUDENT BANKING/BANKING.vbp)::Form8::Timer1_Timer | n/a | n/a | n/a | TRACE_GAP |
#### Form9 (Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| n/a | n/a | n/a | n/a | n/a | n/a | TRACE_GAP |
#### main (Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp])
| Callable | Kind | Event | ActiveX | SQL IDs | Tables | Trace status |
|---|---|---|---|---|---|---|
| Command1_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command1_Click | n/a | sql:28, sql:29 | logi | OK |
| Command2_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command2_Click | n/a | n/a | n/a | TRACE_GAP |
| Command3_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command3_Click | n/a | n/a | n/a | TRACE_GAP |
| Command4_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command4_Click | n/a | n/a | n/a | TRACE_GAP |
| Command5_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command5_Click | n/a | n/a | n/a | TRACE_GAP |
| Command6_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command6_Click | n/a | n/a | n/a | TRACE_GAP |
| Command7_Click | event_handler | Project1 (STUDENT BANKING/BANKING.vbp)::main::Command7_Click | n/a | n/a | n/a | TRACE_GAP |

### Q. Form Traceability Matrix
| Form | Project | has_event_map | has_sql_map | has_business_rules | has_risk_entry | completeness_score | missing_links |
|---|---|---|---|---|---|---:|---|
| BANK_SYSTEM::frmAccTypes | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | yes | no | yes | no | 60 | sql_map, risk_register |
| BANK_SYSTEM::frmCustomers | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | yes | no | no | no | 40 | sql_map, business_rules, risk_register |
| BANK_SYSTEM::frmDeposits | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | no | no | yes | yes | 40 | event_map, sql_map, procedure_summary |
| BANK_SYSTEM::frmSearch | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | no | no | no | no | 0 | event_map, sql_map, business_rules, risk_register, procedure_summary |
| BANK_SYSTEM::frmTransaction | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | yes | no | yes | no | 60 | sql_map, risk_register |
| BANK_SYSTEM::frmTransactions | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | no | no | yes | no | 20 | event_map, sql_map, risk_register, procedure_summary |
| BANK_SYSTEM::frmWithdrawal | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | yes | no | yes | no | 60 | sql_map, risk_register |
| BANK_SYSTEM::Main | BANK_SYSTEM [STUDENT BANKING/banking finish/BANKING/BANK_SYSTEM.vbp] | yes | yes | yes | no | 80 | risk_register |
| Project1 (BANKING.vbp)::Form1 | Project1 (BANKING.vbp) [BANKING.vbp] | yes | no | no | no | 40 | sql_map, business_rules, risk_register |
| Project1 (BANKING.vbp)::Form2 | Project1 (BANKING.vbp) [BANKING.vbp] | no | no | no | no | 0 | event_map, sql_map, business_rules, risk_register, procedure_summary |
| Project1 (BANKING.vbp)::Form3 | Project1 (BANKING.vbp) [BANKING.vbp] | yes | yes | yes | yes | 100 | none |
| Project1 (BANKING.vbp)::Form4 | Project1 (BANKING.vbp) [BANKING.vbp] | yes | yes | yes | yes | 100 | none |
| Project1 (BANKING.vbp)::Form6 | Project1 (BANKING.vbp) [BANKING.vbp] | yes | yes | yes | yes | 100 | none |
| Project1 (BANKING.vbp)::Form7 | Project1 (BANKING.vbp) [BANKING.vbp] | no | no | yes | no | 20 | event_map, sql_map, risk_register, procedure_summary |
| Project1 (BANKING.vbp)::Form8 | Project1 (BANKING.vbp) [BANKING.vbp] | yes | no | no | no | 40 | sql_map, business_rules, risk_register |
| Project1 (BANKING.vbp)::main | Project1 (BANKING.vbp) [BANKING.vbp] | yes | yes | yes | no | 80 | risk_register |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form1 | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | yes | no | no | no | 40 | sql_map, business_rules, risk_register |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form2 | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | no | no | no | no | 0 | event_map, sql_map, business_rules, risk_register, procedure_summary |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | yes | yes | yes | yes | 100 | none |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | yes | yes | yes | yes | 100 | none |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form5 | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | no | no | no | yes | 20 | event_map, sql_map, business_rules, procedure_summary |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | yes | yes | yes | yes | 100 | none |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form7 | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | no | no | yes | no | 20 | event_map, sql_map, risk_register, procedure_summary |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form8 | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | yes | no | no | no | 40 | sql_map, business_rules, risk_register |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form9 | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | no | no | no | no | 0 | event_map, sql_map, business_rules, risk_register, procedure_summary |
| Project1 (STUDENT BANKING/BANKING.vbp)::main | Project1 (STUDENT BANKING/BANKING.vbp) [STUDENT BANKING/BANKING.vbp] | yes | yes | yes | no | 80 | risk_register |

### R. Sprint Dependency Map
| Form | Suggested sprint | Depends on | Shared Components Required | Rationale |
|---|---|---|---|---|
| Project1 (STUDENT BANKING/BANKING.vbp)::Form9 | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map, Q.event_map, Q.business_rules | none | Close traceability gaps before modernization changes. |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form2 | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map, Q.event_map, Q.business_rules | none | Close traceability gaps before modernization changes. |
| Project1 (BANKING.vbp)::Form2 | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map, Q.event_map, Q.business_rules | none | Close traceability gaps before modernization changes. |
| BANK_SYSTEM::frmSearch | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map, Q.event_map, Q.business_rules | none | Close traceability gaps before modernization changes. |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form7 | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map, Q.event_map | none | Close traceability gaps before modernization changes. |
| Project1 (BANKING.vbp)::Form7 | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map, Q.event_map | none | Close traceability gaps before modernization changes. |
| BANK_SYSTEM::frmTransactions | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map, Q.event_map | none | Close traceability gaps before modernization changes. |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form5 | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map, Q.event_map, Q.business_rules, RISK-001 | none | Close traceability gaps before modernization changes. |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form8 | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map, Q.business_rules | Lock_Form_Controls, connectDatabase | Close traceability gaps before modernization changes. |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form1 | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map, Q.business_rules | none | Close traceability gaps before modernization changes. |
| Project1 (BANKING.vbp)::Form8 | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map, Q.business_rules | Lock_Form_Controls, connectDatabase | Close traceability gaps before modernization changes. |
| Project1 (BANKING.vbp)::Form1 | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map, Q.business_rules | none | Close traceability gaps before modernization changes. |
| BANK_SYSTEM::frmCustomers | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map, Q.business_rules | Lock_Form_Controls | Close traceability gaps before modernization changes. |
| BANK_SYSTEM::frmDeposits | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map, Q.event_map, RISK-026 | none | Close traceability gaps before modernization changes. |
| BANK_SYSTEM::frmWithdrawal | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map | ValidNumeric | Close traceability gaps before modernization changes. |
| BANK_SYSTEM::frmTransaction | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map | none | Close traceability gaps before modernization changes. |
| BANK_SYSTEM::frmAccTypes | Sprint 0 (Discovery closure) | DEC-VARIANT-001, Q.sql_map | none | Close traceability gaps before modernization changes. |
| Project1 (STUDENT BANKING/BANKING.vbp)::main | Sprint 2 (Parity hardening) | DEC-VARIANT-001 | frm | Form has baseline traceability and can move into parity build/test. |
| Project1 (BANKING.vbp)::main | Sprint 2 (Parity hardening) | DEC-VARIANT-001 | connectDatabase, frm | Form has baseline traceability and can move into parity build/test. |
| BANK_SYSTEM::Main | Sprint 2 (Parity hardening) | DEC-VARIANT-001 | connectDatabase | Form has baseline traceability and can move into parity build/test. |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form6 | Sprint 1 (Risk-first modernization) | DEC-VARIANT-001, RISK-003, RISK-018 | MoveToPrev, frm | Implement remediation-first changes for high-risk legacy behavior. |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form4 | Sprint 1 (Risk-first modernization) | DEC-VARIANT-001, RISK-016, RISK-017 | clear_Form_Controls, frm | Implement remediation-first changes for high-risk legacy behavior. |
| Project1 (STUDENT BANKING/BANKING.vbp)::Form3 | Sprint 1 (Risk-first modernization) | DEC-VARIANT-001, RISK-010, RISK-011 | clear_Form_Controls | Implement remediation-first changes for high-risk legacy behavior. |
| Project1 (BANKING.vbp)::Form6 | Sprint 1 (Risk-first modernization) | DEC-VARIANT-001, RISK-003, RISK-018 | MoveToPrev, frm | Implement remediation-first changes for high-risk legacy behavior. |
| Project1 (BANKING.vbp)::Form4 | Sprint 1 (Risk-first modernization) | DEC-VARIANT-001, RISK-016, RISK-017 | clear_Form_Controls, frm | Implement remediation-first changes for high-risk legacy behavior. |
| Project1 (BANKING.vbp)::Form3 | Sprint 1 (Risk-first modernization) | DEC-VARIANT-001, RISK-010, RISK-011 | clear_Form_Controls | Implement remediation-first changes for high-risk legacy behavior. |
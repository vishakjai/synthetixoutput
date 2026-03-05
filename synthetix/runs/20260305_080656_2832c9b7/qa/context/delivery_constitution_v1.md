# Delivery Constitution

- Constitution ID: const-8e9f92d7b613
- Run ID: 20260305_080656_2832c9b7
- Workspace/Project: default-workspace / default-project
- Use Case: code_modernization
- Snapshot ID: kctx-b5bbe5ad54a31f2d

## Objective

Analyze this legacy VB6 repository, identify all projects/forms/modules/ActiveX dependencies/business rules/SQL behaviors, and produce a detailed modernization-ready analyst artifact.

## Non-Negotiables

- Preserve legacy behavior first; any deviation requires explicit approval and tests.
- Every major requirement/risk must be traceable to extracted evidence artifacts.
- Do not change data contracts without migration and rollback plans.
- Block planning when unresolved blockers exist (scope, IAM, schema-key, compliance).

## Orchestration Hints

- When: VB6 form/control/event patterns detected (.frm/.frx/.bas/.cls); Route: VB6 UI/Forms specialist; Reason: Preserve event behavior and control wiring parity.
- When: SQL or ADO patterns detected (Recordset, Open, Execute, concatenated SQL); Route: Data/SQL specialist; Reason: Prevent injection and data-contract regressions.
- When: Security/compliance risk detected (credentials, auth gaps, missing constraints); Route: Security/Compliance specialist; Reason: Force citation-backed remediation and blocking decisions.

## Pre-Change Checklist

- Confirm scope variant/project decision is recorded.
- Verify source snapshot and context bundle IDs are pinned.
- Review top blocking risks and required decisions before planning.

## Post-Change Checklist

- Re-run QA structural and semantic gates.
- Re-validate traceability links (event/sql/rule/risk).
- Update delivery docs/artifacts for any changed behavior assumptions.

## Known Failure Modes

- [VB6-001] Default form instances: Implicit global/default form references.. Mitigation: Refactor to explicit instance lifecycle before migration.
- [VB6-002] ADO implicit recordset updates: Recordset cursor updates hidden in UI event handlers.. Mitigation: Isolate data access and add parity tests on read/write flows.
- [VB6-003] COM/OCX registration coupling: Runtime depends on machine-level COM registration.. Mitigation: Inventory controls and map each to target replacements.

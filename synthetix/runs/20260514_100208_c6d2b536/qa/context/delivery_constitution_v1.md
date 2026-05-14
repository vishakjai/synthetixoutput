# Delivery Constitution

- Constitution ID: const-67c7f83f664f
- Run ID: 20260514_100208_c6d2b536
- Workspace/Project: default-workspace / default-project
- Use Case: code_modernization
- Snapshot ID: kctx-15b3c2bc8c946c76

## Objective

Chirp Kotlin Spring -> Go E2E. Validate today: parse_kotlin_methods extractor, kotlin-to-go skill pack + doctor, ARCH-V30 modular-monolith discipline, migration_scope auto-skip of UI Scaffold for a backend-only port.

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

- [GEN-001] Behavioral parity drift: Modernized implementation diverges from legacy runtime behavior.. Mitigation: Use evidence-linked acceptance + regression parity checks.
- [GEN-002] Data contract breakage: Schema/API assumptions changed without migration controls.. Mitigation: Require approved migration plan and rollback evidence.

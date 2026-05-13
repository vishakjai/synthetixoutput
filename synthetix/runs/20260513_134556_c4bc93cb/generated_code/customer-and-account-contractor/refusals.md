# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: PHP constructor method __construct is empty and does not contain any logic.
- **getFormLabel** (14-17) — `lint_gate_failed`: missing_authorization: 1 route(s) without .RequireAuthorization(...): 'app.MapGet("/api/v1/contractor_placement_hr_checklist_details/form_label", async (string text, AppDbContext db) => { var formLabel = (text ='. Chain ``.RequireAuthorization(AuthPolicies.<Policy>)`` on every endpoint — the project's AuthPolicies.cs is in scope.
- **read** (19-46) — `lint_gate_failed`: raw_sql_when_domain_exists: embedded SQL ('SELECT * FROM'). The project has Domain/*.cs entities — use ``db.Set<TEntity>()`` + LINQ (Where/Select/Join) instead of raw SQL strings.
- **create** (48-60) — `lint_gate_failed`: missing_authorization: 1 route(s) without .RequireAuthorization(...): 'grp.MapPost("/", async (CreateRequest request, AppDbContext db) => { var op = string.IsNullOrEmpty(request.Op) ? "" : request.Op'. Chain ``.RequireAuthorization(AuthPolicies.<Policy>)`` on every endpoint — the project's AuthPolicies.cs is in scope.
- **update** (62-93) — `lint_gate_failed`: missing_authorization: 1 route(s) without .RequireAuthorization(...): 'grp.MapPut("/reporting/tsupdatesearchcontroller", async ( UpdateRequest request, AppDbContext db) => { var values = request.Data'. Chain ``.RequireAuthorization(AuthPolicies.<Policy>)`` on every endpoint — the project's AuthPolicies.cs is in scope.

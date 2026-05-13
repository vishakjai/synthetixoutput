# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: The __construct method does not contain any logic that can be translated into an endpoint.
- **read** (19-46) — `lint_gate_failed`: missing_authorization: 1 route(s) without .RequireAuthorization(...): 'grp.MapGet("/{contractor_placement_id}/{onb_hr_checklist_type_id}", async ( string contractor_placement_id, string onb_hr_checklist_type_id,'. Chain ``.RequireAuthorization(AuthPolicies.<Policy>)`` on every endpoint — the project's AuthPolicies.cs is in scope.
- **update** (62-93) — `banned_stub`: endpoint matches a banned stub pattern ('Results.Ok(new { status = "success"'); refuse instead of shipping
- **getFormLabel** (14-17) — `unmappable`: PHP method calls parent::getFormLabel, which is not defined in the provided context.

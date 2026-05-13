# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: The constructor method does not contain any logic or functionality to translate.
- **links** (95-99) — `empty_method`: The PHP method 'links' is empty and does not contain any logic to translate.
- **read** (19-46) — `undefined_helper`: endpoint calls ['IsID'] but none of these are defined in helper_code, in the project scaffold (Domain/Dtos/Validators/Auth), or in the framework whitelist. Either inline the helper logic OR emit the helper definitions in helper_code — see SKILL.md H11.
- **update** (62-93) — `banned_stub`: endpoint matches a banned stub pattern ('Results.Ok(new { status = "success"'); refuse instead of shipping
- **getFormLabel** (14-17) — `unmappable`: PHP method calls parent::getFormLabel, which is not defined in the provided context.
- **__construct** (5-12) — `empty_method`: The constructor method does not contain any logic that can be translated into an endpoint.
- **links** (14-17) — `empty_method`: The links method is empty and does not contain any logic to translate.
- **run** (19-22) — `would_emit_stub`: PHP method calls parent::run, which is not defined in the provided context; cannot translate without knowing its implementation.
- **search** (24-44) — `lint_gate_failed`: missing_authorization: 1 route(s) without .RequireAuthorization(...): 'grp.MapGet("/", async (AppDbContext db) => { // L28-39 → build filter predicate using LINQ var query = from cphcm in db.Set<ContractorPlacem'. Chain ``.RequireAuthorization(AuthPolicies.<Policy>)`` on every endpoint — the project's AuthPolicies.cs is in scope.
- **__construct** (7-40) — `unmappable`: PHP constructor initializes properties and sets up configuration for the class; no equivalent logic in C# for a constructor without a body.
- **getFormLabel** (42-51) — `unmappable`: PHP method calls parent::getFormLabel, which is not defined in the provided context.
- **getHeadline** (53-59) — `unmappable`: PHP method calls parent::getHeadline, which is not defined in the provided context.
- **create** (61-73) — `unmappable`: PHP method calls parent::create which is not defined in the provided context.

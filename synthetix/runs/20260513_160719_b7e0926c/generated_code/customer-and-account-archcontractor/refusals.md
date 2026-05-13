# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: The constructor method does not contain any logic that can be translated into a C# endpoint.
- **links** (95-99) — `empty_method`: The PHP method 'links' is empty and does not contain any logic to translate.
- **read** (19-46) — `undefined_helper`: endpoint calls ['IsID'] but none of these are defined in helper_code, in the project scaffold (Domain/Dtos/Validators/Auth), or in the framework whitelist. Either inline the helper logic OR emit the helper definitions in helper_code — see SKILL.md H11.
- **update** (62-93) — `banned_stub`: endpoint matches a banned stub pattern ('Results.Ok(new { status = "success"'); refuse instead of shipping
- **getFormLabel** (14-17) — `unmappable`: PHP method calls parent::getFormLabel, which is not defined in the provided context.
- **create** (48-60) — `unmappable`: PHP method calls parent::create, which is not defined in the provided context.
- **__construct** (5-12) — `empty_method`: The constructor method does not contain any logic that translates to a C# endpoint.
- **links** (14-17) — `empty_method`: The PHP method 'links' is empty and does not contain any logic to translate.
- **run** (19-22) — `would_emit_stub`: The PHP method simply calls a parent method with a hardcoded parameter; it does not contain any logic to translate.
- **search** (24-44) — `lint_gate_failed`: missing_authorization: 1 route(s) without .RequireAuthorization(...): 'grp.MapGet("/", async (AppDbContext db) => { // L28-39 → build query using LINQ var query = from cphcm in db.Set<ContractorPlacementHrCheckl'. Chain ``.RequireAuthorization(AuthPolicies.<Policy>)`` on every endpoint — the project's AuthPolicies.cs is in scope.
- **getHeadline** (53-59) — `lint_gate_failed`: missing_authorization: 1 route(s) without .RequireAuthorization(...): 'grp.MapGet("/headline", async (string? text, AppDbContext db) => { // L55 → check if text is provided if (!string.IsNullOrEmpty(text)) { ret'. Chain ``.RequireAuthorization(AuthPolicies.<Policy>)`` on every endpoint — the project's AuthPolicies.cs is in scope.
- **__construct** (7-40) — `unmappable`: PHP constructor initializes properties and arrays, but no equivalent logic or context for C# translation is provided.
- **getFormLabel** (42-51) — `unmappable`: PHP method calls parent::getFormLabel with conditional logic that cannot be directly translated without knowing the parent class's implementation.

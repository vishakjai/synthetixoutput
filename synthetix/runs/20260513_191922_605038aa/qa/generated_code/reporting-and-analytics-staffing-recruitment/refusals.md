# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: PHP constructor methods cannot be translated into C# endpoints as they do not contain any logic or functionality to implement.
- **links** (95-99) — `empty_method`: The PHP method 'links' does not contain any logic or return any meaningful data.
- **read** (19-46) — `undefined_helper`: endpoint calls ['IsID'] but none of these are defined in helper_code, in the project scaffold (Domain/Dtos/Validators/Auth), or in the framework whitelist. Either inline the helper logic OR emit the helper definitions in helper_code — see SKILL.md H11.
- **update** (62-93) — `lint_gate_failed`: missing_authorization: 1 route(s) without .RequireAuthorization(...): 'grp.MapPut("/", async ( UpdateChecklistDto dto, IValidator<UpdateChecklistDto> validator, AppDbContext db) => { // L70-72 → validate-or-bail'. Chain ``.RequireAuthorization(AuthPolicies.<Policy>)`` on every endpoint — the project's AuthPolicies.cs is in scope.
- **getFormLabel** (14-17) — `unmappable`: PHP method calls parent::getFormLabel, which is not defined in the provided context.
- **create** (48-60) — `unmappable`: PHP method calls parent::create, which is not defined in the provided context.
- **__construct** (5-12) — `empty_method`: The method is a constructor and does not contain any logic to translate into an endpoint.
- **links** (14-17) — `empty_method`: The PHP method 'links' is empty and does not contain any logic to translate.
- **run** (19-22) — `empty_method`: The method 'run' only calls its parent method without any additional logic or processing.
- **search** (24-44) — `lint_gate_failed`: missing_authorization: 1 route(s) without .RequireAuthorization(...): 'grp.MapGet("/api/reporting-and-analytics-staffing-recruitment/eop-dashboard", async (AppDbContext db) => { // L28-39 → build query using LIN'. Chain ``.RequireAuthorization(AuthPolicies.<Policy>)`` on every endpoint — the project's AuthPolicies.cs is in scope.
- **getFormLabel** (42-51) — `lint_gate_failed`: missing_authorization: 1 route(s) without .RequireAuthorization(...): 'grp.MapGet("/form-label", async (string? text, AppDbContext db) => { // L44-45 → check if text is provided if (!string.IsNullOrEmpty(text)) '. Chain ``.RequireAuthorization(AuthPolicies.<Policy>)`` on every endpoint — the project's AuthPolicies.cs is in scope.
- **getHeadline** (53-59) — `lint_gate_failed`: missing_authorization: 1 route(s) without .RequireAuthorization(...): 'grp.MapGet("/headline", async (string? text, AppDbContext db) => { // L55 → check if text is provided if (!string.IsNullOrEmpty(text)) { ret'. Chain ``.RequireAuthorization(AuthPolicies.<Policy>)`` on every endpoint — the project's AuthPolicies.cs is in scope.
- **__construct** (7-40) — `unmappable`: PHP constructor initializes properties and sets up mappings for rate fields; equivalent C# logic requires additional context not provided in the method body.
- **create** (61-73) — `unmappable`: PHP method calls parent::create, which is not visible in the provided context.

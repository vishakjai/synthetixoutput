# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: The constructor does not contain any logic that can be translated into a C# endpoint.
- **links** (95-99) — `empty_method`: The PHP method 'links' is empty and does not contain any logic to translate.
- **read** (19-46) — `undefined_helper`: endpoint calls ['IsId'] but none of these are defined in helper_code, in the project scaffold (Domain/Dtos/Validators/Auth), or in the framework whitelist. Either inline the helper logic OR emit the helper definitions in helper_code — see SKILL.md H11.
- **update** (62-93) — `lint_gate_failed`: missing_authorization: 1 route(s) without .RequireAuthorization(...): 'grp.MapPut("/{id}", async ( int id, ContractorPlacementHrChecklistDetailsUpdateDto dto, IValidator<ContractorPlacementHrChecklistDetailsUpda'. Chain ``.RequireAuthorization(AuthPolicies.<Policy>)`` on every endpoint — the project's AuthPolicies.cs is in scope.
- **getFormLabel** (14-17) — `unmappable`: PHP method calls parent::getFormLabel with dynamic behavior based on $this->op, which is not defined in the provided context.
- **__construct** (5-12) — `empty_method`: The constructor method does not contain any logic to translate into a C# endpoint.
- **links** (14-17) — `empty_method`: The method 'links' is empty and does not contain any logic to translate.
- **run** (19-22) — `empty_method`: The method 'run' simply calls a parent method without any additional logic or processing.
- **search** (24-44) — `lint_gate_failed`: missing_authorization: 1 route(s) without .RequireAuthorization(...): 'grp.MapGet("/sharedutilities/academybatchsearchcontroller", async (AppDbContext db) => { // L28-39 → build LINQ query to replace raw SQL var'. Chain ``.RequireAuthorization(AuthPolicies.<Policy>)`` on every endpoint — the project's AuthPolicies.cs is in scope.
- **getHeadline** (53-59) — `banned_stub`: (helper_code) endpoint matches a banned stub pattern ('// Placeholder for actual implementation'); refuse instead of shipping
- **__construct** (7-40) — `unmappable`: PHP constructor initializes properties and sets up mappings for various rate fields; the equivalent C# structure and logic cannot be directly translated without additional context on how these mappings are utilized in the application.
- **getFormLabel** (42-51) — `unmappable`: PHP method calls parent::getFormLabel with logic that cannot be directly translated to C# without knowing the parent class's implementation details.

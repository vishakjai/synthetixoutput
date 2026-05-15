# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (6-12) — `empty_method`: The constructor method does not contain any logic that translates to an endpoint or helper function.
- **getFormLabel** (14-16) — `would_emit_stub`: The method returns a static string based on a condition, which cannot be translated into a meaningful C# endpoint.
- **getSaveLabel** (18-20) — `would_emit_stub`: The PHP method getSaveLabel simply returns a static string 'Save'; translating this would result in a stub implementation in C#.
- **onNew** (28-31) — `empty_method`: The PHP method 'onNew' does not perform any operations and simply returns true, which does not translate to a meaningful C# endpoint.
- **preSave** (33-36) — `would_emit_stub`: PHP method preSave() is empty and does not contain any logic to translate.
- **create** (38-50) — `unmappable`: PHP method calls parent::create which is not defined in the provided context; cannot translate without knowing the parent class's behavior.
- **read** (52-54) — `unmappable`: PHP method calls parent::read which is not defined in the provided context; cannot determine the behavior of the parent method.
- **del** (194-196) — `unmappable`: PHP method calls parent::del which is not defined in the provided context; cannot translate without knowing the parent class's implementation.
- **validate** (210-281) — `undefined_helper`: endpoint calls ['FromDisplayDate', 'IsValidEmail'] but none of these are defined in helper_code, in the project scaffold (Domain/Dtos/Validators/Auth), or in the framework whitelist. Either inline the helper logic OR emit the helper definitions in helper_code — see SKILL.md H11.
- **__construct** (8-16) — `empty_method`: The constructor method does not contain any logic that can be translated into a C# endpoint.
- **links** (18-21) — `empty_method`: The PHP method 'links' is empty and does not contain any logic to translate.
- **run** (23-26) — `unmappable`: PHP method calls parent::run which is not defined in the provided context; unable to translate without knowledge of the parent class implementation.

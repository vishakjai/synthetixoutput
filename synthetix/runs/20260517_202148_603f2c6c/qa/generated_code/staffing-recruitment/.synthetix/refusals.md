# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: The constructor does not contain any logic that translates to an endpoint or helper; it merely initializes properties.
- **read** (19-46) — `undefined_helper`: endpoint calls ['IsID'] but none of these are defined in helper_code, in the project scaffold (Domain/Dtos/Validators/Auth), or in the framework whitelist. Either inline the helper logic OR emit the helper definitions in helper_code — see SKILL.md H11.
- **getFormLabel** (14-17) — `unmappable`: PHP method calls parent::getFormLabel with a dynamic string based on the operation type, which cannot be translated without knowing the parent class's implementation.

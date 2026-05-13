# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: The constructor does not contain any logic that translates to a C# endpoint or helper.
- **links** (95-99) — `empty_method`: The PHP method 'links' is empty and does not contain any logic to translate.
- **read** (19-46) — `undefined_helper`: endpoint calls ['IsID'] but none of these are defined in helper_code, in the project scaffold (Domain/Dtos/Validators/Auth), or in the framework whitelist. Either inline the helper logic OR emit the helper definitions in helper_code — see SKILL.md H11.
- **getFormLabel** (14-17) — `unmappable`: PHP method calls parent::getFormLabel which is not defined in the provided context.
- **__construct** (5-12) — `empty_method`: The constructor does not contain any logic that can be translated into a C# endpoint or helper.
- **links** (14-17) — `empty_method`: The PHP method 'links' is empty and does not contain any logic to translate.
- **run** (19-22) — `unmappable`: PHP method calls parent::run, which is not defined in the provided context; cannot translate without knowing the parent class implementation.
- **__construct** (7-40) — `would_emit_stub`: The constructor does not contain any logic that can be translated into a meaningful C# endpoint or helper; it merely initializes properties.
- **getFormLabel** (42-51) — `unmappable`: The method calls parent::getFormLabel, which is not defined in the provided context, making it impossible to translate the logic accurately.

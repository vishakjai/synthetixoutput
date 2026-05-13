# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: The constructor method does not contain any logic to translate into an endpoint.
- **read** (19-46) — `undefined_helper`: endpoint calls ['IsId'] but none of these are defined in helper_code, in the project scaffold (Domain/Dtos/Validators/Auth), or in the framework whitelist. Either inline the helper logic OR emit the helper definitions in helper_code — see SKILL.md H11.
- **update** (62-93) — `banned_stub`: endpoint matches a banned stub pattern ('Results.Ok(new { status = "success"'); refuse instead of shipping
- **getFormLabel** (14-17) — `unmappable`: PHP method calls parent::getFormLabel with a dynamic context; the equivalent C# behavior requires more context than provided.

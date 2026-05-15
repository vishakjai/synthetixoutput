# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (6-12) — `empty_method`: The constructor does not contain any logic that can be translated into a C# endpoint or helper.
- **getFormLabel** (14-16) — `would_emit_stub`: The method simply returns a string based on a condition without any complex logic or data access.
- **getSaveLabel** (18-20) — `empty_method`: The PHP method getSaveLabel is a simple getter that returns a static string without any logic or parameters.
- **onNew** (28-31) — `empty_method`: The PHP method 'onNew' does not contain any logic or meaningful operations, returning only 'true'.
- **preSave** (33-36) — `would_emit_stub`: PHP method preSave() is empty and does not contain any logic to translate.
- **create** (38-50) — `unmappable`: PHP method calls parent::create which is not defined in the provided context; cannot translate without knowing the parent method's behavior.
- **read** (52-54) — `unmappable`: PHP method calls parent::read, which is not defined in the provided context; cannot determine behavior or return type.
- **update** (57-192) — `llm_returned_non_dict`: None
- **del** (194-196) — `unmappable`: PHP method calls parent::del which is not defined in the provided context; cannot translate without knowing the parent class's implementation.
- **__construct** (8-16) — `empty_method`: The constructor method does not contain any logic that can be translated into a C# endpoint.
- **links** (18-21) — `empty_method`: The PHP method 'links' is empty and does not contain any logic to translate.
- **run** (23-26) — `unmappable`: PHP method calls parent::run which is not defined in the provided context; unable to translate without knowledge of the parent class implementation.

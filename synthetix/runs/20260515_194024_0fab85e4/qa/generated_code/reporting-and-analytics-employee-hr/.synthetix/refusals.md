# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **getSaveLabel** (18-20) — `empty_method`: The PHP method getSaveLabel simply returns a static string without any logic or parameters that require translation.
- **onNew** (28-31) — `would_emit_stub`: The PHP method simply returns true without any logic or parameters, which does not translate into a meaningful C# endpoint.
- **preSave** (33-36) — `would_emit_stub`: PHP method preSave() is empty and does not contain any logic to translate.
- **__construct** (6-12) — `unmappable`: PHP constructor method initializes properties and calls a parent constructor; no equivalent logic or behavior to translate into C#.
- **getFormLabel** (14-16) — `unmappable`: PHP method getFormLabel uses $this->op which is not defined in the provided context; cannot translate without knowing its value.
- **create** (38-50) — `unmappable`: PHP method calls parent::create, which is not defined in the provided context; cannot translate without knowing the implementation of the parent method.
- **read** (52-54) — `unmappable`: PHP method calls parent::read, which is not defined in the provided context, making it impossible to translate without additional information.
- **update** (57-192) — `llm_returned_non_dict`: None
- **del** (194-196) — `unmappable`: PHP method calls parent::del which is not defined in the provided context; cannot translate without knowing the parent class's implementation.
- **validate** (210-281) — `unmappable`: PHP method calls multiple helpers (dfv, Contractor::activeByEmail, Contractor::existsActive) with complex business logic; the equivalent C# would require additional context and domain models not provided.
- **__construct** (8-16) — `empty_method`: The constructor method does not contain any logic that can be translated into a C# endpoint.
- **links** (18-21) — `empty_method`: The PHP method 'links' is empty and does not contain any logic to translate.
- **run** (23-26) — `unmappable`: PHP method calls parent::run which is not defined in the provided context; unable to translate without knowledge of the parent class implementation.

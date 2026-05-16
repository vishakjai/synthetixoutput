# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (6-12) — `would_emit_stub`: The constructor does not contain any logic that can be translated into an endpoint; it simply initializes properties without performing any actions.
- **getFormLabel** (14-16) — `would_emit_stub`: The method returns a static string based on a condition, which cannot be translated into a meaningful C# endpoint without additional context or logic.
- **getSaveLabel** (18-20) — `would_emit_stub`: The PHP method getSaveLabel simply returns a static string 'Save', which does not translate to a meaningful C# endpoint.
- **onNew** (28-31) — `empty_method`: The PHP method 'onNew' does not contain any logic or return any meaningful value beyond a simple 'true'.
- **preSave** (33-36) — `would_emit_stub`: The PHP method preSave() simply returns true without any logic or side effects, which does not translate into a meaningful C# endpoint.
- **create** (38-50) — `unmappable`: PHP method calls parent::create, which is not defined in the provided context; cannot translate without knowledge of the parent class's implementation.
- **read** (52-54) — `unmappable`: PHP method calls parent::read, which is not defined in the provided context; cannot translate without knowing the parent class implementation.
- **update** (57-192) — `unmappable`: PHP method calls multiple undefined helpers (Contractor::isActive, Contractor::isTsDirect, Contractor::emailByID, msgoutQueue, etc.) that cannot be translated without their implementations.
- **del** (194-196) — `unmappable`: PHP method calls parent::del which is not defined in the provided context; cannot translate without knowing the parent class's implementation.
- **__construct** (8-16) — `empty_method`: The constructor method does not contain any logic that can be translated into a C# endpoint.
- **links** (18-21) — `empty_method`: The PHP method 'links' is empty and does not contain any logic to translate.
- **run** (23-26) — `unmappable`: PHP method calls parent::run which is not defined in the provided context; unable to translate without knowledge of the parent class implementation.

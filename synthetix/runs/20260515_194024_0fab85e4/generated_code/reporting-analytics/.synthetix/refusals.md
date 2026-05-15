# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (6-12) — `empty_method`: The constructor does not contain any logic that translates to an endpoint or helper; it only initializes properties.
- **getFormLabel** (14-16) — `would_emit_stub`: The method only returns a string based on a condition without any data processing or validation logic.
- **getSaveLabel** (18-20) — `would_emit_stub`: The PHP method getSaveLabel simply returns a static string 'Save', which does not translate to a meaningful C# endpoint.
- **onNew** (28-31) — `empty_method`: The PHP method 'onNew' does not contain any logic or return any meaningful value beyond a simple 'true'.
- **preSave** (33-36) — `would_emit_stub`: The PHP method preSave() contains no logic and simply returns true, which does not translate to a meaningful C# endpoint.
- **read** (52-54) — `empty_method`: The PHP method 'read' simply calls the parent method without any additional logic or modifications.
- **create** (38-50) — `unmappable`: PHP method calls parent::create with a request object; the equivalent C# implementation requires visibility into the parent method's behavior, which is not provided.
- **update** (57-192) — `unmappable`: PHP method calls multiple undefined helpers (Contractor::isActive, Contractor::isTsDirect, Contractor::emailByID, msgoutQueue, etc.) that cannot be translated without their implementations.
- **del** (194-196) — `unmappable`: PHP method calls parent::del which is not defined in the provided context; cannot translate without knowing the parent class's implementation.
- **validate** (210-281) — `unmappable`: PHP method calls various helpers like dfv and Contractor::activeByEmail; the equivalent EF Core LINQ requires navigation properties not yet in the Domain model.
- **__construct** (8-16) — `empty_method`: The constructor method does not contain any logic that can be translated into a C# endpoint.
- **links** (18-21) — `empty_method`: The PHP method 'links' is empty and does not contain any logic to translate.
- **run** (23-26) — `unmappable`: PHP method calls parent::run which is not defined in the provided context; unable to translate without knowledge of the parent class implementation.

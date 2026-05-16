# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (34-37) — `empty_method`: The constructor method does not contain any logic beyond calling the parent constructor and modifying a property, which does not translate to an endpoint.
- **method** (96-98) — `would_emit_stub`: The method always returns 'post', which does not translate to a meaningful endpoint in C#.
- **onNew** (130-132) — `empty_method`: The PHP method simply returns true without any logic or parameters, making it impossible to translate into a meaningful C# endpoint.
- **onExists** (139-141) — `would_emit_stub`: The PHP method is a simple return of true, which does not translate into a meaningful C# endpoint.
- **preSave** (148-150) — `empty_method`: The preSave method does not contain any logic beyond returning true, which does not translate to a meaningful endpoint.
- **postSave** (157-159) — `empty_method`: The PHP method 'postSave' does not contain any logic beyond returning true, which does not translate to a meaningful C# endpoint.
- **validate** (166-168) — `empty_method`: The PHP method 'validate' is empty and does not contain any logic to translate.
- **mandatoryList** (184-186) — `empty_method`: The method 'mandatoryList' simply returns a property without any logic or validation.
- **hasApprovalRights** (49-51) — `unmappable`: PHP method calls _isAllowed, which is a procedural helper not defined in the provided context.
- **handle** (53-86) — `unmappable`: PHP method calls dynamic class instantiation and method delegation based on request properties; this cannot be directly translated to a static endpoint structure in C# without knowing the specific classes and methods involved.
- **links** (88-94) — `unmappable`: PHP method links returns a static array of links; equivalent C# requires a more complex structure that is not defined in the provided context.
- **outputForView** (105-122) — `unmappable`: PHP method outputForView calls readfile and VideoStream, which require specific implementations not visible in the provided context.

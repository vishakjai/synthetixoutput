# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (34-37) — `empty_method`: The constructor method does not contain any logic beyond calling the parent constructor and modifying a property, which does not translate to a meaningful endpoint.
- **method** (96-98) — `would_emit_stub`: The method always returns 'post', which does not translate to a meaningful C# endpoint. The method lacks any logic that would warrant a valid translation.
- **onNew** (130-132) — `would_emit_stub`: The PHP method simply returns true without any logic to translate, making it impossible to create a meaningful C# endpoint.
- **preSave** (148-150) — `empty_method`: The method preSave() contains no logic and simply returns true, which does not translate into a meaningful endpoint.
- **postSave** (157-159) — `empty_method`: The method postSave() simply returns true without any logic or side effects.
- **validate** (166-168) — `empty_method`: The PHP method validate() is empty and does not contain any logic to translate.
- **mandatoryList** (184-186) — `empty_method`: The method 'mandatoryList' returns a property directly without any logic or processing, making it unsuitable for translation into an endpoint.
- **handle** (53-86) — `unmappable`: The method calls dynamic class instantiation and method dispatching based on request properties, which cannot be directly translated to a static endpoint structure in C#. The logic for determining the controller class and method is not visible in the provided code.
- **links** (88-94) — `unmappable`: The method links() returns a static array structure that cannot be directly translated into a C# endpoint without additional context on how these links are utilized within the application. The PHP method does not correspond to a CRUD operation or a specific API endpoint.
- **outputForView** (105-122) — `unmappable`: PHP method outputs file content directly to the response, which cannot be translated to a RESTful endpoint without additional context on how to handle file streams in .NET.

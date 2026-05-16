# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **getSaveLabel** (18-20) — `empty_method`: The method getSaveLabel() simply returns a static string 'Save' without any logic or parameters that require translation.
- **onNew** (28-31) — `empty_method`: The PHP method 'onNew' is empty and does not contain any logic to translate.
- **preSave** (33-36) — `empty_method`: The PHP method preSave() is empty and does not contain any logic to translate.
- **__construct** (6-12) — `unmappable`: The constructor method does not contain any logic that can be translated into a C# endpoint or helper. It only initializes properties and calls the parent constructor, which does not map to an API action.
- **getFormLabel** (14-16) — `unmappable`: PHP method getFormLabel does not perform any data retrieval or manipulation; it simply returns a string based on the state of $this->op, which is not accessible in the C# context.
- **create** (38-50) — `unmappable`: The method calls parent::create, which is not defined in the provided context. The behavior of this parent method is necessary for translation but is not visible.
- **read** (52-54) — `unmappable`: The method simply calls the parent class's read method without additional logic or context. There is no mapping or transformation to translate.
- **update** (57-192) — `llm_returned_non_dict`: None
- **del** (194-196) — `unmappable`: PHP method calls parent::del($request), which is not defined in the provided context. The behavior of the parent method is unknown, making it impossible to translate accurately.
- **validate** (210-281) — `llm_returned_non_dict`: None
- **links** (18-21) — `empty_method`: The PHP method 'links' is empty and does not contain any logic to translate.
- **__construct** (8-16) — `unmappable`: PHP constructor does not contain any logic that translates to a C# endpoint. It sets up properties for the controller, which does not map to a RESTful action.
- **run** (23-26) — `unmappable`: The method calls parent::run($request, true) which relies on the parent class's implementation. Without visibility into the parent class, I cannot translate this method accurately.

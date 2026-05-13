# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **links** (95-99) — `empty_method`: PHP ::links body is a single statement returning an empty array
- **__construct** (5-12) — `unmappable`: PHP ::__construct body is constructor logic setting properties; no HTTP endpoint equivalent.
- **getFormLabel** (14-17) — `unmappable`: PHP ::getFormLabel calls parent::getFormLabel with conditional logic based on $this->op and $this->defaultFormlabel; parent method not visible.
- **links** (14-17) — `empty_method`: PHP ::links body is empty, returning an empty array
- **__construct** (5-12) — `unmappable`: PHP ::__construct body is constructor logic setting properties; no direct .NET endpoint equivalent.
- **__construct** (7-40) — `unmappable`: PHP ::__construct method initializes properties and arrays; no direct .NET endpoint equivalent.
- **getHeadline** (53-59) — `unmappable`: PHP ::getHeadline calls parent::getHeadline; parent method not visible
- **read** (75-206) — `unmappable`: PHP ::read method calls multiple helpers and uses complex logic not fully visible in input.
- **update** (208-940) — `unmappable`: PHP ::update method has complex logic with multiple branches and dependencies on external functions and models not visible in the input.
- **__construct** (99-110) — `unmappable`: PHP ::__construct body sets class properties and calls parent::__construct(); no equivalent .NET endpoint logic.
- **organize** (112-116) — `unmappable`: PHP ::organize calls parent::organize() with no visible parent method body
- **preSearch** (157-302) — `unmappable`: PHP ::preSearch body calls multiple helpers and sets complex state; translation requires full context of helper methods and class state.
- **search** (304-827) — `unmappable`: PHP ::search method is complex with multiple branches, SQL queries, and procedural calls not fully visible in input.
- **getSaveLabel** (18-20) — `would_emit_stub`: PHP ::getSaveLabel body is one-liner returning 'Save'; no logic to translate beyond constant return
- **onNew** (28-31) — `would_emit_stub`: PHP ::onNew body is a single return true statement with no visible logic.
- **preSave** (33-36) — `would_emit_stub`: PHP ::preSave body is a one-liner returning true; no logic to translate
- **read** (52-54) — `unmappable`: PHP ::read body is one-liner returning parent::read($request); parent method not visible in input
- **del** (194-196) — `unmappable`: PHP ::del body is a one-liner returning parent::del($request); parent method not visible in input
- **validate** (210-281) — `llm_returned_non_dict`: None
- **__construct** (6-12) — `unmappable`: PHP constructor method sets internal state; no HTTP endpoint equivalent.
- **update** (57-192) — `unmappable`: PHP ::update method contains complex logic with multiple branches and external dependencies (Contractor class methods, msgoutQueue) not visible in input.
- **links** (18-21) — `would_emit_stub`: PHP ::links body is a one-liner returning an empty array.
- **run** (23-26) — `unmappable`: PHP ::run body is a one-liner returning parent::run($request, true); parent method not visible in input
- **__construct** (8-16) — `unmappable`: PHP ::__construct body initializes properties and calls parent constructor; no equivalent .NET endpoint logic.
- **label** (168-219) — `unmappable`: PHP ::label method uses procedural globals and complex array manipulations not directly translatable to .NET endpoint.
- **handleTsEdit** (554-634) — `unmappable`: PHP ::handleTsEdit method contains complex logic with multiple dependencies and side effects that are not fully visible or mappable to .NET endpoint without guessing.
- **__construct** (8-23) — `unmappable`: PHP ::__construct body initializes class fields and calls global helpers, which cannot be directly mapped to a .NET endpoint.
- **handleTsSave** (114-338) — `unmappable`: PHP ::handleTsSave method calls multiple helpers and uses globals not visible in input, complex business logic with side effects.
- **handleLeaveSave** (340-552) — `unmappable`: PHP ::handleLeaveSave method calls multiple helpers and uses global state not visible in input

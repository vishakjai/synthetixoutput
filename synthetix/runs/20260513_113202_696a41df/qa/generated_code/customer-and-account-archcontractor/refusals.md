# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **links** (95-99) — `would_emit_stub`: PHP ::links body is a one-liner returning an empty array
- **__construct** (5-12) — `unmappable`: PHP ::__construct body sets properties and calls parent constructor; no equivalent .NET endpoint
- **getFormLabel** (14-17) — `unmappable`: PHP ::getFormLabel calls parent::getFormLabel with dynamic string interpolation; parent method not visible.
- **links** (14-17) — `empty_method`: PHP ::links body is empty, returning an empty array.
- **__construct** (5-12) — `unmappable`: PHP ::__construct body sets instance variables; no equivalent .NET endpoint logic.
- **__construct** (7-40) — `unmappable`: PHP ::__construct contains initialization logic without equivalent .NET endpoint behavior
- **getHeadline** (53-59) — `unmappable`: PHP ::getHeadline calls parent::getHeadline with dynamic text; parent method not visible
- **update** (208-940) — `unmappable`: PHP ::update method contains complex logic with multiple case branches and external dependencies not visible in input.
- **__construct** (99-110) — `unmappable`: PHP ::__construct body sets properties and calls parent constructor; no equivalent .NET endpoint logic.
- **preSearch** (157-302) — `unmappable`: PHP ::preSearch method has complex logic with multiple dependencies and side effects that cannot be mapped to a single .NET endpoint without guessing.
- **search** (304-827) — `unmappable`: PHP ::search method contains complex SQL and procedural logic that cannot be mapped to a .NET endpoint without guessing.
- **organize** (112-116) — `unmappable`: PHP ::organize body calls parent::organize(); parent method not visible in input
- **links** (140-155) — `unmappable`: PHP ::links method calls _isAllowed helper which is not visible in input
- **getFormLabel** (14-16) — `would_emit_stub`: PHP ::getFormLabel body is a one-liner returning a conditional string based on $this->op; no visible context for $this->op
- **getSaveLabel** (18-20) — `would_emit_stub`: PHP ::getSaveLabel body is a one-liner returning a static string 'Save'.
- **onNew** (28-31) — `would_emit_stub`: PHP ::onNew body is a one-liner returning true; no logic to translate.
- **preSave** (33-36) — `would_emit_stub`: PHP ::preSave body is a one-liner returning true; no logic to translate
- **read** (52-54) — `unmappable`: PHP ::read body is one-liner returning parent::read($request); no parent method body visible
- **links** (198-208) — `would_emit_stub`: PHP ::links method returns a static array with no dynamic logic or input.
- **validate** (210-281) — `llm_returned_non_dict`: None
- **__construct** (6-12) — `unmappable`: PHP ::__construct body sets class fields and calls parent constructor; no equivalent .NET endpoint
- **links** (18-21) — `empty_method`: PHP ::links body is empty, returning an empty array.
- **__construct** (8-16) — `unmappable`: PHP ::__construct body is constructor logic setting fields; no direct .NET endpoint equivalent
- **__construct** (8-23) — `unmappable`: PHP ::__construct body initializes properties and executes SQL queries; no direct .NET endpoint equivalent.
- **handleTsSave** (114-338) — `unmappable`: PHP ::handleTsSave method is complex with multiple dependencies and side effects that cannot be mapped without guessing.
- **handleLeaveSave** (340-552) — `unmappable`: PHP ::handleLeaveSave method involves complex logic with multiple external dependencies and side effects that are not fully visible or mappable to a .NET endpoint without guessing.
- **handleTsEdit** (554-634) — `unmappable`: PHP ::handleTsEdit has multiple external dependencies and side effects (menu manipulation, file download, complex model population) that cannot be mapped without additional context.

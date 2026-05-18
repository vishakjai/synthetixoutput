# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: The PHP __construct() method is a framework boilerplate initializer that sets controller properties (modelname, baseurl, defaultFormlabel, defaultHeadline). In .NET, this pattern does not translate to an HTTP endpoint — it's a class initialization concern that belongs in the controller's DI registration or base class, not as a Minimal API route. The method contains no business logic, no data access, no validation, and no response generation. Translating it would emit a stub with no semantic value.
- **getFormLabel** (14-17) — `would_emit_stub`: The PHP method `getFormLabel($text = '')` is a 1-line override that calls `parent::getFormLabel(...)` with a ternary expression to conditionally format a label string. This is a view/UI helper that returns a string label for form rendering, not a data-access or business-logic operation. In the .NET architecture, form labels are typically:

1. Hardcoded in the frontend (React/Vue/HTML templates), not served from the API.
2. OR stored in a localization/config table and fetched via a dedicated endpoint (e.g., `GET /api/v1/config/form-labels`).

Translating this 1-line method into a C# endpoint wo
- **read** (19-46) — `unmappable`: PHP method calls Contractor_placement_hr_checklist_details_map::activeHRChecklistEntry($contractor_placement_id, $onb_hr_checklist_type_id) at L24, which is a static model method not visible in the provided context. The method's implementation, return shape, and side effects cannot be determined from the source. Additionally, the method references $this->model, $this->modelname, $this->isID(), $this->onExists(), and $this->display() — all parent-class methods from a base controller not provided. Without visibility into these dependencies, a faithful translation is impossible. The method also e

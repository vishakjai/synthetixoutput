# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **links** (95-99) — `empty_method`: The PHP method 'links' is empty and returns an empty list.
- **__construct** (5-12) — `unmappable`: The constructor method sets properties related to the PHP framework's routing and model configuration, which do not map to .NET endpoint logic.
- **getFormLabel** (14-17) — `unmappable`: The method calls parent::getFormLabel which is not visible in the provided context.
- **read** (19-46) — `unmappable`: PHP method calls Contractor_placement_hr_checklist_details_map::activeHRChecklistEntry and uses $this->model->is_populated(), both of which are not visible in the provided context.
- **create** (48-60) — `unmappable`: The method calls parent::create with a request object, but the parent method implementation is not visible, making it impossible to translate the logic.
- **update** (62-93) — `unmappable`: PHP method uses procedural redirect and session handling with $_SESSION['PREVREQUEST'] and exit; these patterns do not map directly to .NET Minimal API.
- **__construct** (5-12) — `empty_method`: The PHP constructor method only initializes properties and calls the parent constructor. No logic to translate.
- **links** (14-17) — `empty_method`: The method 'links' is empty and returns an empty array.
- **search** (24-44) — `raw_sql_when_domain_exists`: PHP ::search calls db_get_rows with a SQL query that joins multiple tables; equivalent LINQ requires navigation properties not in Domain/ContractorPlacementHrChecklistDetailsMap.cs.
- **run** (19-22) — `unmappable`: The method calls parent::run which is not visible in the provided context, making it impossible to translate.

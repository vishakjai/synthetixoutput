# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: The PHP constructor method only sets properties and does not contain any logic to translate.
- **links** (95-99) — `empty_method`: The PHP method 'links' is empty and returns an empty list.
- **getFormLabel** (14-17) — `unmappable`: The method calls parent::getFormLabel, but the parent class implementation is not visible in the provided context.
- **read** (19-46) — `unmappable`: PHP method calls Contractor_placement_hr_checklist_details_map::activeHRChecklistEntry and uses $this->model which are not visible in the provided context.
- **create** (48-60) — `unmappable`: The method calls parent::create, but the parent method implementation is not visible.
- **update** (62-93) — `unmappable`: PHP method calls dateToDate and Contractor_placement_hr_checklist_details_map::inactivePreviousChecklistEntries with unknown implementations; equivalent C# requires these helpers.
- **links** (14-17) — `empty_method`: The method links() is empty and returns an empty array.
- **search** (24-44) — `raw_sql_when_domain_exists`: PHP ::search calls db_get_rows with a raw SQL query that joins multiple tables; equivalent LINQ requires navigation properties not in Domain/ContractorPlacementHrChecklistDetailsMap.cs.
- **__construct** (5-12) — `unmappable`: Constructor method sets properties and calls parent constructor; no equivalent logic for an endpoint.
- **run** (19-22) — `unmappable`: The method calls parent::run with parameters not visible in the provided context. The parent method implementation is not available for translation.

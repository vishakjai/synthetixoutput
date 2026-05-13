# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: The PHP method is a constructor with no logic relevant to an endpoint.
- **links** (95-99) — `empty_method`: The method 'links' is empty and does not contain any logic to translate.
- **getFormLabel** (14-17) — `unmappable`: The method calls parent::getFormLabel which is not visible in the provided context.
- **read** (19-46) — `unmappable`: PHP method calls Contractor_placement_hr_checklist_details_map::activeHRChecklistEntry and uses model operations not visible in the provided context.
- **create** (48-60) — `unmappable`: The method calls parent::create which is not visible in the provided context, making it impossible to translate without further information.
- **update** (62-93) — `unmappable`: PHP method calls dateToDate and Contractor_placement_hr_checklist_details_map::inactivePreviousChecklistEntries; equivalent C# requires implementations not visible in the source.
- **__construct** (5-12) — `empty_method`: The PHP constructor method only initializes properties and calls the parent constructor. No business logic to translate.
- **links** (14-17) — `empty_method`: The PHP method 'links' is empty and returns an empty array.
- **search** (24-44) — `raw_sql_when_domain_exists`: PHP method calls db_get_rows with a raw SQL query joining contractor_placement_hr_checklist_details_map, contractor_placement, and onb_hr_checklist_type. Equivalent LINQ requires navigation properties not in Domain/ContractorPlacementHrChecklistDetailsMap.cs.
- **run** (19-22) — `unmappable`: The method calls parent::run with parameters, but the parent method implementation is not visible. Cannot determine the behavior or translate accurately.

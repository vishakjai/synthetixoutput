# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: The PHP constructor method only sets properties and calls the parent constructor; it contains no logic to translate.
- **links** (95-99) — `empty_method`: The method 'links' is empty and returns an empty list.
- **getFormLabel** (14-17) — `unmappable`: The method calls parent::getFormLabel, which is not visible in the provided context.
- **read** (19-46) — `unmappable`: PHP method calls Contractor_placement_hr_checklist_details_map::activeHRChecklistEntry and uses $this->model which are not visible in the provided context.
- **create** (48-60) — `unmappable`: The PHP method calls parent::create which is not visible, making it impossible to translate the logic without the parent class implementation.
- **update** (62-93) — `unmappable`: PHP method calls dateToDate and Contractor_placement_hr_checklist_details_map::inactivePreviousChecklistEntries; equivalent C# requires implementations not visible in the source.
- **__construct** (5-12) — `empty_method`: The PHP constructor method only sets class properties and calls the parent constructor. No business logic to translate.
- **links** (14-17) — `empty_method`: The method 'links' is empty and returns an empty array.
- **search** (24-44) — `raw_sql_when_domain_exists`: PHP ::search uses raw SQL with a multi-table join; equivalent LINQ requires navigation properties not in Domain/ContractorPlacementHrChecklistDetailsMap.cs.
- **run** (19-22) — `unmappable`: Method calls parent::run with no visible implementation for translation.

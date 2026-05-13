# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: The constructor method only sets properties and does not contain any logic that maps to an endpoint.
- **links** (95-99) — `empty_method`: The PHP method 'links' is empty, returning an empty list with no logic to translate.
- **getFormLabel** (14-17) — `unmappable`: The method calls parent::getFormLabel, but the parent class and its implementation are not visible.
- **read** (19-46) — `unmappable`: PHP method calls Contractor_placement_hr_checklist_details_map::activeHRChecklistEntry and uses model operations not visible in the provided context.
- **create** (48-60) — `unmappable`: The PHP method calls parent::create which is not visible in the provided context. Cannot translate without knowing the parent method implementation.
- **update** (62-93) — `unmappable`: PHP method calls Contractor_placement_hr_checklist_details_map::inactivePreviousChecklistEntries and uses dynamic model instantiation; equivalent C# would require navigation properties and helper methods not visible in the Domain model.
- **links** (14-17) — `empty_method`: The method 'links' is empty and returns an empty array.
- **search** (24-44) — `raw_sql_when_domain_exists`: PHP method calls db_get_rows with a SQL query that joins three tables; equivalent LINQ requires navigation properties not in Domain/ContractorPlacementHrChecklistDetailsMap.cs.
- **__construct** (5-12) — `unmappable`: Constructor logic is related to setting properties and calling a parent constructor, which doesn't translate to an endpoint.
- **run** (19-22) — `unmappable`: The method calls parent::run with parameters not visible in the provided context, and the parent implementation is not available.

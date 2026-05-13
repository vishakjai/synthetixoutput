# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `would_emit_stub`: The method is a constructor with no logic to translate into an endpoint.
- **links** (95-99) — `empty_method`: The PHP method 'links' is empty, returning an empty list without any logic.
- **getFormLabel** (14-17) — `unmappable`: The method calls parent::getFormLabel, but the parent class implementation is not visible. Cannot translate without understanding the parent method.
- **read** (19-46) — `unmappable`: The PHP method calls Contractor_placement_hr_checklist_details_map::activeHRChecklistEntry and uses $this->model which are not visible in the provided context.
- **create** (48-60) — `unmappable`: The method calls parent::create which is not visible in the provided context. Unable to determine its implementation.
- **update** (62-93) — `unmappable`: PHP method calls Contractor_placement_hr_checklist_details_map::inactivePreviousChecklistEntries and uses dynamic model instantiation; equivalent C# requires navigation properties and helper methods not visible in the Domain model.
- **links** (14-17) — `empty_method`: The method 'links' is empty and only returns an empty array.
- **search** (24-44) — `raw_sql_when_domain_exists`: PHP ::search calls db_get_rows with a SQL query that joins contractor_placement_hr_checklist_details_map, contractor_placement, and onb_hr_checklist_type. Equivalent LINQ requires navigation properties not in Domain/ContractorPlacementHrChecklistDetailsMap.cs.
- **__construct** (5-12) — `unmappable`: Constructor logic involves setting properties and calling a parent constructor, which is not directly translatable to a .NET endpoint.
- **run** (19-22) — `unmappable`: The method calls parent::run with no visible implementation for parent::run. Cannot translate without knowing the parent class behavior.

# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: The PHP constructor method only initializes properties and does not contain any logic that translates to a .NET endpoint.
- **links** (95-99) — `empty_method`: The method 'links' is empty and only returns an empty list.
- **getFormLabel** (14-17) — `unmappable`: The method calls parent::getFormLabel, but the parent class implementation is not visible, making it impossible to translate the logic accurately.
- **read** (19-46) — `unmappable`: PHP method calls Contractor_placement_hr_checklist_details_map::activeHRChecklistEntry and uses dynamic model instantiation; equivalent C# would require navigation properties and dynamic model handling not visible in the Domain model.
- **create** (48-60) — `unmappable`: The method calls parent::create which is not visible in the provided context. Unable to determine its behavior.
- **update** (62-93) — `unmappable`: PHP method calls Contractor_placement_hr_checklist_details_map::inactivePreviousChecklistEntries and uses $_SESSION['PREVREQUEST'] for redirection; equivalent C# requires domain logic and session management not visible in the scaffold.
- **links** (14-17) — `empty_method`: The method 'links' is empty and returns an empty array.
- **search** (24-44) — `raw_sql_when_domain_exists`: PHP ::search calls db_get_rows with a raw SQL query that joins multiple tables; equivalent LINQ requires navigation properties not in Domain/ContractorPlacementHrChecklistDetailsMap.cs.
- **__construct** (5-12) — `unmappable`: The method is a constructor with no logic to translate into an endpoint.
- **run** (19-22) — `unmappable`: Method calls parent::run with no visible parent class implementation; behavior cannot be determined.

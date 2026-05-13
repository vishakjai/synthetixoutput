# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **links** (95-99) — `empty_method`: The PHP method 'links' is empty and returns an empty list.
- **__construct** (5-12) — `unmappable`: The method is a constructor initializing properties for a PHP controller. These properties do not map to a .NET endpoint or helper.
- **getFormLabel** (14-17) — `unmappable`: The method calls parent::getFormLabel, which is not visible in the provided context. The behavior of the parent method is unknown.
- **read** (19-46) — `unmappable`: PHP method calls Contractor_placement_hr_checklist_details_map::activeHRChecklistEntry and uses $this->modelname, neither of which are visible in the source or reference blocks.
- **create** (48-60) — `unmappable`: The method calls parent::create, which is not visible in the provided context. Without the implementation of the parent method, translation is not possible.
- **update** (62-93) — `unmappable`: PHP method calls dateToDate and uses dynamic model instantiation with $this->modelname; equivalent C# requires helper methods and model context not visible in the source.
- **links** (14-17) — `empty_method`: The method 'links' is empty and returns an empty array.
- **search** (24-44) — `raw_sql_when_domain_exists`: PHP method uses raw SQL with joins across contractor_placement_hr_checklist_details_map, contractor_placement, and onb_hr_checklist_type. Equivalent LINQ requires navigation properties not in Domain/ContractorPlacementHrChecklistDetailsMap.cs.
- **__construct** (5-12) — `unmappable`: The PHP constructor method only sets properties and calls a parent constructor. No equivalent .NET endpoint logic is needed.
- **run** (19-22) — `unmappable`: The method calls parent::run with parameters not visible in the provided context, making it impossible to translate without additional information.

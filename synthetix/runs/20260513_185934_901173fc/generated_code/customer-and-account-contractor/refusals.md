# Translator refusals (php → csharp)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **__construct** (5-12) — `empty_method`: The PHP constructor method only sets properties and calls the parent constructor, with no logic to translate into a C# endpoint.
- **links** (95-99) — `empty_method`: The method 'links' is empty and returns an empty list.
- **getFormLabel** (14-17) — `unmappable`: The method calls parent::getFormLabel with dynamic string interpolation based on $this->op and $this->defaultFormlabel. The behavior of parent::getFormLabel is not visible, making translation impossible.
- **read** (19-46) — `unmappable`: PHP method calls Contractor_placement_hr_checklist_details_map::activeHRChecklistEntry and uses $this->model which are not visible in the provided context.
- **create** (48-60) — `unmappable`: The method calls parent::create with no visible implementation, making it impossible to translate without assumptions.
- **update** (62-93) — `unmappable`: PHP method calls dateToDate and uses dynamic model instantiation with $this->modelname; equivalent C# requires helper implementations not visible in the source.
- **__construct** (5-12) — `empty_method`: The PHP constructor method only sets initial properties and calls the parent constructor. No logic to translate.
- **links** (14-17) — `empty_method`: The PHP method 'links' is empty and returns an empty array.
- **search** (24-44) — `raw_sql_when_domain_exists`: PHP ::search calls db_get_rows with a raw SQL query that joins 3 tables; equivalent LINQ requires navigation properties not in Domain/ContractorPlacementHrChecklistDetailsMap.cs.
- **run** (19-22) — `unmappable`: The method calls parent::run with no visible implementation for the parent class method.

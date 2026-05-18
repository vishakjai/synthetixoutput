# Translator refusals (kotlin → go)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **findNewestArticleFilteredBy** (12-47) — `wrong_package_for_monolith`: The method uses MongoDB reactive templates, which do not fit the Go monolith's SQL-based repository pattern.
- **findNewestArticleFilteredBy** (22-40) — `unresolved_parent_method`: The method relies on MongoDB-specific query building and a custom Flux return type, which are not directly translatable without additional context about the MongoDB setup and the Article entity.
- **tagsContains** (42-42) — `unresolved_parent_method`: The method tagsContains calls whereProperty, which is not visible in the provided context.
- **authorIdEquals** (44-44) — `undefined_helper`: The method `authorIdEquals` references `whereProperty` which is not defined in the provided context.
- **isFavoriteArticleByUser** (46-46) — `unresolved_parent_method`: The method isFavoriteArticleByUser calls whereProperty, which is not defined in the provided context.
- **findBySlugOrFail** (24-25) — `empty_method`: The method findBySlugOrFail is a direct delegation with no additional logic.
- **deleteBySlug** (19-25) — `repo_layer_bypass`: The method deleteBySlug is a repository method and should be translated in the repository layer, not as a handler.
- **saveAllTags** (11-12) — `empty_method`: The method body is genuinely empty with only a comment or placeholder.
- **ignoreException** (17-17) — `empty_method`: The method body is empty, containing only a comment or no logic.

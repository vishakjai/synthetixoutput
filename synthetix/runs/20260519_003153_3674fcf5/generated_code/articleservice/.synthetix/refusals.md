# Translator refusals (kotlin → go)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **findNewestArticleFilteredBy** (12-47) — `unresolved_parent_method`: The method calls into a sibling class that was not fully visible. The translation requires the complete context of the sibling methods.
- **tagsContains** (42-42) — `unresolved_parent_method`: The method 'tagsContains' calls 'whereProperty' which is not defined in the provided context.
- **authorIdEquals** (44-44) — `empty_method`: The method 'authorIdEquals' is a single-line function with no body, only a call to 'whereProperty'.
- **isFavoriteArticleByUser** (46-46) — `repo_layer_bypass`: The method isFavoriteArticleByUser involves repository logic that should not be directly translated into a handler. It requires repository layer translation.
- **deleteBySlug** (19-25) — `repo_layer_bypass`: The method deleteBySlug is a repository method and should be translated in the repository layer, not as a handler.
- **findBySlugOrFail** (24-25) — `empty_method`: The method findBySlugOrFail is a direct delegation with no additional logic.
- **saveAllTags** (11-12) — `empty_method`: The method body is genuinely empty with only a comment or placeholder.
- **ignoreException** (17-17) — `empty_method`: The method body is empty, containing only a comment or no logic.

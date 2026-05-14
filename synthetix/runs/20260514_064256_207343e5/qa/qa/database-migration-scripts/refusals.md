# Translator refusals (kotlin → go)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **main** (14-17) — `empty_method`: The source method is empty and does not contain any logic.
- **filterChain** (19-46) — `unresolved_parent_method`: The method filterChain references classes and methods (HttpSecurity, JwtAuthFilter, ApiKeyAuthFilter) that are not defined in the provided context.
- **cacheManager** (22-54) — `empty_method`: The provided method does not correspond to an HTTP handler or service method; it is a configuration method for Redis cache management.
- **contextLoads** (10-11) — `empty_method`: the source method contextLoads() is empty and does not contain any logic.
- **getMessagesForChat** (36-46) — `llm_returned_non_dict`: None
- **getChat** (49-56) — `llm_returned_non_dict`: None
- **createChat** (66-73) — `llm_returned_non_dict`: None
- **addChatParticipants** (76-85) — `llm_returned_non_dict`: None
- **leaveChat** (88-95) — `empty_method`: The source method does not return a value or provide any response to the client.
- **deleteMessage** (16-20) — `unresolved_parent_method`: The method calls chatMessageService.deleteMessage, but the implementation of chatMessageService is not provided.

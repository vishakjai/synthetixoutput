# Translator refusals (kotlin → go)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **main** (14-17) — `empty_method`: The source method body is empty and only contains a single line return.
- **filterChain** (19-46) — `empty_method`: The method filterChain does not contain any logic that can be translated into a Go handler, as it primarily configures security settings and does not return a value that can be mapped to an endpoint.
- **cacheManager** (22-54) — `empty_method`: The provided method does not correspond to a handler or service method, as it is a configuration method for a cache manager.
- **contextLoads** (10-11) — `empty_method`: the source method contextLoads() is empty and does not contain any logic.
- **getChat** (49-56) — `llm_returned_non_dict`: None
- **createChat** (66-73) — `llm_returned_non_dict`: None
- **addChatParticipants** (76-85) — `llm_returned_non_dict`: None
- **leaveChat** (88-95) — `llm_returned_non_dict`: None
- **deleteMessage** (16-20) — `unresolved_parent_method`: The method calls 'chatMessageService.deleteMessage' which is not visible in the provided context.
- **getChatParticipantByUsernameOrEmail** (30-41) — `llm_returned_non_dict`: None

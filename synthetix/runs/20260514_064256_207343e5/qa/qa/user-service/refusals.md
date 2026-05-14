# Translator refusals (kotlin → go)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **main** (14-17) — `empty_method`: the source method is empty and only calls runApplication
- **filterChain** (19-46) — `invented_endpoint`: The method does not correspond to a handler or endpoint that can be translated into Go, as it is a configuration method for security filters.
- **cacheManager** (22-54) — `empty_method`: The provided method does not contain any logic that can be translated to a Go handler or service method.
- **contextLoads** (10-11) — `empty_method`: the source method contextLoads() is empty and does not contain any logic.
- **getMessagesForChat** (36-46) — `llm_returned_non_dict`: None
- **getChat** (49-56) — `llm_returned_non_dict`: None
- **createChat** (66-73) — `llm_returned_non_dict`: None
- **addChatParticipants** (76-85) — `llm_returned_non_dict`: None
- **deleteMessage** (16-20) — `empty_method`: The source method does not contain any logic beyond calling a service method.
- **getChatParticipantByUsernameOrEmail** (30-41) — `llm_returned_non_dict`: None

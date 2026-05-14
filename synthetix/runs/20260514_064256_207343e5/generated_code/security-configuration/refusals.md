# Translator refusals (kotlin → go)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **main** (14-17) — `empty_method`: The source method 'main' does not contain any logic beyond calling 'runApplication'.
- **filterChain** (19-46) — `empty_method`: The method filterChain does not contain any logic that can be translated to a Go handler, as it primarily configures security settings without returning a value that can be processed in a handler context.
- **cacheManager** (22-54) — `empty_method`: The provided method does not contain any logic that can be translated into a Go handler, service, or repository.
- **contextLoads** (10-11) — `empty_method`: The source method contextLoads() is empty and does not contain any logic.
- **getChat** (49-56) — `llm_returned_non_dict`: None
- **createChat** (66-73) — `unused_import`: Output imports ['errors'] but never uses them. Remove the unused imports — Go won't compile otherwise.
- **addChatParticipants** (76-85) — `llm_returned_non_dict`: None
- **leaveChat** (88-95) — `unresolved_parent_method`: The method calls chatService.removeParticipantFromChat, but the implementation details of chatService are not visible.
- **deleteMessage** (16-20) — `unresolved_parent_method`: The method calls chatMessageService.deleteMessage, but the implementation of chatMessageService is not visible.
- **getChatParticipantByUsernameOrEmail** (30-41) — `llm_returned_non_dict`: None

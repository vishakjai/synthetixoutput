# Translator refusals (kotlin → go)

The per-method translator emitted a structured refusal for each row below. HITL should review and either re-dispatch with helper context or accept a manual translation.

- **main** (14-17) — `empty_method`: The source method body is empty and does not contain any logic.
- **filterChain** (19-46) — `empty_method`: The provided method is a configuration method for security filters and does not contain any logic that can be translated into a Go handler, service, or repository method.
- **cacheManager** (22-54) — `empty_method`: The provided method is a configuration method for creating a cache manager and does not translate to a handler, service, or repository method in Go.
- **contextLoads** (10-11) — `empty_method`: the source method contextLoads() is empty and does not contain any logic.
- **getMessagesForChat** (36-46) — `unused_import`: Output imports ['errors'] but never uses them. Remove the unused imports — Go won't compile otherwise.
- **getChat** (49-56) — `llm_returned_non_dict`: None
- **createChat** (66-73) — `llm_returned_non_dict`: None
- **addChatParticipants** (76-85) — `llm_returned_non_dict`: None
- **leaveChat** (88-95) — `llm_returned_non_dict`: None
- **deleteMessage** (16-20) — `unresolved_parent_method`: The method deleteMessage calls chatMessageService.deleteMessage which is not visible to me.
- **getChatParticipantByUsernameOrEmail** (30-41) — `llm_returned_non_dict`: None

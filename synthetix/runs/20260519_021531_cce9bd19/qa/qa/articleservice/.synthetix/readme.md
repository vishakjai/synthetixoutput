# .synthetix/ — translator bookkeeping

Generation artifacts the translator emitted alongside the domain code. These are reference material for HITL review, not files the application needs:

- `REFUSALS*.md` — per-method translator refusals (review + manually translate or re-dispatch with helper context).
- `_translator_result.json` — generation trace for debugging.
- `internal/example*/` — house-style template the translator reads as a reference pattern; not part of the application.

Safe to delete this directory entirely once the deliverable passes review — nothing in `cmd/` or `internal/` imports from it.

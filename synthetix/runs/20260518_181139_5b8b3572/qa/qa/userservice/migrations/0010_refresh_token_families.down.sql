-- Auth migration rollback
DROP TABLE IF EXISTS refresh_tokens CASCADE;
DROP TABLE IF EXISTS refresh_token_families CASCADE;

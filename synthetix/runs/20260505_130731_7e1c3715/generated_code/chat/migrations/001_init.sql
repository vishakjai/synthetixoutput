-- Doctor-generated initial schema. Hand-edit columns to match your domain.
-- Each table gets a UUID primary key + created_at/updated_at timestamps;
-- entity-specific columns are inferred from architect DTO specs when names
-- match. Add NOT NULL / UNIQUE / FK constraints during operator review.

CREATE TABLE IF NOT EXISTS chateventconstantss (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TABLE IF NOT EXISTS chatevents (
    id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    created_at TIMESTAMPTZ NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

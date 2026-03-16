AuthenticationService

Purpose
- Handles user authentication and session management for the BANK_SYSTEM modernization.

Key Endpoints
- GET /health -> {"status":"healthy"}
- GET /ready  -> {"status":"ready"}
- POST /auth/login -> Validates credentials against PostgreSQL credential store and returns a JWT with field "expires_at".

Environment
- PORT (default 8080)
- POSTGRES_CONNECTION_STRING
- JWT_SECRET (required)
- TOKEN_TTL_SECONDS (default 3600)

Build & Run (local)
- dotnet build src/AuthenticationService/AuthenticationService.csproj
- export JWT_SECRET=$(openssl rand -hex 32)
- export POSTGRES_CONNECTION_STRING="Host=localhost;Port=5432;Database=bank_auth;Username=bank_user;Password=..."
- dotnet run --project src/AuthenticationService/AuthenticationService.csproj

Docker
- docker build -t auth-service .
- docker run -e JWT_SECRET=secret -e POSTGRES_CONNECTION_STRING="..." -p 8080:8080 auth-service

Tests
- dotnet test

Database Schema (expected)
- Table auth_users(id uuid primary key, username text unique not null, password_hash text not null)
- Passwords must be stored as BCrypt hashes.

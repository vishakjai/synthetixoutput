# AuthenticationService

Deterministic brownfield authentication vertical slice for `/auth/login`.

## Included behavior
- Implements `POST /auth/login`
- Verifies credentials against PostgreSQL-backed credential data
- Issues JWT access tokens with `sub`, `unique_name`, `iss`, `aud`, and `exp` claims
- Applies a configurable lockout threshold on repeated password failures
- Preserves the scoped parity assertion: Valid credentials result in an authenticated session and navigation to the main customer workflow.

## Environment variables
- `PORT`
- `AUTH_DB_CONNECTION_STRING`
- `AUTH_DB_TABLE` (optional, defaults to `user_credentials`)
- `AUTH_TOKEN_SIGNING_KEY` (minimum 32 bytes)
- `AUTH_TOKEN_ISSUER` (optional)
- `AUTH_TOKEN_AUDIENCE` (optional)
- `AUTH_TOKEN_EXPIRY_MINUTES` (optional)
- `AUTH_LOCKOUT_THRESHOLD` (optional)

## Run
```bash
dotnet restore
dotnet build
dotnet test Tests/AuthenticationService.Tests.csproj
PORT=8080 AUTH_DB_CONNECTION_STRING="Host=localhost;Database=bank;Username=app;Password=secret" AUTH_TOKEN_SIGNING_KEY="a-very-long-signing-key-at-least-32chars" dotnet run --project AuthenticationService.csproj
```

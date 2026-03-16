# AuthenticationService

Deterministic brownfield authentication vertical slice for `/auth/login`.

## Included behavior
- Implements `POST /auth/login`
- Verifies credentials against PostgreSQL-backed credential data
- Returns a signed authentication token and an `expires_at` value
- Preserves the scoped parity assertion: Valid credentials result in an authenticated session and navigation to the main customer workflow.

## Environment variables
- `PORT`
- `AUTH_DB_CONNECTION_STRING`
- `AUTH_DB_TABLE` (optional, defaults to `user_credentials`)
- `AUTH_TOKEN_SIGNING_KEY`
- `AUTH_TOKEN_ISSUER` (optional)
- `AUTH_TOKEN_AUDIENCE` (optional)
- `AUTH_TOKEN_EXPIRY_MINUTES` (optional)

## Run
```bash
dotnet restore
dotnet build
dotnet test Tests/AuthenticationService.Tests.csproj
PORT=8080 AUTH_DB_CONNECTION_STRING="Host=localhost;Database=bank;Username=app;Password=secret" AUTH_TOKEN_SIGNING_KEY="change-me" dotnet run --project AuthenticationService.csproj
```

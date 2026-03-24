# AuthenticationService

UI Developer output generated from Screen Contracts and prebuilt UI scaffolds.

## Included behavior
- screen state and event handlers
- richer validation and status messaging
- event target registry and navigation outcomes
- section-aware page composition
- contract-backed UI tests

## Out of scope
- visual design system decisions
- final styling and branding

## Prebuilt scaffold inputs
- files inherited from UI Scaffold Agent: 17

## Run
```bash
dotnet restore
dotnet build
dotnet test Tests/AuthenticationService.Tests.csproj
PORT=8080 dotnet run --project AuthenticationService.csproj
```

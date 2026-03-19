# AuthenticationService

UI Developer output generated from Screen Contracts and prebuilt UI scaffolds.

## Included behavior
- screen state and event handlers
- validation service
- API/event registry
- navigation-capable page models
- contract-backed UI tests

## Out of scope
- visual design system decisions
- final styling and branding

## Prebuilt scaffold inputs
- files inherited from UI Scaffold Agent: 16

## Run
```bash
dotnet restore
dotnet build
dotnet test Tests/AuthenticationService.Tests.csproj
PORT=8080 dotnet run --project AuthenticationService.csproj
```

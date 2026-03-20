# ExperienceShell

Deterministic UI scaffold slice generated from Screen Contracts.

## Scope
- Structural UI scaffolds only
- Validation wiring
- Event wiring
- Navigation wiring
- Accessibility baseline
- Contract-conformance test hooks

## Out of scope
- Visual styling
- Design system choices
- Typography, spacing, and colors

## Included screens
- Application startup and splash workflow (/ui/frmsplash)
- Application navigation and module routing workflow (/ui/menu)
- Application navigation and module routing workflow (/ui/mdi)

## Run
```bash
dotnet restore
dotnet build
dotnet test Tests/ExperienceShell.Tests.csproj
PORT=8080 dotnet run --project ExperienceShell.csproj
```

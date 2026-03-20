# LegacyCoreService

Deterministic UI scaffold slice generated from Screen Contracts.

## Scope
- Structural UI scaffolds only
- Validation wiring
- Event wiring
- Navigation wiring
- Accessibility baseline
- Contract-conformance test hooks

## Out of scope
- Client-specific visual design decisions
- Brand customization beyond the shared Synthetix stylesheet
- Pixel-perfect layout work or wireframes

## Included screens
- Interest calculation and posting workflow (/ui/frmaddinterest)
- Business workflow executed through event-driven UI controls (/ui/frmdep)
- Interest calculation and posting workflow (/ui/frminterest)

## Run
```bash
dotnet restore
dotnet build
dotnet test Tests/LegacyCoreService.Tests.csproj
PORT=8080 dotnet run --project LegacyCoreService.csproj
```

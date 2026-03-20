# CustomerService

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
- Account closure and settlement workflow (/ui/frmcloseacount)
- Customer profile onboarding and maintenance workflow (/ui/frmcustomer)
- Account type maintenance and account setup workflow (/ui/frmsettings)

## Run
```bash
dotnet restore
dotnet build
dotnet test Tests/CustomerService.Tests.csproj
PORT=8080 dotnet run --project CustomerService.csproj
```

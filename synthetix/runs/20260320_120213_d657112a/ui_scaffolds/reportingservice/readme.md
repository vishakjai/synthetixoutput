# ReportingService

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
- Business workflow executed through event-driven UI controls (/ui/frmwithindate)
- Business workflow executed through event-driven UI controls (/ui/frmdaily)
- Operational reporting and statement generation workflow (/ui/frmmonthlyreport)
- Operational reporting and statement generation workflow (/ui/frmstatement)
- Business workflow executed through event-driven UI controls (/ui/form1)
- Business workflow executed through event-driven UI controls (/ui/frmexpireitemswithindate)
- Operational reporting and statement generation workflow (/ui/frmmonthly)
- Operational reporting and statement generation workflow (/ui/frmreport)
- Transaction ledger management and adjustment workflow (/ui/frmtransaction)
- Transaction ledger management and adjustment workflow (/ui/frmwith)

## Run
```bash
dotnet restore
dotnet build
dotnet test Tests/ReportingService.Tests.csproj
PORT=8080 dotnet run --project ReportingService.csproj
```

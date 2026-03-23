# TransactionService

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
- Deposit capture and balance posting workflow (/ui/frmdeposit)
- Withdrawal processing and balance deduction workflow (/ui/frmwithdraw)
- Balance inquiry and reconciliation workflow (/ui/frmcheckbalance)

## Run
```bash
dotnet restore
dotnet build
dotnet test Tests/TransactionService.Tests.csproj
PORT=8080 dotnet run --project TransactionService.csproj
```

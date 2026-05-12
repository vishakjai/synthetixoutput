# Onboarding and Credentialing

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
- Onboarding and Credentialing workspace (/ui/onboarding-and-credentialing)

## Run
```bash
dotnet restore
dotnet build
dotnet test Tests/Onboarding and Credentialing.Tests.csproj
PORT=8080 dotnet run --project OnboardingAndCredentialing.csproj
```

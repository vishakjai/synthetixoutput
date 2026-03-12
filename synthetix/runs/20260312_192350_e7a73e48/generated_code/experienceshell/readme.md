# ExperienceShell

This is the ExperienceShell component of the BANK_SYSTEM Modernization Architecture.

## Requirements
- .NET 7.0 SDK
- Docker

## Build and Run

```bash
# Build the application
$ dotnet build

# Run the application
$ dotnet run
```

## Docker

```bash
# Build the Docker image
$ docker build -t experienceshell .

# Run the Docker container
$ docker run -p 8080:8080 experienceshell
```

## Endpoints
- Health Check: `GET /health`
- Readiness Check: `GET /ready`

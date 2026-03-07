# ActiveXReplacementService

This service replaces legacy ActiveX and OCX components with modern C# equivalents.

## Requirements
- .NET 6.0 SDK
- Docker

## Build and Run

```bash
# Build the project
$ dotnet build

# Run the project
$ dotnet run --project ActiveXReplacementService

# Build the Docker image
$ docker build -t activex-replacement-service .

# Run the Docker container
$ docker run -p 8080:8080 activex-replacement-service
```

## Endpoints
- GET /health → returns {"status": "healthy"}
- GET /ready → returns {"status": "ready"}
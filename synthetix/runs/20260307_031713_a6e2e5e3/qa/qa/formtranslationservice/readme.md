# FormTranslationService

This service translates VB6 forms to C# forms, preserving controls and event handlers.

## Building the Service

```bash
dotnet build
```

## Running the Service

```bash
dotnet run --project FormTranslationService
```

## Docker

To build the Docker image:

```bash
docker build -t form-translation-service .
```

To run the Docker container:

```bash
docker run -p 8080:8080 form-translation-service
```

## Endpoints

- `GET /health` - Health check endpoint
- `GET /ready` - Readiness check endpoint
- `POST /translation/translate` - Endpoint to translate VB6 form code to C#
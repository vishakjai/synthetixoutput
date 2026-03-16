# AuthenticationService

This service handles user authentication and manages login sessions.

## Build and Run

```bash
dotnet build
```

## Run with Docker

```bash
docker build -t authenticationservice .
docker run -p 8080:8080 authenticationservice
```

## Endpoints

- `POST /api/auth/login`: Authenticates a user and returns a token.
- `GET /api/health/health`: Returns the health status of the service.
- `GET /api/health/ready`: Returns the readiness status of the service.

## Testing

```bash
dotnet test
```
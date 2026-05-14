# SecurityMiddleware

This service provides JWT authentication, role-based authorization, and OAuth2 API security.

## Running the Service

```bash
# Build the Docker image
docker build -t securitymiddleware .

# Run the Docker container
docker run -p 8080:8080 securitymiddleware
```

## Endpoints

- GET `/health`: Returns the health status of the service.
- GET `/ready`: Returns the readiness status of the service.

## Environment Variables

- `PORT`: The port on which the service will run (default: 8080).
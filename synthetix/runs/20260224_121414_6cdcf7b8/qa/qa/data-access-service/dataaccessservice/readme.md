# Data Access Service

This service manages database interactions and data persistence using Entity Framework Core.

## Build and Run

```bash
# Build the Docker image
docker build -t data-access-service .

# Run the Docker container
# Ensure to set the PORT environment variable if you want to change the default port
# Example: docker run -e PORT=8080 -p 8080:8080 data-access-service

docker run -p 8080:8080 data-access-service
```

## Endpoints

- `GET /health`: Returns the health status of the service.
- `GET /ready`: Returns the readiness status of the service.
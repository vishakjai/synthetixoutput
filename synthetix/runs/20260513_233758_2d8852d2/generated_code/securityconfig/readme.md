# SecurityConfig

This service configures security settings including JWT authentication and role-based authorization.

## Running the Service

To build and run the service locally:

```bash
docker build -t securityconfig .
docker run -p 8080:8080 securityconfig
```

## Endpoints

- `GET /health`: Returns the health status of the service.
- `GET /ready`: Returns the readiness status of the service.

## Environment Variables

- `PORT`: The port on which the service will listen (default: 8080).
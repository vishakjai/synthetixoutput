# Security Service

This service handles authentication, authorization, and security checks.

## Build and Run

```bash
# Build the Docker image
$ docker build -t security-service .

# Run the Docker container
$ docker run -p 8080:8080 security-service
```

## Endpoints

- Health Check: `GET /health`
- Readiness Check: `GET /ready`
- Authentication: `GET /api/security/authenticate`
- Authorization: `GET /api/security/authorize`
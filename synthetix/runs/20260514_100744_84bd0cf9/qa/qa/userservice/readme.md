# UserService

This is the UserService component of the Chirp Backend Modernization project. It handles user identity, registration, authentication, profile management, and session handling.

## Requirements
- Go 1.19
- Docker

## Running the Service

```bash
# Build the Docker image
$ docker build -t userservice .

# Run the Docker container
$ docker run -p 8080:8080 userservice
```

## Endpoints
- `GET /health` - Health check endpoint
- `GET /ready` - Readiness check endpoint

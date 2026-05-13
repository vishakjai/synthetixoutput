# ChatService

This is the ChatService component of the Chirp Backend Modernization project. It is responsible for managing direct and group messaging, including chat threads, message persistence, and read receipts.

## Prerequisites

- Docker
- Go 1.20+

## Running the Service

```bash
# Build the Docker image
$ docker build -t chatservice .

# Run the Docker container
$ docker run -p 8080:8080 chatservice
```

## Endpoints

- `GET /health` - Returns the health status of the service.
- `GET /ready` - Returns the readiness status of the service.

## Environment Variables

- `PORT`: The port on which the service will listen (default: 8080).
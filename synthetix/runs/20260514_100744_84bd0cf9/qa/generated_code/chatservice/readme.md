# ChatService

This is the ChatService component of the Chirp Backend Modernization project.

## Building and Running

To build and run the service, use the following commands:

```bash
# Build the Docker image
$ docker build -t chatservice .

# Run the Docker container
$ docker run -p 8080:8080 chatservice
```

## Endpoints

- `GET /health`: Returns the health status of the service.
- `GET /ready`: Returns the readiness status of the service.

## Environment Variables

- `PORT`: The port on which the service listens (default: 8080).
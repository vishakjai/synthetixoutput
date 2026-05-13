# UserService

This is the UserService component responsible for handling user identity, registration, authentication, profile management, and session handling.

## Running the Service

To run the service locally:

```bash
go run main.go
```

## Building the Docker Image

To build the Docker image:

```bash
docker build -t user-service .
```

## Running the Docker Container

To run the Docker container:

```bash
docker run -p 8080:8080 user-service
```

## Endpoints

- `GET /health`: Returns the health status of the service.
- `GET /ready`: Returns the readiness status of the service.
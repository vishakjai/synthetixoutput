# ProfileService

This service manages user profile functionalities including profile picture updates.

## Running the Service

To run the service locally:

```bash
go run main.go
```

## Building the Docker Image

```bash
docker build -t profileservice .
```

## Running the Docker Container

```bash
docker run -p 8080:8080 profileservice
```

## Endpoints

- `GET /health`: Health check endpoint
- `GET /ready`: Readiness check endpoint
- `POST /profile/execute`: Execute profile action

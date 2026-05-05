# Notification Service

This service dispatches notifications for user and chat events.

## Requirements
- Go 1.20+
- Docker

## Running Locally

```bash
# Run the service
PORT=8080 go run main.go
```

## Building Docker Image

```bash
# Build the Docker image
docker build -t notification-service .

# Run the Docker container
docker run -p 8080:8080 notification-service
```

## Testing

```bash
# Run tests
PORT=8080 go test ./...
```

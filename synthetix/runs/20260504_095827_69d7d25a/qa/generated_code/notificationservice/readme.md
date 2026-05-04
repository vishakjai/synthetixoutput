# NotificationService

This is the NotificationService application, responsible for handling notifications, device token management, and event listeners.

## Requirements
- Go 1.20
- Docker

## Building and Running

```bash
# Build the Docker image
$ docker build -t notificationservice .

# Run the Docker container
$ docker run -p 8080:8080 notificationservice
```

## Testing

```bash
# Run tests
$ go test ./...
```

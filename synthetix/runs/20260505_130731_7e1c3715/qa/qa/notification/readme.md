# Notification Service

This service handles the dispatching of notifications for user and chat events.

## Running the Service

```bash
# Build the Docker image
$ docker build -t notification-service .

# Run the Docker container
$ docker run -p 8080:8080 notification-service
```

## Endpoints

- `GET /health`: Health check endpoint.
- `GET /ready`: Readiness check endpoint.
- `POST /api/notification/register`: Register a notification.
- `DELETE /api/notification/{token}`: Delete a notification.

## Testing

```bash
# Run tests
$ go test ./...
```

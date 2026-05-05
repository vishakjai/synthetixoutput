# Notification Service

This service handles the dispatching of notifications for user and chat events.

## Building and Running

```bash
# Build the Docker image
$ docker build -t notification-service .

# Run the Docker container
$ docker run -p 8080:8080 notification-service
```

## Endpoints

- `POST /api/notification/register`: Register a new notification.
- `DELETE /api/notification/{token}`: Delete a notification.
- `GET /health`: Health check endpoint.
- `GET /ready`: Readiness check endpoint.

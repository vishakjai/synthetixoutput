# NotificationService

This is the NotificationService component, responsible for handling notifications and managing ApiKey, DeviceToken, and related workflows.

## Building and Running

To build and run the service locally:

```bash
# Build the Docker image
docker build -t notificationservice .

# Run the Docker container
docker run -p 8080:8080 notificationservice
```

## Endpoints

- `GET /health`: Health check endpoint.
- `GET /ready`: Readiness check endpoint.
- `POST /api/notification/register`: Register a new notification.
- `DELETE /api/notification/{token}`: Delete a notification by token.

## Testing

To run the tests:

```bash
go test ./...
```

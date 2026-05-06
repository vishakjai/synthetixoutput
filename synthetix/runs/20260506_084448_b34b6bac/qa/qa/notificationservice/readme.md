# NotificationService

This service handles notifications, managing ApiKey, DeviceToken, and related workflows.

## Building and Running

To build and run the service locally:

```bash
docker build -t notificationservice .
docker run -p 8080:8080 notificationservice
```

## Endpoints

- `GET /health`: Returns the health status of the service.
- `GET /ready`: Returns the readiness status of the service.
- `POST /api/notification/register`: Registers a new notification.
- `DELETE /api/notification/{token}`: Deletes a notification by token.

## Testing

To run tests:

```bash
go test ./...
```
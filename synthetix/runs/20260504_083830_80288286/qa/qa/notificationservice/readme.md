# NotificationService

This service is responsible for sending notifications, handling device tokens, and managing notification events.

## Running the Service

To run the service locally:

```bash
go run main.go
```

To build and run the Docker container:

```bash
docker build -t notificationservice .
docker run -p 8080:8080 notificationservice
```

## Endpoints

- `GET /health`: Health check endpoint
- `GET /ready`: Readiness check endpoint
- `POST /api/notification/register`: Register a device token
- `DELETE /api/notification/{token}`: Delete a notification token
- `GET /api/participants`: Get participants
- `POST /api/participants/profile-picture-upload`: Upload profile picture
- `POST /api/participants/confirm-profile-picture`: Confirm profile picture
- `DELETE /api/participants/profile-picture`: Delete profile picture

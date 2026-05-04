# NotificationService

This service handles sending notifications for chat events and other activities.

## Running the Service

```bash
uvicorn main:app --host 0.0.0.0 --port 8080
```

## Building the Docker Image

```bash
docker build -t notification-service .
```

## Running the Docker Container

```bash
docker run -p 8080:8080 notification-service
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
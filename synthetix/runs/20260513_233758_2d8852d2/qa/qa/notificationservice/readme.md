# NotificationService

This is the NotificationService component, responsible for handling outbound notification dispatch via email, SMS, and webhooks.

## Running the Service

To run the service locally, use the following commands:

```bash
# Build the Docker image
docker build -t notificationservice .

# Run the Docker container
docker run -p 8080:8080 notificationservice
```

The service will be available at `http://localhost:8080`.

## Endpoints

- `GET /health`: Returns the health status of the service.
- `GET /ready`: Returns the readiness status of the service.
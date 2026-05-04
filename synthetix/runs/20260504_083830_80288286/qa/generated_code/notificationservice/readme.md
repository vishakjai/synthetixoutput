# NotificationService

This service handles sending notifications for chat events and other activities.

## Running the Service

To run the service locally, use the following command:

```bash
uvicorn main:app --host 0.0.0.0 --port 8080
```

## Building the Docker Image

To build the Docker image, use the following command:

```bash
docker build -t notificationservice .
```

## Running Tests

To run the tests, use the following command:

```bash
pytest tests/
```
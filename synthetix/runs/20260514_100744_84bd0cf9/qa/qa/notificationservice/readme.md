# NotificationService

This service handles outbound notification dispatch via email, SMS, and webhooks.

## Running the Service

To run the service locally:

```bash
export PORT=8080
make run
```

## Building the Docker Image

To build the Docker image:

```bash
docker build -t notificationservice .
```

## Running Tests

To run tests:

```bash
make test
```

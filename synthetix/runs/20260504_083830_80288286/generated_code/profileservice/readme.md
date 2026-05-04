# ProfileService

This service manages user profile functionalities including profile picture updates.

## Running the Service

To run the service locally:

```bash
uvicorn main:app --host 0.0.0.0 --port 8080
```

To build and run the Docker container:

```bash
docker build -t profileservice .
docker run -p 8080:8080 profileservice
```

## Endpoints

- `GET /health`: Health check endpoint
- `GET /ready`: Readiness check endpoint
- `POST /profile/execute`: Execute profile action

# ProfileService

This service manages user profile functionalities including profile picture updates.

## Running the Service

To run the service locally:

```bash
uvicorn src.main:app --host 0.0.0.0 --port 8080
```

## Endpoints

- `GET /health`: Health check endpoint
- `GET /ready`: Readiness check endpoint
- `POST /profile/execute`: Execute profile action

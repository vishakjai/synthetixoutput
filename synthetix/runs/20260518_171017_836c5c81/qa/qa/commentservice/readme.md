# CommentService

This service manages comments associated with articles, including retrieval and pagination.

## Building and Running

To build and run the service locally:

```bash
# Build the Docker image
docker build -t commentservice .

# Run the Docker container
docker run -p 8080:8080 commentservice
```

## Endpoints

- `GET /health`: Health check endpoint.
- `GET /ready`: Readiness check endpoint.
- `POST /api/commentservice/commentview`: Handles comment view logic.
- `POST /api/commentservice/createcommentrequest`: Handles comment creation logic.
- `POST /api/commentservice/tagrepository`: Handles tag repository logic.
- `POST /api/commentservice/offsetbasedpageable`: Handles pagination logic.

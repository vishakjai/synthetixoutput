# CommentService

This is the CommentService component, responsible for managing comment operations.

## Building and Running

To build and run the service, you can use the following commands:

```bash
# Build the Docker image
docker build -t commentservice .

# Run the Docker container
docker run -p 8080:8080 commentservice
```

## Endpoints

- `GET /health`: Health check endpoint.
- `GET /ready`: Readiness check endpoint.
- `POST /api/commentservice/commentview`: Handles comment view requests.
- `POST /api/commentservice/createcommentrequest`: Handles create comment requests.
- `POST /api/commentservice/offsetbasedpageable`: Handles offset-based pageable requests.

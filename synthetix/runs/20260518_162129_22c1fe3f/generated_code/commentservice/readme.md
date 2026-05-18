# CommentService

This service manages comments on articles, including pagination and tag management.

## Building and Running

To build and run the service, use the following commands:

```bash
# Build the Docker image
$ docker build -t commentservice .

# Run the Docker container
$ docker run -p 8080:8080 commentservice
```

## API Endpoints

- `GET /health`: Health check endpoint
- `GET /ready`: Readiness check endpoint
- `POST /api/commentservice/commentview`: Handles comment view operations
- `POST /api/commentservice/createcommentrequest`: Handles comment creation requests
- `POST /api/commentservice/tagrepository`: Handles tag repository operations
- `POST /api/commentservice/offsetbasedpageable`: Handles offset-based pagination operations

## Testing

To run tests, use the following command:

```bash
$ go test ./...
```

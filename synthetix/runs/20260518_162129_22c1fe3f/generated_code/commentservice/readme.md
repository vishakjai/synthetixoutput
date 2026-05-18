# CommentService

This service manages comments on articles, including pagination and tag management.

## Running the Service

To build and run the service locally:

```bash
docker build -t commentservice .
docker run -p 8080:8080 commentservice
```

## Endpoints

- `POST /api/commentservice/commentview`
- `POST /api/commentservice/createcommentrequest`
- `POST /api/commentservice/tagrepository`
- `POST /api/commentservice/globalcontrolleradvice`

## Health Check

- `GET /health`
- `GET /ready`

## Testing

To run the tests:

```bash
go test ./...
```

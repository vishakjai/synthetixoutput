# CommentService

This service manages comments, including creation, retrieval, and updates to article comment counts.

## Running the Service

To run the service locally:

```bash
go run main.go
```

To build the Docker image:

```bash
docker build -t commentservice .
```

To run the Docker container:

```bash
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

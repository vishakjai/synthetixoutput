# ArticleService

ArticleService is a Go-based microservice for handling article-related operations including creation, retrieval, and metadata management.

## Requirements

- Go 1.20+
- Docker

## Running the Service

```bash
# Build the Docker image
docker build -t articleservice .

# Run the Docker container
docker run -p 8080:8080 articleservice
```

## Testing

```bash
go test ./...
```

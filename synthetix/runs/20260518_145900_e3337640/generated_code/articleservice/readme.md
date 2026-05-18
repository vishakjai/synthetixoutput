# ArticleService

This service handles article-related operations including creation, management, and retrieval.

## Run Locally

```bash
# Build the Docker image
$ docker build -t articleservice .

# Run the Docker container
$ docker run -p 8080:8080 articleservice
```

## Endpoints

- `POST /api/articles`: Create an article
- `GET /api/articles`: Retrieve articles
- `GET /api/articles/feed`: Get article feed
- `GET /api/articles/{slug}`: Retrieve a specific article
- `PUT /api/articles/{slug}`: Update an article
- `DELETE /api/articles/{slug}`: Delete an article
- `GET /api/articles/{slug}/comments`: Get comments for an article
- `POST /api/articles/{slug}/comments`: Add a comment to an article
- `DELETE /api/articles/{slug}/comments/{commentId}`: Delete a comment
- `POST /api/articles/{slug}/favorite`: Favorite an article
- `DELETE /api/articles/{slug}/favorite`: Unfavorite an article
- `GET /api/tags`: Retrieve tags

## Health Check

- `GET /health`: Returns service health status
- `GET /ready`: Returns service readiness status

## Test

```bash
$ go test ./...
```

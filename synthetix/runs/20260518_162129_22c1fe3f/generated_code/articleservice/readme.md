# ArticleService

This service handles article-related operations including CRUD and workflows.

## Running the Service

### Build the Docker Image
```bash
docker build -t articleservice .
```

### Run the Docker Container
```bash
docker run -p 8080:8080 articleservice
```

## Endpoints
- `POST /api/articles` - Create an article
- `GET /api/articles` - Get all articles
- `GET /api/articles/feed` - Get article feed
- `GET /api/articles/{slug}` - Get an article by slug
- `PUT /api/articles/{slug}` - Update an article by slug
- `DELETE /api/articles/{slug}` - Delete an article by slug
- `POST /api/articles/{slug}/favorite` - Favorite an article
- `DELETE /api/articles/{slug}/favorite` - Unfavorite an article
- `GET /api/articles/{slug}/comments` - Get comments for an article
- `POST /api/articles/{slug}/comments` - Add a comment to an article
- `DELETE /api/articles/{slug}/comments/{commentId}` - Delete a comment from an article
- `GET /api/tags` - Get all tags

## Health Endpoints
- `GET /health` - Health check
- `GET /ready` - Readiness check

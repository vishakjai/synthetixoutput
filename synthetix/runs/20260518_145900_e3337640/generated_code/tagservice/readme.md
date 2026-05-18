# TagService

TagService is a Go-based microservice for handling tag-related operations for categorizing articles.

## Building and Running

```bash
# Build the Docker image
$ docker build -t tagservice .

# Run the Docker container
$ docker run -p 8080:8080 tagservice
```

## Endpoints

- `GET /health` - Health check endpoint
- `GET /ready` - Readiness check endpoint
- `POST /api/tagservice/taglistview` - Tag list view operation
- `POST /api/tagservice/tagrepository` - Tag repository operation
- `POST /api/tagservice/profileview` - Profile view operation
- `POST /api/tagservice/localeconfigurer` - Locale configurer operation
- `POST /api/tagservice/notblankornull` - Not blank or null operation

## Testing

Run tests using:

```bash
$ go test ./...
```

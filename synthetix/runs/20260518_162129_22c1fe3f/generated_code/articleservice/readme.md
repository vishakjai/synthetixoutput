# ArticleService

ArticleService handles article-related operations including CRUD and workflows.

## Building and Running

```bash
docker build -t articleservice .
docker run -p 8080:8080 articleservice
```

## Testing

```bash
go test ./...
```

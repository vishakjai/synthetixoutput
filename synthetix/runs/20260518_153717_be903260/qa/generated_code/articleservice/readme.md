# ArticleService

This is the ArticleService component, responsible for handling article-related operations.

## Building the Service

To build the service, run:

```bash
docker build -t articleservice .
```

## Running the Service

To run the service, use:

```bash
docker run -p 8080:8080 articleservice
```

## Testing the Service

To test the service, execute:

```bash
go test ./...
```
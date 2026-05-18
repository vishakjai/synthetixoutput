# ProfileService

This is the ProfileService component, responsible for handling user profile operations.

## Building the Docker Image

```bash
docker build -t profileservice .
```

## Running the Service

```bash
docker run -p 8080:8080 profileservice
```

## Testing

Run the tests using:

```bash
go test ./...
```

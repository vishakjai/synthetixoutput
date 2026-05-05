# Chat Service

This service manages chat functionalities and interactions between users.

## Building

```bash
docker build -t chat-service .
```

## Running

```bash
docker run -p 8080:8080 chat-service
```

## Testing

Run tests with:

```bash
go test ./...
```
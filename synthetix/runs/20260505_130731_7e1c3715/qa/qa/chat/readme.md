# Chat Service

This is the Chat service, responsible for managing chat functionalities and interactions between users.

## Build

```bash
docker build -t chat-service .
```

## Run

```bash
docker run -p 8080:8080 chat-service
```

## Test

```bash
go test ./...
```
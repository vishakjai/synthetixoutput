# ChatService

This is the ChatService component, responsible for managing chat functionalities, events, and related workflows.

## Building the Docker Image

```bash
docker build -t chatservice .
```

## Running the Service

```bash
docker run -p 8080:8080 chatservice
```

## API Endpoints

- `GET /api/chat/{chatId}/messages`
- `GET /api/chat/{chatId}`
- `GET /api/chat`
- `POST /api/chat`
- `POST /api/chat/{chatId}/add`
- `DELETE /api/chat/{chatId}/leave`

## Health Check

- `GET /health`
- `GET /ready`

## Testing

Run tests using:

```bash
go test ./...
```

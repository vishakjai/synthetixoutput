# ChatService

This service manages chat functionalities including message handling and participant management.

## Running the Service

```bash
# Build the Docker image
$ docker build -t chatservice .

# Run the Docker container
$ docker run -p 8080:8080 chatservice
```

## Endpoints
- `GET /api/chat/{chatId}/messages`
- `GET /api/chat/{chatId}`
- `GET /api/chat`
- `POST /api/chat`
- `POST /api/chat/{chatId}/add`
- `DELETE /api/chat/{chatId}/leave`

## Health Check
- `GET /health`
- `GET /ready`
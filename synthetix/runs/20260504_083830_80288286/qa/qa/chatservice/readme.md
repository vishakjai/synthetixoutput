# ChatService

This service manages chat functionalities including message handling and participant management.

## Running the Service

Build the Docker image:
```bash
docker build -t chatservice .
```

Run the Docker container:
```bash
docker run -p 8080:8080 chatservice
```

## Endpoints
- `GET /api/chat/{chatId}/messages`: Retrieve messages for a chat.
- `GET /api/chat/{chatId}`: Retrieve chat details.
- `GET /api/chat`: List all chats.
- `POST /api/chat`: Create a new chat.
- `POST /api/chat/{chatId}/add`: Add a participant to a chat.
- `DELETE /api/chat/{chatId}/leave`: Leave a chat.

## Health Check
- `GET /health`: Health status of the service.
- `GET /ready`: Readiness status of the service.

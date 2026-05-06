# ChatService

## Overview

ChatService is a Go-based microservice that manages chat functionalities, including chat creation, message handling, and participant management.

## Building and Running

To build and run the ChatService locally:

```bash
# Build the Docker image
$ docker build -t chatservice .

# Run the Docker container
$ docker run -p 8080:8080 chatservice
```

## Endpoints

- `GET /health`: Health check endpoint
- `GET /ready`: Readiness check endpoint
- `GET /api/chat/{chatId}/messages`: Retrieve messages for a chat
- `GET /api/chat/{chatId}`: Retrieve chat details
- `GET /api/chat`: List all chats
- `POST /api/chat`: Create a new chat
- `POST /api/chat/{chatId}/add`: Add a participant to a chat
- `DELETE /api/chat/{chatId}/leave`: Leave a chat
- `DELETE /api/messages/{messageId}`: Delete a message
- `GET /api/participants`: List all participants
- `POST /api/participants/profile-picture-upload`: Upload a profile picture
- `POST /api/participants/confirm-profile-picture`: Confirm profile picture upload
- `DELETE /api/participants/profile-picture`: Delete profile picture

## Testing

Run tests using:

```bash
$ go test ./...
```

# ChatService

This is the ChatService application, which manages chat functionalities including message handling and participant management.

## Building the application

```bash
docker build -t chatservice .
```

## Running the application

```bash
docker run -p 8080:8080 chatservice
```

## Testing the application

Run the tests using:

```bash
go test ./...
```

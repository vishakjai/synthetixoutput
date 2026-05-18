# UserService

This is the UserService application that manages user operations including authentication and profile management.

## Requirements
- Go 1.19
- Docker

## Running the Service

1. Build the Docker image:
   ```bash
   docker build -t userservice .
   ```

2. Run the Docker container:
   ```bash
   docker run -p 8080:8080 userservice
   ```

3. Access the health endpoint to verify the service is running:
   ```bash
   curl http://localhost:8080/health
   ```

## Testing

Run tests using:
```bash
go test ./...
```
# User Service

This service handles user management, authentication, and related workflows.

## Building and Running

```bash
# Build the Docker image
$ docker build -t user-service .

# Run the Docker container
$ docker run -p 8080:8080 user-service
```

## Environment Variables
- `PORT`: Port to run the service on (default 8080).
- `DATABASE_URL`: Database connection URL.

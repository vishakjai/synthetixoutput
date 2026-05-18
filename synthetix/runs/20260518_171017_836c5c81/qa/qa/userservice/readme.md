# UserService

This is the UserService component responsible for handling user authentication, password management, and user data operations.

## Requirements

- Go 1.19
- Docker

## Running the Service

```bash
# Build the Docker image
$ docker build -t userservice .

# Run the Docker container
$ docker run -p 8080:8080 userservice
```

## Testing

Tests can be added in the `main_test.go` file.
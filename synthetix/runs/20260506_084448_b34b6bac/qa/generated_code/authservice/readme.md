# AuthService

AuthService is a Go-based service that provides authentication functionalities.

## Building and Running

To build and run the service, use the following commands:

```bash
# Build the Docker image
docker build -t authservice .

# Run the Docker container
docker run -p 8080:8080 authservice
```

## Environment Variables

- `PORT`: The port on which the server will listen (default is 8080).

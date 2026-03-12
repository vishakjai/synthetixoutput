# AuthenticationService

This service handles user authentication and authorization, managing login forms and JWT token generation.

## Build and Run

```bash
# Build the Docker image
docker build -t authenticationservice .

# Run the Docker container
docker run -p 8080:8080 authenticationservice
```

## Endpoints

- `GET /health` - Health check endpoint
- `GET /ready` - Readiness check endpoint
- `POST /auth/login` - Login endpoint

## Testing

Currently, manual testing can be done using tools like Postman or curl.
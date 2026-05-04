# AuthService

AuthService handles authentication, API key management, and related workflows.

## Building and Running

To build and run the application, you can use the following commands:

```bash
# Build the Docker image
$ docker build -t authservice .

# Run the Docker container
$ docker run -p 8080:8080 authservice
```

## Endpoints

- `GET /health`: Health check endpoint
- `GET /ready`: Readiness check endpoint
- `POST /api/auth/apiKey`: API key management
- `POST /api/auth/register`: Register a new user
- `POST /api/auth/login`: User login
- `POST /api/auth/refresh`: Refresh authentication token
- `POST /api/auth/logout`: Logout user
- `POST /api/auth/resend-verification`: Resend verification email
- `POST /api/auth/verify`: Verify user
- `POST /api/auth/forgot-password`: Initiate password reset

## Testing

To run the tests, use:

```bash
$ go test
```

# User Service

This service handles user management, authentication, and related workflows.

## Building and Running

```bash
# Build the Docker image
$ docker build -t user-service .

# Run the Docker container
$ docker run -p 8080:8080 user-service
```

## Endpoints

- `GET /health`: Health check endpoint.
- `GET /ready`: Readiness check endpoint.
- `POST /api/auth/apiKey`: API key authentication.
- `POST /api/auth/register`: User registration.
- `POST /api/auth/login`: User login.
- `POST /api/auth/refresh`: Refresh token.
- `POST /api/auth/logout`: User logout.
- `POST /api/auth/resend-verification`: Resend verification email.
- `POST /api/auth/verify`: Verify user email.
- `POST /api/auth/forgot-password`: Initiate password reset.
- `POST /api/auth/reset-password`: Reset password.
- `POST /api/auth/change-password`: Change password.

## Environment Variables

- `PORT`: Port to run the service on (default 8080).
- `DATABASE_URL`: Database connection URL.

# UserProfileService

This service handles user profile operations including authentication, registration, and profile updates.

## Building and Running

To build and run the service:

```bash
# Build the Docker image
docker build -t userprofileservice .

# Run the Docker container
docker run -p 8080:8080 userprofileservice
```

## Endpoints

- `POST /api/auth/apiKey`
- `POST /api/auth/register`
- `POST /api/auth/login`
- `POST /api/auth/refresh`
- `POST /api/auth/logout`
- `POST /api/auth/resend-verification`
- `POST /api/auth/verify`
- `POST /api/auth/forgot-password`
- `POST /api/auth/reset-password`
- `POST /api/auth/change-password`

## Health Check

- `GET /health`
- `GET /ready`

# UserProfileService

UserProfileService is a Go-based microservice for managing user profiles, including authentication and profile picture management.

## Requirements
- Go 1.18+
- Docker

## Running Locally

```bash
# Build the Docker image
$ docker build -t userprofileservice .

# Run the Docker container
$ docker run -p 8080:8080 userprofileservice
```

## Testing

```bash
# Run tests
$ go test ./...
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

## Health Checks
- `GET /health`
- `GET /ready`

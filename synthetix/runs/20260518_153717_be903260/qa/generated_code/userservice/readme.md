# UserService

UserService is a Go-based microservice that handles user authentication, profile updates, and interactions with the ArticleService.

## Setup

Ensure you have Go installed and Docker running on your machine.

### Running Locally

```bash
export DATABASE_URL=postgres://user:password@localhost:5432/dbname
export PORT=8080
go run main.go
```

### Building Docker Image

```bash
docker build -t userservice .
```

### Running with Docker

```bash
docker run -p 8080:8080 --env DATABASE_URL=postgres://user:password@localhost:5432/dbname userservice
```

## Endpoints

- `POST /api/users` - Signup a new user
- `POST /api/users/login` - Login a user
- `GET /api/user` - Get current user
- `PUT /api/user` - Update user
- `GET /api/profiles/{username}` - Get user profile
- `POST /api/profiles/{username}/follow` - Follow a user
- `DELETE /api/profiles/{username}/follow` - Unfollow a user

## Health Check

- `GET /health` - Service health
- `GET /ready` - Service readiness

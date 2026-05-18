# UserService

This service manages user authentication, profiles, and related workflows.

## Running the Service

```bash
docker build -t userservice .
docker run -p 8080:8080 userservice
```

## Endpoints

- `POST /api/users` - Sign up a new user
- `POST /api/users/login` - User login
- `GET /api/user` - Get current user
- `PUT /api/user` - Update user
- `GET /api/profiles/{username}` - Get user profile
- `POST /api/profiles/{username}/follow` - Follow a user
- `DELETE /api/profiles/{username}/follow` - Unfollow a user

## Health Check

- `GET /health` - Health status
- `GET /ready` - Ready status

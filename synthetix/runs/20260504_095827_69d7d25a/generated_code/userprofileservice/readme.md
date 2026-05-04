# UserProfileService

This service manages user profiles, authentication, and profile picture services.

## Building the Service

```bash
docker build -t userprofileservice .
```

## Running the Service

```bash
docker run -p 8080:8080 userprofileservice
```

## Testing the Service

You can test the health endpoints using curl:

```bash
curl http://localhost:8080/health
curl http://localhost:8080/ready
```

You can also test the user profile execution endpoint:

```bash
curl -X POST http://localhost:8080/userprofile/execute
```
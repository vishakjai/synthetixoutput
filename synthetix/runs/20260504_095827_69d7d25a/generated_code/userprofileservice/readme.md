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

## Health Check

```bash
curl http://localhost:8080/health
```

## Ready Check

```bash
curl http://localhost:8080/ready
```

## UserProfile Execution

```bash
curl -X POST http://localhost:8080/userprofile/execute
```

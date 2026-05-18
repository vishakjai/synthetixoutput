# UserService

This is the UserService application, which manages user authentication, profiles, and related workflows.

## Building the Application

```bash
docker build -t userservice .
```

## Running the Application

```bash
docker run -p 8080:8080 userservice
```

## Testing the Application

```bash
go test ./...
```

# AuthService

AuthService handles API key management, authentication filters, and related workflows.

## Build and Run

```bash
docker build -t authservice .
docker run -p 8080:8080 authservice
```

## Test

```bash
go test ./...
```

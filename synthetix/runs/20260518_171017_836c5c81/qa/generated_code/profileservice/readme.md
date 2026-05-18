# ProfileService

ProfileService manages user profiles and follow relationships, integrating user data.

## Running the Service

To build and run the service locally:

```bash
docker build -t profileservice .
docker run -p 8080:8080 profileservice
```

## Endpoints

- `POST /api/profileservice/profileview`
- `POST /api/profileservice/localeconfigurer`
- `POST /api/profileservice/notblankornull`
- `GET /health`
- `GET /ready`

## Testing

To run tests:

```bash
go test ./...
```

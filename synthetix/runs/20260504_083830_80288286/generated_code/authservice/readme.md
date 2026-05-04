# AuthService

## Description
AuthService handles API key management and authentication workflows.

## Running the Service

```bash
uvicorn main:app --host 0.0.0.0 --port 8080
```

## Building the Docker Image

```bash
docker build -t authservice .
```

## Running the Docker Container

```bash
docker run -p 8080:8080 authservice
```

## Testing

```bash
pytest tests
```
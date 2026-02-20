# ApplicationStateService

This service manages application-wide state such as total sessions and application name.

## Build

```bash
npm install
npm run build
```

## Run

```bash
npm start
```

## Test

```bash
npm test
```

## Docker

Build the Docker image:

```bash
docker build -t application-state-service .
```

Run the Docker container:

```bash
docker run -p 8080:8080 application-state-service
```

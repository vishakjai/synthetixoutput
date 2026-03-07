# BusinessLogicService

This service translates VB6 business logic to C# while maintaining functional parity.

## Build and Run

```bash
# Build the Docker image
docker build -t businesslogicservice .

# Run the Docker container
docker run -p 8080:8080 businesslogicservice
```

## Test

Currently, there are no automated tests. Manual testing can be done via the `/health` and `/ready` endpoints.
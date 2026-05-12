# CustomerAndAccountContractor API

This is the Customer and Account Contractor API, a sub-module of the Customer and Account service.

## Running the Application

To run the application locally, use the following commands:

```bash
# Build the Docker image
docker build -t customer-and-account-contractor-api .

# Run the Docker container
docker run -d -p 8080:8080 customer-and-account-contractor-api
```

## Endpoints

- `GET /health`: Returns the health status of the service.
- `GET /ready`: Returns the readiness status of the service.
- `GET /contractorplacement/contractorplacemententitycontroller`: Returns contractor placement entity status.
- `GET /contractorplacement/contractorplacementsearchcontroller`: Searches contractor placements.

## Testing

Tests can be run using the `dotnet test` command in the test project directory.
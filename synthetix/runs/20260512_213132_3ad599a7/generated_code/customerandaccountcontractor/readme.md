# Customer and Account Contractor API

This API is part of the MagicBox Contractor Modernization project. It provides endpoints for managing contractor placements and customer accounts.

## Running the Application

To run the application locally, use the following command:

```bash
dotnet run --project src/CustomerAndAccountContractor.Api
```

## Building the Docker Image

To build the Docker image, use the following command:

```bash
docker build -t customer-and-account-contractor-api .
```

## Running the Docker Container

To run the Docker container, use the following command:

```bash
docker run -p 8080:8080 customer-and-account-contractor-api
```

## Endpoints

- GET `/health` - Health check endpoint.
- GET `/ready` - Readiness check endpoint.
- GET `/contractorplacement/contractorplacemententitycontroller` - Contractor placement entity controller.
- ... (other endpoints)

## Testing

To run the tests, use the following command:

```bash
dotnet test
```
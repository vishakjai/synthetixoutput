# CustomerAndAccountAddress API

This is the Customer and Account Address API service, part of the Magicbox Modernization Architecture.

## Building and Running

To build and run the service locally:

```bash
dotnet build src/CustomerAndAccountAddress.Api/CustomerAndAccountAddress.Api.csproj
```

To run the service:

```bash
dotnet run --project src/CustomerAndAccountAddress.Api/CustomerAndAccountAddress.Api.csproj
```

The service will be available at `http://localhost:8080`.

## Docker

To build and run the Docker container:

```bash
docker build -t customer-and-account-address-api .
docker run -p 8080:8080 customer-and-account-address-api
```

## Endpoints

- `GET /health` - Health check endpoint.
- `GET /ready` - Readiness check endpoint.
- `GET /customer/profile` - Example endpoint for customer profile.
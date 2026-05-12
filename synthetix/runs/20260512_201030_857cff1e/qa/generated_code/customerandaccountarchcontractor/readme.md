# Customer and Account Archcontractor API

This project is part of the modernization effort for the MagicBox Contractor Increment. It provides endpoints for managing customer and account data.

## Requirements
- .NET 8 SDK
- Docker

## Running the Application

1. Build the Docker image:
   ```bash
   docker build -t customer-and-account-api .
   ```

2. Run the Docker container:
   ```bash
   docker run -p 8080:80 customer-and-account-api
   ```

3. Access the health endpoints:
   - [http://localhost:8080/health](http://localhost:8080/health)
   - [http://localhost:8080/ready](http://localhost:8080/ready)

## Testing

Run the tests using the .NET CLI:

```bash
cd tests/CustomerAndAccountArchcontractor.Api.Tests
 dotnet test
```
# CustomerAndAccountContractor API

This API is part of the MagicBox Contractor Increment Modernization project, focusing on the Customer and Account contractor module.

## Requirements
- .NET 8 SDK
- PostgreSQL

## Running the Application
1. Set up the PostgreSQL database and update the connection string in `appsettings.Development.json`.
2. Build the project:
   ```bash
   dotnet build
   ```
3. Run the application:
   ```bash
   dotnet run --project src/CustomerAndAccountContractor.Api
   ```

## Testing
Run the tests using:
```bash
   dotnet test
```

## Docker
Build and run the Docker image:
```bash
   docker build -t customer-and-account-contractor-api .
   docker run -p 8080:80 customer-and-account-contractor-api
```
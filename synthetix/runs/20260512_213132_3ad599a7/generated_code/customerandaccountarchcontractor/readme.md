# Customer and Account Archcontractor API

This is the Customer and Account Archcontractor API built with ASP.NET Core.

## Building and Running

1. **Build the Docker image:**
   ```bash
   docker build -t customer-and-account-archcontractor-api .
   ```

2. **Run the Docker container:**
   ```bash
   docker run -p 8080:8080 customer-and-account-archcontractor-api
   ```

3. **Access Swagger UI:**
   Open your browser and navigate to `http://localhost:8080/swagger` to see the API documentation.

## Testing

Run the tests using the following command:

```bash
dotnet test
```
# Customer and Account Account API

This API is part of the Customer and Account service, focusing on account-related operations.

## Building and Running

```bash
# Build the Docker image
docker build -t customer-account-api .

# Run the Docker container
docker run -p 8080:8080 customer-account-api
```

## Endpoints
- GET /health
- GET /ready
- GET /customer/profile

## Configuration
- Ensure the `DefaultConnection` string is set in your environment variables or appsettings.json.
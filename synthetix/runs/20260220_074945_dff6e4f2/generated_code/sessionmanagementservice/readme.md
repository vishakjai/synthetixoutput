# SessionManagementService

This service handles user session lifecycle, including authentication and flash messaging.

## Requirements

- Node.js 18+
- Docker

## Setup

1. Install dependencies:
   ```bash
   npm install
   ```

2. Build the project:
   ```bash
   npm run build
   ```

3. Run the service:
   ```bash
   npm start
   ```

4. Run tests:
   ```bash
   npm test
   ```

## Docker

Build and run the Docker container:

```bash
# Build the Docker image
docker build -t session-management-service .

# Run the Docker container
docker run -p 8080:8080 session-management-service
```

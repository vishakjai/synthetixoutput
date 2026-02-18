# Authentication Service

This service manages user authentication and session state using Express-session and JWT.

## Requirements
- Node.js
- Docker

## Setup

1. Install dependencies:
   ```bash
   npm install
   ```

2. Run the service:
   ```bash
   npm start
   ```

3. Run tests:
   ```bash
   npm test
   ```

## Docker

To build and run the Docker container:

```bash
docker build -t authentication-service .
docker run -p 8080:8080 authentication-service
```

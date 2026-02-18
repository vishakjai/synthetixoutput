# Customer Management Service

This service manages customer data and interfaces with MongoDB.

## Requirements

- Node.js
- MongoDB

## Setup

1. Install dependencies:

   ```bash
   npm install
   ```

2. Run the application:

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
docker build -t customer-management .
docker run -p 8080:8080 customer-management
```

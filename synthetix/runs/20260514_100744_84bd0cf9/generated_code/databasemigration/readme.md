# Database Migration Service

This service handles database schema migration from the legacy system to the new Go-based system.

## Running the Service

### Prerequisites
- Go 1.20
- Docker

### Environment Variables
- `PORT`: Port to run the service on (default: 8080)
- `DATABASE_URL`: Connection string for the PostgreSQL database

### Build and Run
```bash
# Build the Docker image
docker build -t databasemigration .

# Run the Docker container
docker run -p 8080:8080 -e DATABASE_URL=your_database_url databasemigration
```

### Endpoints
- `/health`: Returns the health status of the service
- `/ready`: Returns the readiness status of the service

# DatabaseMigration Service

This service handles the migration of database schemas and data from a legacy system to a new system.

## Running the Service

### Prerequisites
- Docker
- Go 1.18

### Building the Docker Image
```bash
docker build -t databasemigration .
```

### Running the Docker Container
```bash
docker run -p 8080:8080 -e DATABASE_URL=your_database_url databasemigration
```

### Health Check
- `GET /health` returns `{"status": "healthy"}`
- `GET /ready` returns `{"status": "ready"}`

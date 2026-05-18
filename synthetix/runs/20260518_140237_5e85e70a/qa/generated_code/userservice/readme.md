# UserService

This is the UserService application built with Kotlin and Spring Boot.

## Requirements
- JDK 17
- Docker

## Building the application
```bash
./gradlew clean build
```

## Running the application
```bash
docker build -t userservice .
docker run -p 8080:8080 userservice
```

## Testing
```bash
./gradlew test
```
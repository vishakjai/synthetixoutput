# CommentService

This is the CommentService application built with Spring Boot and Kotlin.

## Requirements
- JDK 17
- Docker

## Running the application

```bash
./gradlew bootRun
```

## Building the Docker image

```bash
docker build -t commentservice .
```

## Running the Docker container

```bash
docker run -p 8080:8080 commentservice
```

## Testing

```bash
./gradlew test
```
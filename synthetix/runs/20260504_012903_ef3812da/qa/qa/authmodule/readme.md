# AuthModule

## Build

```bash
docker build -t authmodule .
```

## Run

```bash
docker run -p 8080:8080 authmodule
```

## Test

```bash
curl http://localhost:8080/health
curl http://localhost:8080/ready
curl http://localhost:8080/authmodule/execute
```

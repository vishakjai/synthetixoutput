# NotificationModule

## Description

NotificationModule owns the behavior currently implemented by DeviceTokenController, NotificationExceptionHandler, PushNotification, and related workflows.

## Build

```bash
docker build -t notificationmodule .
```

## Run

```bash
docker run -p 8080:8080 notificationmodule
```

## Test

```bash
go test ./...
```
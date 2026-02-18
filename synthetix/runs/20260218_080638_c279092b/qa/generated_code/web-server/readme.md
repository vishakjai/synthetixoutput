# Web Server

This is a Node.js web server that handles HTTP requests, routing, and session management using Express.js.

## Requirements
- Node.js
- Docker

## Installation

```bash
npm install
```

## Running the Server

```bash
npm start
```

## Running in Docker

```bash
docker build -t web-server .
docker run -p 8080:8080 web-server
```

## Testing

```bash
npm test
```

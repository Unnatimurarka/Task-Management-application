# API Specification

## Auth Endpoints
### POST /api/auth/register
- **Request**: `{ "username": "string", "password": "string" }`
- **Response**: `{ "token": "jwt_string", "username": "string" }` (200 OK)

### POST /api/auth/login
- **Request**: `{ "username": "string", "password": "string" }`
- **Response**: `{ "token": "jwt_string", "username": "string" }` (200 OK)

## Task Endpoints (Require Bearer Token)
### GET /api/tasks
- **Response**: `[ { "id": int, "title": "string", "description": "string", "status": "string", "createdAt": "datetime" } ]` (200 OK)

### POST /api/tasks
- **Request**: `{ "title": "string", "description": "string" }`
- **Response**: Task Object (200 OK)

### PUT /api/tasks/{id}
- **Request**: `{ "title": "string", "description": "string", "status": "string" }`
- **Response**: 204 No Content

### DELETE /api/tasks/{id}
- **Response**: 204 No Content

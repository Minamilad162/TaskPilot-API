# TaskPilot API - API Contract

Base URL:

```text
/api/v1
```

All endpoints except authentication require a Bearer token.

## Standard Response

Success response:

```json
{
  "success": true,
  "message": "Request completed successfully.",
  "data": {}
}
```

Error response:

```json
{
  "success": false,
  "message": "Validation failed.",
  "data": null,
  "errors": []
}
```

## Authentication

### Register

```http
POST /api/v1/auth/register
```

Request:

```json
{
  "fullName": "Bishoy Samuel",
  "email": "bishoy@example.com",
  "password": "P@ssw0rd123"
}
```

Response data:

```json
{
  "userId": "guid",
  "fullName": "Bishoy Samuel",
  "email": "bishoy@example.com",
  "accessToken": "jwt-token",
  "expiresAt": "2026-05-20T18:30:00Z"
}
```

### Login

```http
POST /api/v1/auth/login
```

Request:

```json
{
  "email": "bishoy@example.com",
  "password": "P@ssw0rd123"
}
```

Response data:

```json
{
  "userId": "guid",
  "fullName": "Bishoy Samuel",
  "email": "bishoy@example.com",
  "accessToken": "jwt-token",
  "expiresAt": "2026-05-20T18:30:00Z"
}
```

### Current User

```http
GET /api/v1/auth/me
Authorization: Bearer {token}
```

Response data:

```json
{
  "userId": "guid",
  "email": "bishoy@example.com",
  "fullName": "Bishoy Samuel",
  "roles": ["User"]
}
```

## Projects

### Create Project

```http
POST /api/v1/projects
Authorization: Bearer {token}
```

Request:

```json
{
  "name": "Interview Task",
  "description": "Backend technical assessment"
}
```

Response data:

```json
{
  "id": "guid",
  "name": "Interview Task",
  "description": "Backend technical assessment",
  "createdAt": "2026-05-20T18:30:00Z",
  "totalTasks": 0,
  "completedTasks": 0
}
```

### Get All Projects

```http
GET /api/v1/projects
Authorization: Bearer {token}
```

Response data:

```json
[
  {
    "id": "guid",
    "name": "Interview Task",
    "description": "Backend technical assessment",
    "createdAt": "2026-05-20T18:30:00Z",
    "totalTasks": 3,
    "completedTasks": 1
  }
]
```

### Get Project By Id

```http
GET /api/v1/projects/{id}
Authorization: Bearer {token}
```

Response data:

```json
{
  "id": "guid",
  "name": "Interview Task",
  "description": "Backend technical assessment",
  "createdAt": "2026-05-20T18:30:00Z",
  "tasks": []
}
```

### Update Project

```http
PUT /api/v1/projects/{id}
Authorization: Bearer {token}
```

Request:

```json
{
  "name": "Updated Interview Task",
  "description": "Updated description"
}
```

### Delete Project

```http
DELETE /api/v1/projects/{id}
Authorization: Bearer {token}
```

## Tasks

### Create Task

```http
POST /api/v1/projects/{projectId}/tasks
Authorization: Bearer {token}
```

Request:

```json
{
  "title": "Create clean architecture solution",
  "description": "Implement backend assessment",
  "dueDate": "2026-05-25T12:00:00Z",
  "priority": "High"
}
```

Response data:

```json
{
  "id": "guid",
  "title": "Create clean architecture solution",
  "description": "Implement backend assessment",
  "status": "Todo",
  "dueDate": "2026-05-25T12:00:00Z",
  "priority": "High",
  "projectId": "guid"
}
```

### Get Tasks By Project

```http
GET /api/v1/projects/{projectId}/tasks
Authorization: Bearer {token}
```

### Update Task Status

```http
PATCH /api/v1/tasks/{id}/status
Authorization: Bearer {token}
```

Request:

```json
{
  "status": "Done"
}
```

Valid statuses:

```text
Todo
InProgress
Done
Cancelled
```

### Delete Task

```http
DELETE /api/v1/tasks/{id}
Authorization: Bearer {token}
```

## HTTP Status Codes

| Code | Meaning |
|---:|---|
| 200 | Successful read or update |
| 201 | Resource created |
| 204 | Resource deleted |
| 400 | Validation error |
| 401 | Missing or invalid token |
| 403 | User cannot access the resource |
| 404 | Resource not found |
| 409 | Conflict such as duplicate email |
| 500 | Unexpected server error |

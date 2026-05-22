# API Testing Guide

## Base URLs

- HTTPS: `https://localhost:7001`
- HTTP: `http://localhost:5000`

## Swagger

Open:

```text
https://localhost:7001/swagger
```

## Health Check

```http
GET /health
```

Expected response:

```json
{
  "status": "Healthy"
}
```

## Authentication Flow

### Register

```http
POST /api/v1/auth/register
```

```json
{
  "fullName": "Mina Milad",
  "email": "mina.test@example.com",
  "password": "Mina123$"
}
```

### Login

```http
POST /api/v1/auth/login
```

```json
{
  "email": "mina.test@example.com",
  "password": "Mina123$"
}
```

Copy `data.accessToken` and use it in Swagger/Postman as:

```text
Bearer YOUR_TOKEN_HERE
```

### Current User

```http
GET /api/v1/auth/me
```

Use this endpoint after login to confirm that the token is valid and contains the expected `User` role.

## Projects Flow

### Create Project

```http
POST /api/v1/projects
```

```json
{
  "name": "Interview Task",
  "description": "Backend .NET Clean Architecture implementation"
}
```

### Get Projects

```http
GET /api/v1/projects
```

### Get Project By Id

```http
GET /api/v1/projects/{id}
```

### Update Project

```http
PUT /api/v1/projects/{id}
```

```json
{
  "name": "Interview Task Updated",
  "description": "Updated description"
}
```

### Delete Project

```http
DELETE /api/v1/projects/{id}
```

## Tasks Flow

### Create Task

```http
POST /api/v1/projects/{projectId}/tasks
```

```json
{
  "title": "Implement API documentation",
  "description": "Prepare Swagger and Postman deliverables",
  "dueDate": "2026-05-25T12:00:00Z",
  "priority": "High"
}
```

### Get Tasks By Project

```http
GET /api/v1/projects/{projectId}/tasks
```

### Update Task Status

```http
PATCH /api/v1/tasks/{id}/status
```

```json
{
  "status": "Done"
}
```

Allowed status values:

- `Todo`
- `InProgress`
- `Done`
- `Cancelled`

### Delete Task

```http
DELETE /api/v1/tasks/{id}
```

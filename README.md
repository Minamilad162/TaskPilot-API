# TaskPilot API

A clean and scalable Project & Task Management backend built with .NET 9, ASP.NET Core Web API, Clean Architecture, Entity Framework Core, SQL Server, and JWT Authentication.

## Features

- User registration and login
- JWT authentication
- Role-based authorization
- Project management
- Task management inside projects
- User-owned data isolation
- DTO-based API contracts
- Global exception handling
- Generic API response wrapper
- EF Core migrations
- Swagger with JWT support
- Postman collection
- Optional Docker support

## Architecture

```text
src/
  ProjectTaskManagement.Api
  ProjectTaskManagement.Application
  ProjectTaskManagement.Domain
  ProjectTaskManagement.Infrastructure

tests/
  ProjectTaskManagement.Application.Tests

docs/
  SYSTEM_DESIGN.md
  PROJECT_STRUCTURE.md
  API_CONTRACT.md
  API_TESTING_GUIDE.md
```

## Prerequisites

- .NET 9 SDK
- SQL Server LocalDB or SQL Server Express
- Visual Studio 2022 / VS Code
- Postman optional
- Docker Desktop optional

## Database Connection

Development uses LocalDB by default:

```json
"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TaskPilotDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
```

## Run Locally

```powershell
cd "C:\TaskPilot API"
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet restore
dotnet build
dotnet ef database update --project src/ProjectTaskManagement.Infrastructure --startup-project src/ProjectTaskManagement.Api
dotnet run --project src/ProjectTaskManagement.Api --urls "https://localhost:7001;http://localhost:5000"
```

## Open Swagger

```text
https://localhost:7001/swagger
```

The root URL redirects to Swagger:

```text
https://localhost:7001/
```

## Authentication

Register:

```http
POST /api/v1/auth/register
```

Login:

```http
POST /api/v1/auth/login
```

Use the returned token in protected endpoints:

```text
Bearer YOUR_TOKEN_HERE
```

## Main Endpoints

### Authentication

```text
GET    /api/v1/auth/me
```

### Projects

```text
POST   /api/v1/projects
GET    /api/v1/projects
GET    /api/v1/projects/{id}
PUT    /api/v1/projects/{id}
DELETE /api/v1/projects/{id}
```

### Tasks

```text
POST   /api/v1/projects/{projectId}/tasks
GET    /api/v1/projects/{projectId}/tasks
PATCH  /api/v1/tasks/{id}/status
DELETE /api/v1/tasks/{id}
```

## Postman

Import:

```text
postman/TaskPilot.postman_collection.json
```

Run order:

1. Register or Login
2. Create Project
3. Create Task
4. Get / Update / Delete resources


##Features

- Generic response wrapper for consistent API results.
- Role-based authorization using ASP.NET Core Identity roles and JWT role claims.
- Unit tests for application services.
- Docker support for optional containerized execution.
- API versioning convention through the `/api/v1` route prefix.

Apply migrations:

```powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef database update --project src/ProjectTaskManagement.Infrastructure --startup-project src/ProjectTaskManagement.Api
```

Run the API:

```powershell
dotnet run --project src/ProjectTaskManagement.Api --urls "https://localhost:7001;http://localhost:5000"
```
A detailed checklist is available in:

```text
docs/FINAL_TESTING_CHECKLIST.md
```

## Documentation

- `docs/SYSTEM_DESIGN.md`
- `docs/PROJECT_STRUCTURE.md`
- `docs/API_CONTRACT.md`
- `docs/API_TESTING_GUIDE.md`
- `docs/FINAL_TESTING_CHECKLIST.md`
- `docs/MILESTONE_12_DOCKER_SUPPORT.md`
- `docs/MILESTONE_13_AUTHORIZATION_API_VERSIONING.md`
- `docs/MILESTONE_14_FINAL_REVIEW_CLEANUP.md`
- `SUBMISSION_NOTES.md`




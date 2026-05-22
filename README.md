# TaskPilot API

A clean and scalable Project & Task Management backend built with .NET 9, ASP.NET Core Web API, Clean Architecture, Entity Framework Core, SQL Server, and JWT Authentication.

## Features

- User registration and login
- JWT authentication
- Project management
- Task management inside projects
- User-owned data isolation
- DTO-based API contracts
- Global exception handling
- Generic API response wrapper
- EF Core migrations
- Swagger with JWT support
- Postman collection

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

## Documentation

- `docs/SYSTEM_DESIGN.md`
- `docs/PROJECT_STRUCTURE.md`
- `docs/API_CONTRACT.md`
- `docs/API_TESTING_GUIDE.md`
- `SUBMISSION_NOTES.md`

## Unit Tests

Run the application tests from the repository root:

```bash
dotnet test
```

The test project covers the main application services using EF Core InMemory. The focus is on ownership filtering, project operations, task creation, task status updates, and expected exceptions.

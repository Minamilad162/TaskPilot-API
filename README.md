# TaskPilot API

TaskPilot API is a clean and scalable Project & Task Management backend built with ASP.NET Core Web API, Clean Architecture, Entity Framework Core, SQL Server, and JWT Authentication.

## Current Milestone

Milestone 07 - Tasks Module.

Implemented authenticated task management inside user-owned projects.

## Available Modules

- Authentication: Register and Login using JWT.
- Projects: Create, read, update, and delete authenticated user projects.
- Tasks: Create tasks inside projects, list project tasks, update task status, and delete tasks.

## Run Locally

```bash
dotnet restore
dotnet build
```

To run the API in development mode:

```powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet run --project src/ProjectTaskManagement.Api --urls "http://localhost:5000"
```

Swagger:

```text
http://localhost:5000/swagger
```

## Notes

Database migrations will be finalized in the dedicated database milestone after the main modules are complete.

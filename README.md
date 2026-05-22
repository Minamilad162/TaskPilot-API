# TaskPilot API

TaskPilot API is a clean and scalable Project & Task Management backend built with ASP.NET Core Web API, Clean Architecture, Entity Framework Core, SQL Server, and JWT Authentication.

## Current Milestone

Milestone 09 - Database Migrations.

Implemented the initial EF Core migration and SQL schema script for Identity, Projects, and TaskItems.

## Available Modules

- Authentication: Register and Login using JWT.
- Projects: Create, read, update, and delete authenticated user projects.
- Tasks: Create tasks inside projects, list project tasks, update task status, and delete tasks.
- API Error Handling: Global exception middleware and consistent validation responses.
- Database: Initial migration and SQL schema script.

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

## Database Setup

Make sure SQL Server is running and the `DefaultConnection` in `src/ProjectTaskManagement.Api/appsettings.Development.json` points to your local database.

Install or update the EF Core CLI if needed:

```bash
dotnet tool install --global dotnet-ef
```

If it is already installed:

```bash
dotnet tool update --global dotnet-ef
```

Apply the migration:

```bash
dotnet ef database update --project src/ProjectTaskManagement.Infrastructure --startup-project src/ProjectTaskManagement.Api
```

Alternative SQL script:

```text
database/migrations/001_initial_schema.sql
```

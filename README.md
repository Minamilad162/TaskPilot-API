# TaskPilot API

TaskPilot API is a clean and scalable Project & Task Management backend built with ASP.NET Core Web API, Clean Architecture, Entity Framework Core, SQL Server, and JWT Authentication.

## Current Milestone

Milestone 1: System design and project structure documentation.

## Current Scope

This branch contains:

- Initial Clean Architecture solution structure
- Basic API health endpoint
- System design documentation
- Project structure documentation
- API contract draft

Business implementation will be added incrementally in the next branches.

## Solution Projects

```text
src/ProjectTaskManagement.Api
src/ProjectTaskManagement.Application
src/ProjectTaskManagement.Domain
src/ProjectTaskManagement.Infrastructure
tests/ProjectTaskManagement.Application.Tests
```

## Documentation

```text
docs/SYSTEM_DESIGN.md
docs/PROJECT_STRUCTURE.md
docs/API_CONTRACT.md
```

## Run

```bash
dotnet restore
dotnet build
```

```bash
dotnet run --project src/ProjectTaskManagement.Api
```

## Health Check

```text
GET /health
```

Swagger, authentication, persistence, and business endpoints will be added in later milestones.

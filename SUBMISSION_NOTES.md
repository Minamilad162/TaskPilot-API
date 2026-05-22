# TaskPilot API - Submission Notes

## Candidate

Bishoy Samuel

## Project

TaskPilot API - Project & Task Management Backend

## Stack

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / LocalDB
- ASP.NET Core Identity
- JWT Authentication
- Clean Architecture
- Swagger
- Postman Collection

## How to Run

```powershell
cd "C:\TaskPilot API"
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet restore
dotnet build
dotnet ef database update --project src/ProjectTaskManagement.Infrastructure --startup-project src/ProjectTaskManagement.Api
dotnet run --project src/ProjectTaskManagement.Api --urls "https://localhost:7001;http://localhost:5000"
```

Swagger:

```text
https://localhost:7001/swagger
```

## Review Notes

The solution is structured around Clean Architecture with clear separation between API, Application, Domain, and Infrastructure layers.

User-owned data is protected by filtering projects and tasks using the authenticated user id extracted from the JWT token.

Global exception handling and a generic response wrapper are used to keep API responses consistent.

## Included Deliverables

- GitHub-ready source code
- README setup instructions
- EF Core migration files
- SQL schema script
- Swagger documentation
- Postman collection
- System design documentation

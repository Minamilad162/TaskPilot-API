# TaskPilot API - Project Structure

## Solution Layout

```text
TaskPilot API/
├── src/
│   ├── ProjectTaskManagement.Api/
│   ├── ProjectTaskManagement.Application/
│   ├── ProjectTaskManagement.Domain/
│   └── ProjectTaskManagement.Infrastructure/
│
├── tests/
│   └── ProjectTaskManagement.Application.Tests/
│
├── docs/
├── database/
│   └── migrations/
├── postman/
├── ProjectTaskManagement.sln
├── Directory.Build.props
├── .editorconfig
├── .gitignore
└── README.md
```

The public product name is **TaskPilot API**. The internal .NET namespaces use `ProjectTaskManagement` because they describe the business domain directly and remain clear for reviewers.

## src/ProjectTaskManagement.Api

The API project is the delivery layer.

Responsibilities:

- Configure ASP.NET Core pipeline.
- Register controllers.
- Configure Swagger.
- Configure authentication and authorization.
- Add global exception middleware.
- Expose HTTP endpoints.

Expected folders:

```text
Controllers/
Middleware/
Services/
Extensions/
```

This project should stay thin. It should call application services and return responses.

## src/ProjectTaskManagement.Application

The Application project contains use cases and contracts.

Responsibilities:

- Define service interfaces.
- Implement application services.
- Define DTOs.
- Define request and response models.
- Define custom exceptions.
- Define abstractions used by infrastructure.

Expected folders:

```text
Auth/
Projects/
Tasks/
Common/
```

This layer should not know about SQL Server, EF migrations, or HTTP implementation details.

## src/ProjectTaskManagement.Domain

The Domain project contains the core business model.

Responsibilities:

- Define entities.
- Define enums.
- Keep business rules close to entities where practical.
- Avoid framework dependencies.

Expected folders:

```text
Common/
Entities/
Enums/
```

## src/ProjectTaskManagement.Infrastructure

The Infrastructure project contains technical implementation details.

Responsibilities:

- EF Core DbContext.
- Entity configurations.
- SQL Server setup.
- Identity implementation.
- JWT token generation.
- External service implementations.

Expected folders:

```text
Persistence/
Persistence/Configurations/
Identity/
Authentication/
```

## tests/ProjectTaskManagement.Application.Tests

The Tests project validates application behavior.

Focus areas:

- Project service behavior.
- Task service behavior.
- Ownership checks.
- Not found scenarios.
- Status update scenarios.

## docs

Contains design and delivery documentation:

```text
SYSTEM_DESIGN.md
PROJECT_STRUCTURE.md
API_CONTRACT.md
MILESTONE_00_INITIAL_SETUP.md
MILESTONE_01_SYSTEM_DESIGN.md
```

## database/migrations

Contains database-related deliverables. EF Core migrations will be generated later, and an optional SQL script can be placed here for review.

## postman

Contains the Postman collection used to test the API manually.

## Naming Rules

- Controllers use plural resource names: `ProjectsController`, `TasksController`.
- DTOs end with `Request`, `Response`, or `Dto`.
- Interfaces start with `I`.
- Domain entities use singular names.
- Async methods end with `Async`.

## Dependency Rule

Allowed dependencies:

```text
Api -> Application
Api -> Infrastructure
Application -> Domain
Infrastructure -> Application
Infrastructure -> Domain
Tests -> Application
```

Not allowed:

```text
Domain -> Application
Domain -> Infrastructure
Domain -> Api
Application -> Api
Application -> Infrastructure
```

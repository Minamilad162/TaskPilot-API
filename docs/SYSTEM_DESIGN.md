# TaskPilot API - System Design

## 1. Overview

TaskPilot API is a backend system for managing projects and tasks. The application allows authenticated users to create projects, manage tasks inside those projects, and track task status and priority.

The solution is designed around Clean Architecture to keep business logic independent from frameworks, databases, and delivery mechanisms.

## 2. Main Goals

- Provide a secure Project and Task Management API.
- Keep the codebase maintainable and easy to extend.
- Separate domain logic from infrastructure concerns.
- Use JWT authentication for stateless API access.
- Use Entity Framework Core with SQL Server for persistence.
- Return consistent API responses and predictable errors.

## 3. Architecture Style

The solution follows Clean Architecture with four main layers:

```text
API -> Application -> Domain
          ^
          |
Infrastructure
```

### API Layer

Responsible for HTTP concerns only:

- Controllers
- Request routing
- Authentication middleware
- Swagger configuration
- Global exception handling
- API version route prefix

The API layer should not contain business rules.

### Application Layer

Responsible for use cases and contracts:

- DTOs
- Service interfaces
- Application services
- Validation rules
- Response wrapper
- Custom exceptions
- Current user abstraction

This layer coordinates business workflows but does not depend on EF Core or ASP.NET implementation details.

### Domain Layer

Responsible for core business models:

- Project entity
- Project task entity
- Task status enum
- Task priority enum
- Shared base entity types

The domain layer should stay framework-independent.

### Infrastructure Layer

Responsible for external implementation details:

- EF Core DbContext
- SQL Server configuration
- ASP.NET Core Identity
- JWT token generation
- Entity configurations
- Dependency injection registration

## 4. Domain Model

### Project

A project belongs to one authenticated user and can contain multiple tasks.

Fields:

- Id
- Name
- Description
- CreatedAt
- OwnerId

### ProjectTask

A task belongs to one project and one authenticated user.

Fields:

- Id
- Title
- Description
- Status
- DueDate
- Priority
- ProjectId
- OwnerId
- CreatedAt

## 5. Ownership and Authorization Model

Every project and task stores `OwnerId`.

Application queries must always filter data by the authenticated user's id. This prevents one user from reading or modifying another user's projects or tasks, even if they know the entity id.

Example rule:

```text
A user can only access projects where Project.OwnerId equals CurrentUser.UserId.
```

Task access is also checked through owner and project ownership.

## 6. Authentication Flow

### Register

1. User sends full name, email, and password.
2. Identity creates a new user with hashed password.
3. Default role is assigned.
4. JWT token is generated and returned.

### Login

1. User sends email and password.
2. Identity validates credentials.
3. JWT token is generated and returned.

### JWT Claims

The token should include:

- User id
- Email
- Full name
- Roles

## 7. API Design

Base route:

```text
/api/v1
```

Main endpoints:

```text
POST   /api/v1/auth/register
POST   /api/v1/auth/login

POST   /api/v1/projects
GET    /api/v1/projects
GET    /api/v1/projects/{id}
PUT    /api/v1/projects/{id}
DELETE /api/v1/projects/{id}

POST   /api/v1/projects/{projectId}/tasks
GET    /api/v1/projects/{projectId}/tasks
PATCH  /api/v1/tasks/{id}/status
DELETE /api/v1/tasks/{id}
```

## 8. Error Handling Strategy

A global exception middleware will convert exceptions into consistent HTTP responses.

Expected mapping:

| Exception | HTTP Status |
|---|---:|
| ValidationException | 400 |
| UnauthorizedAccessException | 401 |
| ForbiddenException | 403 |
| NotFoundException | 404 |
| ConflictException | 409 |
| Unhandled exception | 500 |

## 9. Validation Strategy

Incoming requests are validated through request DTOs. Invalid requests should return a consistent error response without reaching the business logic.

Examples:

- Project name is required.
- Task title is required.
- Task priority must be a valid enum value.
- Task status must be a valid enum value.

## 10. Database Design

The database will include Identity tables and application tables.

Main application tables:

```text
Projects
TaskItems
```

Indexes should support common queries:

- Projects by owner and creation date
- Tasks by owner and project
- Tasks by project and status
- Tasks by due date

## 11. Performance Considerations

- Use `AsNoTracking()` for read-only queries.
- Use projections to DTOs instead of returning full entities.
- Avoid loading child collections unless needed.
- Add indexes for frequent filters.
- Keep write operations focused and explicit.

## 12. Scalability Considerations

The current system is intentionally simple, but the design supports future extensions:

- Add CQRS and MediatR if workflows grow.
- Add Redis for caching project summaries or lookup data.
- Add role-based admin operations.
- Add pagination and advanced filtering.
- Add background jobs for overdue task notifications.

## 13. Security Considerations

- Passwords are handled by ASP.NET Core Identity.
- JWT is used for stateless authentication.
- Protected endpoints require authorization.
- User-owned resources are filtered by OwnerId.
- No sensitive values should be committed to source control.
- Production secrets should be provided through environment variables.

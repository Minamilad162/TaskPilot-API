# Milestone 09 - Database Migrations

## Branch

`feature/database-migrations`

## Scope

This milestone adds the first database migration and a SQL schema script for the Project & Task Management API.

## Added Files

- `src/ProjectTaskManagement.Infrastructure/Migrations/20260522160000_InitialCreate.cs`
- `src/ProjectTaskManagement.Infrastructure/Migrations/ApplicationDbContextModelSnapshot.cs`
- `database/migrations/001_initial_schema.sql`

## Database Objects

The migration creates:

- ASP.NET Core Identity tables
- `Projects`
- `TaskItems`
- Required foreign keys
- Indexes for user-owned project and task queries
- EF Core migration history

## Main Tables

### Projects

| Column | Purpose |
|---|---|
| Id | Project primary key |
| Name | Project name |
| Description | Optional project details |
| CreatedAt | UTC creation date |
| OwnerId | Authenticated user ownership key |

### TaskItems

| Column | Purpose |
|---|---|
| Id | Task primary key |
| Title | Task title |
| Description | Optional task details |
| Status | Stored as integer enum |
| DueDate | Task due date |
| Priority | Stored as integer enum |
| ProjectId | Parent project key |
| OwnerId | Authenticated user ownership key |
| CreatedAt | UTC creation date |

## Apply Migration

```bash
dotnet ef database update --project src/ProjectTaskManagement.Infrastructure --startup-project src/ProjectTaskManagement.Api
```

## Notes

This migration is intentionally added after Authentication, Projects, and Tasks so the initial schema is complete and clean.

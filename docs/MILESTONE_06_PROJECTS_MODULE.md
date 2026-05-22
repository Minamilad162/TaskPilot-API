# Milestone 06 - Projects Module

## Branch

`feature/projects-module`

## Scope

This milestone implements the authenticated Projects module.

## Added

- `ProjectService`
- `ProjectsController`
- Dependency injection registration for `IProjectService`

## Endpoints

| Method | Route | Description |
| --- | --- | --- |
| POST | `/api/v1/projects` | Create project |
| GET | `/api/v1/projects` | Get current user's projects |
| GET | `/api/v1/projects/{id}` | Get current user's project details |
| PUT | `/api/v1/projects/{id}` | Update current user's project |
| DELETE | `/api/v1/projects/{id}` | Delete current user's project |

## Design Notes

- All project queries are filtered by `OwnerId` from the JWT-authenticated user.
- Read operations use `AsNoTracking()` to reduce EF Core tracking overhead.
- List and details queries use projection to return DTOs directly instead of loading unnecessary entity state.
- Project deletion relies on configured cascade delete for related task items.

# Milestone 07 - Tasks Module

## Branch

`feature/tasks-module`

## Objective

Implement the task management use cases required by the assessment.

This milestone completes the main business scenario by allowing authenticated users to manage tasks inside their own projects.

## Added Files

```text
src/ProjectTaskManagement.Application/Tasks/Services/ProjectTaskService.cs
src/ProjectTaskManagement.Api/Controllers/TasksController.cs
```

## Updated Files

```text
src/ProjectTaskManagement.Application/DependencyInjection.cs
README.md
```

## Endpoints

```text
POST   /api/v1/projects/{projectId}/tasks
GET    /api/v1/projects/{projectId}/tasks
PATCH  /api/v1/tasks/{id}/status
DELETE /api/v1/tasks/{id}
```

## Design Notes

- Tasks are always scoped to the authenticated user.
- A task can only be created inside a project owned by the current user.
- Task reads are filtered by both `ProjectId` and `OwnerId`.
- Status changes are isolated in a dedicated endpoint because the requirement is to update task status only.
- Entity Framework queries use `AsNoTracking` for read-only operations.
- DTOs return enum values as readable strings to keep API responses clear.

## Validation

The service validates that `DueDate` is provided before creating a task. The task title and description limits are enforced by DTO validation and domain rules.

## Manual Test Flow

1. Register a user.
2. Login and copy the JWT token.
3. Authorize Swagger with `Bearer {token}`.
4. Create a project.
5. Create tasks under the project.
6. Retrieve tasks by project.
7. Update task status.
8. Delete a task.

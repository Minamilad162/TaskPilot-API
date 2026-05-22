# Milestone 11 - Application Unit Tests

This milestone adds focused unit tests for the application layer.

## Scope

- Project service tests
- Project task service tests
- In-memory database setup for isolated test runs
- Test current-user implementation to verify ownership filtering

## Covered Cases

- Creating a project for the authenticated user
- Returning only projects owned by the current user
- Preventing access to projects owned by another user
- Updating an owned project
- Creating tasks inside owned projects
- Rejecting tasks without due dates
- Preventing task creation under another user's project
- Updating task status
- Returning tasks by owned project

## Notes

The tests use EF Core InMemory to keep them fast and isolated. They focus on application behavior and ownership rules instead of controller or database integration details.

# Milestone 10 - API Documentation & Submission Notes

## Branch

`docs/api-documentation`

## Scope

This milestone adds the API testing and submission deliverables required for review:

- Postman collection with grouped requests.
- README setup and API usage instructions.
- Submission notes for HR / technical reviewer.
- LocalDB and EF design-time setup alignment.

## Added / Updated Files

- `postman/TaskPilot.postman_collection.json`
- `SUBMISSION_NOTES.md`
- `docs/API_TESTING_GUIDE.md`
- `docs/MILESTONE_10_API_DOCUMENTATION.md`
- `README.md`
- `src/ProjectTaskManagement.Api/ProjectTaskManagement.Api.csproj`
- `src/ProjectTaskManagement.Api/appsettings.json`
- `src/ProjectTaskManagement.Api/appsettings.Development.json`
- `src/ProjectTaskManagement.Infrastructure/Persistence/ApplicationDbContextFactory.cs`

## Testing Flow

1. Run the API in Development mode.
2. Open Swagger at `/swagger`.
3. Register or login.
4. Copy the access token.
5. Authorize using `Bearer {token}`.
6. Create a project.
7. Create tasks inside the project.
8. Update task status.
9. Delete task / project if needed.

## Notes

The API root `/` redirects to Swagger to make local review easier.

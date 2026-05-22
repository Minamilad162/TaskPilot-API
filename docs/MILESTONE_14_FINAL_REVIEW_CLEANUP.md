# Milestone 14 - Final Review and Cleanup

## Branch

```text
chore/final-review-cleanup
```

## Goal

This milestone prepares the repository for final submission by reviewing the complete solution, adding a final testing checklist, and tightening the submission documentation.

## Scope

- Final testing checklist
- README final verification section
- Submission notes polish
- Clear run and validation commands
- Reminder to verify migrations, Swagger, Postman, and tests
- No schema changes
- No API behavior changes

## Verification Commands

Run from the repository root:

```bash
dotnet restore
dotnet build
dotnet test
```

Apply migrations:

```powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef database update --project src/ProjectTaskManagement.Infrastructure --startup-project src/ProjectTaskManagement.Api
```

Run the API:

```powershell
dotnet run --project src/ProjectTaskManagement.Api --urls "https://localhost:7001;http://localhost:5000"
```

Open Swagger:

```text
https://localhost:7001/swagger
```

## Final Manual Flow

1. Register a user.
2. Login and copy the JWT token.
3. Authorize Swagger using `Bearer {token}`.
4. Call `GET /api/v1/auth/me`.
5. Create a project.
6. Retrieve all projects.
7. Create a task under the project.
8. Update the task status.
9. Verify rows in SSMS under LocalDB.

## Deliverables Confirmed

- Clean Architecture source code
- README with setup instructions
- EF Core migrations
- SQL schema script
- Swagger documentation
- Postman collection
- Unit tests
- Docker support
- System design documentation
- Final testing checklist

## Notes

No CQRS, MediatR, or Redis implementation was added in order to keep the assessment simple, maintainable, and aligned with the task scope. The implemented bonus points are generic response wrapper, role-based authorization, Docker support, unit tests, and API v1 route convention.

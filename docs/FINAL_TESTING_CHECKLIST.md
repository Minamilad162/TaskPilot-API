# Final Testing Checklist

Use this checklist before submitting the repository.

## 1. Environment

Required tools:

- .NET 9 SDK
- SQL Server LocalDB or SQL Server Express
- Visual Studio 2022 or VS Code
- Git
- Postman optional
- Docker Desktop optional

Confirm SDK:

```bash
dotnet --list-sdks
```

Confirm LocalDB:

```powershell
sqllocaldb info MSSQLLocalDB
sqllocaldb start MSSQLLocalDB
```

## 2. Restore, Build, and Test

Run from the repository root:

```bash
dotnet restore
dotnet build
dotnet test
```

Expected result:

- Build succeeds with no errors.
- Test project completes successfully.

## 3. Database Migration

Run:

```powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet ef database update --project src/ProjectTaskManagement.Infrastructure --startup-project src/ProjectTaskManagement.Api
```

Expected database:

```text
TaskPilotDb
```

Expected tables:

```text
AspNetUsers
AspNetRoles
AspNetUserRoles
Projects
TaskItems
__EFMigrationsHistory
```

## 4. Run the API

```powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
dotnet run --project src/ProjectTaskManagement.Api --urls "https://localhost:7001;http://localhost:5000"
```

Expected terminal output:

```text
Hosting environment: Development
Now listening on: https://localhost:7001
Now listening on: http://localhost:5000
```

## 5. Health and Swagger

Open:

```text
https://localhost:7001/health
https://localhost:7001/swagger
```

Expected health response:

```json
{
  "status": "Healthy"
}
```

## 6. Authentication Test

Register:

```http
POST /api/v1/auth/register
```

Request:

```json
{
  "fullName": "Mina Milad",
  "email": "mina.final.test@example.com",
  "password": "Mina123$"
}
```

Login:

```http
POST /api/v1/auth/login
```

Request:

```json
{
  "email": "mina.final.test@example.com",
  "password": "Mina123$"
}
```

Expected:

- Success response.
- JWT access token returned.
- User role contains `User`.

## 7. Authorization Test

Authorize in Swagger using:

```text
Bearer YOUR_ACCESS_TOKEN
```

Call:

```http
GET /api/v1/auth/me
```

Expected:

- Current user id
- Email
- Full name
- Roles

## 8. Projects Flow

Create project:

```http
POST /api/v1/projects
```

```json
{
  "name": "Interview Backend Task",
  "description": "Clean Architecture backend assessment"
}
```

Then test:

```text
GET    /api/v1/projects
GET    /api/v1/projects/{id}
PUT    /api/v1/projects/{id}
DELETE /api/v1/projects/{id}
```

## 9. Tasks Flow

Create task:

```http
POST /api/v1/projects/{projectId}/tasks
```

```json
{
  "title": "Finish final review",
  "description": "Validate the full API before submission",
  "dueDate": "2026-05-30T12:00:00Z",
  "priority": "High"
}
```

Then test:

```text
GET    /api/v1/projects/{projectId}/tasks
PATCH  /api/v1/tasks/{id}/status
DELETE /api/v1/tasks/{id}
```

Status update body:

```json
{
  "status": "Done"
}
```

## 10. Validation and Error Handling

Test invalid create project request:

```json
{
  "description": "Missing name"
}
```

Expected response:

```json
{
  "success": false,
  "message": "Validation failed.",
  "data": null,
  "errors": []
}
```

Test unauthorized access by removing the Bearer token from a protected endpoint.

Expected:

```text
401 Unauthorized
```

## 11. Database Verification

Connect to SSMS using:

```text
(localdb)\MSSQLLocalDB
```

Run:

```sql
SELECT * FROM AspNetUsers;
SELECT * FROM Projects;
SELECT * FROM TaskItems;
```

Expected:

- Registered users exist.
- Created projects exist.
- Created task items exist.

## 12. Git Review

Run:

```bash
git status
git log --oneline --decorate --graph --all --max-count=30
```

Expected:

- Working tree is clean.
- Feature branches have meaningful commit names.
- `develop` contains all merged milestones.

## 13. Submission Files

Confirm these exist:

```text
README.md
SUBMISSION_NOTES.md
docs/SYSTEM_DESIGN.md
docs/PROJECT_STRUCTURE.md
docs/API_CONTRACT.md
docs/API_TESTING_GUIDE.md
docs/FINAL_TESTING_CHECKLIST.md
src/ProjectTaskManagement.Infrastructure/Migrations
postman/TaskPilot.postman_collection.json
database/migrations/001_initial_schema.sql
```

## 14. Optional Docker Check

```bash
docker compose up --build
```

Expected API URL:

```text
http://localhost:8080/swagger
```

Docker support is optional. The primary local development path remains LocalDB + `dotnet run`.

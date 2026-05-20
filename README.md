# ProjectTaskManagement API

Milestone 0: Initial clean architecture solution setup.

## Current Scope

This branch contains the initial repository structure only:

- API project
- Application project
- Domain project
- Infrastructure project
- Tests project
- Documentation folders
- Database migration folder
- Postman folder

Business code will be added incrementally in the next branches.

## Run

```bash
dotnet restore
dotnet build
```

```bash
dotnet run --project src/ProjectTaskManagement.Api
```

Swagger and authentication will be added in later milestones.

# Milestone 12 - Docker Support

## Branch

```text
chore/docker-support
```

## Objective

Add optional Docker support for running TaskPilot API and SQL Server in a local containerized environment.

## Added Files

```text
Dockerfile
docker-compose.yml
.dockerignore
```

## Services

### api

Builds and runs the ASP.NET Core Web API from the multi-stage Dockerfile.

### sqlserver

Runs SQL Server 2022 using a named Docker volume to persist database files between container restarts.

## Ports

```text
API:        http://localhost:8080
SQL Server: localhost,1433
```

## Database Migration

The API container does not run migrations automatically. This keeps startup behavior explicit and avoids hidden schema changes.

After starting SQL Server with Docker Compose, run migrations from the host machine using:

```powershell
$env:ASPNETCORE_ENVIRONMENT="Development"
$env:ConnectionStrings__DefaultConnection="Server=localhost,1433;Database=TaskPilotDb;User Id=sa;Password=TaskPilot_Strong_Password123!;TrustServerCertificate=True;Encrypt=False;MultipleActiveResultSets=true"

dotnet ef database update --project src/ProjectTaskManagement.Infrastructure --startup-project src/ProjectTaskManagement.Api
```

Then start or restart the API container.

## Run With Docker Compose

```bash
docker compose up --build
```

Open Swagger:

```text
http://localhost:8080/swagger
```

## Notes

Docker support is optional. The default local development path remains LocalDB for the simplest setup on Windows.

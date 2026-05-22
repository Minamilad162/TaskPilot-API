FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["ProjectTaskManagement.sln", "./"]
COPY ["src/ProjectTaskManagement.Api/ProjectTaskManagement.Api.csproj", "src/ProjectTaskManagement.Api/"]
COPY ["src/ProjectTaskManagement.Application/ProjectTaskManagement.Application.csproj", "src/ProjectTaskManagement.Application/"]
COPY ["src/ProjectTaskManagement.Domain/ProjectTaskManagement.Domain.csproj", "src/ProjectTaskManagement.Domain/"]
COPY ["src/ProjectTaskManagement.Infrastructure/ProjectTaskManagement.Infrastructure.csproj", "src/ProjectTaskManagement.Infrastructure/"]
COPY ["tests/ProjectTaskManagement.Application.Tests/ProjectTaskManagement.Application.Tests.csproj", "tests/ProjectTaskManagement.Application.Tests/"]

RUN dotnet restore "src/ProjectTaskManagement.Api/ProjectTaskManagement.Api.csproj"

COPY . .
RUN dotnet publish "src/ProjectTaskManagement.Api/ProjectTaskManagement.Api.csproj" \
    --configuration Release \
    --output /app/publish \
    --no-restore

FROM runtime AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ProjectTaskManagement.Api.dll"]

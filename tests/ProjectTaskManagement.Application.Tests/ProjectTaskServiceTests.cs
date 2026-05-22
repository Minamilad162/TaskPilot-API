using ProjectTaskManagement.Application.Common.Exceptions;
using ProjectTaskManagement.Application.Tasks.Dtos;
using ProjectTaskManagement.Application.Tasks.Services;
using ProjectTaskManagement.Application.Tests.Common;
using ProjectTaskManagement.Domain.Entities;
using ProjectTaskManagement.Domain.Enums;
using ProjectTask = ProjectTaskManagement.Domain.Entities.ProjectTask;

namespace ProjectTaskManagement.Application.Tests;

public sealed class ProjectTaskServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldCreateTaskInsideOwnedProject()
    {
        await using var dbContext = TestDbContextFactory.Create();
        var userId = Guid.NewGuid();
        var project = Project.Create(userId, "API project", null);

        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();

        var service = new ProjectTaskService(dbContext, new TestCurrentUserService(userId));
        var dueDate = DateTime.UtcNow.AddDays(2);

        var result = await service.CreateAsync(project.Id, new CreateTaskRequest(
            "Build task module",
            "Create task endpoints",
            dueDate,
            ProjectTaskPriority.High));

        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal("Build task module", result.Title);
        Assert.Equal("Todo", result.Status);
        Assert.Equal("High", result.Priority);
        Assert.Equal(project.Id, result.ProjectId);
        Assert.Single(dbContext.ProjectTasks);
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowValidationException_WhenDueDateIsMissing()
    {
        await using var dbContext = TestDbContextFactory.Create();
        var userId = Guid.NewGuid();
        var project = Project.Create(userId, "API project", null);

        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();

        var service = new ProjectTaskService(dbContext, new TestCurrentUserService(userId));

        await Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(
            project.Id,
            new CreateTaskRequest("Task without due date", null, null, ProjectTaskPriority.Medium)));
    }

    [Fact]
    public async Task CreateAsync_ShouldThrowNotFound_WhenProjectIsNotOwnedByCurrentUser()
    {
        await using var dbContext = TestDbContextFactory.Create();
        var currentUserId = Guid.NewGuid();
        var anotherUserId = Guid.NewGuid();
        var foreignProject = Project.Create(anotherUserId, "Foreign project", null);

        dbContext.Projects.Add(foreignProject);
        await dbContext.SaveChangesAsync();

        var service = new ProjectTaskService(dbContext, new TestCurrentUserService(currentUserId));

        await Assert.ThrowsAsync<NotFoundException>(() => service.CreateAsync(
            foreignProject.Id,
            new CreateTaskRequest("Blocked task", null, DateTime.UtcNow.AddDays(1), ProjectTaskPriority.Low)));
    }

    [Fact]
    public async Task UpdateStatusAsync_ShouldUpdateOwnedTaskStatus()
    {
        await using var dbContext = TestDbContextFactory.Create();
        var userId = Guid.NewGuid();
        var project = Project.Create(userId, "API project", null);
        var task = ProjectTask.Create(
            userId,
            project.Id,
            "Test status update",
            null,
            DateTime.UtcNow.AddDays(1),
            ProjectTaskPriority.Medium);

        dbContext.Projects.Add(project);
        dbContext.ProjectTasks.Add(task);
        await dbContext.SaveChangesAsync();

        var service = new ProjectTaskService(dbContext, new TestCurrentUserService(userId));

        var result = await service.UpdateStatusAsync(task.Id, new UpdateTaskStatusRequest(ProjectTaskStatus.Done));

        Assert.Equal("Done", result.Status);
        Assert.Equal(ProjectTaskStatus.Done, dbContext.ProjectTasks.Single().Status);
    }

    [Fact]
    public async Task GetByProjectAsync_ShouldReturnTasksForOwnedProjectOnly()
    {
        await using var dbContext = TestDbContextFactory.Create();
        var userId = Guid.NewGuid();
        var project = Project.Create(userId, "API project", null);
        var task = ProjectTask.Create(
            userId,
            project.Id,
            "Owned task",
            null,
            DateTime.UtcNow.AddDays(1),
            ProjectTaskPriority.Medium);

        dbContext.Projects.Add(project);
        dbContext.ProjectTasks.Add(task);
        await dbContext.SaveChangesAsync();

        var service = new ProjectTaskService(dbContext, new TestCurrentUserService(userId));

        var result = await service.GetByProjectAsync(project.Id);

        var returnedTask = Assert.Single(result);
        Assert.Equal("Owned task", returnedTask.Title);
    }
}

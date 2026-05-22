using ProjectTaskManagement.Application.Common.Exceptions;
using ProjectTaskManagement.Application.Projects.Dtos;
using ProjectTaskManagement.Application.Projects.Services;
using ProjectTaskManagement.Application.Tests.Common;
using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Application.Tests;

public sealed class ProjectServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldCreateProjectForCurrentUser()
    {
        await using var dbContext = TestDbContextFactory.Create();
        var userId = Guid.NewGuid();
        var service = new ProjectService(dbContext, new TestCurrentUserService(userId));

        var result = await service.CreateAsync(new CreateProjectRequest(" Interview API ", " Clean architecture task "));

        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal("Interview API", result.Name);
        Assert.Equal("Clean architecture task", result.Description);
        Assert.Single(dbContext.Projects);
        Assert.Equal(userId, dbContext.Projects.Single().OwnerId);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnOnlyProjectsOwnedByCurrentUser()
    {
        await using var dbContext = TestDbContextFactory.Create();
        var currentUserId = Guid.NewGuid();
        var anotherUserId = Guid.NewGuid();

        dbContext.Projects.Add(Project.Create(currentUserId, "Owned project", null));
        dbContext.Projects.Add(Project.Create(anotherUserId, "Foreign project", null));
        await dbContext.SaveChangesAsync();

        var service = new ProjectService(dbContext, new TestCurrentUserService(currentUserId));

        var result = await service.GetAllAsync();

        var project = Assert.Single(result);
        Assert.Equal("Owned project", project.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFound_WhenProjectDoesNotBelongToCurrentUser()
    {
        await using var dbContext = TestDbContextFactory.Create();
        var currentUserId = Guid.NewGuid();
        var anotherUserId = Guid.NewGuid();
        var foreignProject = Project.Create(anotherUserId, "Foreign project", null);

        dbContext.Projects.Add(foreignProject);
        await dbContext.SaveChangesAsync();

        var service = new ProjectService(dbContext, new TestCurrentUserService(currentUserId));

        await Assert.ThrowsAsync<NotFoundException>(() => service.GetByIdAsync(foreignProject.Id));
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateOnlyOwnedProject()
    {
        await using var dbContext = TestDbContextFactory.Create();
        var userId = Guid.NewGuid();
        var project = Project.Create(userId, "Old name", "Old description");

        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();

        var service = new ProjectService(dbContext, new TestCurrentUserService(userId));

        var result = await service.UpdateAsync(project.Id, new UpdateProjectRequest("New name", "New description"));

        Assert.Equal("New name", result.Name);
        Assert.Equal("New description", result.Description);
    }
}

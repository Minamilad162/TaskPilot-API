using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.Common.Abstractions;
using ProjectTaskManagement.Application.Common.Exceptions;
using ProjectTaskManagement.Application.Tasks.Dtos;
using ProjectTask = ProjectTaskManagement.Domain.Entities.ProjectTask;

namespace ProjectTaskManagement.Application.Tasks.Services;

public sealed class ProjectTaskService(
    IApplicationDbContext dbContext,
    ICurrentUserService currentUser) : IProjectTaskService
{
    public async Task<TaskDto> CreateAsync(Guid projectId, CreateTaskRequest request, CancellationToken cancellationToken = default)
    {
        if (request.DueDate is null)
        {
            throw new ValidationException("Task due date is required.");
        }

        var projectExists = await dbContext.Projects
            .AsNoTracking()
            .AnyAsync(project => project.Id == projectId && project.OwnerId == currentUser.UserId, cancellationToken);

        if (!projectExists)
        {
            throw new NotFoundException("Project was not found.");
        }

        var task = ProjectTask.Create(
            currentUser.UserId,
            projectId,
            request.Title,
            request.Description,
            request.DueDate.Value,
            request.Priority);

        await dbContext.ProjectTasks.AddAsync(task, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return ToDto(task);
    }

    public async Task<IReadOnlyCollection<TaskDto>> GetByProjectAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        var projectExists = await dbContext.Projects
            .AsNoTracking()
            .AnyAsync(project => project.Id == projectId && project.OwnerId == currentUser.UserId, cancellationToken);

        if (!projectExists)
        {
            throw new NotFoundException("Project was not found.");
        }

        return await dbContext.ProjectTasks
            .AsNoTracking()
            .Where(task => task.ProjectId == projectId && task.OwnerId == currentUser.UserId)
            .OrderBy(task => task.DueDate)
            .ThenByDescending(task => task.Priority)
            .Select(task => new TaskDto(
                task.Id,
                task.Title,
                task.Description,
                task.Status.ToString(),
                task.DueDate,
                task.Priority.ToString(),
                task.ProjectId,
                task.CreatedAt))
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskDto> UpdateStatusAsync(Guid id, UpdateTaskStatusRequest request, CancellationToken cancellationToken = default)
    {
        var task = await dbContext.ProjectTasks
            .FirstOrDefaultAsync(task => task.Id == id && task.OwnerId == currentUser.UserId, cancellationToken);

        if (task is null)
        {
            throw new NotFoundException("Task was not found.");
        }

        task.UpdateStatus(request.Status);
        await dbContext.SaveChangesAsync(cancellationToken);

        return ToDto(task);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var task = await dbContext.ProjectTasks
            .FirstOrDefaultAsync(task => task.Id == id && task.OwnerId == currentUser.UserId, cancellationToken);

        if (task is null)
        {
            throw new NotFoundException("Task was not found.");
        }

        dbContext.ProjectTasks.Remove(task);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private static TaskDto ToDto(ProjectTask task)
    {
        return new TaskDto(
            task.Id,
            task.Title,
            task.Description,
            task.Status.ToString(),
            task.DueDate,
            task.Priority.ToString(),
            task.ProjectId,
            task.CreatedAt);
    }
}

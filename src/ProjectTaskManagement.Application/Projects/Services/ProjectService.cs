using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.Common.Abstractions;
using ProjectTaskManagement.Application.Common.Exceptions;
using ProjectTaskManagement.Application.Projects.Dtos;
using ProjectTaskManagement.Application.Tasks.Dtos;
using ProjectTaskManagement.Domain.Entities;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.Projects.Services;

public sealed class ProjectService(
    IApplicationDbContext dbContext,
    ICurrentUserService currentUser) : IProjectService
{
    public async Task<ProjectDto> CreateAsync(CreateProjectRequest request, CancellationToken cancellationToken = default)
    {
        var project = Project.Create(currentUser.UserId, request.Name, request.Description);

        await dbContext.Projects.AddAsync(project, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ProjectDto(project.Id, project.Name, project.Description, project.CreatedAt, 0, 0);
    }

    public async Task<IReadOnlyCollection<ProjectDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Projects
            .AsNoTracking()
            .Where(project => project.OwnerId == currentUser.UserId)
            .OrderByDescending(project => project.CreatedAt)
            .Select(project => new ProjectDto(
                project.Id,
                project.Name,
                project.Description,
                project.CreatedAt,
                project.Tasks.Count,
                project.Tasks.Count(task => task.Status == ProjectTaskStatus.Done)))
            .ToListAsync(cancellationToken);
    }

    public async Task<ProjectDetailsDto> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await dbContext.Projects
            .AsNoTracking()
            .Where(project => project.Id == id && project.OwnerId == currentUser.UserId)
            .Select(project => new ProjectDetailsDto(
                project.Id,
                project.Name,
                project.Description,
                project.CreatedAt,
                project.Tasks
                    .OrderBy(task => task.DueDate)
                    .Select(task => new TaskDto(
                        task.Id,
                        task.Title,
                        task.Description,
                        task.Status,
                        task.DueDate,
                        task.Priority,
                        task.ProjectId,
                        task.CreatedAt))
                    .ToList()))
            .FirstOrDefaultAsync(cancellationToken);

        return project ?? throw new NotFoundException("Project was not found.");
    }

    public async Task<ProjectDto> UpdateAsync(Guid id, UpdateProjectRequest request, CancellationToken cancellationToken = default)
    {
        var project = await dbContext.Projects
            .FirstOrDefaultAsync(project => project.Id == id && project.OwnerId == currentUser.UserId, cancellationToken);

        if (project is null)
        {
            throw new NotFoundException("Project was not found.");
        }

        project.Update(request.Name, request.Description);
        await dbContext.SaveChangesAsync(cancellationToken);

        return await GetProjectDtoAsync(id, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await dbContext.Projects
            .FirstOrDefaultAsync(project => project.Id == id && project.OwnerId == currentUser.UserId, cancellationToken);

        if (project is null)
        {
            throw new NotFoundException("Project was not found.");
        }

        dbContext.Projects.Remove(project);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<ProjectDto> GetProjectDtoAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Projects
            .AsNoTracking()
            .Where(project => project.Id == id && project.OwnerId == currentUser.UserId)
            .Select(project => new ProjectDto(
                project.Id,
                project.Name,
                project.Description,
                project.CreatedAt,
                project.Tasks.Count,
                project.Tasks.Count(task => task.Status == ProjectTaskStatus.Done)))
            .FirstAsync(cancellationToken);
    }
}

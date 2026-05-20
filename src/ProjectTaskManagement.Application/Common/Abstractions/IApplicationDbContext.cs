using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Domain.Entities;
using ProjectTask = ProjectTaskManagement.Domain.Entities.ProjectTask;

namespace ProjectTaskManagement.Application.Common.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Project> Projects { get; }
    DbSet<ProjectTask> ProjectTasks { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

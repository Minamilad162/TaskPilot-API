using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.Common.Abstractions;
using ProjectTaskManagement.Domain.Entities;
using ProjectTask = ProjectTaskManagement.Domain.Entities.ProjectTask;

namespace ProjectTaskManagement.Infrastructure.Persistence;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<ProjectTask> ProjectTasks => Set<ProjectTask>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

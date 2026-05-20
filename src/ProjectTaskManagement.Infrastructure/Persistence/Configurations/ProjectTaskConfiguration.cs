using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectTaskManagement.Domain.Entities;
using ProjectTask = ProjectTaskManagement.Domain.Entities.ProjectTask;

namespace ProjectTaskManagement.Infrastructure.Persistence.Configurations;

public sealed class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
{
    public void Configure(EntityTypeBuilder<ProjectTask> builder)
    {
        builder.ToTable("TaskItems");

        builder.HasKey(task => task.Id);

        builder.Property(task => task.Title)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(task => task.Description)
            .HasMaxLength(1000);

        builder.Property(task => task.Status)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(task => task.Priority)
            .HasConversion<int>()
            .IsRequired();

        builder.Property(task => task.DueDate)
            .IsRequired();

        builder.Property(task => task.OwnerId)
            .IsRequired();

        builder.Property(task => task.CreatedAt)
            .IsRequired();

        builder.HasOne(task => task.Project)
            .WithMany(project => project.Tasks)
            .HasForeignKey(task => task.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(task => new { task.OwnerId, task.ProjectId });
        builder.HasIndex(task => new { task.ProjectId, task.Status });
        builder.HasIndex(task => task.DueDate);
    }
}

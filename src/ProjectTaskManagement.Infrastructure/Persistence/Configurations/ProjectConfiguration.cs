using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectTaskManagement.Domain.Entities;

namespace ProjectTaskManagement.Infrastructure.Persistence.Configurations;

public sealed class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");

        builder.HasKey(project => project.Id);

        builder.Property(project => project.Name)
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(project => project.Description)
            .HasMaxLength(1000);

        builder.Property(project => project.CreatedAt)
            .IsRequired();

        builder.Property(project => project.OwnerId)
            .IsRequired();

        builder.HasIndex(project => new { project.OwnerId, project.CreatedAt });

        builder.HasMany(project => project.Tasks)
            .WithOne(task => task.Project)
            .HasForeignKey(task => task.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(project => project.Tasks)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

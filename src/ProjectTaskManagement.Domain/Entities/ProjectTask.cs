using ProjectTaskManagement.Domain.Common;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Domain.Entities;

public sealed class ProjectTask : OwnedEntity
{
    private ProjectTask()
    {
    }

    private ProjectTask(
        Guid ownerId,
        Guid projectId,
        string title,
        string? description,
        DateTime dueDate,
        ProjectTaskPriority priority)
        : base(ownerId)
    {
        if (projectId == Guid.Empty)
        {
            throw new ArgumentException("Project id is required.", nameof(projectId));
        }

        ProjectId = projectId;
        Status = ProjectTaskStatus.Todo;
        SetDetails(title, description, dueDate, priority);
    }

    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public ProjectTaskStatus Status { get; private set; }
    public DateTime DueDate { get; private set; }
    public ProjectTaskPriority Priority { get; private set; }
    public Guid ProjectId { get; private set; }
    public Project? Project { get; private set; }

    public static ProjectTask Create(
        Guid ownerId,
        Guid projectId,
        string title,
        string? description,
        DateTime dueDate,
        ProjectTaskPriority priority)
    {
        return new ProjectTask(ownerId, projectId, title, description, dueDate, priority);
    }

    public void UpdateDetails(
        string title,
        string? description,
        DateTime dueDate,
        ProjectTaskPriority priority)
    {
        SetDetails(title, description, dueDate, priority);
    }

    public void UpdateStatus(ProjectTaskStatus status)
    {
        Status = status;
    }

    private void SetDetails(
        string title,
        string? description,
        DateTime dueDate,
        ProjectTaskPriority priority)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Task title is required.", nameof(title));
        }

        var normalizedTitle = title.Trim();

        if (normalizedTitle.Length > 150)
        {
            throw new ArgumentException("Task title cannot exceed 150 characters.", nameof(title));
        }

        Title = normalizedTitle;
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        DueDate = dueDate;
        Priority = priority;
    }
}

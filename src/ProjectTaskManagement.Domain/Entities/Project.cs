using ProjectTaskManagement.Domain.Common;

namespace ProjectTaskManagement.Domain.Entities;

public sealed class Project : OwnedEntity
{
    private readonly List<ProjectTask> _tasks = [];

    private Project()
    {
    }

    private Project(Guid ownerId, string name, string? description)
        : base(ownerId)
    {
        SetDetails(name, description);
    }

    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public IReadOnlyCollection<ProjectTask> Tasks => _tasks.AsReadOnly();

    public static Project Create(Guid ownerId, string name, string? description)
    {
        return new Project(ownerId, name, description);
    }

    public void Update(string name, string? description)
    {
        SetDetails(name, description);
    }

    private void SetDetails(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Project name is required.", nameof(name));
        }

        var normalizedName = name.Trim();

        if (normalizedName.Length > 120)
        {
            throw new ArgumentException("Project name cannot exceed 120 characters.", nameof(name));
        }

        Name = normalizedName;
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
    }
}

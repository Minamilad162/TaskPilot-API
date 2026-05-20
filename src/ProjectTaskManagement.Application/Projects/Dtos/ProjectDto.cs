namespace ProjectTaskManagement.Application.Projects.Dtos;

public sealed record ProjectDto(
    Guid Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    int TotalTasks,
    int CompletedTasks);

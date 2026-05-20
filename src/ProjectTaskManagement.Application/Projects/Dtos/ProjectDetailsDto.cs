using ProjectTaskManagement.Application.Tasks.Dtos;

namespace ProjectTaskManagement.Application.Projects.Dtos;

public sealed record ProjectDetailsDto(
    Guid Id,
    string Name,
    string? Description,
    DateTime CreatedAt,
    IReadOnlyCollection<TaskDto> Tasks);

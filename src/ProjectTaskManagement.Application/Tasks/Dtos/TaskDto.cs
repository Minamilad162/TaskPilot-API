namespace ProjectTaskManagement.Application.Tasks.Dtos;

public sealed record TaskDto(
    Guid Id,
    string Title,
    string? Description,
    string Status,
    DateTime? DueDate,
    string Priority,
    Guid ProjectId,
    DateTime CreatedAt);

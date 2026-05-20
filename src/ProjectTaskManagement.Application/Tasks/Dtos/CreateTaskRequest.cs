using System.ComponentModel.DataAnnotations;
using ProjectTaskManagement.Domain.Enums;

namespace ProjectTaskManagement.Application.Tasks.Dtos;

public sealed record CreateTaskRequest(
    [property: Required, StringLength(150, MinimumLength = 2)] string Title,
    [property: StringLength(1000)] string? Description,
    DateTime? DueDate,
    ProjectTaskPriority Priority = ProjectTaskPriority.Medium);

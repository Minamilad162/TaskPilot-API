using System.ComponentModel.DataAnnotations;

namespace ProjectTaskManagement.Application.Projects.Dtos;

public sealed record CreateProjectRequest(
    [Required, StringLength(120, MinimumLength = 2)] string Name,
    [StringLength(1000)] string? Description);

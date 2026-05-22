namespace ProjectTaskManagement.Application.Auth.Dtos;

public sealed record CurrentUserResponse(
    Guid UserId,
    string? Email,
    string? FullName,
    IReadOnlyCollection<string> Roles);

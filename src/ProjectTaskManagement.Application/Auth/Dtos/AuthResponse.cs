namespace ProjectTaskManagement.Application.Auth.Dtos;

public sealed record AuthResponse(
    Guid UserId,
    string FullName,
    string Email,
    string AccessToken,
    DateTime ExpiresAt);

namespace ProjectTaskManagement.Application.Common.Abstractions;

public interface ICurrentUserService
{
    Guid UserId { get; }
    string? Email { get; }
    string? FullName { get; }
    IReadOnlyCollection<string> Roles { get; }
    bool IsAuthenticated { get; }
}

using ProjectTaskManagement.Application.Common.Abstractions;
using ProjectTaskManagement.Application.Common.Security;

namespace ProjectTaskManagement.Application.Tests.Common;

internal sealed class TestCurrentUserService(Guid userId, string? email = null) : ICurrentUserService
{
    public Guid UserId { get; } = userId;
    public string? Email { get; } = email ?? "tester@taskpilot.local";
    public string? FullName => "Test User";
    public IReadOnlyCollection<string> Roles => [ApplicationRoles.User];
    public bool IsAuthenticated => true;
}

using ProjectTaskManagement.Application.Common.Abstractions;

namespace ProjectTaskManagement.Application.Tests.Common;

internal sealed class TestCurrentUserService(Guid userId, string? email = null) : ICurrentUserService
{
    public Guid UserId { get; } = userId;
    public string? Email { get; } = email ?? "tester@taskpilot.local";
    public bool IsAuthenticated => true;
}

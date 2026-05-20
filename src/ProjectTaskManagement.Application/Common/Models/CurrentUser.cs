namespace ProjectTaskManagement.Application.Common.Models;

public sealed record CurrentUser(Guid UserId, string Email, IReadOnlyCollection<string> Roles);

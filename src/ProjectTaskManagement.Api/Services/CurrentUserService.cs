using System.Security.Claims;
using ProjectTaskManagement.Application.Common.Abstractions;

namespace ProjectTaskManagement.Api.Services;

public sealed class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid UserId
    {
        get
        {
            var value = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Guid.TryParse(value, out var userId)
                ? userId
                : throw new UnauthorizedAccessException("User is not authenticated.");
        }
    }

    public string? Email => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email);

    public string? FullName => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Name);

    public IReadOnlyCollection<string> Roles => httpContextAccessor.HttpContext?.User
        .FindAll(ClaimTypes.Role)
        .Select(claim => claim.Value)
        .Distinct(StringComparer.OrdinalIgnoreCase)
        .ToArray() ?? [];

    public bool IsAuthenticated => httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated == true;
}

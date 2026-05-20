namespace ProjectTaskManagement.Application.Common.Abstractions;

public interface IJwtTokenService
{
    TokenResult CreateToken(Guid userId, string email, string fullName, IEnumerable<string> roles);
}

public sealed record TokenResult(string AccessToken, DateTime ExpiresAt);

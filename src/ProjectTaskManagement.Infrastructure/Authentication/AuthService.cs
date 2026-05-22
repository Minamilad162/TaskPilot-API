using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectTaskManagement.Application.Auth;
using ProjectTaskManagement.Application.Auth.Dtos;
using ProjectTaskManagement.Application.Common.Abstractions;
using ProjectTaskManagement.Application.Common.Exceptions;
using ProjectTaskManagement.Application.Common.Security;
using ProjectTaskManagement.Infrastructure.Identity;

namespace ProjectTaskManagement.Infrastructure.Authentication;

public sealed class AuthService(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole<Guid>> roleManager,
    IJwtTokenService jwtTokenService) : IAuthService
{
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var email = request.Email.Trim();
        var normalizedEmail = email.ToUpperInvariant();
        var exists = await userManager.Users.AnyAsync(user => user.NormalizedEmail == normalizedEmail, cancellationToken);

        if (exists)
        {
            throw new ConflictException("Email is already registered.");
        }

        await EnsureDefaultRoleAsync();

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            FullName = request.FullName.Trim(),
            Email = email,
            UserName = email
        };

        var createResult = await userManager.CreateAsync(user, request.Password);
        if (!createResult.Succeeded)
        {
            throw new ValidationException(createResult.Errors.Select(error => error.Description).ToList());
        }

        var roleResult = await userManager.AddToRoleAsync(user, ApplicationRoles.User);
        if (!roleResult.Succeeded)
        {
            throw new ValidationException(roleResult.Errors.Select(error => error.Description).ToList());
        }

        return await BuildAuthResponseAsync(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var email = request.Email.Trim();
        var user = await userManager.Users.FirstOrDefaultAsync(candidate => candidate.Email == email, cancellationToken);

        if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            throw new ValidationException("Invalid email or password.");
        }

        return await BuildAuthResponseAsync(user);
    }

    private async Task<AuthResponse> BuildAuthResponseAsync(ApplicationUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var token = jwtTokenService.CreateToken(user.Id, user.Email!, user.FullName, roles);

        return new AuthResponse(user.Id, user.FullName, user.Email!, token.AccessToken, token.ExpiresAt);
    }

    private async Task EnsureDefaultRoleAsync()
    {
        if (!await roleManager.RoleExistsAsync(ApplicationRoles.User))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(ApplicationRoles.User));
        }
    }
}

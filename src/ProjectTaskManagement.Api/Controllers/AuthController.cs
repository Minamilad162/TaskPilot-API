using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectTaskManagement.Application.Auth;
using ProjectTaskManagement.Application.Auth.Dtos;
using ProjectTaskManagement.Application.Common.Abstractions;
using ProjectTaskManagement.Application.Common.Models;
using ProjectTaskManagement.Application.Common.Security;

namespace ProjectTaskManagement.Api.Controllers;

[ApiController]
[Route("api/v1/auth")]
public sealed class AuthController(
    IAuthService authService,
    ICurrentUserService currentUserService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResponse<AuthResponse>), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var response = await authService.RegisterAsync(request, cancellationToken);
        return Created(string.Empty, ApiResponse<AuthResponse>.Ok(response, "User registered successfully."));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<AuthResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await authService.LoginAsync(request, cancellationToken);
        return Ok(ApiResponse<AuthResponse>.Ok(response, "User logged in successfully."));
    }

    [Authorize(Roles = ApplicationRoles.User)]
    [HttpGet("me")]
    [ProducesResponseType(typeof(ApiResponse<CurrentUserResponse>), StatusCodes.Status200OK)]
    public IActionResult Me()
    {
        var response = new CurrentUserResponse(
            currentUserService.UserId,
            currentUserService.Email,
            currentUserService.FullName,
            currentUserService.Roles);

        return Ok(ApiResponse<CurrentUserResponse>.Ok(response));
    }
}

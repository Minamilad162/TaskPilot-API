using System.ComponentModel.DataAnnotations;

namespace ProjectTaskManagement.Application.Auth.Dtos;

public sealed record LoginRequest(
    [property: Required, EmailAddress, StringLength(256)] string Email,
    [property: Required, StringLength(100, MinimumLength = 8)] string Password);

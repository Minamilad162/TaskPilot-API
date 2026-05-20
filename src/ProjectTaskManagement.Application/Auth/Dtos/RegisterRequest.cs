using System.ComponentModel.DataAnnotations;

namespace ProjectTaskManagement.Application.Auth.Dtos;

public sealed record RegisterRequest(
    [property: Required, StringLength(150, MinimumLength = 2)] string FullName,
    [property: Required, EmailAddress, StringLength(256)] string Email,
    [property: Required, StringLength(100, MinimumLength = 8)] string Password);

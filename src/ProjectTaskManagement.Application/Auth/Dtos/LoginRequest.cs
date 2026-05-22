using System.ComponentModel.DataAnnotations;

namespace ProjectTaskManagement.Application.Auth.Dtos;

public sealed record LoginRequest(
    [Required, EmailAddress, StringLength(256)] string Email,
    [Required, StringLength(100, MinimumLength = 8)] string Password);

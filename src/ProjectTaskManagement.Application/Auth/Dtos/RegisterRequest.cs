using System.ComponentModel.DataAnnotations;

namespace ProjectTaskManagement.Application.Auth.Dtos;

public sealed record RegisterRequest(
    [Required, StringLength(150, MinimumLength = 2)] string FullName,
    [Required, EmailAddress, StringLength(256)] string Email,
    [Required, StringLength(100, MinimumLength = 8)] string Password);

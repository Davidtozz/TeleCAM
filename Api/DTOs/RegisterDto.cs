namespace Api.DTOs;

public sealed record RegisterDto : LoginDto
{
    public required string Email { get; set; }
}
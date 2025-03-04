namespace Api.DTOs;

public sealed record Register : Login
{
    public required string Email { get; set; }
}
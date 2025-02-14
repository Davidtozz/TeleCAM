namespace Api.DTOs;

public record Login
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}
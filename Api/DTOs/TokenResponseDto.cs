namespace Api.DTOs;

#pragma warning disable CS8618

public record TokenResponseDto
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}

#pragma warning restore CS8618
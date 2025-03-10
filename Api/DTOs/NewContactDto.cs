namespace Api.DTOs;

public sealed record NewContactDto(
    string Name,
    string? Username,
    string? DisplayName, 
    string? Notes
);
namespace Api.DTOs;

public sealed record SendMessageDto(
    string Content,
    string RecipientUsername
);
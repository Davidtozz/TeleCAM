namespace Api.Endpoints.Message;
using Api.Endpoints;

public sealed partial class MessageEndpoint : IEndpoint
{
    public void Map(WebApplication app)
    {
        var group = app.MapGroup("/messages");
        group.MapPost("/send", SendMessage);
        group.MapGet("/history/{recipientId}", GetMessageHistory);
        group.MapGet("/genrandom/{count}", GenerateRandomMessages);
    }
}
namespace Api.Endpoints.Message;

using System.Security.Claims;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

public sealed partial class MessageEndpoint
{
    [Authorize("user")]
    public async Task<IResult> GetMessageHistory(
        [FromRoute] Guid recipientId,
        IMessageService messageService,
        ClaimsPrincipal jwt)
    {
        Guid userId = Guid.Parse(jwt.FindFirstValue("userId")!);

        var messages =  await messageService.GetMessageHistoryAsync(
            userId,
            recipientId
        );
        return Results.Ok(messages);
    }
}
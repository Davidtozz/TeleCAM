namespace Api.Endpoints.Message;

using System.Security.Claims;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

public sealed partial class MessageEndpoint
{
    [Authorize("user")]
    public async Task<ICollection<Message>>? GetMessageHistory(
        [FromRoute] Guid contactId,
        IMessageService messageService,
        ClaimsPrincipal jwt)
    {
        Guid userId = Guid.Parse(jwt.FindFirstValue("userId")!);

        return await messageService.GetMessageHistoryAsync(
            userId,
            contactId
        );
    }
}
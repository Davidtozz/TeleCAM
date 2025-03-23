namespace Api.Endpoints.Message;

using System.Security.Claims;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Api.DTOs;
using Microsoft.AspNetCore.Authorization;

public sealed partial class MessageEndpoint
{
    [Authorize("user")]
    public async Task<IResult> SendMessage(
        [FromBody] SendMessageDto formData,
        IMessageService messageService,
        IUserService<User> userService,
        IContactService contactService,
        ClaimsPrincipal jwt)
    {
        User? sender = await userService.GetUserByUsernameAsync(jwt.FindFirstValue("name")!);
        if (sender is null)
        {
            return Results.BadRequest("Invalid user");
        }

        User? recipient = await userService.GetUserByUsernameAsync(formData.RecipientUsername);
        if (recipient is null)
        {
            return Results.BadRequest("Invalid recipient");
        }

        await messageService.SendMessageAsync(sender, recipient, formData.Content);
        
        return Results.Ok();
    }
}
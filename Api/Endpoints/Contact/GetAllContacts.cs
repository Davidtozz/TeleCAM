namespace Api.Endpoints.Contact;

using System.Security.Claims;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Domain.Entities;

public sealed partial class ContactEndpoint {

    [Authorize("user")]
    private async Task<IResult> GetAllContacts(
        HttpContext context,
        IContactService _contactService,
        IMessageService _messageService,
        IUserService userService,
        ClaimsPrincipal jwt
    )
    {
        ICollection<Contact> contacts = await _contactService.GetContactsByUsernameAsync(jwt.FindFirstValue("name")!);

        await Parallel.ForEachAsync(contacts, async (contact, index) =>
        {
            if (contact.TargetUser is not null)
            {
                var userId = Guid.Parse(jwt.FindFirstValue("userId")!);
                contact.Messages = await _messageService.GetMessageHistoryAsync(userId, contact.TargetUser.Id);
            }
        });

        return Results.Ok(contacts);
    }
}
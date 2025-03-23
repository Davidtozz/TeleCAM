namespace Api.Endpoints.Contact;

using System.Security.Claims;
using Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Api.Data;

public partial class ContactEndpoint
{
    [Authorize("user")]
    private async Task<IResult> GetContactById(
        [FromRoute] Guid id,
        IContactService contactService,
        ClaimsPrincipal user
    )
    {
        Guid userId = Guid.Parse(user.FindFirstValue("userId")!);

        Contact? contact = await contactService.GetContactByIdAsync(
            userId: userId,
            contactId: id
        );
        if(contact is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(contact);
    }
}
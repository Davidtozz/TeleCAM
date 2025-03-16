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
        ClaimsPrincipal jwt
    )
    {
        ICollection<Contact> contacts = await _contactService.GetContactsByUsernameAsync(jwt.FindFirstValue("name")!);
        return Results.Ok(contacts);
    }
}
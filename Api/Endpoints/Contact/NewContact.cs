namespace Api.Endpoints.Contact;

using System.Security.Claims;
using Api.DTOs;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;

public partial class ContactEndpoint
{
    [Authorize("user")]
    private async Task<IResult> NewContact(
        [FromBody] NewContactDto formData,
        IContactService contactService,
        IUserService<User> userService,
        ILogger<ContactEndpoint> logger,
        ClaimsPrincipal jwt)
    {          
        User? user = await userService.GetUserByUsernameAsync(jwt.Identity?.Name!);
        if (user == null)
        {
            return Results.BadRequest("Invalid user");
        }
        await contactService.AddContactAsync(user, formData);
        logger.LogInformation("[user: @{userName}] New contact added: {ContactName}", user.Username,formData.Name);

        return Results.Created();
    }
}
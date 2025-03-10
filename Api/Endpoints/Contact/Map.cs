namespace Api.Endpoints.Contact;

using Microsoft.AspNetCore.Authorization;

[Authorize("user")]
public partial class ContactEndpoint : IEndpoint
{
    public void Map(WebApplication app)
    {
        var group = app.MapGroup("/contact");
        group.MapGet("/{id}", GetContact);
        group.MapPost("/new", NewContact);
    }    
}
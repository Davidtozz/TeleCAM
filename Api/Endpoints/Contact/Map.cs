namespace Api.Endpoints.Contact;

using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Api.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

[Authorize("user")]
public partial class ContactEndpoint : IEndpoint
{
    public void Map(WebApplication app)
    {
        var group = app.MapGroup("/contacts");
        group.MapGet("/{id}", GetContactById);
        group.MapGet("/all", GetAllContacts);
        group.MapPost("/new", NewContact);
    }
}
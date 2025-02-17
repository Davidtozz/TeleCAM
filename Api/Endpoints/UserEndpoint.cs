using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Api.Models;

namespace Api.Endpoints;

public class UserEndpoint : IEndpoint
{
    public void Map(WebApplication app)
    {
        var group = app.MapGroup("/user");

        group.MapGet("/{id}", GetUser);
        group.MapPost("/", CreateUser);
        group.MapDelete("/{id}", DeleteUser);
    }

    private async Task<IResult> DeleteUser([FromRoute] int id, IUserService userService)
    {
        var user = await userService.GetUserByIdAsync(id);
        if (user is null)
        {
            return Results.NotFound();
        }
        await userService.DeleteUserAsync(user.Id);
        return Results.Ok();
    }

    private async Task<IResult> CreateUser([FromBody] User user, IUserService userService)
    {
        try {
            await userService.CreateUserAsync(user);
        } catch {
            return Results.BadRequest();
        }
        return Results.Created();
    }

    private async Task<IResult> GetUser([FromRoute] int id, IUserService userService)
    {
        var user = await userService.GetUserByIdAsync(id);
        if (user is null)
        {
            return Results.NotFound();
        }

        return Results.Ok(user);
    }
}
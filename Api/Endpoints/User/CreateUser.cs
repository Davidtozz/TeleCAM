namespace Api.Endpoints.User;

using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Api.Models;

public partial class UserEndpoint 
{
    private async Task<IResult> CreateUser([FromBody] User user, IUserService userService)
    {
        try {
            await userService.CreateUserAsync(user);
        } catch {
            return Results.BadRequest();
        }
        return Results.Created();
    }
}
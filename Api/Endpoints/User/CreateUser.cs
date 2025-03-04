namespace Api.Endpoints.User;

using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Api.DTOs;

public partial class UserEndpoint 
{
    private async Task<IResult> CreateUser([FromBody] Register registeringUser, IUserService userService)
    {
        try {

            if (await userService.GetUserByUsernameAsync(registeringUser.Username) != null) {
                return Results.BadRequest();
            }

            var user = new User() {
                Username = registeringUser.Username
            };
            await userService.CreateUserAsync(user);
        } catch {
            return Results.BadRequest();
        }
        return Results.Created();
    }
}
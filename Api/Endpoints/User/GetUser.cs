namespace Api.Endpoints.User;

using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Api.Models;

public partial class UserEndpoint 
{
    private async Task<IResult> GetUser([FromRoute] int id, IUserService userService)
    {
        User? user = await userService.GetUserByIdAsync(id);
        return (user is null) ? Results.NotFound() : Results.Ok(user);
    }
}
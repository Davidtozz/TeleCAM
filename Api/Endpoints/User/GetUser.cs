using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Api.Endpoints.User;

using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

public partial class UserEndpoint 
{
    [Authorize("user")]
    private async Task<IResult>  GetUser(
        [FromRoute] Guid id, 
        IUserService userService
        )
    {
        User? user = await userService.GetUserByIdAsync(id);
        return (user is null) ? Results.NotFound() : Results.Ok(user);
    }
}
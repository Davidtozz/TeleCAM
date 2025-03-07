using Microsoft.IdentityModel.Tokens;

namespace Api.Endpoints.User;

using Api.DTOs;
using Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;

public partial class UserEndpoint 
{

    private async Task<IResult> Login(
        [FromBody] LoginDto formData,
        IAuthService authService,
        IPasswordHasher<User> passwordHasher,
        HttpContext ctx)
    {
        var jwt = await authService.LoginAsync(formData);
        if (jwt.IsNullOrEmpty()) {
            return Results.BadRequest("Username or password is incorrect");
        }
        
        ctx.Response.Cookies.Append("x-auth-token", jwt, new CookieOptions()
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = false // set to true in production
        });
        
        return Results.Ok(new{jwt});
    }
}
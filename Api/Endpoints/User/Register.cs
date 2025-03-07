namespace Api.Endpoints.User;

using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Api.DTOs;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public partial class UserEndpoint 
{
    private async Task<IResult> Register(
        [FromBody] RegisterDto formData,
        IAuthService authService,
        IPasswordHasher<User> passwordHasher,
        HttpContext ctx)
    {
        var jwt = await authService.RegisterAsync(formData);
        if(jwt.IsNullOrEmpty()) {
            // User already exists
            return Results.BadRequest();
        }   
        
        ctx.Response.Cookies.Append("x-auth-token", jwt, new CookieOptions {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = false
        });

        return Results.Ok(new{formData.Username, jwt});
    }
}
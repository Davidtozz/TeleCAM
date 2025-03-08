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
        ITokenService _tokenService,
        HttpContext ctx)
    {
        var tokenResponse = await authService.RegisterAsync(formData);
        if(tokenResponse is null) {
            return Results.BadRequest(new { message = "Registration failed. Username may already exist."});
        }   

        
        ctx.Response.Cookies.Append("x-auth-token", tokenResponse.AccessToken, new CookieOptions {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = false
        });
        
        ctx.Response.Cookies.Append("x-refresh-token", tokenResponse.RefreshToken, new CookieOptions {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = false
        });

        return Results.Ok(new {
            Username = formData.Username, 
            Tokens = tokenResponse
        });
    }
}
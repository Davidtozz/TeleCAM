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
        ITokenService _tokenService,
        HttpContext ctx)
    {
        TokenResponseDto? tokenResponse = await authService.LoginAsync(formData);
        if (tokenResponse is null) {
            return Results.BadRequest(new {
                message = "Username or password is incorrect."
            });
        }
        
        ctx.Response.Cookies.Append("x-auth-token", tokenResponse.AccessToken, new CookieOptions()
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = false // set to true in production
        });

        ctx.Response.Cookies.Append("x-refresh-token", tokenResponse.RefreshToken, new CookieOptions()
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = false // set to true in production
        });
        
        return Results.Ok(tokenResponse);
    }
}
using Microsoft.IdentityModel.Tokens;

namespace Api.Endpoints.User;

using Api.DTOs;
using Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Api.Extensions;

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
        
        ctx.SetTokenCookies(tokenResponse.AccessToken, tokenResponse.RefreshToken);
        
        return Results.Ok(tokenResponse);
    }
}
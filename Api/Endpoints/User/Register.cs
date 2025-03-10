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
using Api.Extensions;

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

        
        ctx.SetTokenCookies(tokenResponse.AccessToken, tokenResponse.RefreshToken);

        return Results.Ok(new {
            Username = formData.Username, 
            Tokens = tokenResponse
        });
    }
}
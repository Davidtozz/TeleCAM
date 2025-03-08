namespace Api.Endpoints.User;

using Api.Data;
using Api.Services;
using Microsoft.AspNetCore.Authorization;

public partial class UserEndpoint
{
    [AllowAnonymous]
    public async Task<IResult> Refresh(
        HttpContext ctx,
        IAuthService authService) 
    {
        var refreshToken = ctx.Request.Cookies["x-refresh-token"];
        if (string.IsNullOrEmpty(refreshToken)) {
            return Results.BadRequest();
        }

        var tokenResponse = await authService.RefreshTokenAsync(refreshToken);
        if (tokenResponse is null) {
            return Results.BadRequest();
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
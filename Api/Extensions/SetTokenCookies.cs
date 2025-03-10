namespace Api.Extensions;

public static class TokenCookies
{
    public static void SetTokenCookies(this HttpContext ctx, string accessToken, string refreshToken)
    {
        ctx.Response.Cookies.Append("x-auth-token", accessToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = false,
            Expires = DateTime.UtcNow.AddMinutes(15)
        });

        ctx.Response.Cookies.Append("x-refresh-token", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = false,
            Expires = DateTime.UtcNow.AddMinutes(15)
        });
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Api.Data;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Api.Services;


public sealed class TokenService : ITokenService
{

    private readonly TelecamContext _context;
    private readonly IConfiguration _configuration;

    public TokenService(TelecamContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom to generate the token.</param>
    /// <returns>The generated JWT token.</returns>
    public string GenerateAccessToken(User user) {
        var claims = new Claim[]{
            new ("userId", user.Id.ToString()),
            new ("name", user.Username),
            new("email", user.Email),
        };

        //! dev only
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken(User user)
    {
        var refreshToken = new RefreshToken()
        {
            Token = GenerateRefreshToken(),
            ExpiresOnUtc = DateTime.UtcNow.AddDays(7),  
            User = user
        };

        _context.RefreshTokens.Add(refreshToken);
        _context.SaveChanges();

        return refreshToken;
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }



    public async Task<RefreshToken?> ValidateRefreshTokenAsync(string token)
    {
        if(string.IsNullOrEmpty(token)) {
            return null;
        }

        return await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == token);
    }
}






public interface ITokenService
{
    public string GenerateAccessToken(User user);
    public RefreshToken GenerateRefreshToken(User user);
    public string GenerateRefreshToken();
    Task<RefreshToken?> ValidateRefreshTokenAsync(string token);
}
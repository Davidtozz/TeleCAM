namespace Api.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Data;
using Api.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public sealed class AuthService : IAuthService
{
    private readonly IUserService<User> _userService;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    public AuthService(IUserService<User> userService, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
    {
        _userService = userService;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    /// <summary>
    /// Attempts to log the user and returns a JWT token if successful. 
    /// </summary>
    /// <param name="formData">Username and password provided by the user.</param>
    /// <returns>The jwt token or an empty string</returns>
    public async Task<string> LoginAsync(LoginDto formData)
    {
        User? user = await _userService.GetUserByUsernameAsync(formData.Username);
        if(user is null) {
            return string.Empty;
        }

        if(_passwordHasher.VerifyHashedPassword(
            user, 
            user.PasswordHash, 
            formData.Password) == PasswordVerificationResult.Failed
        ) 
        {
            return string.Empty;
        }
        //todo: refresh token
        return GenerateJwtToken(user);
    }

    /// <summary>
    /// Registers a new user and returns a JWT token if successful.
    /// </summary>
    /// <param name="formData">User registration data.</param>
    /// <returns>The JWT token or an empty string if registration fails.</returns>
    public async Task<string> RegisterAsync(RegisterDto formData)
    {
        if(await _userService.GetUserByUsernameAsync(formData.Username) is not null) {
            return string.Empty;
        }

        User user = new() {
            Username = formData.Username,
            Email = formData.Email,
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, formData.Password);

        await _userService.CreateUserAsync(user);
        return GenerateJwtToken(user);
    }

    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom to generate the token.</param>
    /// <returns>The generated JWT token.</returns>
    private string GenerateJwtToken(User user) {
        var claims = new Claim[]{
            new (JwtRegisteredClaimNames.Sub, user.Username),
            new(JwtRegisteredClaimNames.Email, user.Email),
        };

        //! dev only
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

interface IAuthService 
{
    Task<string> RegisterAsync(RegisterDto formData);
    Task<string> LoginAsync(LoginDto formData);
}
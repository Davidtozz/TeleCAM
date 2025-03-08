namespace Api.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;

    public AuthService(
        IUserService<User> userService, 
        IPasswordHasher<User> passwordHasher, 
        ITokenService tokenService,
        IConfiguration configuration)
    {
        _userService = userService;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
        _tokenService = tokenService;
    }

    /// <summary>
    /// Attempts to log the user and returns a JWT token if successful. 
    /// </summary>
    /// <param name="formData">Username and password provided by the user.</param>
    /// <returns>The jwt token or an empty string</returns>
    public async Task<TokenResponseDto?> LoginAsync(LoginDto formData)
    {
        User? user = await _userService.GetUserByUsernameAsync(formData.Username);
        if(user is null) {
            return null;
        }

        var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(
            user, user.PasswordHash, formData.Password);

        if(passwordVerificationResult == PasswordVerificationResult.Failed) 
        {
            return null; // invalid password
        }

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken(user);

        return new TokenResponseDto 
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
    }

    public async Task<TokenResponseDto?> RefreshTokenAsync(string refreshToken)
    {
        var savedRefreshToken = await _tokenService.ValidateRefreshTokenAsync(refreshToken);

        if(savedRefreshToken is null){
            return null;
        }

        if(savedRefreshToken.ExpiresOnUtc < DateTime.UtcNow) {
            await _tokenService.RemoveRefreshTokenAsync(savedRefreshToken);
            return null;
        }

        var user = savedRefreshToken.User;

        var accessToken = _tokenService.GenerateAccessToken(user);
        await _tokenService.RemoveRefreshTokenAsync(savedRefreshToken);
        var newRefreshToken = _tokenService.GenerateRefreshToken(user);

        return new TokenResponseDto {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken.Token
        };
    }

    /// <summary>
    /// Registers a new user and returns a JWT token if successful.
    /// </summary>
    /// <param name="formData">User registration data.</param>
    /// <returns>The JWT token or an empty string if registration fails.</returns>
    public async Task<TokenResponseDto?> RegisterAsync(RegisterDto formData)
    {
        if(await _userService.GetUserByUsernameAsync(formData.Username) is not null) {
            return null;
        }

        User user = new() {
            Username = formData.Username,
            Email = formData.Email,
            Id = Guid.NewGuid()
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, formData.Password);
        User? newUser = await _userService.CreateUserAsync(user);
        if(newUser is null) {
            return null;
        }

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken(user);

        return new TokenResponseDto {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
    }
}
public interface IAuthService 
{
    Task<TokenResponseDto?> RegisterAsync(RegisterDto formData);
    Task<TokenResponseDto?> LoginAsync(LoginDto formData);
    Task<TokenResponseDto?> RefreshTokenAsync(string refreshToken);
}
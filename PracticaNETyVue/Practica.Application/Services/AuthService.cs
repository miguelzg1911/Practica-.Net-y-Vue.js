using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Practica.Application.DTOs.Auth;
using Practica.Application.Interfaces;
using Practica.Domain.Entities;
using Practica.Domain.Enum;
using Practica.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Practica.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<User> _passwordHasher;

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterUserDto dto)
    {
        var existing = await _userRepository.GetByEmailAsync(dto.Email);
        if (existing != null)
            throw new Exception("Email already registered");

        var allUsers = await _userRepository.GetAllAsync();
        var usersCount = allUsers.Count(); 

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            Role = (usersCount == 0) ? Role.Admin : dto.Role
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        GenerateRefreshToken(user);

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        return CreateAuthResponse(user);
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email)
            ?? throw new Exception("Invalid credentials");

        var result = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            dto.Password
        );

        if (result == PasswordVerificationResult.Failed)
            throw new Exception("Invalid credentials");

        GenerateRefreshToken(user);
        await _userRepository.SaveChangesAsync();

        return CreateAuthResponse(user);
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto)
    {
        var user = (await _userRepository.GetAllAsync())
            .FirstOrDefault(u =>
                u.RefreshToken == dto.RefreshToken &&
                u.RefreshTokenExpiresAt > DateTime.UtcNow);

        if (user == null)
            throw new Exception("Invalid refresh token");

        GenerateRefreshToken(user);
        await _userRepository.SaveChangesAsync();

        return CreateAuthResponse(user);
    }

    public async Task LogoutAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) return;

        user.RefreshToken = null;
        user.RefreshTokenExpiresAt = null;

        await _userRepository.SaveChangesAsync();
    }

    private AuthResponseDto CreateAuthResponse(User user)
    {
        return new AuthResponseDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Role = user.Role.ToString(),
            AccessToken = GenerateJwtToken(user),
            RefreshToken = user.RefreshToken!
        };
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
        );

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private void GenerateRefreshToken(User user)
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        user.RefreshToken = Convert.ToBase64String(randomBytes);
        user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);
    }
}

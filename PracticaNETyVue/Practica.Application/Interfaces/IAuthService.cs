using Practica.Application.DTOs.Auth;

namespace Practica.Application.Interfaces;

public interface IAuthService
{
    Task<UserResponseDto> RegisterAsync(RegisterUserDto dto);
    Task<UserResponseDto> LoginAsync(LoginDto dto);
}
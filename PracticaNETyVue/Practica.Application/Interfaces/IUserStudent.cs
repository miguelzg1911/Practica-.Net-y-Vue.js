using Practica.Application.DTOs.Auth;

namespace Practica.Application.Interfaces;

public interface IUserStudent
{
    Task<UserResponseDto> RegisterAsync(RegisterUserDto dto);
    Task<UserResponseDto> LoginAsync(LoginDto dto);
}
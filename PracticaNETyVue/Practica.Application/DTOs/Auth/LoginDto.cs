namespace Practica.Application.DTOs.Auth;

public class LoginDto
{
    public string Email { get; set; } 
    public string PasswordHash { get; set; }
}
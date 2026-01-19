using Practica.Application.DTOs.Teacher;
using Practica.Application.Interfaces;
using Practica.Domain.Entities;
using Practica.Domain.Enum;
using Practica.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Practica.Application.Services;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher;

    public TeacherService(ITeacherRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _passwordHasher = new PasswordHasher<User>();
    }

    public async Task<TeacherResponseDto> CreateAsync(TeacherInputDto dto)
    {
        var user = new User
        {
            Username = dto.Name.Replace(" ", "").ToLower(), 
            Email = dto.Email,
            Role = Role.Teacher
        };
        
        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Document);

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        var teacher = new Teacher
        {
            Id = user.Id,
            Name = dto.Name,
            Email = dto.Email,
            Subject = dto.Subject,
            Document = dto.Document
        };
        
        await _repository.AddAsync(teacher);
        await _repository.SaveChangesAsync();

        return new TeacherResponseDto
        {
            Id = teacher.Id,
            Name = teacher.Name,
            Email = teacher.Email,
            Subject = teacher.Subject,
            Document = teacher.Document
        };
    }

    public async Task DeleteAsync(int id)
    {
        var teacher = await _repository.GetByIdAsync(id);
        if (teacher == null) return;

        await _repository.DeleteAsync(teacher);

        var user = await _userRepository.GetByIdAsync(id);
        if (user != null)
        {
            await _userRepository.DeleteAsync(user);
        }

        await _repository.SaveChangesAsync();
        await _userRepository.SaveChangesAsync();
    }

    public async Task<IEnumerable<TeacherResponseDto>> GetAllAsync()
    {
        var teachers = await _repository.GetAllAsync();
        return teachers.Select(s => new TeacherResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Email = s.Email,
            Subject = s.Subject,
            Document = s.Document
        });
    }

    public async Task UpdateAsync(int id, TeacherInputDto dto)
    {
        var teacher = await _repository.GetByIdAsync(id);
        if (teacher == null) return;

        teacher.Name = dto.Name;
        teacher.Email = dto.Email;
        teacher.Subject = dto.Subject;
        teacher.Document = dto.Document;

        await _repository.UpdateAsync(teacher);
        await _repository.SaveChangesAsync();

        var user = await _userRepository.GetByIdAsync(id);
        if(user != null) {
            user.Email = dto.Email;
            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();
        }
    }

    public async Task<TeacherResponseDto?> GetByIdAsync(int id)
    {
        var teacher = await _repository.GetByIdAsync(id);
        if (teacher == null) return null;

        return new TeacherResponseDto
        {
            Id = teacher.Id,
            Name = teacher.Name,
            Email = teacher.Email,
            Subject = teacher.Subject,
            Document = teacher.Document,
        };
    }
}
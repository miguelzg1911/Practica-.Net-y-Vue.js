using Practica.Application.DTOs.Student;
using Practica.Application.Interfaces;
using Practica.Domain.Entities;
using Practica.Domain.Enum;
using Practica.Domain.Interfaces;

namespace Practica.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly Microsoft.AspNetCore.Identity.PasswordHasher<User> _passwordHasher;

    public StudentService(IStudentRepository repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _passwordHasher = new Microsoft.AspNetCore.Identity.PasswordHasher<User>();
    }
    
    public async Task<IEnumerable<StudentResponseDto>> GetAllAsync()
    {
        var students = await _repository.GetAllAsync();

        return students.Select(s => new StudentResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Document = s.Document,
            Email = s.User?.Email ?? "Sin Email"
        });
    }

    public async Task<StudentResponseDto?> GetByIdAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        
        if (student == null)
            throw new Exception("Student not found");

        return new StudentResponseDto
        {
            Id = student.Id,
            Name = student.Name,
            Document = student.Document,
            Email = student.User?.Email ?? "Sin Email"
        };
    }

    public async Task<StudentResponseDto> CreateAsync(StudentInputDto dto)
    {
        var user = new User
        {
            Username = dto.Name.Replace(" ", "").ToLower() + new Random().Next(10, 99), 
            Email = dto.Email,
            Role = Role.Student
        };

        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Document);
    
        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        var student = new Student
        {
            Id = user.Id,
            Name = dto.Name,
            Document = dto.Document
        };
    
        await _repository.AddAsync(student);
        await _repository.SaveChangesAsync();

        return new StudentResponseDto
        {
            Id = student.Id,
            Name = student.Name,
            Document = student.Document,
            Email = user.Email
        };
    }

    public async Task UpdateAsync(int id, StudentInputDto dto)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null) return;
        
        student.Name = dto.Name;
        student.Document = dto.Document;
        
        await _repository.UpdateAsync(student);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null) throw new Exception("Student not found");

        var user = await _userRepository.GetByIdAsync(id);

        await _repository.DeleteAsync(student);
        if (user != null)
        {
            await _userRepository.DeleteAsync(user);
        }

        await _userRepository.SaveChangesAsync(); 
    }
}
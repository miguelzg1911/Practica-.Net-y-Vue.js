using Practica.Application.DTOs.Teacher;
using Practica.Application.Interfaces;
using Practica.Domain.Entities;
using Practica.Domain.Interfaces;

namespace Practica.Application.Services;

public class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _repository;

    public TeacherService(ITeacherRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<TeacherResponseDto>> GetAllAsync()
    {
        var teacher =  await _repository.GetAllAsync();

        return teacher.Select(s => new TeacherResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Email =  s.Email,
            Subject = s.Subject,
            Document = s.Document
        });
    }

    public async Task<TeacherResponseDto?> GetByIdAsync(int id)
    {
        var teacherExists = await _repository.GetByIdAsync(id);
        if (teacherExists == null)
            return null;

        return new TeacherResponseDto
        {
            Id = teacherExists.Id,
            Name = teacherExists.Name,
            Email = teacherExists.Email,
            Subject = teacherExists.Subject,
            Document = teacherExists.Document,
        };
    }

    public async Task<TeacherResponseDto> CreateAsync(TeacherInputDto dto)
    {
        var teacher = new Teacher
        {
            Name = dto.Name,
            Email = dto.Email,
            Subject = dto.Subject,
            Document = dto.Document,
        };
        
        await _repository.AddAsync(teacher);
        await _repository.SaveChangesAsync();

        return new TeacherResponseDto
        {
            Id = teacher.Id,
            Name = teacher.Name,
            Email = teacher.Email,
            Subject = teacher.Subject,
            Document =  teacher.Document
        };
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
    }

    public async Task DeleteAsync(int id)
    {
        var teacher = await _repository.GetByIdAsync(id);
        if (teacher == null) return;
        
        await _repository.DeleteAsync(teacher);
        await _repository.SaveChangesAsync();
    }
}
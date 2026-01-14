using Practica.Application.DTOs.Student;
using Practica.Application.Interfaces;
using Practica.Domain.Entities;
using Practica.Domain.Interfaces;

namespace Practica.Application.Services;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repository;

    public StudentService(IStudentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<StudentDto>> GetAllAsync()
    {
        var students = await _repository.GetAllAsync();

        return students.Select(s => new StudentDto
        {
            Id = s.Id,
            Name = s.Name,
            Document = s.Document
        });
    }

    public async Task<StudentDto?> GetByIdAsync(int id)
    {
        var student = await _repository.GetByIdAsync(id);
        if (student == null) return null;

        return new StudentDto
        {
            Id = student.Id,
            Name = student.Name,
            Document = student.Document
        };
    }

    public async Task CreateAsync(CreateStudentDto dto)
    {
        var student = new Student
        {
            Name = dto.Name,
            Document = dto.Document
        };

        await _repository.AddAsync(student);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, UpdateStudentDto dto)
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
        if (student == null) return;

        await _repository.DeleteAsync(student);
        await _repository.SaveChangesAsync();
    }
}
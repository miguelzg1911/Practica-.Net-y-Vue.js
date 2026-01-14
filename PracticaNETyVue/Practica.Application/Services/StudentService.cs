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
    
    public async Task<IEnumerable<StudentResponseDto>> GetAllAsync()
    {
        var students = await _repository.GetAllAsync();

        return students.Select(s => new StudentResponseDto
        {
            Id = s.Id,
            Name = s.Name,
            Document = s.Document
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
            Document = student.Document
        };
    }

    public async Task<StudentResponseDto> CreateAsync(StudentInputDto dto)
    {
        var student = new Student
        {
            Name = dto.Name,
            Document = dto.Document
        };
        
        await _repository.AddAsync(student);
        await _repository.SaveChangesAsync();

        return new StudentResponseDto
        {
            Id = student.Id,
            Name = student.Name,
            Document = student.Document
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
        
        if (student == null)
            throw new Exception("Student not found");
        
        await _repository.DeleteAsync(student);
        await _repository.SaveChangesAsync();
    }
}
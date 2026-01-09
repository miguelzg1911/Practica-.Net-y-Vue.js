using Microsoft.EntityFrameworkCore;
using Practica.Domain.Entities;
using Practica.Domain.Interfaces;
using Practica.Infrastructure.Data;

namespace Practica.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _context;

    public StudentRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Student>> GetAllAsync()
    {
        return await _context.Students.ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context.Students.FindAsync(id);
    }
    
    public async Task<Student?> GetCourseByNameAsync(string courseName)
    {
        return await _context.Students.FirstOrDefaultAsync(c => c.Name == courseName);
    }

    public async Task AddAsync(Student student)
    {
        _context.Students.Add(student);
    }

    public async Task UpdateAsync(Student student)
    {
        _context.Students.Update(student);
    }

    public async Task DeleteAsync(int id)
    {
        var existingStudent = await _context.Students.FindAsync(id);
        _context.Students.Remove(existingStudent);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
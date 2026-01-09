using Microsoft.EntityFrameworkCore;
using Practica.Domain.Entities;
using Practica.Domain.Interfaces;
using Practica.Infrastructure.Data;

namespace Practica.Infrastructure.Repositories;

public class TeacherRepository : ITeacherRepository
{
    private readonly AppDbContext _context;

    public TeacherRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Teacher>> GetAllAsync()
    {
        return await _context.Teachers.ToListAsync();
    }

    public async Task<Teacher?> GetByIdAsync(int id)
    {
        return await _context.Teachers.FindAsync(id);
    }

    public async Task<Teacher?> GetByEmailAsync(string email)
    {
        return await _context.Teachers.FirstOrDefaultAsync(e => e.Email == email);
    }

    public async Task AddAsync(Teacher teacher)
    {
        _context.Teachers.Add(teacher);
    }

    public async Task UpdateAsync(int id, Teacher teacher)
    {
        _context.Teachers.Update(teacher);
    }

    public async Task DeleteAsync(int id, Teacher teacher)
    {
        var existingTeacher = await GetByIdAsync(id);
        _context.Teachers.Remove(existingTeacher);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
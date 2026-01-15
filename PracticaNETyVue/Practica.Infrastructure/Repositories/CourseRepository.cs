using Microsoft.EntityFrameworkCore;
using Practica.Domain.Entities;
using Practica.Domain.Interfaces;
using Practica.Infrastructure.Data;

namespace Practica.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly AppDbContext _context;

    public CourseRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _context.Courses.FindAsync(id);
    }

    public async Task<Course?> GetByNameAsync(string name)
    {
        return await _context.Courses.FirstOrDefaultAsync(c => c.Name == name);
    }

    public async Task AddAsync(Course course)
    {
        await _context.AddAsync(course);
    }

    public async Task UpdateAsync(Course course)
    {
        _context.Update(course);
    }

    public Task DeleteAsync(Course course)
    {
        _context.Courses.Remove(course);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
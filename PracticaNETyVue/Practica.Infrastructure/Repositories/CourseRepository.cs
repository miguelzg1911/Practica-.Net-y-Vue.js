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

    public async Task<Course?> GetCourseByNameAsync(string name)
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

    public async Task DeleteAsync(int id, Course course)
    {
        var existingCourse = await _context.Courses.FindAsync(id, course);
        
        if (existingCourse != null)
            _context.Courses.Remove(existingCourse);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
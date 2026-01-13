using Practica.Domain.Entities;

namespace Practica.Domain.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course?> GetCourseByNameAsync(string name);
    Task AddAsync(Course course);
    Task UpdateAsync(Course course);
    Task DeleteAsync(int id, Course course);
    Task SaveChangesAsync();
}
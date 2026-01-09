using Practica.Domain.Entities;
using Practica.Domain.Interfaces;

namespace Practica.Infrastructure.Repositories;

public class CourseRepository : ICourseRepository
{
    public Task<IEnumerable<Course>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Course?> GetCourseByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Course course)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, Course course)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id, Course course)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
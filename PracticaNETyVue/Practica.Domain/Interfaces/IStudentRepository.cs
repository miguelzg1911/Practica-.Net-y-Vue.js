using Practica.Domain.Entities;

namespace Practica.Domain.Interfaces;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync();
    Task<Student?> GetByIdAsync(int id);
    Task<Student?> GetCourseByNameAsync(string courseName);
    Task AddAsync(Student student);
    Task UpdateAsync(Student student);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}
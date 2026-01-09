using Practica.Domain.Entities;

namespace Practica.Domain.Interfaces;

public interface ITeacherRepository
{
    Task<IEnumerable<Teacher>> GetAllAsync();
    Task<Teacher?> GetByIdAsync(int id);
    Task<Teacher?> GetByEmailAsync(string email);
    Task AddAsync(Teacher teacher);
    Task UpdateAsync(int id, Teacher teacher);
    Task DeleteAsync(int id, Teacher teacher);
    Task SaveChangesAsync();
}
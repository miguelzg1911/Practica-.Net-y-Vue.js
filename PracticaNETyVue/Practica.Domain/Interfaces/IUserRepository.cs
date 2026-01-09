using Practica.Domain.Entities;

namespace Practica.Domain.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task UpdateAsync(int id, User user);
    Task DeleteAsync(int id, User user);
    Task SaveChangesAsync();
}
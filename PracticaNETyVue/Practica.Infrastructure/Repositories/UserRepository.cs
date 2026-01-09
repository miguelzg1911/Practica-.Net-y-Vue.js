using Practica.Domain.Entities;
using Practica.Domain.Interfaces;

namespace Practica.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id, User user)
    {
        throw new NotImplementedException();
    }

    public Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}
using Microsoft.EntityFrameworkCore;
using Practica.Domain.Entities;
using Practica.Domain.Interfaces;
using Practica.Infrastructure.Data;

namespace Practica.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
    }

    public async Task DeleteAsync(int id, User user)
    {
        var existingUser = await _context.Users.FindAsync(id, user);
        if  (existingUser != null)
            _context.Users.Remove(existingUser);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
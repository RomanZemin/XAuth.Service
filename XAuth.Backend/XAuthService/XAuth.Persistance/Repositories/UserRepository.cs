using XAuth.Application.Interfaces;
using XAuth.Domain.Entities;
using XAuth.Persistance.Contexts;

namespace XAuth.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AuthDbContext _context;

    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public Task DeleteAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetUnconfirmedUsersAsync(DateTime cutoffDate)
    {
        throw new NotImplementedException();
    }
}
using XAuth.Domain.Entities;

namespace XAuth.Application.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task DeleteAsync(User user);
    Task<IEnumerable<User>> GetUnconfirmedUsersAsync(DateTime cutoffDate);
}
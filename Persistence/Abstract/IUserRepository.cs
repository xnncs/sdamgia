using Core.Models;

namespace Persistence.Abstract;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByLoginAsync(string login);
    Task<bool> ContainsByLoginAsync(string login);
    Task<bool> ContainsByIdAsync(int id);
    Task<User?> GetByIdAsync(int id);
}
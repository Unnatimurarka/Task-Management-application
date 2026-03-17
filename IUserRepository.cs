using TaskManager.Core.Entities;
namespace TaskManager.Core.Interfaces;
public interface IUserRepository
{
    Task<User?> GetByUsernameAsync(string username);
    Task<int> CreateAsync(User user);
}

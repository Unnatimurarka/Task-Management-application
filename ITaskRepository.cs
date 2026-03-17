using TaskManager.Core.Entities;
namespace TaskManager.Core.Interfaces;
public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetByUserIdAsync(int userId);
    Task<TaskItem?> GetByIdAsync(int id, int userId);
    Task<int> CreateAsync(TaskItem task);
    Task<bool> UpdateAsync(TaskItem task);
    Task<bool> DeleteAsync(int id, int userId);
}

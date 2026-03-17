using TaskManager.API.DTOs;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;
namespace TaskManager.API.Services;
public class TaskService
{
    private readonly ITaskRepository _taskRepo;
    public TaskService(ITaskRepository taskRepo) => _taskRepo = taskRepo;

    public async Task<IEnumerable<TaskResponseDto>> GetUserTasksAsync(int userId)
    {
        var tasks = await _taskRepo.GetByUserIdAsync(userId);
        return tasks.Select(t => new TaskResponseDto(t.Id, t.Title, t.Description, t.Status, t.CreatedAt));
    }

    public async Task<TaskResponseDto> CreateTaskAsync(int userId, CreateTaskDto dto)
    {
        var task = new TaskItem { Title = dto.Title, Description = dto.Description, Status = "pending", UserId = userId };
        var id = await _taskRepo.CreateAsync(task);
        return new TaskResponseDto(id, task.Title, task.Description, task.Status, DateTime.UtcNow);
    }

    public async Task<bool> UpdateTaskAsync(int userId, int taskId, UpdateTaskDto dto)
    {
        var task = await _taskRepo.GetByIdAsync(taskId, userId);
        if (task == null) return false;
        task.Title = dto.Title; task.Description = dto.Description; task.Status = dto.Status;
        return await _taskRepo.UpdateAsync(task);
    }

    public async Task<bool> DeleteTaskAsync(int userId, int taskId) => await _taskRepo.DeleteAsync(taskId, userId);
}

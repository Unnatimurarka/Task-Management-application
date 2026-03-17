using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManager.API.DTOs;
using TaskManager.API.Services;
namespace TaskManager.API.Controllers;
[Authorize]
[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly TaskService _taskService;
    public TasksController(TaskService taskService) => _taskService = taskService;
    private int UserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _taskService.GetUserTasksAsync(UserId));

    [HttpPost]
    public async Task<IActionResult> Post(CreateTaskDto dto) => Ok(await _taskService.CreateTaskAsync(UserId, dto));

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, UpdateTaskDto dto)
    {
        var ok = await _taskService.UpdateTaskAsync(UserId, id, dto);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ok = await _taskService.DeleteTaskAsync(UserId, id);
        return ok ? NoContent() : NotFound();
    }
}

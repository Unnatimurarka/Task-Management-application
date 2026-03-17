namespace TaskManager.API.DTOs;
public record CreateTaskDto(string Title, string Description);
public record UpdateTaskDto(string Title, string Description, string Status);
public record TaskResponseDto(int Id, string Title, string Description, string Status, DateTime CreatedAt);

using Dapper;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;
namespace TaskManager.API.Data;
public class TaskRepository : ITaskRepository
{
    private readonly DatabaseConnectionFactory _db;
    public TaskRepository(DatabaseConnectionFactory db) => _db = db;

    public async Task<IEnumerable<TaskItem>> GetByUserIdAsync(int userId)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TaskItem>(
            "SELECT id, title, description, status, created_at as CreatedAt, user_id as UserId FROM TASKS WHERE user_id = :UserId ORDER BY created_at DESC", 
            new { UserId = userId });
    }

    public async Task<TaskItem?> GetByIdAsync(int id, int userId)
    {
        using var conn = _db.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<TaskItem>(
            "SELECT id, title, description, status, created_at as CreatedAt, user_id as UserId FROM TASKS WHERE id = :Id AND user_id = :UserId", 
            new { Id = id, UserId = userId });
    }

    public async Task<int> CreateAsync(TaskItem task)
    {
        using var conn = _db.CreateConnection();
        var p = new Oracle.ManagedDataAccess.Client.OracleDynamicParameters();
        p.Add("title", task.Title);
        p.Add("desc", task.Description);
        p.Add("status", task.Status);
        p.Add("uid", task.UserId);
        p.Add("id", dbType: Oracle.ManagedDataAccess.Client.OracleDbType.Decimal, direction: System.Data.ParameterDirection.ReturnValue);
        
        await conn.ExecuteAsync(
            "INSERT INTO TASKS (title, description, status, user_id) VALUES (:title, :desc, :status, :uid) RETURNING id INTO :id", p);
        return Convert.ToInt32(p.Get<decimal>("id"));
    }

    public async Task<bool> UpdateAsync(TaskItem task)
    {
        using var conn = _db.CreateConnection();
        var rows = await conn.ExecuteAsync(
            "UPDATE TASKS SET title = :Title, description = :Description, status = :Status WHERE id = :Id AND user_id = :UserId", task);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        using var conn = _db.CreateConnection();
        var rows = await conn.ExecuteAsync("DELETE FROM TASKS WHERE id = :Id AND user_id = :UserId", new { Id = id, UserId = userId });
        return rows > 0;
    }
}

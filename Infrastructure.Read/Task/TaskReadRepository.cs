using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Infrastructure.Read.Task
{
    public interface ITaskReadRepository
    {
        Task<List<TaskListReadDto>> All(IDbConnection connection);
        Task<TaskReadDto> SingleOrDefault(IDbConnection connection, Guid id);
    }

    public class TaskReadRepository : ITaskReadRepository
    {
        private static string TasksTable => "Tasks";

        public TaskReadRepository()
        {

        }

        public async Task<List<TaskListReadDto>> All(IDbConnection connection)
        {
            var items = await connection.QueryAsync<TaskListReadDto>(
              $"SELECT [ID], [Name], [Description], [UserId] FROM {TasksTable} WHERE isDeleted = 0");
            return items.ToList();
        }

        public async Task<TaskReadDto> SingleOrDefault(IDbConnection connection, Guid id)
        {
            var item = await connection.QuerySingleOrDefaultAsync<TaskReadDto>(
              $"SELECT [ID], [Name], [Description], [UserId] FROM {TasksTable} WHERE [ID] = @id AND isDeleted = 0", param: new { id });
            return item;
        }
    }
}

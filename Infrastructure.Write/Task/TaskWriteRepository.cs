using System;
using Dapper;
using Infrastructure.Core;
using Tasks = System.Threading.Tasks;

namespace Infrastructure.Write.Task
{
    public interface ITaskWriteRepository
    {
        Tasks.Task<Domain.Task> SingleOrDefault(IUnitOfWork uow, Guid id);
        Tasks.Task Add(IUnitOfWork uow, Domain.Task taskWriteDto);
        Tasks.Task Update(IUnitOfWork uow, Domain.Task item);
    }

    /// <summary>
    /// This class serves to interact with Database, in particular, each time that a Command are executed
    /// </summary>
    public class TaskWriteRepository : ITaskWriteRepository
    {
        private static string TasksTable => "Tasks";

        private readonly ITaskWriteMapper _taskMapper;

        public TaskWriteRepository(ITaskWriteMapper taskMapper)
        {
            _taskMapper = taskMapper;
        }

        public async Tasks.Task<Domain.Task> SingleOrDefault(IUnitOfWork uow, Guid id)
        {
            var taskDto = await uow.Connection.QuerySingleOrDefaultAsync<TaskWriteDto>(
           $"SELECT [ID], [Name], [Description], [UserId], [IsDeleted] FROM {TasksTable} WHERE [ID] = @Id AND [IsDeleted] = 0", new { id }, uow.Transaction);

            if (taskDto == null) return null;

            return Domain.Task.Create(taskDto.Name, taskDto.Description, taskDto.UserId, taskDto.Id);
        }

        public async Tasks.Task Add(IUnitOfWork uow, Domain.Task taskWriteDto)
        {
            var taskDto = _taskMapper.ToTaskDto(taskWriteDto);

            await uow.Connection.ExecuteAsync(
              $"INSERT INTO {TasksTable} ([ID], [Name], [Description], [UserId], [IsDeleted]) " +
              "VALUES (@Id, @Name, @Description, @UserId, @IsDeleted)",
              taskDto, uow.Transaction);
        }

        public async Tasks.Task Update(IUnitOfWork uow, Domain.Task item)
        {
            var taskDto = _taskMapper.ToTaskDto(item);

            await uow.Connection.ExecuteAsync(
                $"UPDATE {TasksTable} SET [ID] = @Id, [Name] = @Name, [Description] = @Description, [UserId] = @UserId, [IsDeleted]  = @IsDeleted " +
                " WHERE [Id] = @Id",
                taskDto, uow.Transaction);
        }
    }
}

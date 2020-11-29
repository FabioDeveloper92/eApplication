using Dapper;
using Infrastructure.Core;
using Tasks = System.Threading.Tasks;

namespace Infrastructure.Write.Task
{
    public interface ITaskWriteRepository
    {
        Tasks.Task Add(IUnitOfWork uow, Domain.Task taskWriteDto);
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

        public async Tasks.Task Add(IUnitOfWork uow, Domain.Task taskWriteDto)
        {
            var taskDto = _taskMapper.ToTaskDto(taskWriteDto);

            await uow.Connection.ExecuteAsync(
              $"INSERT INTO {TasksTable} ([ID], [Name], [Description], [UserId], [IsDeleted]) " +
              "VALUES (@Id, @Name, @Description, @UserId, @IsDeleted)",
              taskDto, uow.Transaction);
        }
    }
}

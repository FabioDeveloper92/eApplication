namespace Infrastructure.Write.Task
{
    public interface ITaskWriteMapper
    {
        TaskWriteDto ToTaskDto(Domain.Task item);
    }

    /// <summary>
    /// This class has the job of mapper a Domain object into Dto object
    /// </summary>
    public class TaskWriteMapper : ITaskWriteMapper
    {
        public TaskWriteDto ToTaskDto(Domain.Task item)
        {
            var dto = new TaskWriteDto(item.Id, item.Name, item.Description, item.UserId, false);

            return dto;
        }
    }
}

using System.Collections.Generic;
using Application.Interfaces;
using Infrastructure.Read.Task;

namespace Application.Task.Queries
{
    public class GetTasks : IQuery<IList<TaskListReadDto>>
    {
    }
}

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Core;
using Infrastructure.Read.Task;
using MediatR;

namespace Application.Task.Queries
{
    public class TaskReadService : IRequestHandler<GetTasks, IList<TaskListReadDto>>
    {
        private readonly IConnectionFactory _connectionFactory;
        private readonly ITaskReadRepository _taskReadRepository;

        public TaskReadService(IConnectionFactory connectionFactory, ITaskReadRepository taskReadRepository)
        {
            _connectionFactory = connectionFactory;
            _taskReadRepository = taskReadRepository;
        }

        public async Task<IList<TaskListReadDto>> Handle(GetTasks request, CancellationToken cancellationToken)
        {
            return await _taskReadRepository.All(_connectionFactory.Connection);
        }
    }
}

using System.Threading;
using Infrastructure.Core;
using Infrastructure.Write.Task;
using MediatR;
using Tasks = System.Threading.Tasks;

namespace Application.Task.Commads
{
    public class TaskWriteService : IRequestHandler<CreateTask>
    {
        private readonly IMediator _mediator;
        private readonly IConnectionFactory _connectionFactory;
        private readonly ITaskWriteRepository _taskWriteRepository;

        public TaskWriteService(IMediator mediator, IConnectionFactory connectionFactory, ITaskWriteRepository taskWriteRepository)
        {
            _mediator = mediator;
            _connectionFactory = connectionFactory;
            _taskWriteRepository = taskWriteRepository;
        }

        public async Tasks.Task<Unit> Handle(CreateTask command, CancellationToken cancellationToken)
        {
            var entity = Domain.Task.Create(command.Name, command.Description, command.UserId);

            IUnitOfWork uow = null;

            try
            {
                uow = _connectionFactory.BeginUnitOfWork();
                await _taskWriteRepository.Add(uow, entity);

                uow.Commit();
            }
            finally
            {
                uow?.Dispose();
            }

            return Unit.Value;
        }

    }
}

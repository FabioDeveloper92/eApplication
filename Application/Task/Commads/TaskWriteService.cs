using System.Threading;
using Domain.Exceptions;
using Infrastructure.Core;
using Infrastructure.Write.Task;
using MediatR;
using Tasks = System.Threading.Tasks;

namespace Application.Task.Commads
{
    public class TaskWriteService : IRequestHandler<CreateTask>,
                                    IRequestHandler<UpdateTaskName>,
                                    IRequestHandler<UpdateTaskDescription>,
                                    IRequestHandler<UpdateTaskUserId>
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
            var entity = Domain.Task.Create(command.Name, command.Description, command.UserId, command.TaskId);

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

        public async Tasks.Task<Unit> Handle(UpdateTaskName command, CancellationToken cancellationToken)
        {
            using (var uow = _connectionFactory.BeginUnitOfWork())
            {
                var task = await _taskWriteRepository.SingleOrDefault(uow, command.Id);

                if (task == null)
                    throw new NotFoundItemException();

                task.SetName(command.Name);

                await _taskWriteRepository.Update(uow, task);

                uow.Commit();
            }

            return Unit.Value;
        }

        public async Tasks.Task<Unit> Handle(UpdateTaskDescription command, CancellationToken cancellationToken)
        {
            using (var uow = _connectionFactory.BeginUnitOfWork())
            {
                var task = await _taskWriteRepository.SingleOrDefault(uow, command.Id);

                if (task == null)
                    throw new NotFoundItemException();

                task.SetDescription(command.Description);

                await _taskWriteRepository.Update(uow, task);

                uow.Commit();
            }

            return Unit.Value;
        }

        public async Tasks.Task<Unit> Handle(UpdateTaskUserId command, CancellationToken cancellationToken)
        {
            using (var uow = _connectionFactory.BeginUnitOfWork())
            {
                var task = await _taskWriteRepository.SingleOrDefault(uow, command.Id);

                if (task == null)
                    throw new NotFoundItemException();

                task.SetUserId(command.UserId);

                await _taskWriteRepository.Update(uow, task);

                uow.Commit();
            }

            return Unit.Value;
        }
    }
}

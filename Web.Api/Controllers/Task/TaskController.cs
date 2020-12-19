using MediatR;

namespace Web.Api.Controllers.Task
{
    public class TaskController
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpPost]
        //public async Task<Guid> Post([FromBody] NewTask item)
        //{
        //    //var taskId = Guid.NewGuid();

        //    //await _mediator.Send(new CreateTask(taskId, item.Name, item.Description, false,
        //    //    item.TaskOwners.Select(b => new TaskPersonDto(b.Role, new PersonDto(b.UserId, b.Name))).ToArray()));

        //    //return taskId;
        //}
    }
}

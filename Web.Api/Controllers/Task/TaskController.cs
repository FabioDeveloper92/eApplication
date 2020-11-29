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
        //public async Task<Guid> Post([FromBody] NewBoard item)
        //{
        //    //var boardId = Guid.NewGuid();

        //    //await _mediator.Send(new CreateTask(boardId, item.Name, item.Description, false,
        //    //    item.BoardOwners.Select(b => new BoardPersonDto(b.Role, new PersonDto(b.UserId, b.Name))).ToArray()));

        //    //return boardId;
        //}
    }
}

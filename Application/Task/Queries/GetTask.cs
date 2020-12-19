using System;
using Application.Interfaces;
using Infrastructure.Read.Task;

namespace Application.Task.Queries
{
    public class GetTask : IQuery<TaskReadDto>
    {
        public Guid Id { get; }

        public GetTask(Guid id)
        {
            Id = id;
        }
    }
}

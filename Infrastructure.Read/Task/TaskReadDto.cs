using System;
using Infrastructure.Core;

namespace Infrastructure.Read.Task
{
    public class TaskReadDto : Dto
    {
        public string Name { get; }
        public string Description { get; }
        public Guid UserId { get; }

        public TaskReadDto(Guid id, string name, string description, Guid userId) : base(id)
        {
            Name = name;
            Description = description;
            UserId = userId;
        }

    }
}

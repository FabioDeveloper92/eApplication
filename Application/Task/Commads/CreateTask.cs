using System;
using System.Data;
using Application.Interfaces;

namespace Application.Task.Commads
{
    public class CreateTask : ICommand
    {
        public Guid TaskId { get; }
        public string Name { get; }
        public string Description { get; }
        public Guid UserId { get; }

        public CreateTask(Guid taskId, string name, string description, Guid userId)
        {
            TaskId = taskId;
            Name = name;
            Description = description;
            UserId = userId;
        }
    }
}

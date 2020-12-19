using System;
using Application.Interfaces;

namespace Application.Task.Commads
{
    public class UpdateTaskDescription : ICommand
    {
        public Guid Id { get; }
        public string Description { get; }

        public UpdateTaskDescription(Guid id, string description)
        {
            Id = id;
            Description = description;
        }
    }
}

using System;
using Application.Interfaces;

namespace Application.Task.Commads
{
    public class UpdateTaskName : ICommand
    {
        public Guid Id { get; }
        public string Name { get; }

        public UpdateTaskName(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}

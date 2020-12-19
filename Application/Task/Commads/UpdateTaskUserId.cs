using System;
using Application.Interfaces;

namespace Application.Task.Commads
{
    public class UpdateTaskUserId : ICommand
    {
        public Guid Id { get; }
        public Guid UserId { get; }

        public UpdateTaskUserId(Guid id, Guid userId)
        {
            Id = id;
            UserId = userId;
        }
    }
}

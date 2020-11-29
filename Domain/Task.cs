using System;
using System.Data;
using Domain.Core;
using Domain.Exceptions;

namespace Domain
{
    public class Task : Entity<Guid>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid UserId { get; private set; }

        private Task(Guid id, string name, string description, Guid userId) : base(id)
        {
            Name = name;
            Description = description;
            UserId = userId;
        }

        public static Task Create(string name, string description, Guid userId, Guid? taskId = null)
        {
            if (taskId == null)
                taskId = Guid.NewGuid();

            var item = new Task(taskId.Value, name, description, userId);

            item.Validate();

            return item;
        }

        public void SetName(string name)
        {
            Name = name;
            Validate();
        }

        public void SetDescription(string description)
        {
            Description = description;
            Validate();
        }

        public void SetUserId(Guid userId)
        {
            UserId = userId;
            Validate();
        }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new EmptyFieldException(nameof(Name));
            if (string.IsNullOrEmpty(Description))
                throw new EmptyFieldException(nameof(Description));

            // TODO UserId SqlDbType
        }
    }
}

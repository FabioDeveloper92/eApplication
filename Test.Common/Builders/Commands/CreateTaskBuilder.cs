using System;
using Application.Task.Commads;

namespace Test.Common.Builders.Commands
{
    public class CreateTaskBuilder
    {
        private Guid _id;
        private string _name;
        private string _description;
        private Guid _userId;

        public CreateTaskBuilder WithDefaults()
        {
            _id = Guid.NewGuid();
            _name = "Test every moment";
            _description = "This is default value";
            _userId = Guid.NewGuid();

            return this;
        }

        public CreateTaskBuilder WithId(Guid id)
        {
            _id = id;
            return this;
        }

        public CreateTaskBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CreateTaskBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public CreateTaskBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }

        public CreateTask Build()
        {
            return new CreateTask(_id, _name, _description, _userId);
        }
    }
}

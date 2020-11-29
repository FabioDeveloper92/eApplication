using System;
using System.Data;
using Infrastructure.Core;

namespace Infrastructure.Write.Task
{    
    /// <summary>
     /// This class map 1 to 1 object to table on database
     /// Every field corresponds to column on db
     /// </summary>
    public class TaskWriteDto : Dto
    {
        public string Name { get; }
        public string Description { get; }
        public Guid UserId { get; }
        public bool IsDeleted { get; }

        public TaskWriteDto(Guid id, string name, string description, Guid userId, bool isDeleted = false) : base(id)
        {
            Name = name;
            Description = description;
            UserId = userId;
            IsDeleted = isDeleted;
        }
    }
}

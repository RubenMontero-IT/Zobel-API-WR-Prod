using System;

namespace DhubSolutions.Core.Domain.Entity
{
    public abstract class BaseEntity : IEntity
    {
        protected BaseEntity()
        {
            Id = $"{Guid.NewGuid()}";
        }

        public string Id { get; set; }
    }
}

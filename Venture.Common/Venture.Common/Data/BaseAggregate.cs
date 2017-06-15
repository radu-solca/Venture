using System;
using System.Collections.Generic;
using Venture.Common.Events;

namespace Venture.Common.Data
{
    public abstract class BaseAggregate : IAggregate
    {
        public Guid Id { get; }

        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }

        public IEnumerable<IDomainEvent> Events { get; }

        protected BaseAggregate()
        {
            Id = Guid.NewGuid();

            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;

            Events = new List<IDomainEvent>();
        }
    }
}

using System;

namespace Venture.Common.Events
{
    public abstract class DomainEvent : IDomainEvent
    {
        public Guid Id { get; }

        public string Type { get; }
        public DateTime OccuredAt { get; }
        public Guid AggregateId { get; }

        protected DomainEvent(Guid aggregateId)
        {
            Id = Guid.NewGuid();

            Type = GetType().Name;
            OccuredAt = DateTime.Now;
            AggregateId = aggregateId;
        }
    }
}
using System;

namespace Venture.Common.Events
{
    public abstract class BaseDomainEvent : IDomainEvent
    {
        public Guid Id { get; }
        public string Type { get; }
        public DateTime OccuredAt { get; }

        protected BaseDomainEvent()
        {
            Id = Guid.NewGuid();
            Type = GetType().Name;
            OccuredAt = DateTime.Now;
        }
    }
}
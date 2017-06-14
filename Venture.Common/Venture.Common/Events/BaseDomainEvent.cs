using System;

namespace Venture.Common.Events
{
    public class BaseDomainEvent : IDomainEvent
    {
        public string Type { get; }
        public DateTime OccuredAt { get; }

        public BaseDomainEvent()
        {
            Type = GetType().Name;
            OccuredAt = DateTime.Now;
        }
    }
}
using System;
using System.Collections.Generic;

namespace Venture.Common.Events
{
    public interface IEventStore
    {
        void Raise(DomainEvent domainEvent);

        IEnumerable<DomainEvent> GetEvents(Guid aggregateId, int startVersion = Int32.MinValue, int endVersion = Int32.MaxValue);
        IEnumerable<DomainEvent> GetEvents(int startVersion = Int32.MinValue, int endVersion = Int32.MaxValue);
    }
}
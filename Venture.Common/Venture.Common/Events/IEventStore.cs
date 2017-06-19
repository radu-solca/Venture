using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Venture.Common.Events
{
    public interface IEventStore
    {
        Task<IEnumerable<DomainEvent>> GetEventsAsync(
            DateTime startDate,
            DateTime endDate);

        Task<IEnumerable<DomainEvent>> GetEventsAsync();

        Task RaiseAsync(DomainEvent domainEvent);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Venture.Common.Events
{
    public interface IEventStore
    {
        Task<IEnumerable<IDomainEvent>> GetEventsAsync(
            DateTime startDate,
            DateTime endDate);

        Task<IEnumerable<IDomainEvent>> GetEventsAsync();

        Task RaiseAsync(IDomainEvent domainEvent);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Venture.Common.Events
{
    public interface IEventStore
    {
        Task<IEnumerable<DomainEvent>> GetEventsAsync(
            long firstEventSequenceNumber = 0,
            long lastEventSequenceNumber = long.MaxValue);

        Task RaiseAsync(string eventName, object content);
    }
}
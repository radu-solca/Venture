using System.Collections.Generic;
using System.Threading.Tasks;

namespace Venture.ProfileWrite.Data.Events
{
    public interface IEventStore
    {
        Task<IEnumerable<Event>> GetEvents(long firstEventSequenceNumber = 0,
            long lastEventSequenceNumber = long.MaxValue);

        Task Raise(string eventName, object content);
    }
}
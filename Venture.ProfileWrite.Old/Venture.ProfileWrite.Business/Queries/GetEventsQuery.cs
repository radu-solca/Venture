using System.Collections.Generic;
using Venture.ProfileWrite.Data.Events;

namespace Venture.ProfileWrite.Business.Queries
{
    public class GetEventsQuery : IQuery<IEnumerable<Event>>
    {
        public GetEventsQuery(long firstEventSequenceNumber = 0, long lastEventSequenceNumber = long.MaxValue)
        {
            FirstEventSequenceNumber = firstEventSequenceNumber;
            LastEventSequenceNumber = lastEventSequenceNumber;
        }

        public long FirstEventSequenceNumber { get; }
        public long LastEventSequenceNumber { get; }
    }
}

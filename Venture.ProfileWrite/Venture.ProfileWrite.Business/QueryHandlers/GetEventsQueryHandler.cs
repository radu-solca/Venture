using System.Collections.Generic;
using System.Threading.Tasks;
using Venture.ProfileWrite.Business.Queries;
using Venture.ProfileWrite.Data.Events;

namespace Venture.ProfileWrite.Business.QueryHandlers
{
    public class GetEventsQueryHandler : IQueryHandler<GetEventsQuery, IEnumerable<Event>>
    {
        private readonly IEventStore _eventStore;

        public GetEventsQueryHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<IEnumerable<Event>> Retrieve(GetEventsQuery query)
        {
            return await _eventStore.GetEvents(query.FirstEventSequenceNumber, query.LastEventSequenceNumber);
        }
    }
}

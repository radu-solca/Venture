using System.Collections.Generic;
using Venture.ProfileWrite.Business.Queries;
using Venture.ProfileWrite.Data.Events;

namespace Venture.ProfileWrite.Business.QueryHandlers
{
    public class GetEventsQueryHandler : IQueryHandler<GetEventsQuery, IEnumerable<Event>>
    {
        public IEnumerable<Event> Retrieve(GetEventsQuery query)
        {
            return new List<Event>();
        }
    }
}

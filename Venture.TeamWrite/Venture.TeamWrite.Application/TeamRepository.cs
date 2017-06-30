using RawRabbit;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.TeamWrite.Domain;

namespace Venture.TeamWrite.Application
{
    public class TeamRepository : EventRepository<Team>
    {
        public TeamRepository(IEventStore eventStore, IBusClient bus) : base(eventStore, bus)
        {
        }
    }
}

using RawRabbit;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.ProjectWrite.Domain;

namespace Venture.ProjectWrite.Application
{
    public sealed class ProjectRepository : EventRepository<Project>
    {
        public ProjectRepository(IEventStore eventStore, IBusClient bus) : base(eventStore, bus)
        {
        }
    }
}

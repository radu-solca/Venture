using System;
using LiteGuard;
using Newtonsoft.Json;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.TeamWrite.Domain;
using Venture.TeamWrite.Domain.DomainEvents;

namespace Venture.TeamWrite.Application.EventHandlers
{
    public class ProjectDeletedEventHandler : IEventHandler<ProjectDeletedEvent>
    {
        private readonly IRepository<Team> _teamRepository;

        public ProjectDeletedEventHandler(IRepository<Team> teamRepository)
        {
            Guard.AgainstNullArgument(nameof(teamRepository), teamRepository);
            _teamRepository = teamRepository;
        }

        public void Handle(ProjectDeletedEvent domainEvent)
        {
            Console.WriteLine(domainEvent.AggregateId + " " + domainEvent.Type + " " + domainEvent.JsonPayload);

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);
            var teamId = (Guid)eventData.ProjectId;

            _teamRepository.Delete(teamId);
        }
    }
}

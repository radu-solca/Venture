using System;
using LiteGuard;
using Newtonsoft.Json;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.TeamWrite.Domain;
using Venture.TeamWrite.Domain.DomainEvents;

namespace Venture.TeamWrite.Application.EventHandlers
{
    public class ProjectCreatedEventHandler : IEventHandler<ProjectCreatedEvent>
    {
        private readonly IRepository<Team> _teamRepository;

        public ProjectCreatedEventHandler(IRepository<Team> teamRepository)
        {
            Guard.AgainstNullArgument(nameof(teamRepository), teamRepository);
            _teamRepository = teamRepository;
        }

        public void Handle(ProjectCreatedEvent domainEvent)
        {
            Console.WriteLine(domainEvent.AggregateId + " " + domainEvent.Type + " " + domainEvent.JsonPayload);

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);

            var newTeam = new Team();
            newTeam.Create(
                domainEvent.AggregateId,
                new User((Guid)eventData.OwnerId));

            _teamRepository.Add(newTeam);
        }
    }
}

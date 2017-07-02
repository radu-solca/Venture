using System;
using LiteGuard;
using Newtonsoft.Json;
using RawRabbit;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.Common.Extensions;
using Venture.TeamWrite.Application.Commands;
using Venture.TeamWrite.Domain;
using Venture.TeamWrite.Domain.DomainEvents;

namespace Venture.TeamWrite.Application.EventHandlers
{
    public class ProjectCreatedEventHandler : IEventHandler<ProjectCreatedEvent>
    {
        private readonly IRepository<Team> _teamRepository;
        private readonly IBusClient _bus;

        public ProjectCreatedEventHandler(IRepository<Team> teamRepository, IBusClient bus)
        {
            Guard.AgainstNullArgument(nameof(teamRepository), teamRepository);
            _teamRepository = teamRepository;
            _bus = bus;
        }

        public void Handle(ProjectCreatedEvent domainEvent)
        {
            Console.WriteLine(domainEvent.AggregateId + " " + domainEvent.Type + " " + domainEvent.JsonPayload);

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);

            var projectOwner = new User((Guid) eventData.OwnerId);

            var newTeam = new Team();
            newTeam.Create(
                domainEvent.AggregateId,
                projectOwner);

            _teamRepository.Add(newTeam);

            // the creator is by default part of the team.
            var joinCommand = new JoinTeamCommand(projectOwner.Id, domainEvent.AggregateId);
            var approveCommand = new ApproveTeamUserCommand(projectOwner.Id, domainEvent.AggregateId);

            _bus.PublishCommand(joinCommand);
            _bus.PublishCommand(approveCommand);
        }
    }
}

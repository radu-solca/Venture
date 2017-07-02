using System;
using System.Linq;
using Newtonsoft.Json;
using RawRabbit;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.Common.Extensions;
using Venture.TeamRead.Data.DomainEvents;
using Venture.TeamRead.Data.Entities;
using Venture.Users.Application;

namespace Venture.TeamRead.Application
{
    public class TeamDenormalizer :
        IEventHandler<TeamCreatedEvent>,
        IEventHandler<TeamJoinedEvent>,
        IEventHandler<TeamUserApprovedEvent>,
        IEventHandler<TeamCommentPostedEvent>,
        IEventHandler<TeamLeftEvent>,
        IEventHandler<TeamDeletedEvent>
    {

        private readonly IRepository<Team> _teamRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<TeamMembership> _teamMembershipRepository;
        private readonly IBusClient _bus;


        public TeamDenormalizer(IRepository<Team> teamRepository, IRepository<Comment> commentRepository, IRepository<TeamMembership> teamMembershipRepository, IBusClient bus)
        {
            _teamRepository = teamRepository;
            _commentRepository = commentRepository;
            _teamMembershipRepository = teamMembershipRepository;
            _bus = bus;
        }


        public void Handle(TeamCreatedEvent domainEvent)
        {
            LogToConsole(domainEvent);

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);

            var newProject = new Team
            {
                Id = domainEvent.AggregateId,
                ProjectOwnerId = (Guid)eventData.ProjectOwnerId,
            };

            _teamRepository.Add(newProject);
        }

        public void Handle(TeamJoinedEvent domainEvent)
        {
            LogToConsole(domainEvent);

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);

            var query = new GetUserQuery((Guid)eventData.UserId);
            dynamic user = JsonConvert.DeserializeObject(_bus.PublishQuery(query));

            var newMembership = new TeamMembership
            {
                Id = Guid.NewGuid(),
                Approved = false,
                TeamId = domainEvent.AggregateId,
                UserId = (Guid) eventData.UserId,
                UserName = (string) user.UserName
            };

            _teamMembershipRepository.Add(newMembership);
        }

        public void Handle(TeamUserApprovedEvent domainEvent)
        {
            LogToConsole(domainEvent);

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);

            var membership = _teamMembershipRepository.Get()
                .FirstOrDefault(m => 
                    m.TeamId == domainEvent.AggregateId && 
                    m.UserId == eventData.UserId);

            membership.Approved = true;

            _teamMembershipRepository.Update(membership);
        }

        public void Handle(TeamCommentPostedEvent domainEvent)
        {
            LogToConsole(domainEvent);

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);

            var query = new GetUserQuery((Guid)eventData.AuthorId);
            dynamic user = JsonConvert.DeserializeObject(_bus.PublishQuery(query));

            //TODO: add usernames.
            var newComment = new Comment
            {
                TeamId = domainEvent.AggregateId,
                AuthorId = (Guid)eventData.AuthorId,
                AuthorName = (string)user.UserName,
                PostedOn = (DateTime)eventData.PostedOn,
                Content = (string)eventData.Content
            };

            _commentRepository.Add(newComment);
        }

        public void Handle(TeamLeftEvent domainEvent)
        {
            LogToConsole(domainEvent);

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);

            var membership = _teamMembershipRepository.Get()
                .FirstOrDefault(m =>
                    m.TeamId == domainEvent.AggregateId &&
                    m.UserId == eventData.UserId);

            _teamMembershipRepository.Delete(membership.Id);
        }

        public void Handle(TeamDeletedEvent domainEvent)
        {
            LogToConsole(domainEvent);

            _teamRepository.Delete(domainEvent.AggregateId);
        }

        private void LogToConsole(DomainEvent domainEvent)
        {
            Console.WriteLine("Got event of type " + domainEvent.Type + " for aggregate with id=" + domainEvent.AggregateId + " v" + domainEvent.Version + " occured on " + domainEvent.OccuredOn);
            Console.WriteLine("With payload: " + domainEvent.JsonPayload);
        }
    }
}

using System;
using Newtonsoft.Json;
using RawRabbit;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.Common.Extensions;
using Venture.ProjectRead.Data.DomainEvents;
using Venture.ProjectRead.Data.Entities;
using Venture.Users.Application;

namespace Venture.ProjectRead.Application
{
    public sealed class ProjectDenormalizer :
        IEventHandler<ProjectCreatedEvent>,
        IEventHandler<ProjectTitleUpdatedEvent>,
        IEventHandler<ProjectDescriptionUpdatedEvent>,
        IEventHandler<ProjectCommentPostedEvent>,
        IEventHandler<ProjectDeletedEvent>
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IBusClient _bus;

        public ProjectDenormalizer(IRepository<Project> projectRepository, IRepository<Comment> commentRepository, IBusClient bus)
        {
            _projectRepository = projectRepository;
            _commentRepository = commentRepository;
            _bus = bus;
        }

        public void Handle(ProjectCreatedEvent domainEvent)
        {
            LogToConsole(domainEvent);

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);

            var query = new GetUserQuery((Guid)eventData.OwnerId);
            dynamic user = JsonConvert.DeserializeObject(_bus.PublishQuery(query));

            var newProject = new Project
            {
                Id = domainEvent.AggregateId,
                OwnerId = (Guid) eventData.OwnerId,
                OwnerName = (string) user.UserName,
                Title = (string) eventData.Title,
                Description = (string) eventData.Description
            };

            _projectRepository.Add(newProject);
        }

        public void Handle(ProjectTitleUpdatedEvent domainEvent)
        {
            LogToConsole(domainEvent);

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);

            var project = _projectRepository.Get(domainEvent.AggregateId);

            project.Title = (string)eventData.NewTitle;

            _projectRepository.Update(project);
        }

        public void Handle(ProjectDescriptionUpdatedEvent domainEvent)
        {
            LogToConsole(domainEvent);

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);

            var project = _projectRepository.Get(domainEvent.AggregateId);

            project.Description = (string)eventData.NewDescription;

            _projectRepository.Update(project);
        }

        public void Handle(ProjectCommentPostedEvent domainEvent)
        {
            LogToConsole(domainEvent);

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);

            var query = new GetUserQuery((Guid)eventData.AuthorId);
            dynamic user = JsonConvert.DeserializeObject(_bus.PublishQuery(query));

            // TODO: add usernames.
            var newComment = new Comment
            {
                ProjectId = domainEvent.AggregateId,
                AuthorId = (Guid) eventData.AuthorId,
                AuthorName = (string) user.UserName,
                PostedOn = (DateTime) eventData.PostedOn,
                Content = (string) eventData.Content
            };


            _commentRepository.Add(newComment);
        }

        public void Handle(ProjectDeletedEvent domainEvent)
        {
            _projectRepository.Delete(domainEvent.AggregateId);
        }

        private void LogToConsole(DomainEvent domainEvent)
        {
            Console.WriteLine("Got event of type " + domainEvent.Type + " for aggregate with id=" + domainEvent.AggregateId + " v" + domainEvent.Version + " occured on " + domainEvent.OccuredOn);
            Console.WriteLine("With payload: " + domainEvent.JsonPayload);
        }
    }
}

using System;
using Newtonsoft.Json;
using Venture.Common.Data;
using Venture.Common.Events;
using Venture.ProjectRead.Application.DomainEvents;
using Venture.ProjectRead.Data.Entities;

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

        public ProjectDenormalizer(IRepository<Project> projectRepository, IRepository<Comment> commentRepository)
        {
            _projectRepository = projectRepository;
            _commentRepository = commentRepository;
        }

        public void Handle(ProjectCreatedEvent domainEvent)
        {
            LogToConsole(domainEvent);

            var newProject = new Project();

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);

            newProject.Id = domainEvent.AggregateId;
            newProject.OwnerId = (Guid) eventData.ownerId;
            newProject.Title = (string) eventData.title;
            newProject.Description = (string) eventData.description;

            _projectRepository.Add(newProject);
        }

        public void Handle(ProjectTitleUpdatedEvent domainEvent)
        {
            LogToConsole(domainEvent);

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);

            var project = _projectRepository.Get(domainEvent.AggregateId);

            project.Title = (string)eventData.newTitle;

            _projectRepository.Update(project);
        }

        public void Handle(ProjectDescriptionUpdatedEvent domainEvent)
        {
            LogToConsole(domainEvent);

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);

            var project = _projectRepository.Get(domainEvent.AggregateId);

            project.Description = (string)eventData.newDescription;

            _projectRepository.Update(project);
        }

        public void Handle(ProjectCommentPostedEvent domainEvent)
        {
            LogToConsole(domainEvent);

            dynamic eventData = JsonConvert.DeserializeObject(domainEvent.JsonPayload);

            var newComment = new Comment();

            newComment.ProjectId = domainEvent.AggregateId;
            newComment.AuthorId = (Guid) eventData.authorId;
            newComment.AuthorName = "Tim";
            newComment.PostedOn = (DateTime) eventData.postedOn;
            newComment.Content = (string) eventData.content;

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

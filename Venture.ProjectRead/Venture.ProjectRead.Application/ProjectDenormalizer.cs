using System;
using System.Dynamic;
using Venture.Common.Events;
using Venture.ProjectRead.Application.DomainEvents;

namespace Venture.ProjectRead.Application
{
    public sealed class ProjectDenormalizer :
        IEventHandler<ProjectCreatedEvent>,
        IEventHandler<ProjectTitleUpdatedEvent>,
        IEventHandler<ProjectDescriptionUpdatedEvent>,
        IEventHandler<ProjectCommentPostedEvent>,
        IEventHandler<ProjectTagsUpdatedEvent>
    {
        public void Handle(ProjectCreatedEvent domainEvent)
        {
            DELETEME(domainEvent);
        }

        public void Handle(ProjectTitleUpdatedEvent domainEvent)
        {
            DELETEME(domainEvent);
        }

        public void Handle(ProjectDescriptionUpdatedEvent domainEvent)
        {
            DELETEME(domainEvent);
        }

        public void Handle(ProjectCommentPostedEvent domainEvent)
        {
            DELETEME(domainEvent);
        }

        public void Handle(ProjectTagsUpdatedEvent domainEvent)
        {
            DELETEME(domainEvent);
        }

        private void DELETEME(DomainEvent domainEvent)
        {
            Console.WriteLine("Got event of type " + domainEvent.Type + " for aggregate with id=" + domainEvent.AggregateId + " v" + domainEvent.Version + " occured on " + domainEvent.OccuredOn);
            Console.WriteLine("With payload: " + domainEvent.JsonPayload);
        }
    }
}

using System;
using Venture.Common.Events;

namespace Venture.ProjectRead.Application.DomainEvents
{
    public sealed class ProjectTitleUpdatedEvent : DomainEvent
    {
        public ProjectTitleUpdatedEvent(Guid aggregateId, int version, string jsonPayload) : base("ProjectTitleUpdatedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
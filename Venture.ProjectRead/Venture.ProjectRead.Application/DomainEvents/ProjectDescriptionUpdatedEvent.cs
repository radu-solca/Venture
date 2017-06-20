using System;
using Venture.Common.Events;

namespace Venture.ProjectRead.Application.DomainEvents
{
    public sealed class ProjectDescriptionUpdatedEvent : DomainEvent
    {
        public ProjectDescriptionUpdatedEvent(Guid aggregateId, int version, string jsonPayload) : base("ProjectDescriptionUpdatedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
using System;
using Venture.Common.Events;

namespace Venture.ProjectWrite.Domain.DomainEvents
{
    public sealed class ProjectDescriptionUpdatedEvent : DomainEvent
    {
        public ProjectDescriptionUpdatedEvent(Guid aggregateId, int version, string jsonPayload) : base("ProjectDescriptionUpdatedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
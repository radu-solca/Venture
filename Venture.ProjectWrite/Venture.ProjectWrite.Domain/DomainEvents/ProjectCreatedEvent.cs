using System;
using Venture.Common.Events;

namespace Venture.ProjectWrite.Domain.DomainEvents
{
    public sealed class ProjectCreatedEvent : DomainEvent
    {
        public ProjectCreatedEvent(Guid aggregateId, int version, string jsonPayload) : base("ProjectCreatedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}

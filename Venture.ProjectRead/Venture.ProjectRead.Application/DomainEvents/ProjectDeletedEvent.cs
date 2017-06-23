using System;
using Venture.Common.Events;

namespace Venture.ProjectRead.Application.DomainEvents
{
    public sealed class ProjectDeletedEvent : DomainEvent
    {
        public ProjectDeletedEvent(Guid aggregateId, int version, string jsonPayload) : base((string) "ProjectDeletedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
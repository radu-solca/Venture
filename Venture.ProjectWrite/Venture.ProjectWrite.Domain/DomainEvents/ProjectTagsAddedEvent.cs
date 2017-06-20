using System;
using Venture.Common.Events;

namespace Venture.ProjectWrite.Domain.DomainEvents
{
    public sealed class ProjectTagsAddedEvent : DomainEvent
    {
        public ProjectTagsAddedEvent(Guid aggregateId, int version, string jsonPayload) : base("ProjectTagsAddedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
using System;
using Venture.Common.Events;

namespace Venture.ProjectRead.Application.DomainEvents
{
    public sealed class ProjectTagsAddedEvent : DomainEvent
    {
        public ProjectTagsAddedEvent(Guid aggregateId, int version, string jsonPayload) : base("ProjectTagsAddedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
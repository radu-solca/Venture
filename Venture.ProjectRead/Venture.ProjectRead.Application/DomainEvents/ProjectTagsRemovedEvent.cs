using System;
using Venture.Common.Events;

namespace Venture.ProjectRead.Application.DomainEvents
{
    public sealed class ProjectTagsRemovedEvent : DomainEvent
    {
        public ProjectTagsRemovedEvent(Guid aggregateId, int version, string jsonPayload) : base((string) "ProjectTagsRemovedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
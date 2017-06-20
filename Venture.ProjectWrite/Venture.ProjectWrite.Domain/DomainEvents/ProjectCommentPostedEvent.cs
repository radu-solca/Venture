using System;
using Venture.Common.Events;

namespace Venture.ProjectWrite.Domain.DomainEvents
{
    public sealed class ProjectCommentPostedEvent : DomainEvent
    {
        public ProjectCommentPostedEvent(Guid aggregateId, int version, string jsonPayload) : base("ProjectCommentPostedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
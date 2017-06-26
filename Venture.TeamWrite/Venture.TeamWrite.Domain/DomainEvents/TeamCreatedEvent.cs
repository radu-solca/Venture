using System;
using Venture.Common.Events;

namespace Venture.TeamWrite.Domain.DomainEvents
{
    public sealed class TeamCreatedEvent : DomainEvent
    {
        public TeamCreatedEvent(Guid aggregateId, int version, string jsonPayload) : base((string) "TeamCreatedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
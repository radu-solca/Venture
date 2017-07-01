using System;
using Venture.Common.Events;

namespace Venture.TeamRead.Data.DomainEvents
{
    public sealed class TeamCreatedEvent : DomainEvent
    {
        public TeamCreatedEvent(Guid aggregateId, int version, string jsonPayload) : base((string) "TeamCreatedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
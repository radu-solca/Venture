using System;
using Venture.Common.Events;

namespace Venture.TeamWrite.Domain.DomainEvents
{
    public sealed class TeamDeletedEvent : DomainEvent
    {
        public TeamDeletedEvent(Guid aggregateId, int version, string jsonPayload) : base((string) "TeamDeletedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
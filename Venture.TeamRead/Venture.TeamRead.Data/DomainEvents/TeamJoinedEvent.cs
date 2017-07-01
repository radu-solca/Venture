using System;
using Venture.Common.Events;

namespace Venture.TeamRead.Data.DomainEvents
{
    public sealed class TeamJoinedEvent : DomainEvent
    {
        public TeamJoinedEvent(Guid aggregateId, int version, string jsonPayload) : base("TeamJoinedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
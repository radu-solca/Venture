using System;
using Venture.Common.Events;

namespace Venture.TeamRead.Data.DomainEvents
{
    public sealed class TeamLeftEvent : DomainEvent
    {
        public TeamLeftEvent(Guid aggregateId, int version, string jsonPayload) : base((string) "TeamLeftEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
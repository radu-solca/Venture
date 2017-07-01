using System;
using Venture.Common.Events;

namespace Venture.TeamRead.Data.DomainEvents
{
    public sealed class TeamUserApprovedEvent : DomainEvent
    {
        public TeamUserApprovedEvent(Guid aggregateId, int version, string jsonPayload) : base((string) "TeamUserApprovedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
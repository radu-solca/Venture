﻿using System;
using Venture.Common.Events;

namespace Venture.TeamWrite.Domain.DomainEvents
{
    public sealed class TeamCommentPostedEvent : DomainEvent
    {
        public TeamCommentPostedEvent(Guid aggregateId, int version, string jsonPayload) : base((string) "TeamCommentPostedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
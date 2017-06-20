﻿using System;
using Venture.Common.Events;

namespace Venture.ProjectWrite.Domain.DomainEvents
{
    public sealed class ProjectTagsUpdatedEvent : DomainEvent
    {
        public ProjectTagsUpdatedEvent(Guid aggregateId, int version, string jsonPayload) : base("ProjectTagsUpdatedEvent", aggregateId, version, jsonPayload)
        {
        }
    }
}
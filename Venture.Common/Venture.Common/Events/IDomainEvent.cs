using System;

namespace Venture.Common.Events
{
    public interface IDomainEvent
    {
        Guid Id { get; }

        string Type { get; }
        DateTime OccuredAt { get; }
        Guid AggregateId { get; }
    }
}
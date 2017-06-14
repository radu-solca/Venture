using System;

namespace Venture.Common.Events
{
    public interface IDomainEvent
    {
        string Type { get; }
        DateTime OccuredAt { get; }
    }
}
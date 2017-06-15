using System;
using System.Collections.Generic;
using Venture.Common.Events;

namespace Venture.Common.Data
{
    interface IAggregate
    {
        Guid Id { get; }

        DateTime CreatedAt { get; }
        DateTime UpdatedAt { get; }

        IEnumerable<IDomainEvent> Events { get; }
    }
}

using System.Collections.Generic;
using Venture.Common.Events;

namespace Venture.Common.Data.Interaces
{
    public interface IEventStoreable
    {
        int Version { get; }
        IList<DomainEvent> UncommitedChanges { get; }
        void MarkChangesAsCommited();
        void LoadFromHistory(IEnumerable<DomainEvent> history);
    }
}
using System.Collections.Generic;
using Venture.Common.Events;

namespace Venture.Common.Data.Interfaces
{
    public interface IEventStoreable
    {
        int Version { get; }
        IList<DomainEvent> UncommitedChanges { get; }
        void MarkChangesAsCommited();
        void LoadFromHistory(IEnumerable<DomainEvent> history);
        void Delete();
    }
}
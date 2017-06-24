using System.Collections.Generic;
using Venture.Common.Events;

namespace Venture.Common.Data.Interfaces
{
    public interface IEventStoreable
    {
        /// <summary>
        /// Gets the version of the last event that changed the event storeable's state.
        /// </summary>
        int Version { get; }

        /// <summary>
        /// Gets a list of events that changed this event storeables state,
        /// but were not yet marked as commited.
        /// </summary>
        IList<DomainEvent> UncommitedChanges { get; }

        /// <summary>
        /// Mark all uncomitted changes as commited. 
        /// </summary>
        void MarkChangesAsCommited();

        /// <summary>
        /// Load the event storeable object from a history of events.
        /// </summary>
        /// <param name="history"></param>
        void LoadFromHistory(IEnumerable<DomainEvent> history);

        /// <summary>
        /// Delete the event storeable object.
        /// </summary>
        void Delete();
    }
}
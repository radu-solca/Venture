using System;
using System.Collections.Generic;
using System.Linq;
using LiteGuard;
using RawRabbit;
using Venture.Common.Data.Interfaces;
using Venture.Common.Events;
using Venture.Common.Extensions;

namespace Venture.Common.Data
{
    public abstract class EventRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, IEventStoreable, new()
    {
        private readonly IEventStore _eventStore;
        private readonly IBusClient _bus;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="eventStore">An event store.</param>
        /// <param name="bus">A bus client.</param>
        protected EventRepository(IEventStore eventStore, IBusClient bus)
        {
            Guard.AgainstNullArgument(nameof(eventStore), eventStore);
            Guard.AgainstNullArgument(nameof(bus), bus);

            _eventStore = eventStore;
            _bus = bus;
        }

        /// <summary>
        /// Gets all entities
        /// </summary>
        /// <returns></returns>
        public ICollection<TEntity> Get()
        {
            var history = _eventStore.GetEvents();

            var sortedHistory = new Dictionary<Guid, List<DomainEvent>>();
            foreach (var domainEvent in history)
            {
                if (! sortedHistory.ContainsKey(domainEvent.AggregateId))
                {
                    sortedHistory.Add(domainEvent.AggregateId, new List<DomainEvent>());
                }

                sortedHistory[domainEvent.AggregateId].Add(domainEvent);
            }

            var results = new List<TEntity>();
            foreach (var individualHistory in sortedHistory.Values)
            {
                var entity = new TEntity();
                entity.LoadFromHistory(individualHistory);
                results.Add(entity);
            }

            return results;
        }

        /// <summary>
        /// Gets entity with the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public TEntity Get(Guid id)
        {
            var entity = new TEntity();

            var history = _eventStore.GetEvents(id).OrderBy(e => e.Version);

            entity.LoadFromHistory(history);

            if (entity.Id != id)
            {
                // no entity found with specified id
                return null;
            }

            return entity;
        }

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(TEntity entity)
        {
            Guard.AgainstNullArgument(nameof(entity), entity);
            Update(entity);
        }

        /// <summary>
        /// Deletes the entity with the specified id.
        /// </summary>
        /// <param name="id">The id.</param>
        public void Delete(Guid id)
        {
            var entity = Get(id);
            entity.Delete();
            Update(entity);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(TEntity entity)
        {
            Guard.AgainstNullArgument(nameof(entity), entity);

            var uncommitedChanges = entity.UncommitedChanges;

            foreach (var change in uncommitedChanges)
            {
                _eventStore.Raise(change);
                _bus.PublishEvent(change);
            }

            entity.MarkChangesAsCommited();
        }
    }
}

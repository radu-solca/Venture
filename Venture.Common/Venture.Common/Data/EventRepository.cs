using System;
using System.Collections.Generic;
using System.Linq;
using RawRabbit;
using Venture.Common.Data.Interaces;
using Venture.Common.Events;
using Venture.Common.Extensions;

namespace Venture.Common.Data
{
    public abstract class EventRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity, IEventStoreable, new()
    {
        private readonly IEventStore _eventStore;
        private readonly IBusClient _bus;

        protected EventRepository(IEventStore eventStore, IBusClient bus)
        {
            _eventStore = eventStore;
            _bus = bus;
        }

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

        public TEntity Get(Guid id)
        {
            var entity = new TEntity();

            var history = _eventStore.GetEvents(id).OrderBy(e => e.Version);

            entity.LoadFromHistory(history);

            return entity;
        }

        public void Add(TEntity entity)
        {
            Update(entity);
        }

        public void Delete(Guid id)
        {
            var entity = Get(id);
            entity.Delete();
            Update(entity);
        }

        public void Update(TEntity aggregate)
        {
            var uncommitedChanges = aggregate.UncommitedChanges;

            foreach (var change in uncommitedChanges)
            {
                _eventStore.Raise(change);
                _bus.PublishEvent(change);
            }

            aggregate.MarkChangesAsCommited();
        }
    }
}

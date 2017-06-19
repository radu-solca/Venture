using System;
using System.Linq;
using RawRabbit;
using Venture.Common.Events;
using Venture.Common.Extensions;

namespace Venture.Common.Data
{
    public abstract class Repository<TAggregate> where TAggregate : AggregateRoot, new()
    {
        private readonly IEventStore _eventStore;
        private readonly IBusClient _bus;

        protected Repository(IEventStore eventStore, IBusClient bus)
        {
            _eventStore = eventStore;
            _bus = bus;
        }

        public TAggregate Get(Guid id)
        {
            var aggregate = new TAggregate();

            var history = _eventStore.GetEvents(id).OrderBy(e => e.Version);

            aggregate.LoadFromHistory(history);

            return aggregate;
        }

        public void Update(TAggregate aggregate)
        {
            var uncommitedChanges = aggregate.UncommitedChanges;

            foreach (var change in uncommitedChanges)
            {
                _eventStore.Raise(change);
                _bus.PublishEvent(change);
            }
        }
    }
}

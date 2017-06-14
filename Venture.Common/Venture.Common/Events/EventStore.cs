using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiteGuard;
using Newtonsoft.Json;
using RawRabbit;
using StackExchange.Redis;

namespace Venture.Common.Events
{
    public class EventStore : IEventStore
    {
        private readonly IBusClient _bus;
        private readonly IDatabaseAsync _db;
        private readonly string _dbname;

        public EventStore(IBusClient bus, string connectionString, string dbname)
        {
            Guard.AgainstNullArgument(nameof(bus), bus);
            Guard.AgainstNullArgument(nameof(connectionString), connectionString);
            Guard.AgainstNullArgument(nameof(dbname), dbname);

            _bus = bus;
            _db = ConnectionMultiplexer.Connect(connectionString).GetDatabase();
            _dbname = dbname;
        }

        public async Task<IEnumerable<IDomainEvent>> GetEventsAsync(
            DateTime startDate,
            DateTime endDate)
        {
            var eventsRedis = await _db.ListRangeAsync(_dbname + ".events");

            List<IDomainEvent> events = new List<IDomainEvent>();

            foreach (var eventJson in eventsRedis)
            {
                IDomainEvent domainEvent = Deserialize(eventJson);

                if (domainEvent.OccuredAt >= startDate &&
                    domainEvent.OccuredAt <= endDate)
                {
                    events.Add(domainEvent);
                }
            }

            return events;
        }

        public Task<IEnumerable<IDomainEvent>> GetEventsAsync()
        {
            return GetEventsAsync(DateTime.MinValue, DateTime.MaxValue);
        }

        public async Task RaiseAsync(IDomainEvent domainEvent)
        {
            var eventJson = Serialize(domainEvent);

            await _db.ListRightPushAsync(_dbname + ".events", eventJson, flags: CommandFlags.FireAndForget);

            // TODO: Find a better place to publish events.
            await _bus.PublishAsync(
                domainEvent,
                configuration: config =>
                {
                    config.WithExchange(exchange => exchange.WithName("Venture." + _dbname + ".Events"));
                    config.WithRoutingKey(_dbname + ".Event");
                }
            );
        }

        private string Serialize(IDomainEvent eventToSerialize)
        {
            return JsonConvert.SerializeObject(eventToSerialize);
        }

        private IDomainEvent Deserialize(string setializedEvent)
        {
            var result = JsonConvert.DeserializeObject<IDomainEvent>(setializedEvent);

            return result;
        }
    }
}

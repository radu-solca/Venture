using System;
using System.Collections.Generic;
using System.Linq;
using LiteGuard;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Venture.Common.Events
{
    public class EventStore : IEventStore
    {
        private readonly IDatabaseAsync _db;
        private readonly string _dbname;

        public EventStore(string connectionString, string dbname)
        {
            Guard.AgainstNullArgument(nameof(connectionString), connectionString);
            Guard.AgainstNullArgument(nameof(dbname), dbname);

            _db = ConnectionMultiplexer.Connect(connectionString).GetDatabase();
            _dbname = dbname;
        }

        public IEnumerable<DomainEvent> GetEvents(
            Guid aggregateId, 
            int startVersion = Int32.MinValue, 
            int endVersion = Int32.MaxValue)
        {
            return GetEvents(startVersion, endVersion).Where(e => e.AggregateId == aggregateId);
        }

        public IEnumerable<DomainEvent> GetEvents(
            int startVersion = Int32.MinValue,
            int endVersion = Int32.MaxValue)
        {
            var eventsRedis = _db.ListRangeAsync(_dbname + ".Events").Result;

            List<DomainEvent> events = new List<DomainEvent>();

            foreach (var eventJson in eventsRedis)
            {
                DomainEvent domainEvent = Deserialize(eventJson);

                if (domainEvent.Version >= startVersion &&
                    domainEvent.Version <= endVersion)
                {
                    events.Add(domainEvent);
                }
            }

            return events;
        }

        public void Raise(DomainEvent domainEvent)
        {
            var eventJson = Serialize(domainEvent);
            _db.ListRightPushAsync(_dbname + ".Events", eventJson, flags: CommandFlags.FireAndForget);
        }

        private string Serialize(DomainEvent eventToSerialize)
        {
            return JsonConvert.SerializeObject(eventToSerialize);
        }

        private DomainEvent Deserialize(string setializedEvent)
        {
            var result = JsonConvert.DeserializeObject<DomainEvent>(setializedEvent);

            return result;
        }
    }
}

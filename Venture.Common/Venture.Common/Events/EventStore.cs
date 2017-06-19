using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<DomainEvent>> GetEventsAsync(
            DateTime startDate,
            DateTime endDate)
        {
            var eventsRedis = await _db.ListRangeAsync(_dbname + ".Events");

            List<DomainEvent> events = new List<DomainEvent>();

            foreach (var eventJson in eventsRedis)
            {
                DomainEvent domainEvent = Deserialize(eventJson);

                if (domainEvent.OccuredAt >= startDate &&
                    domainEvent.OccuredAt <= endDate)
                {
                    events.Add(domainEvent);
                }
            }

            return events;
        }

        public Task<IEnumerable<DomainEvent>> GetEventsAsync()
        {
            return GetEventsAsync(DateTime.MinValue, DateTime.MaxValue);
        }

        public async Task RaiseAsync(DomainEvent domainEvent)
        {
            var eventJson = Serialize(domainEvent);

            await _db.ListRightPushAsync(_dbname + ".Events", eventJson, flags: CommandFlags.FireAndForget);

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

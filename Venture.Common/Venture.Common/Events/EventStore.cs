using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            _dbname = dbname;
            _db = ConnectionMultiplexer.Connect(connectionString).GetDatabase();
        }

        public async Task<IEnumerable<DomainEvent>> GetEventsAsync(
            long firstEventSequenceNumber = 0, 
            long lastEventSequenceNumber = long.MaxValue)
        {
            var eventsRedis = await _db.ListRangeAsync(_dbname + ".events");

            List<DomainEvent> events = new List<DomainEvent>();

            foreach (var eventJson in eventsRedis)
            {
                DomainEvent domainEvent = Deserialize(eventJson);

                if (domainEvent.SequenceNumber >= firstEventSequenceNumber &&
                    domainEvent.SequenceNumber <= lastEventSequenceNumber)
                {
                    events.Add(domainEvent);
                }
            }

            return events;
        }

        public async Task RaiseAsync(string type, object content)
        {
            var nextSequenceNumberRedis = await _db.StringGetAsync(_dbname + ".nextSequenceNumber");

            await _db.StringIncrementAsync(_dbname + ".nextSequenceNumber", flags: CommandFlags.FireAndForget);

            long nextSequenceNumber;
            if (!nextSequenceNumberRedis.TryParse(out nextSequenceNumber))
            {
                throw new Exception("nextSequenceNumber missing or invalid.");
            }

            var domainEvent = new DomainEvent(type, nextSequenceNumber, DateTime.Now, content);
            var eventJson = Serialize(domainEvent);

            await _db.ListRightPushAsync(_dbname + ".events", eventJson, flags: CommandFlags.FireAndForget);
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

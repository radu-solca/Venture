using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Venture.ProfileWrite.Data.Events
{
    public class EventStore : IEventStore
    {
        private readonly IDatabaseAsync _db;

        public EventStore()
        {
            _db = ConnectionMultiplexer.Connect("localhost").GetDatabase();
        }

        public async Task<IEnumerable<Event>> GetEvents(long firstEventSequenceNumber = 0, long lastEventSequenceNumber = long.MaxValue)
        {
            var eventsRedis = await _db.ListRangeAsync("profile.events");

            List<Event> events = new List<Event>();

            foreach (var eventJson in eventsRedis)
            {
                Event domainEvent = (Event) Deserialize(eventJson);

                if (domainEvent.SequenceNumber >= firstEventSequenceNumber &&
                    domainEvent.SequenceNumber <= lastEventSequenceNumber)
                {
                    events.Add(domainEvent);
                }
            }

            return events;
        }

        public async Task Raise(string eventName, object content)
        {
            var nextSequenceNumberRedis = await _db.StringGetAsync("profile.events.nextSequenceNumber");

            await _db.StringIncrementAsync("profile.events.nextSequenceNumber", flags: CommandFlags.FireAndForget);

            long nextSequenceNumber;
            if (!nextSequenceNumberRedis.TryParse(out nextSequenceNumber))
            {
                throw new Exception("nextSequenceNumber missing or invalid.");
            }

            var domainEvent = new Event(nextSequenceNumber, DateTime.Now, eventName, content);
            var eventJson = Serialize(domainEvent);

            await _db.ListRightPushAsync("profile.events", eventJson, flags: CommandFlags.FireAndForget);
        }

        private string Serialize(Event eventToSerialize)
        {
            return JsonConvert.SerializeObject(eventToSerialize);
        }

        private Event Deserialize(string setializedEvent)
        {
            var result = JsonConvert.DeserializeObject<Event>(setializedEvent);
            return result;
        }
    }
}

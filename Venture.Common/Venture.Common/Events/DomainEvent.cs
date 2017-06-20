using System;

namespace Venture.Common.Events
{
    public class DomainEvent
    {
        public Guid Id { get; }

        public string Type { get; }
        public DateTime OccuredOn { get; }

        public Guid AggregateId { get; }
        public int Version { get; }

        public string JsonPayload { get; }

        public DomainEvent(string type, Guid aggregateId, int version, string jsonPayload)
        {
            if (version <= 0)
            {
                throw new ArgumentException("Version number must be at least 1. Got " + version);
            }

            Type = type;

            Id = Guid.NewGuid();
            OccuredOn = DateTime.Now;

            AggregateId = aggregateId;
            Version = version;

            JsonPayload = jsonPayload;
        }
    }
}
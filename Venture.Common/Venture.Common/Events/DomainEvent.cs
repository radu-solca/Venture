using System;

namespace Venture.Common.Events
{
    public sealed class DomainEvent
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

            Id = Guid.NewGuid();
            OccuredOn = DateTime.Now;

            Type = type;
            AggregateId = aggregateId;
            Version = version;

            JsonPayload = jsonPayload;
        }
    }
}
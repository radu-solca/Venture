using System;

namespace Venture.Common.Events
{
    public class DomainEvent
    {
        public string Type { get; }
        public long SequenceNumber { get; }
        public DateTime OccuredAt { get; }
        public object Content { get; }

        public DomainEvent(
            string type,
            long sequenceNumber,
            DateTime occuredAt,
            object content)
        {
            Type = type;
            SequenceNumber = sequenceNumber;
            OccuredAt = occuredAt;
            Content = content;
        }
    }
}
using System;

namespace Venture.ProfileWrite.Data.Events
{
    public class Event
    {
        public long SequenceNumber { get; }
        public DateTime OccuredAt { get; }
        public string Name { get; }
        public object Content { get; }

        public Event(
            long sequenceNumber,
            DateTime occuredAt,
            string name,
            object content)
        {
            SequenceNumber = sequenceNumber;
            OccuredAt = occuredAt;
            Name = name;
            Content = content;
        }
    }
}

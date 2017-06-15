using System;
using Venture.Common.Events;

namespace Venture.ProfileRead.Business.Events
{
    public class ProfileCreatedEvent : DomainEvent
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ProfileCreatedEvent(Guid aggregateId) : base(aggregateId)
        {
        }
    }
}
using System;
using RawRabbit;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Events;
using Venture.Common.Extensions;
using Venture.ProfileWrite.Business.Commands;

namespace Venture.ProfileWrite.Business.CommandHandlers
{
    public class CreateProfileComandHandler : ICommandHandler<CreateProfileCommand>
    {
        private readonly IEventStore _eventStore;
        private readonly IBusClient _bus;

        public CreateProfileComandHandler(IEventStore eventStore, IBusClient bus)
        {
            _eventStore = eventStore;
            _bus = bus;
        }

        public void Execute(CreateProfileCommand command)
        {
            var profileCreatedEvent = new ProfileCreatedEvent(Guid.NewGuid())
            {
                Email = command.Email,
                LastName = command.LastName,
                FirstName = command.FirstName
            };

            _eventStore.RaiseAsync(profileCreatedEvent);
            _bus.PublishEvent(profileCreatedEvent, "Profile");
        }
    }

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

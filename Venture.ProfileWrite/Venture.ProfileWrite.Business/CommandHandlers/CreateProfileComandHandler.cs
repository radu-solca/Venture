using Venture.Common.Cqrs.Commands;
using Venture.Common.Events;
using Venture.ProfileWrite.Business.Commands;

namespace Venture.ProfileWrite.Business.CommandHandlers
{
    public class CreateProfileComandHandler : ICommandHandler<CreateProfileCommand>
    {
        private readonly IEventStore _eventStore;

        public CreateProfileComandHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void Execute(CreateProfileCommand command)
        {
            var profileCreatedEvent = new ProfileCreatedEvent()
            {
                Email = command.Email,
                LastName = command.LastName,
                FirstName = command.FirstName
            };

            _eventStore.RaiseAsync(profileCreatedEvent);
        }
    }

    public class ProfileCreatedEvent : BaseDomainEvent
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

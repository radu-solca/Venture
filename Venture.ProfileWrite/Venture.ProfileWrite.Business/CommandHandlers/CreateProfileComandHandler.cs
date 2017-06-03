using RawRabbit;
using Venture.Common.Cqrs.Commands;
using Venture.Common.Events;
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
            _eventStore.RaiseAsync("ProfileCreatedEvent", command);
        }
    }
}

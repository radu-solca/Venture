using Venture.Common.Cqrs.Commands;
using Venture.ProfileWrite.Business.Commands;
using Venture.ProfileWrite.Data.Events;

namespace Venture.ProfileWrite.Business.CommandHandlers
{
    public class CreateProfileComandHandler : ICommandHandler<CreateProfileCommand>
    {
        private readonly IEventStore _eventStore;

        public CreateProfileComandHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public void ExecuteAsync(CreateProfileCommand command)
        {
            _eventStore.Raise("ProfileCreated", command.UserProfile);
        }
    }
}

using LiteGuard;
using Venture.ProfileWrite.Business.Commands;
using Venture.ProfileWrite.Data.Entities;
using Venture.ProfileWrite.Data.Events;
using Venture.ProfileWrite.Data.Repositories;

namespace Venture.ProfileWrite.Business.CommandHandlers
{
    public class CreateProfileComandHandler : ICommandHandler<CreateProfileCommand>
    {
        private readonly EventStore _eventStore;
        private readonly IRepository<Profile> _profileRepository;

        public CreateProfileComandHandler(EventStore eventStore, IRepository<Profile> profileRepository)
        {
            Guard.AgainstNullArgument(nameof(eventStore), eventStore);
            Guard.AgainstNullArgument(nameof(profileRepository), profileRepository);

            _eventStore = eventStore;
            _profileRepository = profileRepository;
        }

        public async void ExecuteAsync(CreateProfileCommand command)
        {
            Guard.AgainstNullArgument(nameof(command), command);



            await _eventStore.Raise("ProfileCreated", command.Profile);
        }
    }
}

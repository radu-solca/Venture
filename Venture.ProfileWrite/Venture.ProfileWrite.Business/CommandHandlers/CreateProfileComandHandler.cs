using AutoMapper;
using LiteGuard;
using Venture.ProfileWrite.Business.Commands;
using Venture.ProfileWrite.Business.Models;
using Venture.ProfileWrite.Data.Entities;
using Venture.ProfileWrite.Data.Events;
using Venture.ProfileWrite.Data.Repositories;

namespace Venture.ProfileWrite.Business.CommandHandlers
{
    public class CreateProfileComandHandler : ICommandHandler<CreateProfileCommand>
    {
        private readonly EventStore _eventStore;
        private readonly IRepository<UserProfile> _profileRepository;
        private readonly IMapper _mapper;

        public CreateProfileComandHandler(
            EventStore eventStore, 
            IRepository<UserProfile> profileRepository,
            IMapper mapper)
        {
            Guard.AgainstNullArgument(nameof(eventStore), eventStore);
            Guard.AgainstNullArgument(nameof(profileRepository), profileRepository);

            _eventStore = eventStore;
            _profileRepository = profileRepository;
            _mapper = mapper;
        }

        public async void ExecuteAsync(CreateProfileCommand command)
        {
            //Guard.AgainstNullArgument(nameof(command), command);

            //var profile = _mapper.Map<UserProfileCreateModel, UserProfile>(command.UserProfile);
            //var profileViewModel = _mapper.Map<UserProfile, UserProfileViewModel>(profile);

            //_profileRepository.Add(profile);

            //await _eventStore.Raise("ProfileCreated", profileViewModel);
        }
    }
}

using Venture.ProfileWrite.Business.Models;

namespace Venture.ProfileWrite.Business.Commands
{
    public class CreateProfileCommand : ICommand
    {
        public ProfileCreateModel Profile { get; }

        public CreateProfileCommand(ProfileCreateModel profile)
        {
            Profile = profile;
        }
    }
}

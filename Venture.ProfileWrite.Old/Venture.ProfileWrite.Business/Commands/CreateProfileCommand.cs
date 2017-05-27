//using Venture.ProfileWrite.Business.Models;

using Venture.ProfileWrite.Business.Models;

namespace Venture.ProfileWrite.Business.Commands
{
    public class CreateProfileCommand : ICommand
    {
        public UserProfileCreateModel UserProfile { get; }

        public CreateProfileCommand(UserProfileCreateModel userProfile)
        {
            UserProfile = userProfile;
        }
    }
}

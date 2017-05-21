using System.Linq;
using FluentValidation;
using Venture.ProfileWrite.Business.Models;
using Venture.ProfileWrite.Data.Entities;
using Venture.ProfileWrite.Data.Repositories;

namespace Venture.ProfileWrite.Business.ModelValidators
{
    public class ProfileCreateModelValidator : AbstractValidator<UserProfileCreateModel>
    {
        public ProfileCreateModelValidator(IRepository<UserProfile> profileRepository)
        {
            // Check if email is unique;
            RuleFor(p => p.Email).Must(email =>
            {
                var emailExists = profileRepository
                .GetAll()
                .Any(
                    profile => !profile.Deleted && 
                    profile.Email == email
                    );

                return !emailExists;
            });
        }
    }
}

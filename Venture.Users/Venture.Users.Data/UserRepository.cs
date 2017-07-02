using Venture.Common.Data;

namespace Venture.Users.Data
{
    public class UserRepository : EFRepository<User>
    {
        public UserRepository(UsersContext context) : base(context)
        {
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace Venture.Users.Data
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(UsersContext context) : base(context)
        {
        }
    }
}

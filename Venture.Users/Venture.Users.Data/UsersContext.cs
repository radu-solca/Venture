using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Venture.Users.Data
{
    public class UsersContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        { }
    }
}

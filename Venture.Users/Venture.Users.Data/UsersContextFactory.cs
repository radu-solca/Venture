using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Venture.Users.Data
{
    public class UsersContextFactory : IDbContextFactory<UsersContext>
    {
        public UsersContext Create(DbContextFactoryOptions options)
        {
            var dbOptions = new DbContextOptionsBuilder<UsersContext>()
                .UseSqlServer("Server=NASA_PC_2-0-1\\SQLEXPRESS;Database=Venture.Users.Store;Trusted_Connection=True;")
                .Options;
            return new UsersContext(dbOptions);
        }
    }
}
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace Venture.Users.Data
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        public UsersContext()
        {
            // TODO: remove this constructor after you figure out how to not hardcode conn strings.
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source =.\SQLEXPRESS; Initial Catalog = Venture.Users.UserStore; Integrated Security = True; MultipleActiveResultSets = True"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: move this in its own class.
            modelBuilder.Entity<User>().HasKey(user => user.Id);
        }
    }
}

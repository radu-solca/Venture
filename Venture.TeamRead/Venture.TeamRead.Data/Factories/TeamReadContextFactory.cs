
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Venture.TeamRead.Data.Factories
{
    /// <summary>
    /// This factory exists only so entity framework can create its context to update the database.
    /// Unfortunately, EF doesn't recognise AddDbContext calls unless they're on an ASP.NET project.
    /// </summary>
    internal class TeamReadContextFactory : IDbContextFactory<TeamReadContext>
    {
        public TeamReadContext Create(DbContextFactoryOptions options)
        {
            var dbOptions = new DbContextOptionsBuilder<TeamReadContext>()
                .UseSqlServer("Server=NASA_PC_2-0-1\\SQLEXPRESS;Database=Venture.TeamRead.Store;Trusted_Connection=True;")
                .Options;
            return new TeamReadContext(dbOptions);
        }
    }
}

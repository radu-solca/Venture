using Microsoft.EntityFrameworkCore;
using Venture.ProjectRead.Data.Entities;

namespace Venture.ProjectRead.Data
{
    public sealed class ProjectReadContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(
                "Server=NASA_PC_2-0-1\\SQLEXPRESS;Database=Venture.ProjectRead.Store;Trusted_Connection=True;");
        }
    }
}

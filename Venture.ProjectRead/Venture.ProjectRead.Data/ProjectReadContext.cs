using Microsoft.EntityFrameworkCore;
using Venture.ProjectRead.Data.Entities;

namespace Venture.ProjectRead.Data
{
    public sealed class ProjectReadContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ProjectReadContext(DbContextOptions<ProjectReadContext> options)
            : base(options)
        { }
    }
}

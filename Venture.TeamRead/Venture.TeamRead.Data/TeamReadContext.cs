using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Venture.TeamRead.Data.Entities;

namespace Venture.TeamRead.Data
{
    public sealed class TeamReadContext : DbContext
    {
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMembership> TeamMemberships { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public TeamReadContext(DbContextOptions<TeamReadContext> options)
            : base(options)
        { }
    }
}

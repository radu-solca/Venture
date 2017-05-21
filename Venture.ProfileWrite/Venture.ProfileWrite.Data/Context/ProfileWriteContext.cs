using Microsoft.EntityFrameworkCore;
using Venture.ProfileWrite.Data.Entities;

namespace Venture.ProfileWrite.Data.Context
{
    public class ProfileWriteContext : DbContext
    {
        private DbSet<Profile> Profiles { get; set; }
    }
}

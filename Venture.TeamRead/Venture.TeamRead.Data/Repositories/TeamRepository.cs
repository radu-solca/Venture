using Venture.Common.Data;
using Venture.TeamRead.Data.Entities;

namespace Venture.TeamRead.Data
{
    public class TeamRepository : EFRepository<Team>
    {
        public TeamRepository(TeamReadContext context) : base(context)
        {
        }
    }
}

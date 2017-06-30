using System;
using System.Collections.Generic;
using System.Linq;
using Venture.Common.Data;
using Venture.TeamRead.Data.Entities;

namespace Venture.TeamRead.Data
{
    public class TeamMembershipRepository : EFRepository<TeamMembership>
    {
        public TeamMembershipRepository(TeamReadContext context) : base(context)
        {
        }

        public ICollection<TeamMembership> GetByTeamId(Guid teamId)
        {
            return Get().Where(c => c.TeamId == teamId).ToList();
        }
    }
}
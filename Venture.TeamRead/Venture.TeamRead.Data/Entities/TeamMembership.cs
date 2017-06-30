using System;
using Venture.Common.Data.Interfaces;

namespace Venture.TeamRead.Data.Entities
{
    public class TeamMembership : IEntity
    {
        public Guid Id { get; set; }

        public Guid TeamId { get; set; }
        public Team Team { get; set; }

        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public bool Approved { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Venture.Common.Data;
using Venture.TeamRead.Data.Entities;

namespace Venture.TeamRead.Data
{
    public class CommentRepository : EFRepository<Comment>
    {
        public CommentRepository(TeamReadContext context) : base(context)
        {
        }

        public ICollection<Comment> GetByTeamId(Guid teamId)
        {
            return Get().Where(c => c.TeamId == teamId).ToList();
        }
    }
}
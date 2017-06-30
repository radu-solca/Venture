using System;
using System.Collections.Generic;
using System.Linq;
using Venture.Common.Data;
using Venture.ProjectRead.Data.Entities;

namespace Venture.ProjectRead.Data
{
    public sealed class CommentRepository : EFRepository<Comment>
    {
        public CommentRepository(ProjectReadContext context) : base(context)
        {
        }

        public ICollection<Comment> GetByProjectId(Guid projectId)
        {
            return Get().Where(c => c.ProjectId == projectId).ToList();
        }
    }
}
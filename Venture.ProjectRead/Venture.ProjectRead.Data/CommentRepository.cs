using Venture.Common.Data;
using Venture.ProjectRead.Data.Entities;

namespace Venture.ProjectRead.Data
{
    public sealed class CommentRepository : EFRepository<Comment>
    {
        public CommentRepository(ProjectReadContext context) : base(context)
        {
        }
    }
}
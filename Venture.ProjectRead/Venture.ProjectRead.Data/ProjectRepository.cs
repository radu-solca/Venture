using Venture.Common.Data;
using Venture.ProjectRead.Data.Entities;

namespace Venture.ProjectRead.Data
{
    public sealed class ProjectRepository : EFRepository<Project>
    {
        public ProjectRepository(ProjectReadContext context) : base(context)
        {
        }
    }
}

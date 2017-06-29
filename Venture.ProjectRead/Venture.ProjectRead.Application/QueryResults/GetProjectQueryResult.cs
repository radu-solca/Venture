using Venture.ProjectRead.Data.Entities;

namespace Venture.ProjectRead.Application.QueryResults
{
    public sealed class GetProjectQueryResult
    {
        public Project Project { get; }

        public GetProjectQueryResult(Project project)
        {
            Project = project;
        }
    }
}
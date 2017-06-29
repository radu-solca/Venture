using Venture.Gateway.Business.Models;

namespace Venture.Gateway.Business.QueryResults
{
    public sealed class GetProjectQueryResult
    {
        public ProjectViewModel Project { get; }

        public GetProjectQueryResult(ProjectViewModel project)
        {
            Project = project;
        }
    }
}
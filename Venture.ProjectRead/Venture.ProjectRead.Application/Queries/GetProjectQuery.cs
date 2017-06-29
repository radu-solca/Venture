using System;
using Venture.Common.Cqrs.Queries;

namespace Venture.ProjectRead.Application.Queries
{
    public sealed class GetProjectQuery : IQuery
    {
        public Guid ProjectId { get; }

        public GetProjectQuery(Guid projectId)
        {
            ProjectId = projectId;
        }
    }
}

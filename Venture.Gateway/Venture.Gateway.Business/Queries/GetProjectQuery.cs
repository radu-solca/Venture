using System;
using Venture.Common.Cqrs.Queries;
using Venture.Gateway.Business.QueryResults;

namespace Venture.Gateway.Business.Queries
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

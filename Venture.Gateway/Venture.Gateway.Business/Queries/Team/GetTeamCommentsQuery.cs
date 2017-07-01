using System;
using Venture.Common.Cqrs.Queries;

namespace Venture.Gateway.Business.Queries
{
    public class GetTeamCommentsQuery : IQuery
    {
        public GetTeamCommentsQuery(Guid projectId)
        {
            ProjectId = projectId;
        }

        public Guid ProjectId { get; }
    }
}
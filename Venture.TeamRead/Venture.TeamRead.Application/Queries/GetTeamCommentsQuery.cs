using System;
using Venture.Common.Cqrs.Queries;

namespace Venture.TeamRead.Application.Queries
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
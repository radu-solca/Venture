using System;
using Venture.Common.Cqrs.Queries;

namespace Venture.TeamRead.Application.Queries
{
    public class GetTeamUsersQuery : IQuery
    {
        public GetTeamUsersQuery(Guid projectId)
        {
            ProjectId = projectId;
        }

        public Guid ProjectId { get; }
    }
}

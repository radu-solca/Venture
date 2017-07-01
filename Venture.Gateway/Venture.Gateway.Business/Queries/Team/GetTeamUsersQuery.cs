using System;
using Venture.Common.Cqrs.Queries;

namespace Venture.Gateway.Business.Queries
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

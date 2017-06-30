using System;
using Venture.Common.Cqrs.Queries;

namespace Venture.Gateway.Service.Controllers
{
    public class GetProjectCommentsQuery : IQuery
    {
        public GetProjectCommentsQuery(Guid projectId)
        {
            ProjectId = projectId;
        }

        public Guid ProjectId { get; }
    }
}
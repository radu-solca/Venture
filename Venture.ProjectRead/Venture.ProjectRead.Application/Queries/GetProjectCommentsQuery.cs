using System;
using Venture.Common.Cqrs.Queries;

namespace Venture.ProjectRead.Application.Queries
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
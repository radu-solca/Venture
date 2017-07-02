using System;
using Venture.Common.Cqrs.Queries;

namespace Venture.Gateway.Business.Queries
{
    public class GetUserQuery : IQuery
    {
        public GetUserQuery(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}

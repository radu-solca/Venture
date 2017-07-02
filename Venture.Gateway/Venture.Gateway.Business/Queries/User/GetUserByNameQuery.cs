using Venture.Common.Cqrs.Queries;

namespace Venture.Gateway.Business.Queries
{
    public class GetUserByNameQuery : IQuery
    {
        public GetUserByNameQuery(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}
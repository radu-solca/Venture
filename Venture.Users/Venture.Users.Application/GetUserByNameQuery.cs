using Venture.Common.Cqrs.Queries;

namespace Venture.Users.Application
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
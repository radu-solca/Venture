using System.Linq;
using Newtonsoft.Json;
using Venture.Common.Cqrs.Queries;
using Venture.Common.Data;
using Venture.Users.Data;

namespace Venture.Users.Application
{
    public class GetUserByNameQueryHandler : IQueryHandler<GetUserByNameQuery>
    {
        private readonly IRepository<User> _useRepository;

        public GetUserByNameQueryHandler(IRepository<User> useRepository)
        {
            _useRepository = useRepository;
        }

        public string Handle(GetUserByNameQuery query)
        {
            var user = _useRepository.Get().FirstOrDefault(u => u.UserName == query.UserName);
            return JsonConvert.SerializeObject(user);
        }
    }
}
using Newtonsoft.Json;
using Venture.Common.Cqrs.Queries;
using Venture.Common.Data;
using Venture.Users.Data;

namespace Venture.Users.Application
{
    public class GetUserQueryHandler : IQueryHandler<GetUserQuery>
    {
        private readonly IRepository<User> _useRepository;

        public GetUserQueryHandler(IRepository<User> useRepository)
        {
            _useRepository = useRepository;
        }

        public string Handle(GetUserQuery query)
        {
            var user = _useRepository.Get(query.UserId);
            return JsonConvert.SerializeObject(user);
        }
    }
}
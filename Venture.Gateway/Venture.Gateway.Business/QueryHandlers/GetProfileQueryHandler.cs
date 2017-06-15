using RawRabbit;
using Venture.Common.Cqrs.Queries;
using Venture.Common.Extensions;
using Venture.Gateway.Business.Queries;

namespace Venture.Gateway.Business.QueryHandlers
{
    public class GetProfileQueryHandler : IQueryHandler<GetProfileQuery, string>
    {
        private readonly IBusClient _bus;

        public GetProfileQueryHandler(IBusClient bus)
        {
            _bus = bus;
        }

        public string Retrieve(GetProfileQuery query)
        {
            return _bus.Query<GetProfileQuery, string>(query);
        }
    }
}

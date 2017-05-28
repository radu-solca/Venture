using System.Threading.Tasks;
using LiteGuard;
using RawRabbit;
using Venture.Gateway.Business.Queries;

namespace Venture.Gateway.Business.QueryDispatcher
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IBusClient _bus;

        public QueryDispatcher(IBusClient bus)
        {
            _bus = bus;
        }

        public async Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query)
        {
            Guard.AgainstNullArgument(nameof(query), query);

            return await _bus.RequestAsync<IQuery<TResult>, TResult>(query);
        }
    }
}

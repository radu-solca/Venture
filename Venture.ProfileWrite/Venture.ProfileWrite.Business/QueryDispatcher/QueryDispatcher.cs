using System;
using System.Threading.Tasks;
using LiteGuard;
using Venture.ProfileWrite.Business.Queries;
using Venture.ProfileWrite.Business.QueryHandlers;

namespace Venture.ProfileWrite.Business.QueryDispatcher
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider serviceProvider)
        {
            Guard.AgainstNullArgument(nameof(serviceProvider), serviceProvider);
            _serviceProvider = serviceProvider;
        }

        public Task<TResult> Handle<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>
        {
            Guard.AgainstNullArgument(nameof(query), query);

            IQueryHandler<TQuery, TResult> handler = (IQueryHandler<TQuery, TResult>)_serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResult>));

            if (handler == null)
            {
                throw new Exception("Query handler not found for type " + typeof(IQueryHandler<TQuery, TResult>));
            }

            return handler.Retrieve(query);
        }

        public Task<TResult> Handle<TResult>(IQuery<TResult> query)
        {
            return Handle<IQuery<TResult>, TResult>(query);
        }
    }
}

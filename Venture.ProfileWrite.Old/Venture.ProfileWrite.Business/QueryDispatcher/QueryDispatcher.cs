using System;
using System.Reflection;
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

        public TResult Handle<TResult>(IQuery<TResult> query)
        {
            Guard.AgainstNullArgument(nameof(query), query);

            Type parameterType = query.GetType();
            Type resultType = typeof(TResult);
            Type[] types = { parameterType, resultType };

            Type listType = typeof(IQueryHandler<,>);
            Type queryType = listType.MakeGenericType(types);

            object queryHandler = _serviceProvider.GetService(queryType);

            if (queryHandler == null)
            {
                throw new Exception("Query handler not found for type " + queryType);
            }

            MethodInfo methodInfo = queryType.GetMethod("Retrieve", new[] { parameterType });
            TResult result = (TResult)methodInfo.Invoke(queryHandler, new object[] { query });

            return result;
        }
    }
}

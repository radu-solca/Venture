using Venture.Gateway.Business.Queries;

namespace Venture.Gateway.Business.QueryHandlers
{
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        TResult Retrieve(TQuery query);
    }
}

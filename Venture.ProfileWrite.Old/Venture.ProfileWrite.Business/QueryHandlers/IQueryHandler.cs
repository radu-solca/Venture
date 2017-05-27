using Venture.ProfileWrite.Business.Queries;

namespace Venture.ProfileWrite.Business.QueryHandlers
{
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        TResult Retrieve(TQuery query);
    }
}

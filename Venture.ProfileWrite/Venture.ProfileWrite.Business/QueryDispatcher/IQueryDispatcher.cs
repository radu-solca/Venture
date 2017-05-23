using Venture.ProfileWrite.Business.Queries;

namespace Venture.ProfileWrite.Business.QueryDispatcher
{
    public interface IQueryDispatcher
    {
        TResult Handle<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>;
        TResult Handle<TResult>(IQuery<TResult> query);
    }
}

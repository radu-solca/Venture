using Venture.ProfileWrite.Business.Queries;

namespace Venture.ProfileWrite.Business.QueryDispatcher
{
    public interface IQueryDispatcher
    {
        TResult Handle<TResult>(IQuery<TResult> query);
    }
}

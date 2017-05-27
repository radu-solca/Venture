using Venture.Gateway.Business.Queries;

namespace Venture.Gateway.Business.QueryDispatcher
{
    public interface IQueryDispatcher
    {
        TResult Dispatch<TResult>(IQuery<TResult> query);
    }
}

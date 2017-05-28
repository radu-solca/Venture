using System.Threading.Tasks;
using Venture.Gateway.Business.Queries;

namespace Venture.Gateway.Business.QueryDispatcher
{
    public interface IQueryDispatcher
    {
        Task<TResult> DispatchAsync<TResult>(IQuery<TResult> query);
    }
}

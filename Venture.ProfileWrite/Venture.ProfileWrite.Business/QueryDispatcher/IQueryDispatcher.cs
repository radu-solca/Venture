using System.Threading.Tasks;
using Venture.ProfileWrite.Business.Queries;

namespace Venture.ProfileWrite.Business.QueryDispatcher
{
    public interface IQueryDispatcher
    {
        Task<TResult> Handle<TQuery, TResult>(TQuery query) where TQuery : class, IQuery<TResult>;
        Task<TResult> Handle<TResult>(IQuery<TResult> query);
    }
}

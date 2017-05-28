using System.Threading.Tasks;
using Venture.Gateway.Business.Queries;

namespace Venture.Gateway.Business.QueryHandlers
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> RetrieveAsync(TQuery query);
    }
}

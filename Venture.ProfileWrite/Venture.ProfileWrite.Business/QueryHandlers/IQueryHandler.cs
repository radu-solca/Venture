using System.Collections.Generic;
using System.Threading.Tasks;
using Venture.ProfileWrite.Business.Queries;
using Venture.ProfileWrite.Data.Events;

namespace Venture.ProfileWrite.Business.QueryHandlers
{
    public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> Retrieve(TQuery query);
    }
}

namespace Venture.Common.Cqrs.Queries
{
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        TResult Retrieve(TQuery query);
    }
}

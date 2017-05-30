namespace Venture.Common.Cqrs.Queries
{
    public interface IQueryDispatcher
    {
        TResult Handle<TResult>(IQuery<TResult> query);
    }
}

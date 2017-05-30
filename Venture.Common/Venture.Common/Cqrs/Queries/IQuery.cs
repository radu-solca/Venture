namespace Venture.Common.Cqrs.Queries
{
    /// <summary>
    /// Represents a query.
    /// </summary>
    /// <typeparam name="TResult">Represents the result that the query is supposed to return.</typeparam>
    public interface IQuery<out TResult>
    {
    }
}

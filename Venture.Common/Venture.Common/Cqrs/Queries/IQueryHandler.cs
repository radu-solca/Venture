namespace Venture.Common.Cqrs.Queries
{
    public interface IQueryHandler<in TQuery> where TQuery : IQuery
    {
        /// <summary>
        ///  Handles a query.
        /// </summary>
        /// <returns>A JSON string.</returns>
        string Handle(TQuery query);
    }
}

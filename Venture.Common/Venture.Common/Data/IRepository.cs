using System;

namespace Venture.Common.Data
{
    public interface IRepository<TAggregate> where TAggregate : class, IAggregateRoot
    {
        TAggregate Get(Guid aggregateId);
        void Add(TAggregate aggregate);
        void Update(TAggregate aggregate);
        void Delete(Guid aggregateId);
    }
}

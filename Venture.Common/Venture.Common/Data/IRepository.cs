using System;
using System.Collections.Generic;
using Venture.Common.Data.Interaces;

namespace Venture.Common.Data
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        ICollection<TEntity> Get();

        TEntity Get(Guid id);

        void Add(TEntity entity);

        void Delete(Guid id);

        void Update(TEntity entity);
    }
}

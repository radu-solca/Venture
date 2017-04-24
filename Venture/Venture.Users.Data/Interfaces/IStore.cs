using System;
using System.Collections.Generic;

namespace Venture.Users.Data
{
    public interface IStore<TEntity> 
        where TEntity: class, IEntity, new()
    {
        void Add(TEntity entity);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(Guid id);

        void Update(TEntity entity);

        void Delete(Guid id);
    }
}

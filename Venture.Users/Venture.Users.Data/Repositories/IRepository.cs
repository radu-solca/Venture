using System;
using System.Collections.Generic;

namespace Venture.Users.Data
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        List<TEntity> GetAll();

        TEntity GetById(Guid id);

        void Add(TEntity entity);

        void Delete(Guid id);

        void Update(TEntity entity);
    }
}

using System;
using System.Collections.Generic;

namespace Venture.Users.Data
{
    public class BaseSqlStore<TEntity> : IStore<TEntity> 
        where TEntity : class, IEntity, new()
    {
        private ISqlConnection _sqlConnection;

        public void Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

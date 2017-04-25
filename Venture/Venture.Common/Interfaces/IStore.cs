using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Venture.Users.Data
{
    public interface IStore<TEntity> 
        where TEntity: class, IEntity, new()
    {
        void Add(TEntity entity);

        Task<IEnumerable<TEntity>> GetAll();

        Task<TEntity> GetById(Guid id);

        void Update(TEntity entity);

        void Delete(Guid id);
    }
}

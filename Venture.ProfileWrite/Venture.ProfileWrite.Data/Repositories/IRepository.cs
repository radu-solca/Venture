using System;
using System.Collections.Generic;
using System.Text;
using Venture.ProfileWrite.Data.Entities;

namespace Venture.ProfileWrite.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        List<TEntity> GetAll();

        TEntity GetById(Guid id);

        void Add(TEntity entity);

        void Delete(Guid id);

        void Update(TEntity entity);

        void Save();
    }
}

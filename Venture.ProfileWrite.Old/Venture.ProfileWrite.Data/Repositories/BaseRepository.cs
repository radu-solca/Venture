using System;
using System.Collections.Generic;
using System.Linq;
using LiteGuard;
using Microsoft.EntityFrameworkCore;
using Venture.ProfileWrite.Data.Entities;

namespace Venture.ProfileWrite.Data.Repositories
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected DbContext Context;

        public BaseRepository(DbContext context)
        {
            Context = context;
        }

        /**
         * Get the DbSet corresponding to the entity type.
         */
        private DbSet<TEntity> DbSet => Context.Set<TEntity>();


        public List<TEntity> GetAll()
        {
            return DbSet.Where(entity => !entity.Deleted).ToList();
        }

        public TEntity GetById(Guid id)
        {
            return DbSet
                .Where(entity => !entity.Deleted)
                .FirstOrDefault(e => e.Id == id);
        }

        public void Add(TEntity entity)
        {
            Guard.AgainstNullArgument(nameof(entity), entity);

            DbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            Guard.AgainstNullArgument(nameof(entity), entity);

            DbSet.Attach(entity);
            var entry = Context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(Guid id)
        {
            var entity = DbSet.FirstOrDefault(e => e.Id == id);

            entity.Delete();
            Context.Entry(entity).State = EntityState.Modified;
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Venture.Users.Data
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected DbContext Context;

        protected BaseRepository(DbContext context)
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
            Debug.Assert(entity != null, "entity != null");
            DbSet.Add(entity);
            Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            Debug.Assert(entity != null, "entity != null");

            var target = DbSet.FirstOrDefault(e => e.Id == entity.Id);

            Debug.Assert(target != null, "entity != null");

            // TODO: make a decent update; This will lose some data;
            DbSet.Remove(target);
            entity.Update();
            DbSet.Add(entity);

            Context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var entity = DbSet.FirstOrDefault(e => e.Id == id);
            Debug.Assert(entity != null, "entity != null");

            entity.Delete();
            Context.Entry(entity).State = EntityState.Modified;

            Context.SaveChanges();
        }
    }
}

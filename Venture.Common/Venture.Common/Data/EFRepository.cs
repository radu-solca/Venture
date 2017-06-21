using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using LiteGuard;
using Microsoft.EntityFrameworkCore;
using Venture.Common.Data.Interaces;

namespace Venture.Common.Data
{
    public abstract class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity, new()
    {
        protected DbContext Context;

        public EFRepository(DbContext context)
        {
            Context = context;
        }

        /**
         * Get the DbSet corresponding to the entity type.
         */
        private DbSet<TEntity> DbSet => Context.Set<TEntity>();


        public ICollection<TEntity> Get()
        {
            return DbSet.ToList();
        }

        public TEntity Get(Guid id)
        {
            return DbSet
                .FirstOrDefault(e => e.Id == id);
        }

        public void Add(TEntity entity)
        {
            Guard.AgainstNullArgument(nameof(entity), entity);

            DbSet.Add(entity);
            Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            Guard.AgainstNullArgument(nameof(entity), entity);

            Context.Entry(entity).State = EntityState.Modified;

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

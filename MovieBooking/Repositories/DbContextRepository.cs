using System;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MovieBooking.Repositories
{
    public abstract class DbContextRepository<TEntity>(DbContext dbContext) : IRepository<TEntity>
        where TEntity : class
    {
        public virtual IQueryable<TEntity> Entities => GetDbSet();

        public virtual bool Add(TEntity entity)
        {
            EntityEntry<TEntity> entry = GetDbSet().Add(entity);
            EntityState state = entry.State;
            dbContext.SaveChanges();
            return state == EntityState.Added;
        }

        public virtual bool Remove(TEntity entity)
        {
            EntityEntry<TEntity> entry = GetDbSet().Remove(entity);
            EntityState state = entry.State;
            dbContext.SaveChanges();
            return state == EntityState.Deleted;
        }

        public virtual bool Update(TEntity entity)
        {
            EntityEntry<TEntity> entry = GetDbSet().Update(entity);
            EntityState state = entry.State;
            dbContext.SaveChanges();
            return state == EntityState.Modified;
        }

        protected abstract DbSet<TEntity> GetDbSet();
    }
}

using System.Linq;

namespace MovieBooking.Repositories
{
    public interface IRepository<TEntity>
    {
        public IQueryable<TEntity> Entities { get; }

        public bool Add(TEntity entity);

        public bool Remove(TEntity entity);

        public bool Update(TEntity entity);
    }
}

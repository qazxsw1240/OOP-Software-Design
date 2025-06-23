using Microsoft.EntityFrameworkCore;

using MovieBooking.Entity;

namespace MovieBooking.Repositories
{
    public class MovieRepository(EntityDbContext dbContext) : DbContextRepository<Movie>(dbContext)
    {
        protected override DbSet<Movie> GetDbSet()
        {
            return dbContext.Movies;
        }
    }
}

using Microsoft.EntityFrameworkCore;

using MovieBooking.Entity;

namespace MovieBooking.Repositories
{
    public class ShowTimeRepository(EntityDbContext dbContext) : DbContextRepository<ShowTime>(dbContext)
    {
        protected override DbSet<ShowTime> GetDbSet()
        {
            return dbContext.ShowTimes;
        }
    }
}

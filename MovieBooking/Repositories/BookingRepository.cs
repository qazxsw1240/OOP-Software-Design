using Microsoft.EntityFrameworkCore;

using MovieBooking.Entity;

namespace MovieBooking.Repositories
{
    public class BookingRepository(EntityDbContext dbContext) : DbContextRepository<Booking>(dbContext)
    {
        protected override DbSet<Booking> GetDbSet()
        {
            return dbContext.Bookings;
        }
    }
}

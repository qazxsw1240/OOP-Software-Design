using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MovieBooking.Entity;

namespace MovieBooking.Services.Bookings
{
    public class BookingService(EntityDbContext context) : BaseDbContextService<EntityDbContext>(context), IBookingService
    {
        public Task<IEnumerable<Booking>> GetBookingsByUser(User user, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_context.Bookings.Where(booking => true).AsEnumerable());
        }
    }
}

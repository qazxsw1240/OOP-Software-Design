using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MovieBooking.Entity;

namespace MovieBooking.Services.Bookings
{
    public interface IBookingService
    {
        public Task<IEnumerable<Booking>> GetBookingsByUser(User user, CancellationToken cancellationToken = default);
    }
}

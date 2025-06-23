using System.Linq;

using MovieBooking.Entity;

namespace MovieBooking.Services
{
    public interface IBookingService
    {
        public IQueryable<Booking> GetBookingsByUser(User user);
    }
}

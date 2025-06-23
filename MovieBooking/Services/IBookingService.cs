using System;
using System.Linq;

using MovieBooking.Entity;

namespace MovieBooking.Services
{
    public interface IBookingService
    {
        public IQueryable<Booking> GetBookingsByUser(User user);

        public IQueryable<Booking> GetUpcomingBookingsByUser(User user);

        public Booking? GetBooking(User user, Movie movie);

        public bool CreateBookingByUser(BookingCreateRequest request);

        public bool UpdateBookingByUser(BookingCreateRequest request, ShowTime newShowTime);
    }
}

using MovieBooking.Services.Bookings;
using MovieBooking.Services.Movies;
using MovieBooking.Services.Users;

namespace MovieBooking.Services
{
    public interface IDbContextCollection
    {
        public IUserService UserService { get; }

        public IMovieService MovieService { get; }

        public IBookingService BookingService { get; }
    }
}

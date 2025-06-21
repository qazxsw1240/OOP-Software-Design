using MovieBooking.Services.Bookings;
using MovieBooking.Services.Movies;
using MovieBooking.Services.Users;

namespace MovieBooking.Services
{
    public class DbContextCollection(
        IUserService userService,
        IMovieService movieService,
        IBookingService bookingService) : IDbContextCollection
    {
        public IUserService UserService => userService;

        public IMovieService MovieService => movieService;

        public IBookingService BookingService => bookingService;
    }
}

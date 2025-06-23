using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MovieBooking.Entity;
using MovieBooking.Services;

namespace MovieBooking.Applications.States
{
    public class MovieState(
        IoProcessor ioProcessor,
        IDbContextCollection contextCollection,
        User user) : ActionListApplicationStateBase(ioProcessor)
    {
        private readonly IMovieService _movieService = contextCollection.MovieService;
        private readonly IBookingService _bookingService = contextCollection.BookingService;

        public new async Task<IApplicationState?> HandleAsync(CancellationToken cancellationToken = default)
        {
            await _ioProcessor.WriteLinesAsync(
                _movieService.Movies
                    .Select(movie => $"{movie.Title}({movie.ReleaseDate.Year}) - {movie.Director}")
                    .Index()
                    .Select(pair => $"{pair.Index + 1}. {pair.Item}"),
                cancellationToken);
            IEnumerable<Booking> bookings = _bookingService.GetBookingsByUser(user);
            await _ioProcessor.WriteLineAsync($"Bookings: {bookings.Count()}", cancellationToken);
            return null;
        }

        protected override Task BeforeItemSelectAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        protected override Task AfterItemSelectAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        protected override List<IApplicationStateAction<IApplicationState?>> GetActions()
        {
            return [];
        }
    }
}

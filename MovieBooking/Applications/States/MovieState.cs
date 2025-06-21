using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MovieBooking.Entity;
using MovieBooking.Services;
using MovieBooking.Services.Bookings;
using MovieBooking.Services.Movies;

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
                _movieService
                    .GetAllMovies()
                    .Select(movie => string.Format("{0}({1}) - {2}", movie.Title, movie.ReleaseDate.Year, movie.Director))
                    .Index()
                    .Select(pair => string.Format("{0}. {1}", pair.Index + 1, pair.Item)),
                cancellationToken);
            IEnumerable<Booking> bookings = await _bookingService.GetBookingsByUser(user, cancellationToken);
            await _ioProcessor.WriteLineAsync(string.Format("Bookings: {0}", bookings.Count()), cancellationToken);
            return null;
        }

        protected override Task BeforeItemSelectAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        protected override Task AfterItemSelectAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

        protected override List<IApplicationStateAction<IApplicationState?>> GetActions() => [];
    }
}

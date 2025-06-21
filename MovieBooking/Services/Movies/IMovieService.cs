using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using MovieBooking.Entity;

namespace MovieBooking.Services.Movies
{
    public interface IMovieService
    {
        public IEnumerable<Movie> GetAllMovies();

        public Task<IEnumerable<Movie>> GetMoviesByDirectorAsync(string director, CancellationToken cancellationToken = default);

        public Task<IEnumerable<Movie>> GetMoviesByName(string name, CancellationToken cancellationToken = default);
    }
}

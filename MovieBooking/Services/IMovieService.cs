using System.Collections.Generic;

using MovieBooking.Entity;

namespace MovieBooking.Services
{
    public interface IMovieService
    {
        public IEnumerable<Movie> Movies { get; }

        public IEnumerable<Movie> GetMoviesByDirector(string director);

        public IEnumerable<Movie> GetMoviesByName(string name);
    }
}

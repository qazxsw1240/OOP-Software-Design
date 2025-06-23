using System.Collections.Generic;
using System.Linq;

using MovieBooking.Entity;
using MovieBooking.Repositories;

namespace MovieBooking.Services
{
    public class MovieService(IRepository<Movie> movieRepository) : IMovieService
    {
        public IEnumerable<Movie> Movies => movieRepository.Entities;

        public IEnumerable<Movie> GetMoviesByName(string name)
        {
            return movieRepository.Entities.Where(movie => movie.Title == name);
        }

        public IEnumerable<Movie> GetMoviesByDirector(string director)
        {
            return movieRepository.Entities.Where(movie => movie.Director == director);
        }
    }
}

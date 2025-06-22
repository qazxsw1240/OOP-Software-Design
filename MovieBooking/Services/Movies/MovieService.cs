using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MovieBooking.Entity;

namespace MovieBooking.Services.Movies
{
    public class MovieService(EntityDbContext context)
        : BaseDbContextService<EntityDbContext>(context), IMovieService
    {
        public IEnumerable<Movie> GetAllMovies()
        {
            return _context.Movies;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByName(
            string name,
            CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .Where(movie => movie.Title == name)
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Movie>> GetMoviesByDirectorAsync(
            string director,
            CancellationToken cancellationToken = default)
        {
            return await _context.Movies
                .Where(movie => movie.Director == director)
                .ToListAsync(cancellationToken);
        }

        public override async Task StartingAsync(CancellationToken cancellationToken)
        {
            await base.StartingAsync(cancellationToken);
            if (await _context.Movies.AnyAsync(cancellationToken))
            {
                return;
            }
            await _context.Movies.AddRangeAsync(
                new Movie
                {
                    Title = "Inception",
                    Director = "Christopher Nolan",
                    RunningTime = new(2, 28, 0),
                    ReleaseDate = new(2010, 7, 16)
                },
                new Movie
                {
                    Title = "The Grand Budapest Hotel",
                    Director = "Wes Anderson",
                    RunningTime = new(1, 39, 0),
                    ReleaseDate = new(2014, 3, 28)
                },
                new Movie
                {
                    Title = "Parasite",
                    Director = "Bong Joon-ho",
                    RunningTime = new(2, 12, 0),
                    ReleaseDate = new(2019, 5, 30)
                },
                new Movie
                {
                    Title = "Spirited Away",
                    Director = "Hayao Miyazaki",
                    RunningTime = new(2, 5, 0),
                    ReleaseDate = new(2001, 7, 20)
                },
                new Movie
                {
                    Title = "The Godfather",
                    Director = "Francis Ford Coppola",
                    RunningTime = new(2, 55, 0),
                    ReleaseDate = new(1972, 3, 24)
                },
                new Movie
                {
                    Title = "Pulp Fiction",
                    Director = "Quentin Tarantino",
                    RunningTime = new(2, 34, 0),
                    ReleaseDate = new(1994, 10, 14)
                },
                new Movie
                {
                    Title = "The Dark Knight",
                    Director = "Christopher Nolan",
                    RunningTime = new(2, 32, 0),
                    ReleaseDate = new(2008, 7, 18)
                },
                new Movie
                {
                    Title = "Schindler's List",
                    Director = "Steven Spielberg",
                    RunningTime = new(3, 15, 0),
                    ReleaseDate = new(1993, 12, 15)
                },
                new Movie
                {
                    Title = "The Shawshank Redemption",
                    Director = "Frank Darabont",
                    RunningTime = new(2, 22, 0),
                    ReleaseDate = new(1994, 9, 23)
                },
                new Movie
                {
                    Title = "Fight Club",
                    Director = "David Fincher",
                    RunningTime = new(2, 19, 0),
                    ReleaseDate = new(1999, 10, 15)
                },
                new Movie
                {
                    Title = "Forrest Gump",
                    Director = "Robert Zemeckis",
                    RunningTime = new(2, 22, 0),
                    ReleaseDate = new(1994, 7, 6)
                },
                new Movie
                {
                    Title = "The Matrix",
                    Director = "Lana Wachowski",
                    RunningTime = new(2, 16, 0),
                    ReleaseDate = new(1999, 3, 31)
                },
                new Movie
                {
                    Title = "The Matrix",
                    Director = "Lilly Wachowski",
                    RunningTime = new(2, 16, 0),
                    ReleaseDate = new(1999, 3, 31)
                },
                new Movie
                {
                    Title = "Titanic",
                    Director = "James Cameron",
                    RunningTime = new(3, 14, 0),
                    ReleaseDate = new(1997, 12, 19)
                },
                new Movie
                {
                    Title = "Goodfellas",
                    Director = "Martin Scorsese",
                    RunningTime = new(2, 26, 0),
                    ReleaseDate = new(1990, 9, 19)
                });
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

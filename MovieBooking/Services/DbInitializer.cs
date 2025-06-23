using System;
using System.Collections.Generic;
using System.Linq;

using MovieBooking.Entity;

namespace MovieBooking.Services
{
    public static class DbInitializer
    {
        public static void Initialize(EntityDbContext dbContext)
        {
            if (!dbContext.Movies.Any())
            {
                IEnumerable<Movie> movies =
                [
                    new()
                    {
                        Title = "Inception",
                        Director = "Christopher Nolan",
                        RunningTime = new(2, 28, 0),
                        ReleaseDate = new(2010, 7, 16)
                    },
                    new()
                    {
                        Title = "The Grand Budapest Hotel",
                        Director = "Wes Anderson",
                        RunningTime = new(1, 39, 0),
                        ReleaseDate = new(2014, 3, 28)
                    },
                    new()
                    {
                        Title = "Parasite",
                        Director = "Bong Joon-ho",
                        RunningTime = new(2, 12, 0),
                        ReleaseDate = new(2019, 5, 30)
                    },
                    new()
                    {
                        Title = "Spirited Away",
                        Director = "Hayao Miyazaki",
                        RunningTime = new(2, 5, 0),
                        ReleaseDate = new(2001, 7, 20)
                    },
                    new()
                    {
                        Title = "The Godfather",
                        Director = "Francis Ford Coppola",
                        RunningTime = new(2, 55, 0),
                        ReleaseDate = new(1972, 3, 24)
                    },
                    new()
                    {
                        Title = "Pulp Fiction",
                        Director = "Quentin Tarantino",
                        RunningTime = new(2, 34, 0),
                        ReleaseDate = new(1994, 10, 14)
                    },
                    new()
                    {
                        Title = "The Dark Knight",
                        Director = "Christopher Nolan",
                        RunningTime = new(2, 32, 0),
                        ReleaseDate = new(2008, 7, 18)
                    },
                    new()
                    {
                        Title = "Schindler's List",
                        Director = "Steven Spielberg",
                        RunningTime = new(3, 15, 0),
                        ReleaseDate = new(1993, 12, 15)
                    },
                    new()
                    {
                        Title = "The Shawshank Redemption",
                        Director = "Frank Darabont",
                        RunningTime = new(2, 22, 0),
                        ReleaseDate = new(1994, 9, 23)
                    },
                    new()
                    {
                        Title = "Fight Club",
                        Director = "David Fincher",
                        RunningTime = new(2, 19, 0),
                        ReleaseDate = new(1999, 10, 15)
                    },
                    new()
                    {
                        Title = "Forrest Gump",
                        Director = "Robert Zemeckis",
                        RunningTime = new(2, 22, 0),
                        ReleaseDate = new(1994, 7, 6)
                    },
                    new()
                    {
                        Title = "The Matrix",
                        Director = "Lana Wachowski",
                        RunningTime = new(2, 16, 0),
                        ReleaseDate = new(1999, 3, 31)
                    },
                    new()
                    {
                        Title = "The Matrix",
                        Director = "Lilly Wachowski",
                        RunningTime = new(2, 16, 0),
                        ReleaseDate = new(1999, 3, 31)
                    },
                    new()
                    {
                        Title = "Titanic",
                        Director = "James Cameron",
                        RunningTime = new(3, 14, 0),
                        ReleaseDate = new(1997, 12, 19)
                    },
                    new()
                    {
                        Title = "Goodfellas",
                        Director = "Martin Scorsese",
                        RunningTime = new(2, 26, 0),
                        ReleaseDate = new(1990, 9, 19)
                    }
                ];
                dbContext.Movies.AddRange(movies);
                dbContext.SaveChanges();
            }
            if (dbContext.ShowTimes.Any())
            {
                return;
            }
            DateTime today = DateTime.Today;
            int[] showTimeHours = [8, 10, 12, 14, 16, 18, 20, 22];
            List<DateTime> times = Enumerable.Range(0, 10)
                .Select(days => today.AddDays(days))
                .Select(days => showTimeHours.Select(hours => days.AddHours(hours)))
                .SelectMany(hours => hours)
                .ToList();
            IEnumerable<ShowTime> showTimes = times.SelectMany(time =>
                dbContext.Movies.Select(movie => new ShowTime { Movie = movie, Time = time }));
            dbContext.ShowTimes.AddRange(showTimes);
            dbContext.SaveChanges();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using MovieBooking.Entity;
using MovieBooking.Services.Bookings;
using MovieBooking.Services.Movies;
using MovieBooking.Services.Users;

namespace MovieBooking.Services
{
    public static class DbContextCollectionExtensions
    {
        public static IServiceCollection AddDbContextCollection(this IServiceCollection services)
        {
            services.AddDbContextFactory<EntityDbContext>();
            services.TryAddSingleton<IUserService, UserService>();
            services.TryAddSingleton<IMovieService, MovieService>();
            services.TryAddSingleton<IBookingService, BookingService>();
            services.TryAddSingleton<IDbContextCollection, DbContextCollection>();
            services.AddHostedService<MovieService>();
            services.AddHostedService<UserService>();
            return services;
        }
    }
}

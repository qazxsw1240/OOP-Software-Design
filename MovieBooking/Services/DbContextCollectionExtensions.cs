using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using MovieBooking.Entity;
using MovieBooking.Repositories;

namespace MovieBooking.Services
{
    public static class DbContextCollectionExtensions
    {
        public static IServiceCollection AddDbContextCollection(this IServiceCollection services)
        {
            services.AddDbContext<EntityDbContext>();
            services.TryAddSingleton<IDbContextCollection, DbContextCollectionService>();
            services
                .AddUserService()
                .AddMovieService()
                .AddBookingService()
                .AddHostedService<DbContextCollectionService>();
            return services;
        }

        private static IServiceCollection AddUserService(this IServiceCollection services)
        {
            services.TryAddSingleton<EntityDbContext>();
            services.TryAddSingleton<IRepository<User>, UserRepository>();
            services.TryAddSingleton<IUserService, UserService>();
            return services;
        }

        private static IServiceCollection AddMovieService(this IServiceCollection services)
        {
            services.TryAddSingleton<EntityDbContext>();
            services.TryAddSingleton<IRepository<Movie>, MovieRepository>();
            services.TryAddSingleton<IMovieService, MovieService>();
            return services;
        }

        private static IServiceCollection AddBookingService(this IServiceCollection services)
        {
            services.TryAddSingleton<EntityDbContext>();
            services.TryAddSingleton<IRepository<ShowTime>, ShowTimeRepository>();
            services.TryAddSingleton<IRepository<Booking>, BookingRepository>();
            services.TryAddSingleton<IBookingService, BookingService>();
            return services;
        }
    }
}

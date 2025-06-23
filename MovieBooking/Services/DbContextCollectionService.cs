using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

using MovieBooking.Entity;

namespace MovieBooking.Services
{
    public class DbContextCollectionService(
        EntityDbContext dbContext,
        IUserService userService,
        IMovieService movieService,
        IBookingService bookingService) : IDbContextCollection, IHostedLifecycleService
    {
        public IUserService UserService => userService;

        public IMovieService MovieService => movieService;

        public IBookingService BookingService => bookingService;

        public async Task StartingAsync(CancellationToken cancellationToken)
        {
            await dbContext.Database.EnsureCreatedAsync(cancellationToken);
            DbInitializer.InitializeAsync(dbContext);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StartedAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StoppingAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task StoppedAsync(CancellationToken cancellationToken)
        {
            await dbContext.SaveChangesAsync(cancellationToken);
            await dbContext.Database.CloseConnectionAsync();
        }
    }
}

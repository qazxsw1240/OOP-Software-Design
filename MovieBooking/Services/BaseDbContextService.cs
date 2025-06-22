using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace MovieBooking.Services
{
    public abstract class BaseDbContextService<TDbContext>(TDbContext context) : IHostedLifecycleService
        where TDbContext : DbContext
    {
        protected readonly TDbContext _context = context;

        public virtual async Task StartingAsync(CancellationToken cancellationToken)
        {
            await _context.Database.EnsureCreatedAsync(cancellationToken);
        }

        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public virtual Task StartedAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public virtual Task StoppingAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public virtual async Task StoppedAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
            await _context.Database.CloseConnectionAsync();
        }
    }
}

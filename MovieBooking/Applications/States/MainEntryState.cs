using System.Threading;
using System.Threading.Tasks;

using MovieBooking.Entity;
using MovieBooking.Services;

namespace MovieBooking.Applications.States
{
    public class MainEntryState(
        IoProcessor ioProcessor,
        IDbContextCollection dbContextCollection,
        User user) : IApplicationState
    {
        public Task<IApplicationState?> HandleAsync(
            ApplicationStateStack stack,
            CancellationToken cancellationToken = default)
        {
            return Task.FromResult<IApplicationState?>(new MovieState(ioProcessor, dbContextCollection, user));
        }
    }
}

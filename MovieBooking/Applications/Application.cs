using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

using MovieBooking.Applications.States;
using MovieBooking.Services;

namespace MovieBooking.Applications
{
    public class Application(
        IoProcessor ioProcessor,
        IDbContextCollection dbContextCollection,
        IHostApplicationLifetime lifetime)
    {
        private readonly ApplicationStateStack _stack = new();
        private IApplicationState? _applicationState = new UserAuthenticationState(ioProcessor, dbContextCollection);

        public async Task StartAsync(CancellationToken cancellationToken = default)
        {
            while (_applicationState is not null)
            {
                _applicationState = await _applicationState.HandleAsync(_stack, cancellationToken);
                if (_applicationState is not null)
                {
                    _stack.Push(_applicationState);
                }
            }
            lifetime.StopApplication();
        }

        public async Task StopAsync(CancellationToken cancellationToken = default)
        {
            await ioProcessor.WriteLineAsync("Terminating application...", cancellationToken);
        }
    }
}

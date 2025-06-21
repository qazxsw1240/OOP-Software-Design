using System.Threading;
using System.Threading.Tasks;

namespace MovieBooking.Applications.States
{
    public interface IApplicationState
    {
        public Task<IApplicationState?> HandleAsync(CancellationToken cancellationToken = default);
    }
}

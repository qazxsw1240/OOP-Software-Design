using System.Threading;
using System.Threading.Tasks;

namespace MovieBooking.Applications
{
    public interface IApplicationStateAction<TActionResult>
    {
        public string Name { get; }

        public Task<TActionResult> ExecuteAsync(CancellationToken cancellationToken = default);
    }
}

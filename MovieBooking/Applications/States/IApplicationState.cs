using System.Threading;
using System.Threading.Tasks;

namespace MovieBooking.Applications.States
{
    public interface IApplicationState
    {
        public Task<IApplicationState?> HandleAsync(
            ApplicationStateStack stack,
            CancellationToken cancellationToken = default);
    }

    public interface IListableApplicationState : IApplicationState
    {
        public string Name { get; }
    }
}

using MovieBooking.Entity;
using MovieBooking.Services;

namespace MovieBooking.Applications.States
{
    public class MovieState(
        IoProcessor ioProcessor,
        IDbContextCollection contextCollection,
        User user) : ListableApplicationState(ioProcessor, []) {}
}

using MovieBooking.Services;

namespace MovieBooking.Applications.States
{
    public class UserAuthenticationState(
        IoProcessor ioProcessor,
        IDbContextCollection dbContextCollection)
        : ListableApplicationState(
            ioProcessor,
            [
                new UserSignUpState(ioProcessor, dbContextCollection),
                new UserSignInState(ioProcessor, dbContextCollection)
            ]);
}

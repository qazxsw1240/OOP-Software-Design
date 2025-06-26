using System.Threading;
using System.Threading.Tasks;

using MovieBooking.Entity;
using MovieBooking.Services;

namespace MovieBooking.Applications.States
{
    public class UserSignInState(
        IoProcessor ioProcessor,
        IDbContextCollection dbContextCollection) : IListableApplicationState
    {
        private readonly IUserService _userService = dbContextCollection.UserService;

        public string Name => "Sign in";

        public async Task<IApplicationState?> HandleAsync(
            ApplicationStateStack stack,
            CancellationToken cancellationToken = default)
        {
            TaskCompletionSource<User> taskSource = new();
            bool result = await ioProcessor.ReadLinesUntil(
                "User name (CTRL + Z to cancel): ",
                userName =>
                {
                    UserSignInRequest request = new(userName);
                    User user = _userService.SignIn(request);
                    taskSource.SetResult(user);
                    return Task.FromResult(true);
                },
                _ => Task.CompletedTask,
                async (_, e) =>
                {
                    if (e is UserSignInFailedException)
                    {
                        await ioProcessor.WriteLineAsync("Failed to sign in. Try again.", cancellationToken);
                    }
                });
            if (!result)
            {
                return new UserAuthenticationState(ioProcessor, dbContextCollection);
            }
            User user = await taskSource.Task;
            await ioProcessor.WriteLinesAsync(
                [
                    $"Welcome, {user.Name}.",
                    $"Last login at {user.LastLoginAt} (UTC)"
                ],
                cancellationToken);
            return new MainEntryState(ioProcessor, dbContextCollection, user);
        }
    }
}

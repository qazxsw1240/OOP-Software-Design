using System;
using System.Threading;
using System.Threading.Tasks;

using MovieBooking.Services;
using MovieBooking.Utility;

namespace MovieBooking.Applications.States
{
    public class UserSignUpState(
        IoProcessor ioProcessor,
        IDbContextCollection dbContextCollection) : IListableApplicationState
    {
        private readonly IUserService _userService = dbContextCollection.UserService;

        public string Name => "Sign up";

        public async Task<IApplicationState?> HandleAsync(
            ApplicationStateStack stack,
            CancellationToken cancellationToken = default)
        {
            TaskCompletionSource<string> taskSource = new();
            bool result = await ioProcessor.ReadLinesUntil(
                "User name (CTRL + Z to cancel): ",
                userName =>
                {
                    new Validator<string>()
                        .With(
                            name => !string.IsNullOrWhiteSpace(name),
                            _ => new ArgumentException("Invalid name. Try again."))
                        .With(
                            name => !_userService.IsUserRegistered(name),
                            name => new ArgumentException($"The name {name} is already in use. Try again."))
                        .Check(userName);
                    taskSource.SetResult(userName);
                    return Task.FromResult(true);
                },
                userName =>
                {
                    taskSource.SetResult(userName);
                    return Task.CompletedTask;
                },
                async (_, e) =>
                {
                    if (e is ArgumentException)
                    {
                        string message = e.Message;
                        await ioProcessor.WriteLineAsync(message, cancellationToken);
                    }
                });
            if (!result)
            {
                return new UserAuthenticationState(ioProcessor, dbContextCollection);
            }
            string userName = await taskSource.Task;
            UserSignUpRequest request = new(userName);
            _userService.SignUp(request);
            return new UserAuthenticationState(ioProcessor, dbContextCollection);
        }
    }
}

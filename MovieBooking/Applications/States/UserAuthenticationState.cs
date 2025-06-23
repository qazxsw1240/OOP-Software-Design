using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using MovieBooking.Entity;
using MovieBooking.Services;

namespace MovieBooking.Applications.States
{
    public class UserAuthenticationState(
        IoProcessor ioProcessor,
        IDbContextCollection dbContextCollection) : ActionListApplicationStateBase(ioProcessor)
    {
        private readonly IUserService _userService = dbContextCollection.UserService;

        protected override List<IApplicationStateAction<IApplicationState?>> GetActions()
        {
            return
            [
                new UserRegistrationAction(_ioProcessor, _userService, this),
                new UserSignInAction(_ioProcessor, dbContextCollection, this)
            ];
        }

        protected override Task BeforeItemSelectAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        protected override Task AfterItemSelectAsync(CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        private class UserRegistrationAction(
            IoProcessor ioProcessor,
            IUserService userService,
            UserAuthenticationState nextState) : IApplicationStateAction<IApplicationState?>
        {
            public string Name => "Sign up";

            public async Task<IApplicationState?> ExecuteAsync(CancellationToken cancellationToken = default)
            {
                string? userName = null;
                while (userName is null)
                {
                    try
                    {
                        userName = await ioProcessor.ReadLineAsync(
                            "User name (CTRL + Z to cancel): ",
                            cancellationToken);
                        if (string.IsNullOrWhiteSpace(userName))
                        {
                            await ioProcessor.WriteLineAsync("Invalid name. Try again.", cancellationToken);
                            userName = null;
                            continue;
                        }
                        if (!userService.IsUserRegistered(userName))
                        {
                            continue;
                        }
                        await ioProcessor.WriteLineAsync(
                            $"The name {userName} is already in use. Try again.",
                            cancellationToken);
                        userName = null;
                    }
                    catch (IOException)
                    {
                        return nextState;
                    }
                }
                UserSignUpRequest request = new(userName);
                userService.SignUp(request);
                return nextState;
            }
        }

        private class UserSignInAction(
            IoProcessor ioProcessor,
            IDbContextCollection dbContextCollection,
            UserAuthenticationState nextState) : IApplicationStateAction<IApplicationState?>
        {
            private readonly IUserService _userService = dbContextCollection.UserService;

            public string Name => "Sign in";

            public async Task<IApplicationState?> ExecuteAsync(CancellationToken cancellationToken = default)
            {
                User? user = null;
                while (user is null)
                {
                    try
                    {
                        string userName = await ioProcessor.ReadLineAsync(
                            "User name (CTRL + Z to cancel): ",
                            cancellationToken);
                        UserSignInRequest request = new(userName);
                        user = _userService.SignIn(request);
                    }
                    catch (UserSignInFailedException)
                    {
                        await ioProcessor.WriteLineAsync("Failed to sign in. Try again.", cancellationToken);
                    }
                    catch (IOException)
                    {
                        return nextState;
                    }
                }
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
}

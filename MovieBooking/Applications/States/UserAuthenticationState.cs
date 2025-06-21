using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using MovieBooking.Entity;
using MovieBooking.Services;
using MovieBooking.Services.Users;

namespace MovieBooking.Applications.States
{
    public class UserAuthenticationState(
        IoProcessor ioProcessor,
        IDbContextCollection dbContextCollection) : ActionListApplicationStateBase(ioProcessor)
    {
        private readonly IUserService _userService = dbContextCollection.UserService;

        protected override List<IApplicationStateAction<IApplicationState?>> GetActions()
        {
            return [
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
                        userName = await ioProcessor.ReadLineAsync("User name (CTRL + Z to cancel): ", cancellationToken);
                        if (string.IsNullOrWhiteSpace(userName))
                        {
                            await ioProcessor.WriteLineAsync("Invalid name. Try again.", cancellationToken);
                            userName = null;
                            continue;
                        }
                        if (await userService.IsUserRegistered(userName, cancellationToken))
                        {
                            await ioProcessor.WriteLineAsync(
                                string.Format("The name {0} is already in use. Try again.", userName),
                                cancellationToken);
                            userName = null;
                            continue;
                        }
                    }
                    catch (IOException)
                    {
                        return nextState;
                    }
                }
                UserRegistrationRequest request = new(userName);
                await userService.RegisterUser(request, cancellationToken);
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
                        user = await _userService.SignInAsync(request, cancellationToken);
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
                        string.Format("Welcome, {0}.", user.Name),
                        string.Format("Last login at {0} (UTC)", user.LastLoginAt)
                    ],
                    cancellationToken);
                return new MainEntryState(ioProcessor, dbContextCollection, user);
            }
        }
    }
}

using System.Threading;
using System.Threading.Tasks;

using MovieBooking.Entity;

namespace MovieBooking.Services.Users
{
    public interface IUserService
    {
        public Task<bool> IsUserRegistered(string userName, CancellationToken cancellationToken = default);

        public Task<User?> GetUserAsync(string userName, CancellationToken cancellationToken = default);

        public Task UpdateUserNameAsync(User user, string newUserName, CancellationToken cancellationToken = default);

        public Task RegisterUser(UserRegistrationRequest request, CancellationToken cancellationToken = default);

        public Task<User> SignInAsync(UserSignInRequest request, CancellationToken cancellationToken = default);
    }
}

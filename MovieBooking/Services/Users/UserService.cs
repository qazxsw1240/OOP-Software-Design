using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MovieBooking.Entity;

namespace MovieBooking.Services.Users
{
    public class UserService(EntityDbContext context)
        : BaseDbContextService<EntityDbContext>(context), IUserService
    {
        public async Task<bool> IsUserRegistered(string userName, CancellationToken cancellationToken = default)
        {
            return await _context.Users.AnyAsync(user => user.Name == userName, cancellationToken);
        }

        public async Task<User?> GetUserAsync(string userName, CancellationToken cancellationToken = default)
        {
            return await _context.Users.SingleOrDefaultAsync(user => user.Name == userName, cancellationToken);
        }

        public async Task UpdateUserNameAsync(
            User user,
            string newUserName,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(newUserName))
            {
                throw new UserNameUpdateFailedException("User name cannot be null or empty.");
            }
            if (await _context.Users.AnyAsync(u => u.Name == newUserName, cancellationToken))
            {
                throw new UserNameUpdateFailedException("User already exists.");
            }
            user.Name = newUserName;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task RegisterUser(UserRegistrationRequest request, CancellationToken cancellationToken = default)
        {
            string name = request.UserName;
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Cannot register user with empty name.", nameof(request));
            }
            if (await _context.Users.AnyAsync(user => user.Name == name, cancellationToken))
            {
                throw new ArgumentException("User with the the same name already exists.", nameof(request));
            }
            User user = new() { Name = name, RegisteredAt = DateTime.UtcNow };
            await _context.Users.AddAsync(user, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<User> SignInAsync(UserSignInRequest request, CancellationToken cancellationToken = default)
        {
            User user = await GetUserAsync(request.UserName, cancellationToken)
                ?? throw new UserSignInFailedException();
            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }
    }

    public class UserSignInFailedException : Exception;

    public class UserNameUpdateFailedException(string? message = null) : Exception(message);
}

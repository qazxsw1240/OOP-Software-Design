using System;
using System.Linq;

using MovieBooking.Entity;
using MovieBooking.Repositories;

namespace MovieBooking.Services
{
    public class UserService(IRepository<User> userRepository) : IUserService
    {
        public bool IsUserRegistered(string userName)
        {
            return userRepository.Entities.Any(user => user.Name == userName);
        }

        public User? GetUser(string userName)
        {
            return userRepository.Entities.SingleOrDefault(user => user.Name == userName);
        }

        public void UpdateUserName(User user, string newUserName)
        {
            user.Name = newUserName;
            if (!userRepository.Update(user))
            {
                throw new UserNameUpdateFailedException();
            }
        }

        public void SignUp(UserSignUpRequest request)
        {
            User user = new()
            {
                Name = request.UserName,
                RegisteredAt = DateTime.UtcNow
            };
            if (!userRepository.Add(user))
            {
                throw new UserSignUpFailedException();
            }
        }

        public User SignIn(UserSignInRequest request)
        {
            User user = GetUser(request.UserName) ?? throw new UserSignInFailedException();
            user.LastLoginAt = DateTime.UtcNow;
            userRepository.Update(user);
            return user;
        }
    }

    public class UserSignUpFailedException : Exception;

    public class UserSignInFailedException : Exception;

    public class UserNameUpdateFailedException : Exception;
}

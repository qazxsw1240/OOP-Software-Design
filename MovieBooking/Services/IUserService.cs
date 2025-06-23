using MovieBooking.Entity;

namespace MovieBooking.Services
{
    public interface IUserService
    {
        public bool IsUserRegistered(string userName);

        public User? GetUser(string userName);

        public void UpdateUserName(User user, string newUserName);

        public void SignUp(UserSignUpRequest request);

        public User SignIn(UserSignInRequest request);
    }
}

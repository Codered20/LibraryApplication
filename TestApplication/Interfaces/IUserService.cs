using TestApplication.Models;

namespace TestApplication.Interfaces
{
    public interface IUserService
    {
        public Task<User> registerUser(User user);
        public string? login(string email, string password);
    }
}

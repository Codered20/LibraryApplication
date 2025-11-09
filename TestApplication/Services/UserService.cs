using Microsoft.AspNetCore.Identity;
using TestApplication.Data;
using TestApplication.Interfaces;
using TestApplication.Models;

namespace TestApplication.Services
{
    public class UserService : IUserService
    {
        private AppDbContext _context;
        private TokenService _tokenService;

        public UserService(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }
        public string? login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user == null) return null;

            var hasher = new PasswordHasher<User>();
            var verificationResult = hasher.VerifyHashedPassword(user, user.Password, password);
            if (verificationResult == PasswordVerificationResult.Failed) return null;

            return _tokenService.CreateToken(user);
        }

        public async Task<User> registerUser(User user)
        {
            var hasher = new PasswordHasher<User>();
            user.Password = hasher.HashPassword(user, user.Password);

            var result = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
    }
}

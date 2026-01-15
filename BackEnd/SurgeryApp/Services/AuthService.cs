using Microsoft.EntityFrameworkCore;
using SurgeryApp.Data;
using SurgeryApp.Models;
using SurgeryApp.Services.Security;

namespace SurgeryApp.Services
{
    public class AuthService
    {
        private readonly SurgeryAppUsersDbContext _context;

        public AuthService(SurgeryAppUsersDbContext context)
        {
            _context = context;
        }

        // ===============================
        // VALIDATE LOGIN CREDENTIALS
        // ===============================
        public async Task<Users?> ValidateUserAsync(string usernameOrEmail, string password)
        {
            var user = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u =>
                    u.UserName == usernameOrEmail ||
                    u.Email == usernameOrEmail);

            if (user == null)
                return null;

            if (!PasswordHasher.Verify(password, user.Password))
                throw new UnauthorizedAccessException("Invalid username or password");

            return user;
        }
    }
}

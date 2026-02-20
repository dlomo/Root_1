using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SurgeryApp.Data;
using SurgeryApp.Models;
using SurgeryApp.Repositories;
using SurgeryApp.Services.Security;
using System.Data;

namespace SurgeryApp.Services
{
    public class UserService : IUserService
    {
        private readonly SurgeryAppUsersDbContext _context;

        public UserService(SurgeryAppUsersDbContext context)
        {
            _context = context;
        }

        public async Task<RegisterResult> RegisterUserAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                return RegisterResult.InvalidInput;
            }

            //  Hash password using hardened PBKDF2
            var storedPassword = PasswordHasher.Hash(password);

            // Output parameter
            var resultParam = new SqlParameter("@Result", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            // Execute stored procedure
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC dbo.api_RegisterUser @Username, @Password, @Result OUTPUT",
                new SqlParameter("@Username", username),
                new SqlParameter("@Password", storedPassword),
                resultParam
            );

            var resultCode = (int)(resultParam.Value ?? -99);

            return resultCode switch
            {
                0 => RegisterResult.Success,
                1 => RegisterResult.UserExists,
                -1 => RegisterResult.InvalidInput,
                _ => RegisterResult.Failed
            };
        }
    }

}

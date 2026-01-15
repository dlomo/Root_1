
using Microsoft.EntityFrameworkCore;
using SurgeryApp.Data;
using SurgeryApp.Models;
using System.Security.Cryptography;
using System.Text;


namespace SurgeryApp.Services
{
    public interface IUserService
    {
        Task<RegisterResult> RegisterUserAsync(string username, string password);
    }
}
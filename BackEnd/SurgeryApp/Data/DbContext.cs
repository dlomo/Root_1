using Microsoft.EntityFrameworkCore;
using SurgeryApp.Models;


namespace SurgeryApp.Data
{
    public class SurgeryAppUsersDbContext : DbContext
    {
        public SurgeryAppUsersDbContext(DbContextOptions<SurgeryAppUsersDbContext> options)
            : base(options)
        {
        }

        public DbSet<Users> Users { get; set; } = null!;
    }
}

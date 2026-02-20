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

    public class SurgeryAccountsContext : DbContext
    {
        public SurgeryAccountsContext(DbContextOptions<SurgeryAccountsContext> options) : base(options) { }
        public DbSet<Accounts> Accounts { get; set; } = null!;
    }

    public class SurgeryRolesDbContext : DbContext
    {
        public SurgeryRolesDbContext(DbContextOptions<SurgeryRolesDbContext> options) : base(options) { }
        public DbSet<UserRole> UserRole { get; set; } = null!;
    }

    public class SurgeryDetailbContext : DbContext
    {
        public SurgeryDetailbContext(DbContextOptions<SurgeryDetailbContext> options) : base(options) { }
        public DbSet<UserDetails> PatientDetails { get; set; } = null!;
    }
}

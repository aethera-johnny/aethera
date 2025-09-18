using AuthService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Contexts
{
    public class AetheraDbContext : DbContext
    {
        public AetheraDbContext()
        {

        }

        public AetheraDbContext(DbContextOptions<AetheraDbContext> options) : base(options)
        {
        }

        public DbSet<Tb_User_Info> Users { get; set; }
    }
}

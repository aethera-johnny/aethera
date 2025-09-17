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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tb_User_Info>(entity =>
            {
                entity.Property(e => e.CreatedDatetime)
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.UpdatedDatetime)
                    .ValueGeneratedOnAddOrUpdate();
            });
        }

        public DbSet<Tb_User_Info> Users { get; set; }
    }
}

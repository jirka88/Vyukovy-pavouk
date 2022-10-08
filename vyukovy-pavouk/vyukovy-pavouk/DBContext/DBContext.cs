using Microsoft.EntityFrameworkCore;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.DBContext
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<Kapitola> Kapitoly { get; set; }
        public DbSet<Predmet> Predmet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1:M --> Predmet : kapitoly
            modelBuilder.Entity<Kapitola>()
               .HasOne(p => p.predmet)
               .WithMany(p => p.Kapitoly)
               .HasForeignKey(p => p.IdPredmetu);
              
        }
    }
}


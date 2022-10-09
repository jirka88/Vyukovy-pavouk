using Microsoft.EntityFrameworkCore;
using System.Reflection;
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
        public DbSet<Videa> Videa { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());                        
        }
    }
}


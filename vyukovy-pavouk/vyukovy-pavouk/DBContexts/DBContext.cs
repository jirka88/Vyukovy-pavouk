using Microsoft.EntityFrameworkCore;
using System.Reflection;
using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.DBContexts
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<Kapitola> Kapitoly { get; set; }
        public DbSet<Predmet> Predmet { get; set; }
        public DbSet<Videa> Videa { get; set; }
        public DbSet<Zadani> Zadani { get; set; }
        public DbSet<Skupina> Skupina { get; set; }
        public DbSet<SkupinaPredmet> SkupinaPredmet {get; set;}

        //public DbSet<Test> Test { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());                   
        }
    }
}


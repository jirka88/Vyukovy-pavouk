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
        public DbSet<SkupinaStudent> SkupinaStudent { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentSplneni> StudentSplneni { get; set; }
        public DbSet<Splneni> Splneni { get; set; }
        public DbSet<KapitolaPrerekvizita> kapitolaPrerekvizita { get; set; }
        public DbSet<Prerekvizity> Prerekvizity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());                   
        }
    }
}


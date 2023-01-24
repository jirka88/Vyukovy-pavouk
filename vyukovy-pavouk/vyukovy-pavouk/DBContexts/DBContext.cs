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
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Link> Link { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<GroupStudent> GroupStudent { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<StudentCompletion> StudentCompletion { get; set; }
        public DbSet<Completion> Completion { get; set; }
        public DbSet<ChapterPrerequisite> ChapterPrerequisite { get; set; }
        public DbSet<Prerequisite> Prerequisite { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());                   
        }
    }
}


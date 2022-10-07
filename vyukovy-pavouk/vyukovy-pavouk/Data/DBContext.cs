using Microsoft.EntityFrameworkCore;

namespace vyukovy_pavouk.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}

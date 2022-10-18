using Microsoft.EntityFrameworkCore;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Services
{
    public class UzivatelManager : IUzivatel
    {
        readonly DBContext _dbContext;
        public UzivatelManager(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //vybere všechny studenty se splněnými kapitoly, které patří do Teamu 
        public List<Student> GetStudents(int ID)
        {
            return _dbContext.Student
                .Include(s => s.Splneni)
                .ToList();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Services
{
    public class StudentiManager : IStudenti
    {
        readonly DBContext _dbContext;
        public StudentiManager(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public SkupinaStudent GetStudent(int IdSkupiny, string EmailStudenta)
        {
            return _dbContext.SkupinaStudent
                    .Where(s => s.SkupinaId == IdSkupiny)
                    .Include(s => s.Student)
                    .Where(e => e.Student.email == EmailStudenta)              
                    .SingleOrDefault();
                  
        }

        //vybere všechny studenty se splněnými kapitoly, které patří do Teamu 
        public List<SkupinaStudent> GetStudents(int ID)
        {
            //vrátí v souhrnu její studenty a jejich splnění kapitol (ID kapitol) 
            return _dbContext.SkupinaStudent
                    .Where(s => s.SkupinaId == ID)
                    .Include(s => s.Student)
                    .ThenInclude(s => s.Splneni
                    .Where(id => id.Id_skupiny == ID))
                    .ToList();
                    
                /*_dbContext.Student
                //vybere studenty patřící pod team 
                .Include(sk => sk.SkupinaStudent
                .Where(s => s.SkupinaId == ID))
                 .AsSplitQuery()
                //vybere jejich zadání jednotlivá 
                .Include(s => s.Splneni
                .Where(s => s.Id_skupiny == ID))    
                .ToList();*/
        }
    }
}

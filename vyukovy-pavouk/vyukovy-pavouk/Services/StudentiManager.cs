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

        public void CreateNewConnect(SkupinaStudent skupinaStudent)
        {
            try
            {
                _dbContext.SkupinaStudent.Add(skupinaStudent);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CreateNewStudent(Student student)
        {
            try
            {
                _dbContext.Student.Add(student);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Student GetStudent(int IdSkupiny, string EmailStudenta)
        {
           return _dbContext.Student
                .Where(e => e.email == EmailStudenta)
                .Include(s => s.SkupinaStudent)
                .Include(s => s.Splneni.Where(id => id.Id_skupiny == IdSkupiny))
                .SingleOrDefault();                             
        }

        //vybere všechny studenty se splněnými kapitoly, které patří do Teamu 
        public List<SkupinaStudent> GetStudents(int ID)
        {
            //vrátí v souhrnu její studenty a jejich splnění kapitol (ID kapitol) krom prerekvizity u úvodní kapitoly - ta je automaticky daná studentovi
            return _dbContext.SkupinaStudent
                    .Where(s => s.SkupinaId == ID)
                    .Include(s => s.Student)
                    .ThenInclude(s => s.Splneni
                    .Where(id => id.Id_skupiny == ID && id.Id_kapitoly != 0))
                    .ToList();                         
        }
    }
}

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

        public void CreateNewConnectPrerekvizita(StudentSplneni splneni)
        {
            try
            {
                _dbContext.StudentSplneni.Add(splneni);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        //vytvoří nového studenta 
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
        //zjistí progres (plnění jeho kapitol) u určitého studenta 
        public Student GetStudentProgres(int Id, int IdSkupiny)
        {
            return _dbContext.Student
                .Where(id => id.Id == Id)
                .Include(s => s.StudentSplneni.Where(id => id.splneni.IdSkupiny == IdSkupiny))
                .ThenInclude(s => s.splneni)
                .SingleOrDefault();
        }
        //získa studenta zda-li v dané skupině je 
        public Student GetStudent(int IdSkupiny, string EmailStudenta)
        {
           return _dbContext.Student
                .Where(e => e.Email == EmailStudenta)
                .Include(s => s.SkupinaStudent)
                .Include(s => s.StudentSplneni.Where(id => id.splneni.IdSkupiny == IdSkupiny))
                    .ThenInclude(s => s.splneni)
                .SingleOrDefault();                             
        }      

        //vybere všechny studenty se splněnými kapitoly, které patří do Teamu 
        public List<SkupinaStudent> GetStudents(int ID)
        {
            //vrátí v souhrnu její studenty a jejich splnění kapitol (ID kapitol) krom prerekvizity u úvodní kapitoly - ta je automaticky daná studentovi
            return _dbContext.SkupinaStudent
                    .Where(s => s.IdSkupina == ID)
                    .Include(s => s.Student)
                    .ThenInclude(s => s.StudentSplneni.Where(id => id.splneni.IdSkupiny == ID && id.splneni.IdKapitoly != 0))
                        .ThenInclude(s => s.splneni)            
                    .ToList();                         
        }
        //vrátí všechny splnění studentů v dané skupině 
        public List<StudentSplneni> GetSplneni(int IdSkupiny)
        {
            return _dbContext.StudentSplneni
                .Include(s => s.splneni).Where(s => s.splneni.IdSkupiny == IdSkupiny)
                .ToList();
        }
        //vymaže studentům splnění kapitoly --> při mazání kapitoly 
        public void DeleteSplneni(int Id)
        {
            try
            {
                List<StudentSplneni> studentiSplneni = new List<StudentSplneni>();
                studentiSplneni = _dbContext.StudentSplneni.Where(id => id.IdSplneni == Id).ToList();
                Splneni splneni = _dbContext.Splneni.Find(Id);

                foreach (StudentSplneni studentSplneni in studentiSplneni)
                {
                    if (studentSplneni != null)
                    {
                        _dbContext.StudentSplneni.Remove(studentSplneni);                    
                    }
                    else
                    {
                        throw new ArgumentNullException();
                    }
                }
                _dbContext.Splneni.Remove(splneni);
                _dbContext.SaveChanges();

            }
            catch
            {
                throw;
            }
        }
    }
}

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

        public async Task CreateNewConnect(SkupinaStudent skupinaStudent)
        {         
                _dbContext.SkupinaStudent.Add(skupinaStudent);
                //vytvoří spojení mezi úvodní kapitolou a studentem 
                StudentSplneni studentSplneni = new StudentSplneni();
                studentSplneni.IdStudent = skupinaStudent.IdStudent;
                //získá uvodní prerekvizitu
                Splneni splneni = await _dbContext.Splneni.Where(x => x.IdSkupiny == skupinaStudent.IdSkupina && x.IdKapitoly == 0).SingleOrDefaultAsync();
                studentSplneni.IdSplneni = splneni.Id;
                _dbContext.StudentSplneni.Add(studentSplneni);
                await _dbContext.SaveChangesAsync();                  
        }
        //vytvoří nového studenta 
        public async Task CreateNewStudent(Student student)
        {                       
                _dbContext.Student.Add(student);
                await _dbContext.SaveChangesAsync();
                //vytvoří spojení mezi úvodní kapitolou a studentem 
                StudentSplneni studentSplneni = new StudentSplneni();
                studentSplneni.IdStudent = student.Id;
                //získá uvodní prerekvizitu
                Splneni splneni = await _dbContext.Splneni.Where(x => x.IdSkupiny == student.SkupinaStudent[0].IdSkupina && x.IdKapitoly == 0).SingleOrDefaultAsync();
                studentSplneni.IdSplneni = splneni.Id;

                _dbContext.Add(studentSplneni);
                await _dbContext.SaveChangesAsync();
        }
        //zjistí progres (plnění jeho kapitol) u určitého studenta 
        //TO DO 
        public Student GetStudentProgres(int Id, int IdSkupiny)
        {
            return _dbContext.Student
                .Where(id => id.Id == Id)
                .Include(s => s.StudentSplneni.Where(id => id.splneni.IdSkupiny == IdSkupiny))
                .ThenInclude(s => s.splneni)
                .SingleOrDefault();
        }
        //získa studenta zda-li v dané skupině je 
        //TO DO 
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
        //TO DO 
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
        //TO DO 
        public void DeleteSplneni(int Id)
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
                }
                _dbContext.Splneni.Remove(splneni);
                _dbContext.SaveChanges();                 
        }

        public async Task DeleteStudent(int IdStudenta, int IdSkupiny)
        {
            //vymaže studentoho navázaní na skupinu
            SkupinaStudent SkupinaStudent = await _dbContext.SkupinaStudent.Where(x => x.IdStudent == IdStudenta && x.IdSkupina == IdSkupiny).SingleOrDefaultAsync();
            _dbContext.Remove(SkupinaStudent);
            //vymaže jeho progres 
            List<StudentSplneni> StudentSplneni = await _dbContext.StudentSplneni.Where(x => x.IdStudent == IdStudenta && x.splneni.IdSkupiny == IdSkupiny).ToListAsync();
            _dbContext.RemoveRange(StudentSplneni);
            await _dbContext.SaveChangesAsync();
        }
    }
}

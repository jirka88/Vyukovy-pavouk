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
                studentSplneni.StudentID = skupinaStudent.StudentID;
                //získá uvodní prerekvizitu
                Splneni splneni = await _dbContext.Splneni.Where(x => x.SkupinaID == skupinaStudent.SkupinaID && x.KapitolaID == 0).SingleOrDefaultAsync();
                studentSplneni.SplneniID = splneni.Id;
                studentSplneni.Uspech = true;
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
                studentSplneni.StudentID = student.Id;
                //získá uvodní prerekvizitu
                Splneni splneni = await _dbContext.Splneni.Where(x => x.SkupinaID == student.SkupinaStudent[0].SkupinaID && x.KapitolaID == 0).SingleOrDefaultAsync();
                studentSplneni.SplneniID = splneni.Id;
                studentSplneni.Uspech = true;

                _dbContext.Add(studentSplneni);
                await _dbContext.SaveChangesAsync();
        }
        //zjistí progres (plnění jeho kapitol) u určitého studenta 
        public Student GetStudentProgres(int Id, int IdSkupiny)
        {
            return _dbContext.Student
                .Where(id => id.Id == Id)
                .Include(s => s.StudentSplneni.Where(id => id.splneni.SkupinaID == IdSkupiny))
                .ThenInclude(s => s.splneni)
                .SingleOrDefault();
        }
        //získa studenta zda-li v dané skupině je 
        public Student GetStudent(int IdSkupiny, string EmailStudenta)
        {
           return _dbContext.Student
                .Where(e => e.Email == EmailStudenta)
                .Include(s => s.SkupinaStudent)
                .Include(s => s.StudentSplneni.Where(id => id.splneni.SkupinaID == IdSkupiny))
                    .ThenInclude(s => s.splneni)
                .SingleOrDefault();                             
        }

        //vybere všechny studenty se splněnými kapitoly, které patří do Teamu 
        public List<SkupinaStudent> GetStudents(int ID)
        {
            //vrátí v souhrnu její studenty a jejich splnění kapitol (ID kapitol) krom prerekvizity u úvodní kapitoly - ta je automaticky daná studentovi
            return _dbContext.SkupinaStudent
                    .Where(s => s.SkupinaID == ID)
                    .Include(s => s.Student)
                    .ThenInclude(s => s.StudentSplneni.Where(id => id.splneni.SkupinaID == ID && id.splneni.KapitolaID != 0))
                        .ThenInclude(s => s.splneni)            
                    .ToList();                         
        }
        //vrátí všechny splnění studentů v dané skupině 
        public List<StudentSplneni> GetSplneni(int IdSkupiny)
        {
            return _dbContext.StudentSplneni
                .Include(s => s.splneni).Where(s => s.splneni.SkupinaID == IdSkupiny)
                .ToList();
        }
        //vymaže studentům splnění kapitoly --> při mazání kapitoly 
        public void DeleteSplneni(int Id)
        {          
                List<StudentSplneni> studentiSplneni = new List<StudentSplneni>();
                studentiSplneni = _dbContext.StudentSplneni.Where(id => id.SplneniID == Id).ToList();
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
            SkupinaStudent SkupinaStudent = await _dbContext.SkupinaStudent.Where(x => x.StudentID == IdStudenta && x.SkupinaID == IdSkupiny).SingleOrDefaultAsync();
            _dbContext.Remove(SkupinaStudent);
            //vymaže jeho progres 
            List<StudentSplneni> StudentSplneni = await _dbContext.StudentSplneni.Where(x => x.StudentID == IdStudenta && x.splneni.SkupinaID == IdSkupiny).ToListAsync();
            _dbContext.RemoveRange(StudentSplneni);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateSplneni(StudentSplneni studentSplneni)
        {
            bool pokracuj = true;
            Splneni splneni = await _dbContext.Splneni.Where(x => x.KapitolaID == studentSplneni.splneni.KapitolaID && x.SkupinaID == studentSplneni.splneni.SkupinaID).SingleOrDefaultAsync();
            //vytvoří nové splnění 
            if (splneni == null)
            {
                _dbContext.Splneni.Add(studentSplneni.splneni);
            }
            //připojí s existujícím splněním 
            else
            {
                StudentSplneni studentSplneniPropoj = new StudentSplneni();
                studentSplneniPropoj.SplneniID = splneni.Id;
                studentSplneniPropoj.StudentID = studentSplneni.StudentID;
                studentSplneniPropoj.Uspech = studentSplneni.Uspech;
                _dbContext.StudentSplneni.Add(studentSplneniPropoj);
                pokracuj = false;
            }
            await _dbContext.SaveChangesAsync();
            //nové splnění připojí 
            if(pokracuj)
            {
                splneni = await _dbContext.Splneni.Where(x => x.KapitolaID == studentSplneni.splneni.KapitolaID && x.SkupinaID == studentSplneni.splneni.SkupinaID).SingleOrDefaultAsync();
                studentSplneni.SplneniID = splneni.Id;
                _dbContext.StudentSplneni.Add(studentSplneni);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task OpravaSplneni(StudentSplneni studentSplneni)
        {
          _dbContext.Entry(studentSplneni).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}

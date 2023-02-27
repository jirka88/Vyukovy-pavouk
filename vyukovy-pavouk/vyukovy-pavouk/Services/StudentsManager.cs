using Microsoft.EntityFrameworkCore;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Services
{
    public class StudentsManager : IStudents
    {
        readonly DBContext _dbContext;
        public StudentsManager(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateNewConnect(GroupStudent groupStudent)
        {         
                _dbContext.GroupStudent.Add(groupStudent);
                //vytvoří spojení mezi úvodní kapitolou a studentem 
                StudentCompletion studentSplneni = new StudentCompletion();
                studentSplneni.StudentID = groupStudent.StudentID;
                //získá uvodní prerekvizitu
                Completion splneni = await _dbContext.Completion.Where(x => x.GroupID == groupStudent.GroupID && x.ChapterID == 0).SingleOrDefaultAsync();
                studentSplneni.CompletionID = splneni.Id;
                studentSplneni.Success = true;
                _dbContext.StudentCompletion.Add(studentSplneni);
                await _dbContext.SaveChangesAsync();                  
        }
        //vytvoří nového studenta 
        public async Task CreateNewStudent(Student student)
        {                       
                _dbContext.Student.Add(student);
                await _dbContext.SaveChangesAsync();
                //vytvoří spojení mezi úvodní kapitolou a studentem 
                StudentCompletion studentSplneni = new StudentCompletion();
                studentSplneni.StudentID = student.Id;
                //získá uvodní prerekvizitu
                Completion splneni = await _dbContext.Completion.Where(x => x.GroupID == student.GroupStudents[0].GroupID && x.ChapterID == 0).SingleOrDefaultAsync();
                studentSplneni.CompletionID = splneni.Id;
                studentSplneni.Success = true;

                _dbContext.Add(studentSplneni);
                await _dbContext.SaveChangesAsync();
        }
        //zjistí progres (plnění jeho kapitol) u určitého studenta 
        public Student GetStudentProgres(int Id, int IdGroup)
        {
            return _dbContext.Student
                .Where(id => id.Id == Id)
                .Include(s => s.StudentCompletion.Where(id => id.Completion.GroupID == IdGroup))
                .ThenInclude(s => s.Completion)
                .SingleOrDefault();
        }
        //získa studenta zda-li v dané skupině je 
        public Student GetStudent(int IdGroup, string EmailStudent)
        {
           return _dbContext.Student
                .Where(e => e.Email == EmailStudent)
                .Include(s => s.GroupStudents)
                .Include(s => s.StudentCompletion.Where(id => id.Completion.GroupID == IdGroup))
                    .ThenInclude(s => s.Completion)
                .SingleOrDefault();                             
        }

        //vybere všechny studenty se splněnými kapitoly, které patří do Teamu 
        public List<GroupStudent> GetStudents(int ID)
        {
            //vrátí v souhrnu její studenty a jejich splnění kapitol (ID kapitol) krom prerekvizity u úvodní kapitoly - ta je automaticky daná studentovi
            return _dbContext.GroupStudent
                    .Where(s => s.GroupID == ID)
                    .Include(s => s.Student)                   
                    .ThenInclude(s => s.StudentCompletion.Where(id => id.Completion.GroupID == ID && id.Completion.ChapterID != 0))
                        .ThenInclude(s => s.Completion)
                    .OrderBy(s => s.Student.Surname)
                    .ToList();                         
        }
        public async Task DeleteStudent(int IdStudent, int IdGroup)
        {
            //vymaže studentoho navázaní na skupinu
            GroupStudent groupStudent = await _dbContext.GroupStudent.Where(x => x.StudentID == IdStudent && x.GroupID == IdGroup).SingleOrDefaultAsync();
            _dbContext.Remove(groupStudent);
            //vymaže jeho progres 
            List<StudentCompletion> StudentSplneni = await _dbContext.StudentCompletion.Where(x => x.StudentID == IdStudent && x.Completion.GroupID == IdGroup).ToListAsync();
            _dbContext.RemoveRange(StudentSplneni);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateSplneni(StudentCompletion studentSplneni)
        {
            bool continues = true;
            Completion splneni = await _dbContext.Completion.Where(x => x.ChapterID == studentSplneni.Completion.ChapterID && x.GroupID == studentSplneni.Completion.GroupID).SingleOrDefaultAsync();
            //vytvoří nové splnění 
            if (splneni == null)
            {
                _dbContext.Completion.Add(studentSplneni.Completion);
            }
            //připojí s existujícím splněním 
            else
            {
                StudentCompletion studentSplneniPropoj = new StudentCompletion();
                studentSplneniPropoj.CompletionID = splneni.Id;
                studentSplneniPropoj.StudentID = studentSplneni.StudentID;
                studentSplneniPropoj.Success = studentSplneni.Success;
                _dbContext.StudentCompletion.Add(studentSplneniPropoj);
                continues = false;
            }
            await _dbContext.SaveChangesAsync();
            //nové splnění připojí 
            if(continues)
            {
                splneni = await _dbContext.Completion.Where(x => x.ChapterID == studentSplneni.Completion.ChapterID && x.GroupID == studentSplneni.Completion.GroupID).SingleOrDefaultAsync();
                studentSplneni.CompletionID = splneni.Id;
                _dbContext.StudentCompletion.Add(studentSplneni);
                await _dbContext.SaveChangesAsync();
            }

        }

        public async Task OpravaSplneni(StudentCompletion studentSplneni)
        {
          _dbContext.Entry(studentSplneni).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}

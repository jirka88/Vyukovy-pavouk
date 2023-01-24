using Microsoft.EntityFrameworkCore;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
using vyukovy_pavouk.Interface;
using vyukovy_pavouk.Pages;


namespace vyukovy_pavouk.Services
{
    public class SubjectManager : ISubject
{
        readonly DBContext _dbContext;
        public SubjectManager(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ChangeVisibilitySubject(Subject subject)
        {
            _dbContext.Entry(subject).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        //vrátí počet všech kapitol v daném předmětu 
        public int GetCountChapters(int IdSubject)
        {
            return _dbContext.Chapters.Where(idPredmetu => idPredmetu.SubjectID == IdSubject).Count();
        }
        //vrátí všechny předměty na začátku při vytváření 
        public async Task <List<Subject>> GetSubjects()
        {
            return await _dbContext.Subject.ToListAsync();
        }
        //uloží daný předmět 
        public async Task SaveSubject(Subject subject)
        {
            _dbContext.Subject.Add(subject);
            await _dbContext.SaveChangesAsync();
        }
        //úprava viditelnosti předmětu
        public async Task EditSubject(Group group)
        {
            _dbContext.Entry(group.Subject).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Subject> GetSubjectWithConnectedGroups(int IdSubject)
        {
            Subject subject = await _dbContext.Subject.Where(x => x.Id == IdSubject)
                               .Include(x => x.Group).SingleOrDefaultAsync();
            return subject;                          
        }
    }
}

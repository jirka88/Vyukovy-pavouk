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

        public async Task ChangeVisibilitySubject(Predmet subject)
        {
            _dbContext.Entry(subject).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        //vrátí počet všech kapitol v daném předmětu 
        public int GetCountChapters(int IdSubject)
        {
            return _dbContext.Kapitoly.Where(idPredmetu => idPredmetu.PredmetID == IdSubject).Count();
        }
        //vrátí všechny předměty na začátku při vytváření 
        public async Task <List<Predmet>> GetSubjects()
        {
            return await _dbContext.Predmet.ToListAsync();
        }
        //uloží daný předmět 
        public async Task SaveSubject(Predmet subject)
        {
            _dbContext.Predmet.Add(subject);
            await _dbContext.SaveChangesAsync();
        }
        //úprava viditelnosti předmětu
        public async Task EditSubject(Skupina group)
        {
            _dbContext.Entry(group.predmet).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Predmet> GetSubjectWithConnectedGroups(int IdSubject)
        {
            Predmet subject = await _dbContext.Predmet.Where(x => x.Id == IdSubject)
                               .Include(x => x.Skupina).SingleOrDefaultAsync();
            return subject;                          
        }
    }
}

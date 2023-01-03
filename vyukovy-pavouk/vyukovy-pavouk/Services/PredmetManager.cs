using Microsoft.EntityFrameworkCore;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
using vyukovy_pavouk.Interface;
using vyukovy_pavouk.Pages;


namespace vyukovy_pavouk.Services
{
    public class PredmetManager : IPredmet
{
        readonly DBContext _dbContext;
        public PredmetManager(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task ChangeVisibilitySubject(Predmet predmet)
        {
            _dbContext.Entry(predmet).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        //vrátí počet všech kapitol v daném předmětu 
        public int GetCountChapters(int IDPredmetu)
        {
            return _dbContext.Kapitoly.Where(idPredmetu => idPredmetu.PredmetID == IDPredmetu).Count();
        }
        //vrátí všechny předměty na začátku při vytváření 
        public async Task <List<Predmet>> GetSubjects()
        {
            return await _dbContext.Predmet.ToListAsync();
        }
        //uloží daný předmět 
        public async Task SaveSubject(Predmet predmet)
        {
            _dbContext.Predmet.Add(predmet);
            await _dbContext.SaveChangesAsync();
        }
        //vymaže strukturu (předmět) 
        public async Task DeleteSubject(int IdPredmetu)
        {
            Predmet subject = await _dbContext.Predmet.Where(x => x.Id == IdPredmetu)
                                                .Include(x => x.Skupiny.Where(x => x.PredmetID == IdPredmetu))
                                                .Include(x => x.Kapitoly.Where(x => x.PredmetID == IdPredmetu))
                                                .SingleOrDefaultAsync();

            _dbContext.Remove(subject);
            //vymaže všechny možné splnění v tabulce splnění i s navazaním 
            foreach (Skupina group in subject.Skupiny)
            {
                List <Splneni> splneni = await _dbContext.Splneni.Where(x => x.SkupinaID == group.Id)
                                                           .Include(x => x.StudentSplneni).ToListAsync();               
                _dbContext.RemoveRange(splneni);        
            }
            //vymaže všechny prerekvizity kapitol
            foreach(vyukovy_pavouk.Data.Kapitola kapitola in subject.Kapitoly)
            {
                List<Prerekvizity> prerekvizizy = await _dbContext.Prerekvizity.Where(x => x.PrerekvizityID == kapitola.Id || x.PrerekvizityID == 0).ToListAsync();             
                _dbContext.RemoveRange(prerekvizizy);
            }
            await _dbContext.SaveChangesAsync();

        }
        //úprava viditelnosti předmětu
        public async Task EditSubject(Skupina skupina)
        {
            _dbContext.Entry(skupina.predmet).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Predmet> GetSubjectWithConnectedGroups(int IDSubject)
        {
            Predmet subject = await _dbContext.Predmet.Where(x => x.Id == IDSubject)
                               .Include(x => x.Skupiny).SingleOrDefaultAsync();
            return subject;                          
        }
    }
}

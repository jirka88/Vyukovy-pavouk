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
        public int GetCountKapitoly(int IDPredmetu)
        {
            return _dbContext.Kapitoly.Where(idPredmetu => idPredmetu.PredmetID == IDPredmetu).Count();
        }
        //vrátí všechny předměty na začátku při vytváření 
        public List<Predmet> GetSubjects()
        {
            return _dbContext.Predmet.ToList();
        }
        //uloží daný předmět 
        public async Task SaveSubject(Predmet predmet)
        {
            _dbContext.Predmet.Add(predmet);
            await _dbContext.SaveChangesAsync();
        }
        public void DeleteSubject(int IdPredmetu)
        {
            Predmet predmet = _dbContext.Predmet.Where(x => x.Id == IdPredmetu)
                                                .Include(x => x.Skupiny.Where(x => x.PredmetID == IdPredmetu))
                                                .Include(x => x.Kapitoly.Where(x => x.PredmetID == IdPredmetu))
                                                .SingleOrDefault();

            _dbContext.Remove(predmet);
            //vymazání všech splnění 
            foreach (Skupina item in predmet.Skupiny)
            {
                List <Splneni> splneni = _dbContext.Splneni.Where(x => x.SkupinaID == item.Id)
                                                           .Include(x => x.StudentSplneni).ToList();
                //vymaže všechny možné splnění v tabulce splnění i s navazaním 
                _dbContext.RemoveRange(splneni);        
            }
            //vymaže všechny prerekvizity kapitol
            foreach(vyukovy_pavouk.Data.Kapitola kapitola in predmet.Kapitoly)
            {
                List<Prerekvizity> prerekvizizy = _dbContext.Prerekvizity.Where(x => x.PrerekvizityID == kapitola.Id || x.PrerekvizityID == 0).ToList();             
                _dbContext.RemoveRange(prerekvizizy);
            }
            _dbContext.SaveChanges();

        }

        public async Task EditSubject(Skupina skupina)
        {
            _dbContext.Entry(skupina.predmet).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }
    }
}

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
        //vrátí počet všech kapitol v daném předmětu 
        public int GetCountKapitoly(int IDPredmetu)
        {
            return _dbContext.Kapitoly.Where(idPredmetu => idPredmetu.IdPredmetu == IDPredmetu).Count();
        }
        //vrátí všechny předměty na začátku při vytváření 
        public List<Predmet> GetPredmety()
        {
            return _dbContext.Predmet.ToList();
        }
        //
        public void SavePredmet(Predmet predmet)
        {
            try
            {
                _dbContext.Predmet.Add(predmet);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void SmazPredmet(int IdPredmetu)
        {
            Predmet predmet = _dbContext.Predmet.Where(x => x.Id == IdPredmetu)
                                                .Include(x => x.Skupiny.Where(x => x.IDPredmetu == IdPredmetu))
                                                .Include(x => x.Kapitoly.Where(x => x.IdPredmetu == IdPredmetu))
                                                .SingleOrDefault();

            _dbContext.Remove(predmet);
            //vymazání všech splnění 
            foreach (Skupina item in predmet.Skupiny)
            {
                List <Splneni> splneni = _dbContext.Splneni.Where(x => x.IdSkupiny == item.Id)
                                                           .Include(x => x.StudentSplneni).ToList();
                //vymaže všechny možné splnění v tabulce splnění i s navazaním 
                _dbContext.RemoveRange(splneni);        
            }
            //vymaže všechny prerekvizity kapitol
            foreach(vyukovy_pavouk.Data.Kapitola kapitola in predmet.Kapitoly)
            {
                List<Prerekvizity> prerekvizizy = _dbContext.Prerekvizity.Where(x => x.IdPrerekvizity == kapitola.Id || x.IdPrerekvizity == 0).ToList();             
                _dbContext.RemoveRange(prerekvizizy);
            }
            _dbContext.SaveChanges();

        }

        public void UpravPredmet(Skupina skupina)
        {
            _dbContext.Entry(skupina.predmet).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}

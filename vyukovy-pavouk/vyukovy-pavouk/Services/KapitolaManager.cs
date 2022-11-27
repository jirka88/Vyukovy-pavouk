using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
using Microsoft.EntityFrameworkCore;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Services
{
    public class KapitolaManager : IKapitola
    {
        readonly DBContext _dbContext;
        public KapitolaManager(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void CreateKapitola(Kapitola kapitola)
        {
            try
            {
                _dbContext.Kapitoly.Add(kapitola);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Kapitola GetKapitolaDetail(int IdKapitoly)
        {
            try
            {
                Kapitola Kapitola = _dbContext.Kapitoly
               .Where(id => id.Id == IdKapitoly)
               .Include(v => v.Videa)
               .Include(z => z.Zadani)
               .Include(p => p.KapitolaPrerekvizita)
               .ThenInclude(p => p.prerekvizita)
               .FirstOrDefault();
                return Kapitola;
            }
            catch
            {
                throw;
            }
        }

        public void DeleteKapitola(int Id)
        {
            try
            {
                Kapitola kapitola = _dbContext.Kapitoly.Find(Id);
                if(kapitola != null)
                {
                    _dbContext.Kapitoly.Remove(kapitola);
                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateKapitola(Kapitola kapitola)
        {
            _dbContext.Entry(kapitola).State = EntityState.Modified;
            //zjištění změn u videí 
            foreach (Videa odkaz in kapitola.Videa)
            {
                //pokud nastala změna
                if(odkaz.Id != 0)
                {
                    _dbContext.Entry(odkaz).State = EntityState.Modified;
                }
                //pokud nastalo přidání 
                else
                {
                    _dbContext.Entry(odkaz).State = EntityState.Added;
                }
            }
            //pokud nastalo smazání učitého odkazu 
            List<int> odkazy = new List <int>();
            //zjištění jaké ID se nachází v naší kapitole po změně dat 
            odkazy = kapitola.Videa.Select(x => x.Id).ToList();
            //načtení dat z databáze a porovnání naších změněných dat ty co se zde uloží půjdou k smazání 
            List<Videa> odkazyNaSmazani = new List<Videa>();
            odkazyNaSmazani = await _dbContext.Videa.Where(x => !odkazy.Contains(x.Id) && x.IdKapitoly == kapitola.Id).ToListAsync();

            _dbContext.RemoveRange(odkazyNaSmazani);
            _dbContext.SaveChanges();
        }
    }
}

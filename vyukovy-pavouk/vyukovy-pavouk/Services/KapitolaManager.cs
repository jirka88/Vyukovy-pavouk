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

        public async Task DeleteKapitola(int Idkapitoly)
        {
            Kapitola kapitola = _dbContext.Kapitoly.Find(Idkapitoly);
            if (kapitola != null)
            {
                _dbContext.Kapitoly.Remove(kapitola);
                //vymazání prerekvizity, která mohla být navazána 
                Prerekvizity prerekvizita = await _dbContext.Prerekvizity.Where(x => x.IdPrerekvizity == Idkapitoly).SingleOrDefaultAsync();
                if (prerekvizita != null)
                {
                    _dbContext.Prerekvizity.Remove(prerekvizita);
                }
                //vymazání splnění a navázaní splnění na studentech 
                List <Splneni> splneni = await _dbContext.Splneni.Where(x => x.IdKapitoly == Idkapitoly).ToListAsync();
                if(splneni.Count != 0)
                {
                    _dbContext.Splneni.RemoveRange(splneni);
                }           
                await _dbContext.SaveChangesAsync();
            }
        }
        // TO DO vymazat prerekvizity 
        public async Task UpdateKapitola(Kapitola kapitola)
        {

            List<int> prerekvizityProtiSmazani = new List<int>();
            _dbContext.Entry(kapitola).State = EntityState.Modified;
            foreach (KapitolaPrerekvizita kapitolaPrerekvizita in kapitola.KapitolaPrerekvizita)
            {
                //pokud se jedná o změněnou hodnotu 
                if (kapitolaPrerekvizita.Id != 0)
                {
                    //pokud je prerekvizita null víme, že už je v databázi a pouze upravíme vztah
                    if (kapitolaPrerekvizita.prerekvizita != null)
                    {
                        VyresVztahy(kapitolaPrerekvizita);
                    }
                    _dbContext.Entry(kapitolaPrerekvizita).State = EntityState.Modified;
                    //ukládádám změněné kapitolyPrerekvizity id --> u mazání se pak tyto id nevyberou a tím pádem nesmažou, protože se jedná pouze o změnu 
                    prerekvizityProtiSmazani.Add(kapitolaPrerekvizita.Id);
                }
                //pokud to je přidaná hodnota 
                else
                {
                    if (kapitolaPrerekvizita.prerekvizita != null)
                    {
                        VyresVztahy(kapitolaPrerekvizita);
                    }
                    _dbContext.Entry(kapitolaPrerekvizita).State = EntityState.Added;
                }
            }
            void VyresVztahy(KapitolaPrerekvizita kapitolaPrerekvizita)
            {
                //jdeme zjistit zda-li se naše prerekvizita nenachází v DB
                Prerekvizity prerekvizita = _dbContext.Prerekvizity.Where(id => id.IdPrerekvizity == kapitolaPrerekvizita.prerekvizita.IdPrerekvizity).SingleOrDefault();
                //pokud prerekvizita není musíme ji vytvořit 
                if (prerekvizita == null)
                {
                    kapitolaPrerekvizita.prerekvizita.Id = 0;
                    _dbContext.Entry(kapitolaPrerekvizita.prerekvizita).State = EntityState.Added;
                }
                //pokud je nastavíme u kapitolaPrerekvizita propojení s existující prerekvizitou 
                else
                {
                    kapitolaPrerekvizita.IdPrerekvizita = prerekvizita.Id;
                }
            }
            //zjištění změn u videí 
            foreach (Videa odkaz in kapitola.Videa)
            {
                //pokud nastala změna
                if (odkaz.Id != 0)
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
            List<int> odkazy = new List<int>();
            //zjištění jaké ID se nachází v naší kapitole po změně dat 
            odkazy = kapitola.Videa.Select(x => x.Id).ToList();
            //načtení dat z databáze a porovnání naších změněných dat, ty co se zde uloží půjdou k smazání 
            List<Videa> odkazyNaSmazani = new List<Videa>();
            odkazyNaSmazani = await _dbContext.Videa.Where(x => !odkazy.Contains(x.Id) && x.IdKapitoly == kapitola.Id).ToListAsync();

            //zjištění změn/y u zadaní
            foreach (Zadani zadani in kapitola.Zadani)
            {
                if (zadani.Id != 0)
                {
                    _dbContext.Entry(zadani).State = EntityState.Modified;
                }
                else
                {
                    _dbContext.Entry(zadani).State = EntityState.Added;
                }
            }

            List<int> zadaniList = new List<int>();
            //zjištění jaké ID se nachází v naší kapitole po změně dat 
            zadaniList = kapitola.Zadani.Select(x => x.Id).ToList();
            //načtení dat z databáze a porovnání naších změněných dat ty co se zde uloží půjdou k smazání 
            List<Zadani> zadaniNaSmazani = new List<Zadani>();
            zadaniNaSmazani = await _dbContext.Zadani.Where(x => !zadaniList.Contains(x.Id) && x.IdKapitoly == kapitola.Id).ToListAsync();

            //pokud nastalo smazání učitého odkazu 
            List<int> prerekvizity = new List<int>();
            //zjištění jaké ID se nachází v naší kapitole po změně dat 
            prerekvizity = kapitola.KapitolaPrerekvizita.Select(x => x.IdPrerekvizita).ToList();
            //načtení dat z databáze a porovnání naších změněných dat ty co se zde uloží půjdou k smazání 
            List<KapitolaPrerekvizita> prerekvizityNaSmazani = new List<KapitolaPrerekvizita>();

            prerekvizityNaSmazani = await _dbContext.kapitolaPrerekvizita.Where(x => !prerekvizity.Contains(x.IdPrerekvizita) && !prerekvizityProtiSmazani.Contains(x.Id) && x.KapitolaId == kapitola.Id).ToListAsync();

            _dbContext.RemoveRange(odkazyNaSmazani);
            _dbContext.RemoveRange(zadaniNaSmazani);
            _dbContext.RemoveRange(prerekvizityNaSmazani);
            await _dbContext.SaveChangesAsync();
        }
    }
}

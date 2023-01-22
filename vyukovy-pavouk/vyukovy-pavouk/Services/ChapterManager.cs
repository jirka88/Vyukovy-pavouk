using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
using Microsoft.EntityFrameworkCore;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Services
{
    public class ChapterManager : IChapter
    {
        readonly DBContext _dbContext;
        public ChapterManager(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateKapitola(Kapitola chapter)
        {         
                _dbContext.Kapitoly.Add(chapter);
                await _dbContext.SaveChangesAsync();           
         
        }

        public async Task<Kapitola> GetKapitolaDetail(int IdChapter)
        {           
                Kapitola Chapter = await _dbContext.Kapitoly
               .Where(id => id.Id == IdChapter)
               .Include(v => v.Videa)
               .Include(z => z.Zadani)
               .Include(p => p.KapitolaPrerekvizita)
               .ThenInclude(p => p.prerekvizita)
               .FirstOrDefaultAsync();
                if(Chapter == null)
                {
                    Chapter = new Kapitola();
                }
                return Chapter;       
        }

        public async Task DeleteKapitola(int IdChapter)
        {
            Kapitola chapter = _dbContext.Kapitoly.Find(IdChapter);
            if (chapter != null)
            {
                _dbContext.Kapitoly.Remove(chapter);
                //vymazání prerekvizity, která mohla být navazána 
                Prerekvizity prerekvizita = await _dbContext.Prerekvizity.Where(x => x.PrerekvizityID == IdChapter).SingleOrDefaultAsync();
                if (prerekvizita != null)
                {
                    _dbContext.Prerekvizity.Remove(prerekvizita);
                }
                //vymazání splnění a navázaní splnění na studentech 
                List <Splneni> splneni = await _dbContext.Splneni.Where(x => x.KapitolaID == IdChapter).ToListAsync();
                if(splneni.Count != 0)
                {
                    _dbContext.Splneni.RemoveRange(splneni);
                }           
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task UpdateKapitola(Kapitola chapter)
        {

            List<int> prerekvizityProtiSmazani = new List<int>();
            _dbContext.Entry(chapter).State = EntityState.Modified;
            foreach (KapitolaPrerekvizita kapitolaPrerekvizita in chapter.KapitolaPrerekvizita)
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
                Prerekvizity prerekvizita = _dbContext.Prerekvizity.Where(id => id.PrerekvizityID == kapitolaPrerekvizita.prerekvizita.PrerekvizityID).SingleOrDefault();
                //pokud prerekvizita není musíme ji vytvořit 
                if (prerekvizita == null)
                {
                    kapitolaPrerekvizita.prerekvizita.Id = 0;
                    _dbContext.Entry(kapitolaPrerekvizita.prerekvizita).State = EntityState.Added;
                }
                //pokud je nastavíme u kapitolaPrerekvizita propojení s existující prerekvizitou 
                else
                {
                    kapitolaPrerekvizita.PrerekvizitaID = prerekvizita.Id;
                }
            }
            //zjištění změn u videí 
            foreach (Videa link in chapter.Videa)
            {
                //pokud nastala změna
                if (link.Id != 0)
                {
                    _dbContext.Entry(link).State = EntityState.Modified;
                }
                //pokud nastalo přidání 
                else
                {
                    _dbContext.Entry(link).State = EntityState.Added;
                }
            }
            //pokud nastalo smazání učitého odkazu 
            List<int> links = new List<int>();
            //zjištění jaké ID se nachází v naší kapitole po změně dat 
            links = chapter.Videa.Select(x => x.Id).ToList();
            //načtení dat z databáze a porovnání naších změněných dat, ty co se zde uloží půjdou k smazání 
            List<Videa> linkForDelete = new List<Videa>();
            linkForDelete = await _dbContext.Videa.Where(x => !links.Contains(x.Id) && x.KapitolaID == chapter.Id).ToListAsync();

            //zjištění změn/y u zadaní
            foreach (Zadani assignment in chapter.Zadani)
            {
                if (assignment.Id != 0)
                {
                    _dbContext.Entry(assignment).State = EntityState.Modified;
                }
                else
                {
                    _dbContext.Entry(assignment).State = EntityState.Added;
                }
            }

            List<int> assignmentList = new List<int>();
            //zjištění jaké ID se nachází v naší kapitole po změně dat 
            assignmentList = chapter.Zadani.Select(x => x.Id).ToList();
            //načtení dat z databáze a porovnání naších změněných dat ty co se zde uloží půjdou k smazání 
            List<Zadani> assignmentForDelete = new List<Zadani>();
            assignmentForDelete = await _dbContext.Zadani.Where(x => !assignmentList.Contains(x.Id) && x.KapitolaID == chapter.Id).ToListAsync();

            //pokud nastalo smazání učitého odkazu 
            List<int> prerequisite = new List<int>();
            //zjištění jaké ID se nachází v naší kapitole po změně dat 
            prerequisite = chapter.KapitolaPrerekvizita.Select(x => x.PrerekvizitaID).ToList();
            //načtení dat z databáze a porovnání naších změněných dat ty co se zde uloží půjdou k smazání 
            List<KapitolaPrerekvizita> prerequisiteForDelete = new List<KapitolaPrerekvizita>();

            prerequisiteForDelete = await _dbContext.kapitolaPrerekvizita.Where(x => !prerequisite.Contains(x.PrerekvizitaID) && !prerekvizityProtiSmazani.Contains(x.Id) && x.KapitolaID == chapter.Id).ToListAsync();

            _dbContext.RemoveRange(linkForDelete);
            _dbContext.RemoveRange(assignmentForDelete);
            _dbContext.RemoveRange(prerequisiteForDelete);
            await _dbContext.SaveChangesAsync();
        }
    }
}

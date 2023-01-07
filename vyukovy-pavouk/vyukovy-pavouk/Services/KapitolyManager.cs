using Microsoft.EntityFrameworkCore;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Services
{
    public class KapitolyManager : IKapitoly
    {
        readonly DBContext _dbContext;
        public KapitolyManager(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //vytvoří kopie kapitol z existující struktury (předmětu) 
        public async Task CreateCopyOfChapters(List<Kapitola> chapters)
        {
            List<int> OldId = new List<int>();
            List<int> NewId = new List<int>();
            for (int i = 0; i < chapters.Count(); i++)
            {
                //OldIdNewId[0,i] = chapters[i].Id;
                OldId.Add(chapters[i].Id);
                //resetování starého id u skopírované kapitoly 
                chapters[i].Id = 0;
                //resetování starého id u skopírováného videa
                foreach (Videa video in chapters[i].Videa)
                {
                    video.Id = 0;
                    video.KapitolaID = 0;
                }
                //resetování starého id u zadaná
                foreach (Zadani zadani in chapters[i].Zadani)
                {
                    zadani.Id = 0;
                    zadani.KapitolaID = 0;
                }
                //Změna ID u prerekvizit --> rovnou se uloží v DB
                foreach (KapitolaPrerekvizita kapitolaPrerekvizita in chapters[i].KapitolaPrerekvizita)
                {
                    Prerekvizity prerekvizita = await _dbContext.Prerekvizity.Where(x => x.Id == kapitolaPrerekvizita.PrerekvizitaID).SingleOrDefaultAsync();
                    if (prerekvizita != null)
                    {
                        prerekvizita.Id = 0;
                        _dbContext.Add(prerekvizita);
                        await _dbContext.SaveChangesAsync();
                        //změna starého id za nové ID 
                        kapitolaPrerekvizita.PrerekvizitaID = prerekvizita.Id;
                    }
                    kapitolaPrerekvizita.Id = 0;                 
                }
                _dbContext.Add(chapters[i]);
                await _dbContext.SaveChangesAsync();
                NewId.Add(chapters[i].Id);
            }
        
            //ted je potreba změnit ID prerekvizit v tabulce prerekvizity 
            foreach (Prerekvizity prerekvizita in _dbContext.Prerekvizity.Include(x => x.KapitolaPrerekvizita))
            {
                for (int i = 0; i < chapters.Count; i++)
                {
                    KapitolaPrerekvizita TempPrerekvizita = prerekvizita.KapitolaPrerekvizita.Where(x => x.KapitolaID == NewId[i]).SingleOrDefault();
                    if(TempPrerekvizita != null)
                    {
                        for (int j = 0; j < chapters.Count; j++)
                        {
                            if (OldId[j] == prerekvizita.PrerekvizityID)
                            {
                                prerekvizita.PrerekvizityID = NewId[j];
                                _dbContext.Entry(prerekvizita).State = EntityState.Modified;                           
                                break;
                            }                        
                        }
                        break;
                    }
                }
            }
            await _dbContext.SaveChangesAsync();
        }

        //vrátí všechny kapitoly podle předmětu --> pouze v main zobrazení 
        public async Task <List<Kapitola>> GetChapters(int IdPredmetu)
        {
            return await _dbContext.Kapitoly
                .Where(p => p.PredmetID == IdPredmetu) 
                .Include(p => p.KapitolaPrerekvizita)
                .ThenInclude(p => p.prerekvizita)
                .ToListAsync();
        }
        public async Task<List<Kapitola>> GetChaptersWithAll(int IdPredmetu)
        {
            return await _dbContext.Kapitoly
                .Where(p => p.PredmetID == IdPredmetu)
                .Include(p => p.Zadani)
                .Include(p => p.Videa)
                .Include(p => p.KapitolaPrerekvizita)
                /*.ThenInclude(p => p.prerekvizita)*/
                .ToListAsync();
        }
        //vrátí pouze kapitoly --> použití u načtení kapitol (prerekvizit) při vytváření kapitoly 
        public async Task <List<Kapitola>> GetChaptersOnly(int IdPredmetu)
        {
            return await _dbContext.Kapitoly
                .Where(p => p.PredmetID == IdPredmetu)
                .ToListAsync();
        }

        public async Task <List<KapitolaPrerekvizita>> GetChaptersPrerequisites(int IdPredmetu)
        {
            return await _dbContext.kapitolaPrerekvizita
                .Include(p => p.prerekvizita)
                .Include(k => k.kapitola).Where(x => x.kapitola.PredmetID == IdPredmetu)
                .ToListAsync();
        }
    }
}

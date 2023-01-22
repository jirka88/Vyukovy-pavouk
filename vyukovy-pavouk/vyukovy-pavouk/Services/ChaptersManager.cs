using Microsoft.EntityFrameworkCore;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Services
{
    public class ChaptersManager : IChapters
    {
        readonly DBContext _dbContext;
        public ChaptersManager(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //vytvoří kopie kapitol z existující struktury (předmětu) 
        public async Task CreateCopyOfChapters(List<Kapitola> chapters)
        {
            List<int> OldId = new List<int>();
            List<int> NewId = new List<int>();
            List<int> NewKapitolaPrerekvizitaId = new List<int>();
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
                foreach (Zadani assignment in chapters[i].Zadani)
                {
                    assignment.Id = 0;
                    assignment.KapitolaID = 0;
                }
                //Změna ID u prerekvizit --> rovnou se uloží v DB
               
                foreach (KapitolaPrerekvizita kapitolaPrerekvizita in chapters[i].KapitolaPrerekvizita)
                {
                    bool pass = true;
                    Prerekvizity prerequisite = await _dbContext.Prerekvizity.Where(x => x.Id == kapitolaPrerekvizita.PrerekvizitaID).SingleOrDefaultAsync();
                    if (prerequisite != null)
                    {
                        //kontrola zdali v mezitabulce (kapitola-Prerekvizita) nelze navázat na nově existující prerekvizitu (jinak by vznikaly duplicitní prerekvizity)
                        for (int j = 0; j < OldId.Count; j++)
                        {
                            //pokud se prerekvizita rovná starému ID, víme, že už jsme ho zakladáli
                            if(prerequisite.PrerekvizityID == OldId[j] && NewKapitolaPrerekvizitaId.Count != 0)
                            {
                                //projíždíme nově vytvořené prerekvizity 
                                for (int k = 0; k < NewKapitolaPrerekvizitaId.Count; k++)
                                {
                                    //zjistíme prerekvizitu, podle starého PrerekvizitaId a nového ID --> ted to stačí propojit v mezitabulce 
                                    Prerekvizity prerequisiteForConnect = await _dbContext.Prerekvizity.Where(x => x.PrerekvizityID == OldId[j] && x.Id == NewKapitolaPrerekvizitaId[k])
                                        .SingleOrDefaultAsync();
                                    if (prerequisiteForConnect != null)
                                    {
                                        //propojí s existující prerekvizitu                
                                        kapitolaPrerekvizita.PrerekvizitaID = prerequisiteForConnect.Id;
                                        pass = false;
                                        break;
                                    }
                                }                                        
                            }
                        }
                        if (pass) {
                            prerequisite.Id = 0;
                            _dbContext.Add(prerequisite);
                            await _dbContext.SaveChangesAsync();
                            //změna starého id za nové ID 
                            NewKapitolaPrerekvizitaId.Add(prerequisite.Id);
                            kapitolaPrerekvizita.PrerekvizitaID = prerequisite.Id;
                        }                     
                        
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
        public async Task <List<Kapitola>> GetChapters(int IdSubject)
        {
            return await _dbContext.Kapitoly
                .Where(p => p.PredmetID == IdSubject) 
                .Include(p => p.KapitolaPrerekvizita)
                .ThenInclude(p => p.prerekvizita)
                .ToListAsync();
        }
        public async Task<List<Kapitola>> GetChaptersWithAll(int IdSubject)
        {
            return await _dbContext.Kapitoly
                .Where(p => p.PredmetID == IdSubject)
                .Include(p => p.Zadani)
                .Include(p => p.Videa)
                .Include(p => p.KapitolaPrerekvizita)
                /*.ThenInclude(p => p.prerekvizita)*/
                .ToListAsync();
        }
        //vrátí pouze kapitoly --> použití u načtení kapitol (prerekvizit) při vytváření kapitoly 
        public async Task <List<Kapitola>> GetChaptersOnly(int IdSubject)
        {
            return await _dbContext.Kapitoly
                .Where(p => p.PredmetID == IdSubject)
                .ToListAsync();
        }

        public async Task <List<KapitolaPrerekvizita>> GetChaptersPrerequisites(int IdSubject)
        {
            return await _dbContext.kapitolaPrerekvizita
                .Include(p => p.prerekvizita)
                .Include(k => k.kapitola).Where(x => x.kapitola.PredmetID == IdSubject)
                .ToListAsync();
        }
    }
}

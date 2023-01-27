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
        public async Task CreateCopyOfChapters(List<Chapter> chapters)
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
                foreach (Link video in chapters[i].Links)
                {
                    video.Id = 0;
                    video.ChapterID = 0;
                }
                //resetování starého id u zadaná
                foreach (Assignment assignment in chapters[i].Assignments)
                {
                    assignment.Id = 0;
                    assignment.ChapterID = 0;
                }
                //Změna ID u prerekvizit --> rovnou se uloží v DB
               
                foreach (ChapterPrerequisite kapitolaPrerekvizita in chapters[i].ChapterPrerequisites)
                {
                    bool pass = true;
                    Prerequisite prerequisite = await _dbContext.Prerequisite.Where(x => x.Id == kapitolaPrerekvizita.PrerequisiteID).SingleOrDefaultAsync();
                    if (prerequisite != null)
                    {
                        //kontrola zdali v mezitabulce (kapitola-Prerekvizita) nelze navázat na nově existující prerekvizitu (jinak by vznikaly duplicitní prerekvizity)
                        for (int j = 0; j < OldId.Count; j++)
                        {
                            //pokud se prerekvizita rovná starému ID, víme, že už jsme ho zakladáli
                            if(prerequisite.PrerequisiteID == OldId[j] && NewKapitolaPrerekvizitaId.Count != 0)
                            {
                                //projíždíme nově vytvořené prerekvizity 
                                for (int k = 0; k < NewKapitolaPrerekvizitaId.Count; k++)
                                {
                                    //zjistíme prerekvizitu, podle starého PrerekvizitaId a nového ID --> ted to stačí propojit v mezitabulce 
                                    Prerequisite prerequisiteForConnect = await _dbContext.Prerequisite.Where(x => x.PrerequisiteID == OldId[j] && x.Id == NewKapitolaPrerekvizitaId[k])
                                        .SingleOrDefaultAsync();
                                    if (prerequisiteForConnect != null)
                                    {
                                        //propojí s existující prerekvizitu                
                                        kapitolaPrerekvizita.PrerequisiteID = prerequisiteForConnect.Id;
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
                            kapitolaPrerekvizita.PrerequisiteID = prerequisite.Id;
                        }                     
                        
                    }
                        kapitolaPrerekvizita.Id = 0;                                       
                }
                _dbContext.Add(chapters[i]);
                await _dbContext.SaveChangesAsync();
                NewId.Add(chapters[i].Id);
            }
        
            //ted je potreba změnit ID prerekvizit v tabulce prerekvizity 
            foreach (Prerequisite prerekvizita in _dbContext.Prerequisite.Include(x => x.ChapterPrerequisites))
            {
                for (int i = 0; i < chapters.Count; i++)
                {
                    ChapterPrerequisite TempPrerekvizita = prerekvizita.ChapterPrerequisites.Where(x => x.ChapterID == NewId[i]).SingleOrDefault();
                    if(TempPrerekvizita != null)
                    {
                        for (int j = 0; j < chapters.Count; j++)
                        {
                            if (OldId[j] == prerekvizita.PrerequisiteID)
                            {
                                prerekvizita.PrerequisiteID = NewId[j];
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
        public async Task <List<Chapter>> GetChapters(int IdSubject)
        {
            return await _dbContext.Chapters
                .Where(p => p.SubjectID == IdSubject) 
                .Include(p => p.ChapterPrerequisites.OrderBy(x => x.Prerequisite.PrerequisiteID))
                .ThenInclude(p => p.Prerequisite)
                .ToListAsync();
        }
        public async Task<List<Chapter>> GetChaptersWithAll(int IdSubject)
        {
            return await _dbContext.Chapters
                .Where(p => p.SubjectID == IdSubject)
                .Include(p => p.Assignments)
                .Include(p => p.Links)
                .Include(p => p.ChapterPrerequisites)
                /*.ThenInclude(p => p.prerekvizita)*/
                .ToListAsync();
        }
        //vrátí pouze kapitoly --> použití u načtení kapitol (prerekvizit) při vytváření kapitoly 
        public async Task <List<Chapter>> GetChaptersOnly(int IdSubject)
        {
            return await _dbContext.Chapters
                .Where(p => p.SubjectID == IdSubject)
                .ToListAsync();
        }

        public async Task <List<ChapterPrerequisite>> GetChaptersPrerequisites(int IdSubject)
        {
            return await _dbContext.ChapterPrerequisite
                .Include(p => p.Prerequisite)
                .Include(k => k.Chapter).Where(x => x.Chapter.SubjectID == IdSubject)
                .ToListAsync();
        }
    }
}

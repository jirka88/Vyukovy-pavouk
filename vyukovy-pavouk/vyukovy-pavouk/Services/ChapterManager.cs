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
        public async Task CreateChapter(Chapter chapter)
        {         
                _dbContext.Chapters.Add(chapter);
                await _dbContext.SaveChangesAsync();           
        }
        public async Task<Chapter> GetChapterDetail(int IdChapter)
        {           
                Chapter Chapter = await _dbContext.Chapters
               .Where(id => id.Id == IdChapter)
               .Include(v => v.Links)
               .Include(z => z.Assignments)
               .Include(p => p.ChapterPrerequisites.OrderBy(x => x.Prerequisite.PrerequisiteID))
               .ThenInclude(p => p.Prerequisite)        
               .FirstOrDefaultAsync();
                if(Chapter == null)
                {
                    Chapter = new Chapter();
                }
                return Chapter;       
        }

        public async Task DeleteChapter(int IdChapter)
        {
            Chapter chapter = _dbContext.Chapters.Where(x => x.Id == IdChapter).Include(x => x.ChapterPrerequisites).SingleOrDefault();
            if (chapter != null)
            {
                _dbContext.Chapters.Remove(chapter);
                //vymazání prerekvizity, kde byla použita tato kapitola 
                Prerequisite prerequisite = await _dbContext.Prerequisite.Where(x => x.PrerequisiteID == IdChapter).SingleOrDefaultAsync();
                if (prerequisite != null)
                {
                    _dbContext.Prerequisite.Remove(prerequisite);
                }
                //vymazání prerekvizit, které nenavazují na nic 
                foreach (ChapterPrerequisite chapterPrerequisite in chapter.ChapterPrerequisites)
                {
                   List<ChapterPrerequisite> chapterPrerequisites = await _dbContext.ChapterPrerequisite.Where(x => x.PrerequisiteID == chapterPrerequisite.PrerequisiteID).Include(x => x.Prerequisite).ToListAsync();
                   if(chapterPrerequisites.Count() == 1)
                    {
                        _dbContext.Prerequisite.Remove(chapterPrerequisites.Select(x => x.Prerequisite).SingleOrDefault());
                    }
                   }
                //vymazání splnění a navázaní splnění na studentech 
                List <Completion> completion = await _dbContext.Completion.Where(x => x.ChapterID == IdChapter).ToListAsync();
                if(completion.Count != 0)
                {
                    _dbContext.Completion.RemoveRange(completion);
                }           
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task UpdateChapter(Chapter chapter)
        {

            List<int> prerekvizityProtiSmazani = new List<int>();
            _dbContext.Entry(chapter).State = EntityState.Modified;
            foreach (ChapterPrerequisite chapterPrerequisite in chapter.ChapterPrerequisites.ToList())
            {
                //pokud se jedná o změněnou hodnotu 
                if (chapterPrerequisite.Id != 0)
                {
                    //pokud není prerekvizita null víme, že už je v databázi a pouze upravíme vztah
                    if (chapterPrerequisite.Prerequisite != null)
                    {
                        VyresVztahy(chapterPrerequisite);
                    }
                    //ukládádám změněné kapitolyPrerekvizity id --> u mazání se pak tyto id nevyberou a tím pádem nesmažou, protože se jedná pouze o změnu 
                    prerekvizityProtiSmazani.Add(chapterPrerequisite.Id);
                    _dbContext.Entry(chapterPrerequisite).State = EntityState.Modified;

                }
                //pokud to je přidaná hodnota 
                else
                {
                    //složí k uložení prerekvizity
                    Prerequisite prerequisitePre = new Prerequisite();
                    //slouží k uložení existujícího vztahu v DB mezi chapterPrerequisite a Prerequisite 
                    Prerequisite TestIfPrerequisiteExistInDb = new Prerequisite();
                    //pokud ChapterPrerequisite nemá načtenou svojí Prerequisite 
                    if (chapterPrerequisite.Prerequisite == null)
                    {
                        prerequisitePre = await _dbContext.Prerequisite.Where(x => x.Id == chapterPrerequisite.PrerequisiteID).SingleOrDefaultAsync();
                        TestIfPrerequisiteExistInDb = await _dbContext.Prerequisite.Where(x => x.PrerequisiteID == prerequisitePre.PrerequisiteID).SingleOrDefaultAsync();
                    }
                    //pokud jí už má 
                    else
                    {                
                        TestIfPrerequisiteExistInDb = await _dbContext.Prerequisite.Where(x => x.PrerequisiteID == chapterPrerequisite.Prerequisite.PrerequisiteID).SingleOrDefaultAsync();
                    }      
                    //pokud prerekvizita nebyla vytvořena 
                    if(TestIfPrerequisiteExistInDb == null)
                    {
                        Prerequisite NewPrerequisite = new Prerequisite();
                        NewPrerequisite.PrerequisiteID = chapterPrerequisite.Prerequisite.PrerequisiteID;
                        _dbContext.Prerequisite.Add(NewPrerequisite);
                        await _dbContext.SaveChangesAsync();
                        TestIfPrerequisiteExistInDb = NewPrerequisite;
                    }
                    ChapterPrerequisite test = _dbContext.ChapterPrerequisite.Where(x => x.ChapterID == chapter.Id && x.Prerequisite.PrerequisiteID == TestIfPrerequisiteExistInDb.PrerequisiteID).SingleOrDefault();
                    //pokud je test null --> je potřeba navázat na kapitolu chapterPrerequisite s prerequisite, pokud není null --> je v DB už propojena a nic se nevytváří
                    if (test == null)
                    {
                        if (chapterPrerequisite.Prerequisite != null)
                        {
                            VyresVztahy(chapterPrerequisite);
                        }
                        _dbContext.Entry(chapterPrerequisite).State = EntityState.Added;
                    }
                      

                }
            }
            void VyresVztahy(ChapterPrerequisite chapterPrerequisite)
            {
                //jdeme zjistit zda-li se naše prerekvizita nenachází v DB
                Prerequisite prerequisite = _dbContext.Prerequisite.Where(id => id.PrerequisiteID == chapterPrerequisite.Prerequisite.PrerequisiteID).SingleOrDefault();
                //pokud prerekvizita není musíme ji vytvořit 
                if (prerequisite == null)
                {
                    chapterPrerequisite.Prerequisite.Id = 0;
                    _dbContext.Entry(chapterPrerequisite.Prerequisite).State = EntityState.Added;
                }
                //pokud je nastavíme u kapitolaPrerekvizita propojení s existující prerekvizitou 
                else
                {
                    chapterPrerequisite.PrerequisiteID = prerequisite.Id;
                }
            }
            //zjištění změn u videí 
            foreach (Link link in chapter.Links)
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
            links = chapter.Links.Select(x => x.Id).ToList();
            //načtení dat z databáze a porovnání naších změněných dat, ty co se zde uloží půjdou k smazání 
            List<Link> linkForDelete = new List<Link>();
            linkForDelete = await _dbContext.Link.Where(x => !links.Contains(x.Id) && x.ChapterID == chapter.Id).ToListAsync();

            //zjištění změn/y u zadaní
            foreach (Assignment assignment in chapter.Assignments)
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
            assignmentList = chapter.Assignments.Select(x => x.Id).ToList();
            //načtení dat z databáze a porovnání naších změněných dat ty co se zde uloží půjdou k smazání 
            List<Assignment> assignmentForDelete = new List<Assignment>();
            assignmentForDelete = await _dbContext.Assignment.Where(x => !assignmentList.Contains(x.Id) && x.ChapterID == chapter.Id).ToListAsync();

            //pokud nastalo smazání učitého odkazu 
            List<int> prerequisite = new List<int>();
            //zjištění jaké ID se nachází v naší kapitole po změně dat 
            prerequisite = chapter.ChapterPrerequisites.Select(x => x.PrerequisiteID).ToList();
            //načtení dat z databáze a porovnání naších změněných dat ty co se zde uloží půjdou k smazání 
            List<ChapterPrerequisite> prerequisiteForDelete = new List<ChapterPrerequisite>();

            prerequisiteForDelete = await _dbContext.ChapterPrerequisite.Where(x => !prerequisite.Contains(x.PrerequisiteID) && !prerekvizityProtiSmazani.Contains(x.Id) && x.ChapterID == chapter.Id).ToListAsync();

            _dbContext.RemoveRange(linkForDelete);
            _dbContext.RemoveRange(assignmentForDelete);
            _dbContext.RemoveRange(prerequisiteForDelete);
            //vymazání duplicitních prerekvizit 

            await _dbContext.SaveChangesAsync();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Services
{
    public class GroupManager : IGroup
    {
        readonly DBContext _dbContext;
        public GroupManager(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //připojí resetovaný předmět k nové skupině 
        public async Task ConnectGroup(Group group)
        {
     
            Subject SubjectWithGroup = await _dbContext.Subject.Where(x => x.Name == group.Subject.Name)
                                                               .Include(x => x.Group)
                                                               .SingleOrDefaultAsync();
            if(SubjectWithGroup.Group.TmGroup == "")
            {
                SubjectWithGroup.Group.TmGroup = group.TmGroup;
                _dbContext.Entry(SubjectWithGroup).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
        }
        //vytvoří Teams skupinu pod existující předmět v databázi 
        public async Task AddGroup(Group group)
        {        
            _dbContext.Group.Add(group);   
            await _dbContext.SaveChangesAsync();                
        }

        public async Task CreateIntroductionPrerequisite(Completion splneni)
        {      
                _dbContext.Completion.Add(splneni);
                await _dbContext.SaveChangesAsync();                
        }

        //zeptá se zda-li náš Teams v MS Teamu je v databázi --> pokud není, učitel ho bude muset založit s názvem předmetu v Tab 
        public async Task<Group> GetGroup(string IDTeam)
        {           
                Group group = await _dbContext.Group
                    .Where(s => s.TmGroup == IDTeam)
                    .Include(p => p.Subject)
                    .FirstOrDefaultAsync();
            if(group == null)
            {
                group = new Group();
            }
            return group;           
      
        }
   
        public async Task DeleteGroup(int Id)
        {
            //načteme (strukturu) předmět s jeho kapitolamy 
            Subject subject = await _dbContext.Subject.Where(x => x.GroupID == Id)
                                               .Include(x => x.Chapters)
                                               .SingleOrDefaultAsync();
            //vymaže veškeré splnění
            List<Completion> vsechnySplneni = await _dbContext.Completion
                                            .Where(x => x.GroupID == Id)
                                            .ToListAsync();
            _dbContext.RemoveRange(vsechnySplneni);
            //vymaže napojení studentů na skupinu
            List<GroupStudent> SkupinaStudent = await _dbContext.GroupStudent
                                                   .Where(x => x.GroupID == Id)
                                                   .ToListAsync();
            _dbContext.RemoveRange(SkupinaStudent);
            //vymaže samostaně prerekvizity krom úvodní - úvod má ID 0 
            foreach (vyukovy_pavouk.Data.Chapter chapter in subject.Chapters)
            {
               
                Prerequisite prerekvizizy = await _dbContext.Prerequisite.Where(x => x.PrerequisiteID == chapter.Id).SingleOrDefaultAsync();
                if(prerekvizizy != null)
                {
                    _dbContext.Remove(prerekvizizy);
                }              
            }
            //vymaže úvodní prerekvizitu patřící pod určitou skupinou 
            foreach (vyukovy_pavouk.Data.Chapter chapter in subject.Chapters)
            {
                ChapterPrerequisite kapitolaPrerekvizita = await _dbContext.ChapterPrerequisite
                    .Include(x => x.Prerequisite).Where(x => x.Prerequisite.PrerequisiteID == 0 && x.ChapterID == chapter.Id).SingleOrDefaultAsync();
                if(kapitolaPrerekvizita != null)
                {
                    _dbContext.Remove(kapitolaPrerekvizita.Prerequisite);
                    break;
                }
            }
                //vymaže skupinu s předmětem --> to následně vymaže veškeré kaptitoly navázané prerevizity, videa, zadání...
                Group group = _dbContext.Group.Find(Id);
            _dbContext.Group.Remove(group);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ResetGroup(int Id)
        {
            //vymazání skupiny
            Group group = await _dbContext.Group
                .Where(x => x.Id == Id)
                .Include(x => x.Subject).SingleOrDefaultAsync();
            group.Subject.Private = true;
            group.TmGroup = "";
            _dbContext.Entry(group).State = EntityState.Modified;

            //vymazání napojení
            List<GroupStudent> SkupinaStudent = await _dbContext.GroupStudent.Where(x => x.GroupID == Id).ToListAsync();    
           _dbContext.RemoveRange(SkupinaStudent);
            //vymazání splnění 
            List<Completion> splneni = await _dbContext.Completion.Where(x => x.GroupID == Id).ToListAsync();
            _dbContext.RemoveRange(splneni);    
            await _dbContext.SaveChangesAsync();
        }
    }
}

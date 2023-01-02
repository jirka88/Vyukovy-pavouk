using Microsoft.EntityFrameworkCore;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Services
{
    public class SkupinaManager : ISkupina
    {
        readonly DBContext _dbContext;
        public SkupinaManager(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //vytvoří Teams skupinu pod existující předmět v databázi 
        public async Task AddGroup(Skupina skupina)
        {         
                _dbContext.Skupina.Add(skupina);
                await _dbContext.SaveChangesAsync();                
        }

        public async Task CreateIntroductionPrerequisite(Splneni splneni)
        {      
                _dbContext.Splneni.Add(splneni);
                await _dbContext.SaveChangesAsync();                
        }

        //zeptá se zda-li náš Teams v MS Teamu je v databázi --> pokud není, učitel ho bude muset založit s názvem předmetu v Tab 
        public async Task<Skupina> GetGroup(string IDTeamu)
        {           
                Skupina skupina = await _dbContext.Skupina
                    .Where(s => s.TmSkupina == IDTeamu)
                    .Include(p => p.predmet)
                    .FirstOrDefaultAsync();
            if(skupina == null)
            {
                skupina = new Skupina();
            }
            return skupina;           
      
        }
   
        public async Task ResetGroup(int Id)
        {
            List<Splneni> vsechnySplneni = await _dbContext.Splneni
                                            .Where(x => x.SkupinaID == Id)
                                            .ToListAsync();
            _dbContext.RemoveRange(vsechnySplneni);

            List<SkupinaStudent> SkupinaStudent = await _dbContext.SkupinaStudent
                                                   .Where(x => x.SkupinaID == Id)
                                                   .ToListAsync();
            _dbContext.RemoveRange(SkupinaStudent);

            Skupina group = _dbContext.Skupina.Find(Id);
            _dbContext.Skupina.Remove(group);
            await _dbContext.SaveChangesAsync();
        }
    }
}

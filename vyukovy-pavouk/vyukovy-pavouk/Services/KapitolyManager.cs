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
  
        //vrátí všechny kapitoly podle předmětu --> pouze v main zobrazení 
        public async Task <List<Kapitola>> GetChapters(int IdPredmetu)
        {
            return await _dbContext.Kapitoly
                .Where(p => p.PredmetID == IdPredmetu) 
                .Include(p => p.KapitolaPrerekvizita)
                .ThenInclude(p => p.prerekvizita)
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

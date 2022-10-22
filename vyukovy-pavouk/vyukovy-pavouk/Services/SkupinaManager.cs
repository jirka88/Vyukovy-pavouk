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
        //zeptá se zda-li náš Teams v MS Teamu je v databázi --> pokud není, učitel ho bude muset založit s názvem předmetu v Tab 
        public Skupina GetSkupina(string IDTeamu)
        {
            try
            {
                var skupina = _dbContext.Skupina
                    .Where(s => s.TmSkupina == IDTeamu)
                    .Include(p => p.predmet)
                    .FirstOrDefault();
                return skupina;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

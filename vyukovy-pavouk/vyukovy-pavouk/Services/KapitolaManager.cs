using Microsoft.EntityFrameworkCore;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
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
        //vrátí všechny kapitoly podle předmětu --> pouze v main zobrazení 
        public List<Kapitola> GetKapitoly(int IdPredmetu)
        {
            return _dbContext.Kapitoly
                .Where(p => p.IdPredmetu == IdPredmetu)             
                .ToList();
            //zde bude include na prerekvizity 
        }
    }
}

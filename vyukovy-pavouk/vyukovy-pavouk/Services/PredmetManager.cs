using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
using vyukovy_pavouk.Interface;


namespace vyukovy_pavouk.Services
{
    public class PredmetManager : IPredmet
{
        readonly DBContext _dbContext;
        public PredmetManager(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Predmet> GetPredmety()
        {
            return _dbContext.Predmet.ToList();
        }
    }
}

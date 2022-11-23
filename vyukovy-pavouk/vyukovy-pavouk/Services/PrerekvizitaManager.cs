using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Services
{
    public class PrerekvizitaManager : IPrerekvizita
    {
        readonly DBContext _dbContext;
        public PrerekvizitaManager(DBContext dbContext)
        {
            _dbContext = dbContext;
        }
        //získání úvodní prerekvizity --> 0 --> pak se přidá při vytvoření uživatele a tím se odemkne úvodní kapitola 
        public Splneni GetUvodniPrerekvizitu(int Idkapitoly)
        {
            return _dbContext.Splneni
                .Where(id => id.IdSkupiny == Idkapitoly)
                .SingleOrDefault();
        }
    }
}

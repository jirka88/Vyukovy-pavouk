using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Services
{
    public class TestManager : ITest
{
        readonly DBContext _dbContext;
        public TestManager(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Test> GetItems()
        {
            return _dbContext.Test.ToList();
        }
    }
}

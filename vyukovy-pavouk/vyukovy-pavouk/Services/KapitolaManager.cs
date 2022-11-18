using vyukovy_pavouk.Data;
using vyukovy_pavouk.DBContexts;
using Microsoft.EntityFrameworkCore;
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

        public void CreateKapitola(Kapitola kapitola)
        {
            try
            {
                _dbContext.Kapitoly.Add(kapitola);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Kapitola GetKapitolaDetail(int IdKapitoly)
        {
            try
            {
                Kapitola Kapitola = _dbContext.Kapitoly
               .Where(id => id.Id == IdKapitoly)
               .Include(v => v.Videa)
               .Include(z => z.Zadani)
               .Include(p => p.KapitolaPrerekvizita)
               .ThenInclude(p => p.prerekvizita)
               .FirstOrDefault();
                return Kapitola;
            }
            catch
            {
                throw;
            }
        }

        public void DeleteKapitola(int Id)
        {
            try
            {
                Kapitola kapitola = _dbContext.Kapitoly.Find(Id);
                if(kapitola != null)
                {
                    _dbContext.Kapitoly.Remove(kapitola);
                    _dbContext.SaveChanges();
                }
                else
                {
                    throw new ArgumentNullException();
                }
            }
            catch
            {
                throw;
            }
        }
    
    }
}

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
        public void AddSkupina(Skupina skupina)
        {
            try
            {
                _dbContext.Skupina.Add(skupina);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CreateUvodniPrerekvizita(Splneni splneni)
        {
            try
            {
                _dbContext.Splneni.Add(splneni);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
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
        //TO DO upravit na Splneni
        public void ResetSkupina(int Id)
        {
             List<StudentSplneni> VsechnySplneni = _dbContext.StudentSplneni
                                                    .Include(x => x.splneni)
                                                    .Where(x => x.splneni.IdSkupiny == Id).ToList();
            //zde vymažeme i všechny splnění studentů (jak spojení StudentSplneni tak i samotné splnění) 
            if(VsechnySplneni.Count() != 0)
            {
                _dbContext.RemoveRange(VsechnySplneni);
                _dbContext.Splneni.RemoveRange(VsechnySplneni.Select(x => x.splneni));
                List<SkupinaStudent> SkupinaStudent = _dbContext.SkupinaStudent.Where(x => x.IdSkupina == Id).ToList();
                _dbContext.RemoveRange(SkupinaStudent);

            }
            //pokud resetujeme skupinu, ale žádný student se k ní ještě nepřipojil (nevzniklo navázání na splnění přes StudentSplneni)
            else
            {
                Splneni uvodni = _dbContext.Splneni.Where(x => x.IdSkupiny == Id && x.IdKapitoly == 0).SingleOrDefault();         
                _dbContext.Splneni.Remove(uvodni);
            }
            Skupina skupina = _dbContext.Skupina.Find(Id);
            _dbContext.Skupina.Remove(skupina);
            _dbContext.SaveChanges();
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
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
        public async Task<Skupina> GetSkupina(string IDTeamu)
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
   
        public void ResetGroup(int Id)
        {
            List<Splneni> vsechnySplneni = _dbContext.Splneni
                                            .Where(x => x.SkupinaID == Id)
                                            .ToList();
            _dbContext.RemoveRange(vsechnySplneni);

            List<SkupinaStudent> SkupinaStudent = _dbContext.SkupinaStudent
                                                   .Where(x => x.SkupinaID == Id)
                                                   .ToList();
            _dbContext.RemoveRange(SkupinaStudent);

            Skupina skupina = _dbContext.Skupina.Find(Id);
            _dbContext.Skupina.Remove(skupina);
            _dbContext.SaveChanges();
        }
    }
}

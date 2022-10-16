﻿using vyukovy_pavouk.Data;
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
        //zeptá se zda-li náš Teams v MS Teamu je v databázi --> pokud není učitel ho bude muset založit v Tab 
        public Skupina GetSkupina(string IDTeamu)
        {
            try
            {
                var skupina = _dbContext.Skupina.Where(s => s.TmSkupina == IDTeamu).SingleOrDefault<Skupina>();
                return skupina;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
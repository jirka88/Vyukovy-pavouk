﻿using vyukovy_pavouk.Data;
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
        //vrátí počet všech kapitol v daném předmětu 
        public int GetCountKapitoly(int IDPredmetu)
        {
            return _dbContext.Kapitoly.Where(idPredmetu => idPredmetu.IdPredmetu == IDPredmetu).Count();
        }
        //vrátí všechny předměty na začátku při vytváření 
        public List<Predmet> GetPredmety()
        {
            return _dbContext.Predmet.ToList();
        }
    }
}
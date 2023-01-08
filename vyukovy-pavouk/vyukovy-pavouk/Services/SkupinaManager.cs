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
        public async Task AddGroup(Skupina skupina)
        {         
                _dbContext.Skupina.Add(skupina);
                await _dbContext.SaveChangesAsync();                
        }

        public async Task CreateIntroductionPrerequisite(Splneni splneni)
        {      
                _dbContext.Splneni.Add(splneni);
                await _dbContext.SaveChangesAsync();                
        }

        //zeptá se zda-li náš Teams v MS Teamu je v databázi --> pokud není, učitel ho bude muset založit s názvem předmetu v Tab 
        public async Task<Skupina> GetGroup(string IDTeamu)
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
   
        public async Task DeleteGroup(int Id)
        {
            //načteme (strukturu) předmět s jeho kapitolamy 
            Predmet subject = await _dbContext.Predmet.Where(x => x.SkupinaID == Id)
                                               .Include(x => x.Kapitoly)
                                               .SingleOrDefaultAsync();
            //vymaže veškeré splnění
            List<Splneni> vsechnySplneni = await _dbContext.Splneni
                                            .Where(x => x.SkupinaID == Id)
                                            .ToListAsync();
            _dbContext.RemoveRange(vsechnySplneni);
            //vymaže napojení studentů na skupinu
            List<SkupinaStudent> SkupinaStudent = await _dbContext.SkupinaStudent
                                                   .Where(x => x.SkupinaID == Id)
                                                   .ToListAsync();
            _dbContext.RemoveRange(SkupinaStudent);
            //vymaže samostaně prerekvizity krom úvodní - úvod má ID 0 
            foreach (vyukovy_pavouk.Data.Kapitola kapitola in subject.Kapitoly)
            {
                Prerekvizity prerekvizizy = await _dbContext.Prerekvizity.Where(x => x.PrerekvizityID == kapitola.Id).SingleOrDefaultAsync();
                if(prerekvizizy != null)
                {
                    _dbContext.Remove(prerekvizizy);
                }              
            }
            //vymaže úvodní kapitolu
            foreach (vyukovy_pavouk.Data.Kapitola kapitola in subject.Kapitoly)
            {
                KapitolaPrerekvizita kapitolaPrerekvizita = await _dbContext.kapitolaPrerekvizita
                    .Include(x => x.prerekvizita).Where(x => x.prerekvizita.PrerekvizityID == 0 && x.KapitolaID == kapitola.Id).SingleOrDefaultAsync();
                if(kapitolaPrerekvizita != null)
                {
                    _dbContext.Remove(kapitolaPrerekvizita.prerekvizita);
                    break;
                }
            }
                //vymaže skupinu s předmětem --> to následně vymaže veškeré navázané prerevizity, videa, zadání...
                Skupina group = _dbContext.Skupina.Find(Id);
            _dbContext.Skupina.Remove(group);
            await _dbContext.SaveChangesAsync();
        }
    }
}

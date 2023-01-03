using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class PredmetController : ControllerBase
{
        private readonly IPredmet _IPredmet;
        public PredmetController(IPredmet iPredmet)
        {
            _IPredmet = iPredmet;
        }
        //získání všech předmětu --> proběhne při prvotním vytvoření Teamu 
        [HttpGet]
        public async Task<List<Predmet>> Get()
        {
            return await _IPredmet.GetSubjects();
        }
        //získání počtů všech kapitol patřící pod jednotlivý předmět --> použití u souhrnu 
        [HttpGet("{IDPredmetu}")]
        public async Task<int> Get(int IDPredmetu)
        {
            return await Task.FromResult(_IPredmet.GetCountChapters(IDPredmetu));
        }
        [HttpGet]
        [Route("skupiny/{IdPredmetu}")]
        public async Task<Predmet> GetPredmetWithGroups(int IdPredmetu)
        {
            return await _IPredmet.GetSubjectWithConnectedGroups(IdPredmetu);
        }

        //vytvoří Teams skupinu s novým předmětem 
        [HttpPost]
        public async Task CreateNew([FromBody] Predmet predmet)
        {
            await _IPredmet.SaveSubject(predmet);
        }

        [HttpPut]
        public async Task EditSubject(Skupina skupina)
        {
             await _IPredmet.EditSubject(skupina);
        }
        //změna viditelnosti předmětu --> veřejná x privátní
        [Route("visibility")]
        [HttpPut]
        public async Task ChangeVisibilitySubject(Predmet predmet)
        {
            await _IPredmet.ChangeVisibilitySubject(predmet);
        }

        [HttpDelete("{Id}")]
        public async Task DeleteSubject(int Id)
        {
            await _IPredmet.DeleteSubject(Id);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkupinaController : ControllerBase
    {
        private readonly ISkupina _ISkupina;
        public SkupinaController(ISkupina iSkupina)
        {
            _ISkupina = iSkupina;
        }
        //prvotní kontrola zda-li team existuje, pak následně se používá pro získání ID skupiny a předmětu 
        [HttpGet("{IDTeamu}")]
        public async Task<Skupina> Get(string IDTeamu)
        {
            Skupina skupina = await _ISkupina.GetGroup(IDTeamu);            
            return skupina;
        }
        //vytvoří Teams skupinu pod existující předmět v databázi 
        [HttpPost]
        public async Task Create([FromBody] Skupina skupina)
        {
            await _ISkupina.AddGroup(skupina);
        }
        //vytvoří úvodní prerekvizitu patřící pod skupinu 
        [Route("uvod")]
        [HttpPost]
        public async Task CreateIntroductionPrerequisite([FromBody] Splneni splneni)
        {
            await _ISkupina.CreateIntroductionPrerequisite(splneni);
        }
        //resetuje danou skupinu
        [HttpDelete]
        [Route("reset/{Id}")]
        public async Task ResetGroup(int Id)
        {
            await _ISkupina.ResetGroup(Id);
        }
       

}
}

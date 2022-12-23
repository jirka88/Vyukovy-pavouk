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
            Skupina skupina = await _ISkupina.GetSkupina(IDTeamu);            
            return skupina;
        }
        //vytvoří Teams skupinu pod existující předmět v databázi 
        [HttpPost]
        public void Create([FromBody] Skupina skupina)
        {
            _ISkupina.AddSkupina(skupina);
        }
        //vytvoří úvodní prerekvizitu patřící pod skupinu 
        [Route("uvod")]
        [HttpPost]
        public void CreateUvodniPrerekvizitu([FromBody] Splneni splneni)
        {
            _ISkupina.CreateUvodniPrerekvizita(splneni);
        }

        [HttpDelete]
        [Route("reset/{Id}")]
        public IActionResult ResetSkupinu(int Id)
        {
            _ISkupina.ResetSkupina(Id);
            return Ok();
        }
       

}
}

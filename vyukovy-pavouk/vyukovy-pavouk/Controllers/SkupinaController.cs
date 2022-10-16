using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("{IDTeamu}")] 
        public IActionResult Get(string IDTeamu)
        {
            Skupina skupina = _ISkupina.GetSkupina(IDTeamu);
            //pokud skupina není v databázi je vrácena prázdná třída (null), pokud ne nastane chyba v JSON 
            if(skupina == null)
            {
                skupina = new Skupina();
            }
            return Ok(skupina);        
           
        }
}
}

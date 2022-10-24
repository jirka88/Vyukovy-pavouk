using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KapitolyController : ControllerBase
    {

        private readonly IKapitoly _IKapitola;
        public KapitolyController(IKapitoly iKapitola)
        {
            _IKapitola = iKapitola;
        }
        //získání všech kapitol u mainu podle předmětu 
        [HttpGet("{IDPredmetu}")]
        public async Task<List<Kapitola>> Get(int IDPredmetu)
        {
            return await Task.FromResult(_IKapitola.GetKapitoly(IDPredmetu));
        }
    }
}

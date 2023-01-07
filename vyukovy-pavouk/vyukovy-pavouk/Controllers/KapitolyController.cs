using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        //získání všech kapitol a prerekvizit u mainu podle předmětu 
        [HttpGet("{IDPredmetu}")]
        public async Task<List<Kapitola>> Get(int IDPredmetu)
        {
            return await _IKapitola.GetChapters(IDPredmetu);
        }
        [HttpGet]
        [Route("vsechno/{IDPredmetu}")] 
        public async Task<List<Kapitola>> GetAllInfo(int IDPredmetu)
        {
            return await _IKapitola.GetChaptersWithAll(IDPredmetu);
        }
        [HttpGet]
        [Route("nazvy/{idPredmetu}")]
        public async Task<List<Kapitola>> GetNames(int idPredmetu)
        {
            return await _IKapitola.GetChaptersOnly(idPredmetu);
        }
        [HttpGet]
        [Route("prerekvizity/{idPredmetu}")]
        public async Task<List<KapitolaPrerekvizita>> GetPrerekvizity(int idPredmetu)
        {
            return await _IKapitola.GetChaptersPrerequisites(idPredmetu);
        }
        [HttpPost]
        public async Task CreateCopyOfChapters([FromBody] List<Kapitola> chapters)
        {
            await _IKapitola.CreateCopyOfChapters(chapters);
        }
       
    }
    
}

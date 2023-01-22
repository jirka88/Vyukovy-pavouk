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

        private readonly IChapters _IChapter;
        public KapitolyController(IChapters IChapter)
        {
            _IChapter = IChapter;
        }
        //získání všech kapitol a prerekvizit u mainu podle předmětu 
        [HttpGet("{IdSubject}")]
        public async Task<List<Kapitola>> Get(int IdSubject)
        {
            return await _IChapter.GetChapters(IdSubject);
        }
        [HttpGet]
        [Route("vsechno/{IdSubject}")] 
        public async Task<List<Kapitola>> GetAllInfo(int IdSubject)
        {
            return await _IChapter.GetChaptersWithAll(IdSubject);
        }
        [HttpGet]
        [Route("nazvy/{IdSubject}")]
        public async Task<List<Kapitola>> GetNames(int IdSubject)
        {
            return await _IChapter.GetChaptersOnly(IdSubject);
        }
        [HttpGet]
        [Route("prerekvizity/{IdSubject}")]
        public async Task<List<KapitolaPrerekvizita>> GetPrerekvizity(int IdSubject)
        {
            return await _IChapter.GetChaptersPrerequisites(IdSubject);
        }
        [HttpPost]
        public async Task CreateCopyOfChapters([FromBody] List<Kapitola> chapters)
        {
            await _IChapter.CreateCopyOfChapters(chapters);
        }
       
    }
    
}

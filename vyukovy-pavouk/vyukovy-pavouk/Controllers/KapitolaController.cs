using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KapitolaController : ControllerBase
    {
        private readonly IChapter _IChapter;
        public KapitolaController(IChapter IChapter)
        {
            _IChapter = IChapter;
        }
        [HttpGet("{IdChapter}")]
        public async Task<Kapitola> Get(int IdChapter)
        {
            return await _IChapter.GetKapitolaDetail(IdChapter);
        }
        [HttpPost]
        public async Task Create([FromBody] Kapitola Chapter)
        {
            await _IChapter.CreateKapitola(Chapter);
        }
        [HttpPut]
        public async Task Update(Kapitola Chapter)
        {
            await _IChapter.UpdateKapitola(Chapter);
        }
        [HttpDelete]
        [Route("delete/{IdChapter}")]
        public async Task Delete(int IdChapter)
        {
          await _IChapter.DeleteKapitola(IdChapter);
        }
    }
}

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
        private readonly IChapter _IKapitola;
        public KapitolaController(IChapter iKapitola)
        {
            _IKapitola = iKapitola;
        }
        [HttpGet("{IdChapter}")]
        public async Task<Kapitola> Get(int IdChapter)
        {
            return await _IKapitola.GetKapitolaDetail(IdChapter);
        }
        [HttpPost]
        public async Task Create([FromBody] Kapitola Chapter)
        {
            await _IKapitola.CreateKapitola(Chapter);
        }
        [HttpPut]
        public async Task Update(Kapitola Chapter)
        {
            await _IKapitola.UpdateKapitola(Chapter);
        }
        [HttpDelete]
        [Route("delete/{IdChapter}")]
        public async Task Delete(int IdChapter)
        {
          await _IKapitola.DeleteKapitola(IdChapter);
        }
    }
}

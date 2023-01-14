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
        [HttpGet("{IdKapitola}")]
        public async Task<Kapitola> Get(int IdKapitola)
        {
            return await _IKapitola.GetKapitolaDetail(IdKapitola);
        }
        [HttpPost]
        public async Task Create([FromBody] Kapitola kapitola)
        {
            await _IKapitola.CreateKapitola(kapitola);
        }
        [HttpPut]
        public async Task Update(Kapitola kapitola)
        {
            await _IKapitola.UpdateKapitola(kapitola);
        }
        [HttpDelete]
        [Route("delete/{IdKapitoly}")]
        public async Task Delete(int Idkapitoly)
        {
          await _IKapitola.DeleteKapitola(Idkapitoly);
        }
    }
}

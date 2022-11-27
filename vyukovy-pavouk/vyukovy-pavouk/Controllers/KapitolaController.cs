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
        private readonly IKapitola _IKapitola;
        public KapitolaController(IKapitola iKapitola)
        {
            _IKapitola = iKapitola;
        }
        [HttpGet("{IdKapitola}")]
        public async Task<Kapitola> Get(int IdKapitola)
        {
            return await Task.FromResult(_IKapitola.GetKapitolaDetail(IdKapitola));
        }
        [HttpPost]
        public void Create([FromBody] Kapitola kapitola)
        {
            _IKapitola.CreateKapitola(kapitola);
        }
        [HttpPut]
        public async Task Update(Kapitola kapitola)
        {
            await _IKapitola.UpdateKapitola(kapitola);
        }
        [HttpDelete]
        [Route("delete/{Id}")]
        public IActionResult Delete(int Id)
        {
          _IKapitola.DeleteKapitola(Id);
          return Ok();
        }
    }
}

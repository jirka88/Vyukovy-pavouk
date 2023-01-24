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
        public async Task<Chapter> Get(int IdChapter)
        {
            return await _IChapter.GetChapterDetail(IdChapter);
        }
        [HttpPost]
        public async Task Create([FromBody] Chapter Chapter)
        {
            await _IChapter.CreateChapter(Chapter);
        }
        [HttpPut]
        public async Task Update(Chapter Chapter)
        {
            await _IChapter.UpdateChapter(Chapter);
        }
        [HttpDelete]
        [Route("delete/{IdChapter}")]
        public async Task Delete(int IdChapter)
        {
          await _IChapter.DeleteChapter(IdChapter);
        }
    }
}

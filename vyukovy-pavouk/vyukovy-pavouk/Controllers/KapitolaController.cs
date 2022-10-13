using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
public class KapitolaController : ControllerBase
{
        private readonly ITest _ITest;
        public KapitolaController(ITest iTest)
        {
            _ITest = iTest;
        }
        [HttpGet]
        public async Task<List<Test>> Get()
        {
            return await Task.FromResult(_ITest.GetItems());
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UzivatelController : ControllerBase
    {
        private readonly IUzivatel _IUzivatel;
        public UzivatelController(IUzivatel iUzivatel)
        {
            _IUzivatel = iUzivatel;
        }
        [HttpGet("{Id}") ]
        public async Task<List<SkupinaStudent>> Get(int Id)
        {
            return await Task.FromResult(_IUzivatel.GetStudents(Id));
        }
    }
}

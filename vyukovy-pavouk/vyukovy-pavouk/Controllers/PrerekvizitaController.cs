using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrerekvizitaController : ControllerBase
    {
        private readonly IPrerekvizita _IPrerekvizita;
        public PrerekvizitaController(IPrerekvizita iPrerekvizita)
        {
            _IPrerekvizita = iPrerekvizita;
        }
        [HttpGet("{IdSkupiny}")]
        public async Task<Splneni> Get(int IdSkupiny)
        {
            return await Task.FromResult(_IPrerekvizita.GetUvodniPrerekvizitu(IdSkupiny));
        }
}
}

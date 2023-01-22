using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkupinaController : ControllerBase
    {
        private readonly IGroup _IGroup;
        public SkupinaController(IGroup IGroup)
        {
            _IGroup = IGroup;
        }
        //prvotní kontrola zda-li team existuje, pak následně se používá pro získání ID skupiny a předmětu 
        [HttpGet("{IDTeam}")]
        public async Task<Skupina> Get(string IDTeam)
        {
            Skupina group = await _IGroup.GetGroup(IDTeam);            
            return group;
        }
        //vytvoří Teams skupinu pod existující předmět v databázi 
        [HttpPost]
        public async Task Create([FromBody] Skupina group)
        {
            await _IGroup.AddGroup(group);
        }
        //vytvoří úvodní prerekvizitu patřící pod skupinu 
        [Route("uvod")]
        [HttpPost]
        public async Task CreateIntroductionPrerequisite([FromBody] Splneni splneni)
        {
            await _IGroup.CreateIntroductionPrerequisite(splneni);
        }
        //resetuje danou skupinu
        [HttpDelete]
        [Route("vymaz/{Id}")]
        public async Task DeleteGroup(int Id)
        {
            await _IGroup.DeleteGroup(Id);
        }
        //vymaže všechny splnění a napojení studentů --> odstraní ID Teamu a strukturu nastaví na privátní 
        [HttpDelete]
        [Route("reset/{IdGroup}")]
        public async Task ResetGroup(int IdGroup)
        {
            await _IGroup.ResetGroup(IdGroup);
        }
        [Route("pripoj")]
        [HttpPost]
        public async Task TryConnectGroup(Skupina group)
        {
            await _IGroup.ConnectGroup(group);
        }
       

}
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Controllers
{
    [Route("api/[controller]")]
[ApiController]
public class PredmetController : ControllerBase
{
        private readonly ISubject _ISubject;
        public PredmetController(ISubject ISubject)
        {
            _ISubject = ISubject;
        }
        //získání všech předmětu --> proběhne při prvotním vytvoření Teamu 
        [HttpGet]
        public async Task<List<Subject>> Get()
        {
            return await _ISubject.GetSubjects();
        }
        //získání počtů všech kapitol patřící pod jednotlivý předmět --> použití u souhrnu 
        [HttpGet("{IdSubject}")]
        public async Task<int> Get(int IdSubject)
        {
            return await Task.FromResult(_ISubject.GetCountChapters(IdSubject));
        }
        [HttpGet]
        [Route("skupiny/{IdSubject}")]
        public async Task<Subject> GetSubjectWithGroups(int IdSubject)
        {
            return await _ISubject.GetSubjectWithConnectedGroups(IdSubject);
        }

        //vytvoří Teams skupinu s novým předmětem 
        [HttpPost]
        public async Task CreateNew([FromBody] Subject subject)
        {
            await _ISubject.SaveSubject(subject);
        }

        [HttpPut]
        public async Task EditSubject(Group group)
        {
             await _ISubject.EditSubject(group);
        }
        //změna viditelnosti předmětu --> veřejná x privátní
        [Route("visibility")]
        [HttpPut]
        public async Task ChangeVisibilitySubject(Subject subject)
        {
            await _ISubject.ChangeVisibilitySubject(subject);
        }

    }
}

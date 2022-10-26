using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vyukovy_pavouk.Data;
using vyukovy_pavouk.Interface;

namespace vyukovy_pavouk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentiController : ControllerBase
    {
        private readonly IStudenti _IStudenti;
        public StudentiController(IStudenti iStudent)
        {
            _IStudenti = iStudent;
        }
        [HttpGet("{Id}")]
        //vrátí všechny studenty z dané skupiny 
        public async Task<List<SkupinaStudent>> Get(int Id)
        {
            return await Task.FromResult(_IStudenti.GetStudents(Id));
        }
        //získa studenta zda-li v dané skupině je a pokud není vrátí null
        [HttpGet("{IdSkupiny}/{EmailStudenta}")]
        public async Task<SkupinaStudent> GetStudent(int IdSkupiny, string EmailStudenta)
        {
            SkupinaStudent Student = await Task.FromResult(_IStudenti.GetStudent(IdSkupiny, EmailStudenta));
            if(Student == null)
            {
                return new SkupinaStudent();
            }
            return Student;
        }

    }
}

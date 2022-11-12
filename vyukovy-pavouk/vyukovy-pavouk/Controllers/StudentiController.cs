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
        //zjistí jednotlivého studenta a jeho stav plnění kapitol --> u progresu 
        [HttpGet("progres/{IdSkupiny}/{Id}")]
        public async Task<Student> GetStudentProgres (int IdSkupiny, int Id)
        {
            Student student = await Task.FromResult(_IStudenti.GetStudentProgres(Id, IdSkupiny));
            return student;
        }
        //získa studenta zda-li v dané skupině je a pokud není vrátí null
        [HttpGet("{IdSkupiny}/{EmailStudenta}")]
        public async Task<Student> GetStudent(int IdSkupiny, string EmailStudenta)
        {
            Student student = await Task.FromResult(_IStudenti.GetStudent(IdSkupiny, EmailStudenta));
            if(student == null)
            {
                return new Student();
            }
            return student;
        }
        //vytvoří studenta pod patřící skupinu 
        [HttpPost]
        public void CreateNewStudent([FromBody] Student student)
        {
            _IStudenti.CreateNewStudent(student);
        }
        //vytvoří navázaní studenta ke skupině 
        [Route("connect")]
        [HttpPost]
        public void CreateNewConnect([FromBody] SkupinaStudent skupinaStudent)
        {
            _IStudenti.CreateNewConnect(skupinaStudent);
        }
        //vytvoří navázání na úvodní prerekvizitu (splnění) a studenta --> StudentSplneni
        [Route("connectPrerekvizita")]
        [HttpPost]
        public void CreateNewUvodniPrerekvizita([FromBody] StudentSplneni splneni)
        {
            _IStudenti.CreateNewConnectPrerekvizita(splneni);
        }

    }
}

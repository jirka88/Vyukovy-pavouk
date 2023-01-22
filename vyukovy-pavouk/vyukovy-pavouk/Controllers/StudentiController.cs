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
        private readonly IStudents _IStudenti;
        public StudentiController(IStudents iStudent)
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
        [HttpGet("progres/{IdGroup}/{Id}")]
        public async Task<Student> GetStudentProgres (int IdGroup, int Id)
        {
            Student student = await Task.FromResult(_IStudenti.GetStudentProgres(Id, IdGroup));
            return student;
        }
        //získa studenta zda-li v dané skupině je a pokud není vrátí null
        [HttpGet("{IdGroup}/{EmailStudent}")]
        public async Task<Student> GetStudent(int IdGroup, string EmailStudent)
        {
            Student student = await Task.FromResult(_IStudenti.GetStudent(IdGroup, EmailStudent));
            if(student == null)
            {
                return new Student();
            }
            return student;
        }
        //vytvoří studenta pod patřící skupinu 
        [HttpPost]
        public async Task CreateNewStudent([FromBody] Student student)
        {
            await _IStudenti.CreateNewStudent(student);
        }
        //vytvoří navázaní studenta ke skupině 
        [Route("connect")]
        [HttpPost]
        public async Task CreateNewConnect([FromBody] SkupinaStudent groupStudent)
        {
            await _IStudenti.CreateNewConnect(groupStudent);
        }
        [Route("splneni")]
        [HttpPost]
        public async Task PripojSplneni([FromBody] StudentSplneni studentSplneni)
        {
            await _IStudenti.CreateSplneni(studentSplneni);
        }
        //vymaže studenta u určité skupiny 
        [HttpDelete]
        [Route("deleteStudent/{IdStudent}/{IdGroup}")]
        public async Task deleteStudenta(int IdStudent, int IdGroup)
        {
            await _IStudenti.DeleteStudent(IdStudent, IdGroup);
        }
        [HttpPut]
        public async Task OpravSplneni(StudentSplneni studentSplneni)
        {
            await _IStudenti.OpravaSplneni(studentSplneni);
        }

    }
}

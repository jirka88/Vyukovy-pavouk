using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.Interface
{
    public interface IStudents
{
        public List<SkupinaStudent> GetStudents(int Id);
        public Student GetStudentProgres(int Id, int IdSkupiny);
        public Student GetStudent(int IdSkupiny, string EmailStudenta);
        public List<StudentSplneni> GetSplneni(int IdSkupiny);
        public void DeleteSplneni(int id);
        public Task CreateNewStudent(Student student);
        public Task CreateNewConnect(SkupinaStudent skupinaStudent);
        public Task DeleteStudent(int IdStudenta, int IdSkupiny);
        public Task CreateSplneni(StudentSplneni studentSplneni);
        public Task OpravaSplneni(StudentSplneni studentSplneni);
}
}

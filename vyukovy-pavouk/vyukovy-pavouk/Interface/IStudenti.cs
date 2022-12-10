using vyukovy_pavouk.Data;

namespace vyukovy_pavouk.Interface
{
    public interface IStudenti
{
        public List<SkupinaStudent> GetStudents(int Id);
        public Student GetStudentProgres(int Id, int IdSkupiny);
        public Student GetStudent(int IdSkupiny, string EmailStudenta);
        public List<StudentSplneni> GetSplneni(int IdSkupiny);
        public void DeleteSplneni(int id);
        public void CreateNewStudent(Student student);
        public void CreateNewConnect(SkupinaStudent skupinaStudent);
        public void CreateNewConnectPrerekvizita(StudentSplneni splneni);
        public Task DeleteStudent(int IdStudenta, int IdSkupiny);

}
}
